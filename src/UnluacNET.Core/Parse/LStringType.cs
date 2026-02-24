using System.Globalization;
using System.Text;
using UnluacNET.Core.Extensions;

namespace UnluacNET.Core.Parse;

public class LStringType : BObjectType<LString>
{
    public override LString Parse(Stream stream, BHeader header)
    {
        var sizeT = header.SizeT.Parse(stream, header);
        var xx = sizeT.AsInteger();
        var sb = new StringBuilder();
        //stream.ReadChars();
        var dec = Encoding.Default.GetDecoder();
        var charData = new char[xx];
        var tempChars = stream.ReadBytes(xx);
        dec.GetChars(tempChars, 0, tempChars.Length, charData, 0);
        var tempSring = new string(charData);
        var str = tempSring.Replace("\0", "")
            .Split('?')[0] + "\0";
        if (header.Debug)
            Console.WriteLine("-- parsed <string> \"" + str + "\"");

        return new LString(sizeT, str);
    }

    public static string Ascii2Str(string textAscii)
    {
        try
        {
            var k = 0; //字节移动偏移量

            var buffer = new byte[textAscii.Length / 2]; //存储变量的字节

            for (var i = 0; i < textAscii.Length / 2; i++)
            {
                //每两位合并成为一个字节
                buffer[i] = byte.Parse(textAscii.Substring(k, 2), NumberStyles.HexNumber);
                k = k + 2;
            }

            //将字节转化成汉字
            return Encoding.Default.GetString(buffer);
        }
        catch (Exception ex)
        {
            Console.WriteLine("ASCII转含中文字符串异常" + ex.Message);
        }

        return "";
    }
}
namespace UnluacNET.Core.Parse;

public class LSourceLines
{
    public static LSourceLines Parse(Stream stream)
    {
        // TODO: Encapsulate a LuaStream of some sort to automatically support Big-Endian
        throw new InvalidOperationException("LSourceLines::Parse isn't implemented properly!");

        /*
        var number = stream.ReadInt32();

        //while (number-- > 0)
        //    stream.ReadInt32();

        if (number > 0)
            stream.Position += (number * sizeof(int));

        return null;
        */
    }
}
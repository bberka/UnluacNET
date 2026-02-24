namespace UnluacNET.Core.Parse;

public class LConstantType : BObjectType<LObject>
{
    private static readonly string[] MConstantTypes =
    {
        "<nil>",
        "<boolean>",
        null, // no type?
        "<number>",
        "<string>"
    };

    public override LObject Parse(Stream stream, BHeader header)
    {
        var type = stream.ReadByte();

        if (header.Debug)
            if (type < MConstantTypes.Length)
            {
                var cType = MConstantTypes[type];

                Console.WriteLine("-- parsing <constant>, type is {0}", type != 2 ? cType : "illegal " + type);
            }

        switch (type)
        {
            case 0:
                return LNil.NIL;
            case 1:
                return header.Bool.Parse(stream, header);
            case 3:
                return header.Number.Parse(stream, header);
            case 4:
                return header.String.Parse(stream, header);
            default:
                throw new InvalidOperationException();
        }
    }
}
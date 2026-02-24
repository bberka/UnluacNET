namespace UnluacNET.Core.Parse;

public class LUpvalueType : BObjectType<LUpvalue>
{
    public override LUpvalue Parse(Stream stream, BHeader header)
    {
        return new LUpvalue
        {
            InStack = stream.ReadByte() != 0,
            Index = stream.ReadByte()
        };
        ;
    }
}
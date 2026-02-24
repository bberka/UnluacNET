namespace UnluacNET.Core.Parse;

public class BSizeTType : BObjectType<BSizeT>
{
    private readonly BIntegerType m_integerType;

    public BSizeTType(int sizeTSize)
    {
        SizeTSize = sizeTSize;
        m_integerType = new BIntegerType(sizeTSize);
    }

    public int SizeTSize { get; private set; }

    public override BSizeT Parse(Stream stream, BHeader header)
    {
        var value = new BSizeT(m_integerType.RawParse(stream, header));

        if (header.Debug)
            Console.WriteLine("-- parsed <size_t> " + value.AsInteger());

        return value;
    }
}
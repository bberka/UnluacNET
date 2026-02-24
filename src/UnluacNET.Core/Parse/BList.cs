namespace UnluacNET.Core.Parse;

public class BList<T> : BObject where T : BObject
{
    private readonly List<T> m_values;

    public BList(BInteger length, List<T> values)
    {
        Length = length;
        m_values = values;
    }

    public BInteger Length { get; private set; }

    public T this[int index] => m_values[index];

    public T[] AsArray()
    {
        return m_values.AsParallel()
            .ToArray();
    }
}
namespace UnluacNET.Core.Parse;

public class LBoolean : LObject
{
    public static readonly LBoolean LTRUE = new(true);
    public static readonly LBoolean LFALSE = new(false);

    private readonly bool m_value;

    private LBoolean(bool value)
    {
        m_value = value;
    }

    public override bool Equals(object obj)
    {
        return this == obj;
    }

    public override string ToString()
    {
        return m_value.ToString();
    }
}
namespace UnluacNET.Core.Parse;

public class LString : LObject
{
    public LString(BSizeT size, string value)
    {
        Size = size;
        Value = value.Length == 0 ? string.Empty : value.Substring(0, value.Length - 1);
    }

    public BSizeT Size { get; private set; }
    public string Value { get; }

    public override string DeRef()
    {
        return Value;
    }

    public override bool Equals(object obj)
    {
        if (obj is LString)
            return Value == ((LString)obj).Value;

        return false;
    }

    public override string ToString()
    {
        return string.Format("\"{0}\"", Value);
    }
}
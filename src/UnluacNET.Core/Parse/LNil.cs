namespace UnluacNET.Core.Parse;

public class LNil : LObject
{
    public static readonly LNil NIL = new();

    private LNil()
    {
    }

    public override bool Equals(object obj)
    {
        return this == obj;
    }
}
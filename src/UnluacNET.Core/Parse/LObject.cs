namespace UnluacNET.Core.Parse;

public abstract class LObject : BObject
{
    public new abstract bool Equals(object obj);

    public virtual string DeRef()
    {
        throw new NotImplementedException();
    }
}
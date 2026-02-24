namespace UnluacNET.Core.Decompile.Target;

public abstract class Target
{
    public virtual bool IsFunctionName => true;

    public virtual bool IsLocal => false;

    public virtual int GetIndex()
    {
        throw new InvalidOperationException();
    }

    public virtual bool IsDeclaration(Declaration decl)
    {
        return false;
    }

    public abstract void Print(Output output);
    public abstract void PrintMethod(Output output);
}
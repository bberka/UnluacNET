namespace UnluacNET.Core.Decompile.Target;

public class VariableTarget : Target
{
    public VariableTarget(Declaration decl)
    {
        Declaration = decl;
    }

    public Declaration Declaration { get; }

    public override bool IsLocal => true;

    public override bool IsDeclaration(Declaration decl)
    {
        return Declaration == decl;
    }

    public override bool Equals(object obj)
    {
        if (obj is VariableTarget)
            return Declaration == ((VariableTarget)obj).Declaration;
        return false;
    }

    public override int GetIndex()
    {
        return Declaration.Register;
    }

    public override void Print(Output output)
    {
        output.Print(Declaration.Name);
    }

    public override void PrintMethod(Output output)
    {
        throw new InvalidOperationException();
    }
}
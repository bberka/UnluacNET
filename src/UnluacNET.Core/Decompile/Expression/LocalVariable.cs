namespace UnluacNET.Core.Decompile.Expression;

public class LocalVariable : Expression
{
    public LocalVariable(Declaration decl) : base(PRECEDENCE_ATOMIC)
    {
        Declaration = decl;
    }

    public Declaration Declaration { get; }

    public override int ConstantIndex => -1;

    public override bool IsBrief => true;

    public override bool IsDotChain => true;

    public override void Print(Output output)
    {
        output.Print(Declaration.Name);
    }
}
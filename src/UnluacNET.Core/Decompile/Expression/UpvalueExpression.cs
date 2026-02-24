namespace UnluacNET.Core.Decompile.Expression;

public class UpvalueExpression : Expression
{
    private readonly string m_name;

    public UpvalueExpression(string name) : base(PRECEDENCE_ATOMIC)
    {
        m_name = name;
    }

    public override int ConstantIndex => -1;

    public override bool IsBrief => true;

    public override bool IsDotChain => true;

    public override void Print(Output output)
    {
        output.Print(m_name);
    }
}
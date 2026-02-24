namespace UnluacNET.Core.Decompile.Expression;

public class GlobalExpression : Expression
{
    private readonly int m_index;
    private readonly string m_name;

    public GlobalExpression(string name, int index) : base(PRECEDENCE_ATOMIC)
    {
        m_name = name;
        m_index = index;
    }

    public override int ConstantIndex => m_index;

    public override bool IsBrief => true;

    public override bool IsDotChain => true;

    public override void Print(Output output)
    {
        output.Print(m_name);
    }
}
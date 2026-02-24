namespace UnluacNET.Core.Decompile.Branch;

public class AssignNode : Branch
{
    private Expression.Expression m_expression;

    public AssignNode(
        int line,
        int begin,
        int end
    ) : base(line, begin, end)
    {
    }

    public override Expression.Expression AsExpression(Registers registers)
    {
        return m_expression;
    }

    public override int GetRegister()
    {
        throw new InvalidOperationException();
    }

    public override Branch Invert()
    {
        throw new InvalidOperationException();
    }

    public override void UseExpression(Expression.Expression expression)
    {
        m_expression = expression;
    }
}
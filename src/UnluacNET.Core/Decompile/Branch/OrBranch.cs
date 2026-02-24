namespace UnluacNET.Core.Decompile.Branch;

public class OrBranch : Branch
{
    private readonly Branch m_left;
    private readonly Branch m_right;

    public OrBranch(Branch left, Branch right) : base(right.Line, right.Begin, right.End)
    {
        m_left = left;
        m_right = right;
    }

    public override Expression.Expression AsExpression(Registers registers)
    {
        return Expression.Expression.MakeOr(m_left.AsExpression(registers), m_right.AsExpression(registers));
    }

    public override int GetRegister()
    {
        var rLeft = m_left.GetRegister();
        var rRight = m_right.GetRegister();

        return rLeft == rRight ? rLeft : -1;
    }

    public override Branch Invert()
    {
        return new AndBranch(m_left.Invert(), m_right.Invert());
    }

    public override void UseExpression(Expression.Expression expression)
    {
        m_left.UseExpression(expression);
        m_right.UseExpression(expression);
    }
}
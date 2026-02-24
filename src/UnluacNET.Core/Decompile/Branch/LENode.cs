using UnluacNET.Core.Decompile.Expression;

namespace UnluacNET.Core.Decompile.Branch;

public class LeNode : Branch
{
    private readonly bool m_invert;
    private readonly int m_left;
    private readonly int m_right;

    public LeNode(
        int left,
        int right,
        bool invert,
        int line,
        int begin,
        int end
    ) : base(line, begin, end)
    {
        m_left = left;
        m_right = right;
        m_invert = invert;
    }

    public override Expression.Expression AsExpression(Registers registers)
    {
        var transpose = false;

        var leftExpr = registers.GetKExpression(m_left, Line);
        var rightExpr = registers.GetKExpression(m_right, Line);

        if (((m_left | m_right) & 256) == 0)
            transpose = registers.GetUpdated(m_left, Line) > registers.GetUpdated(m_right, Line);
        else
            transpose = rightExpr.ConstantIndex < leftExpr.ConstantIndex;

        var op = !transpose ? "<=" : ">=";

        Expression.Expression rtn = new BinaryExpression(op, !transpose ? leftExpr : rightExpr, !transpose ? rightExpr : leftExpr,
            Expression.Expression.PRECEDENCE_COMPARE, Expression.Expression.ASSOCIATIVITY_LEFT);

        if (m_invert)
            rtn = Expression.Expression.MakeNot(rtn);

        return rtn;
    }

    public override int GetRegister()
    {
        return -1;
    }

    public override Branch Invert()
    {
        return new LeNode(m_left, m_right, !m_invert, Line, End, Begin);
    }

    public override void UseExpression(Expression.Expression expression)
    {
        // Do nothing
    }
}
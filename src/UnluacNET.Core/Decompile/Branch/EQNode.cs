using UnluacNET.Core.Decompile.Expression;

namespace UnluacNET.Core.Decompile.Branch;

public class EqNode : Branch
{
    private readonly bool m_invert;
    private readonly int m_left;
    private readonly int m_right;

    public EqNode(
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

    public override Branch Invert()
    {
        return new EqNode(m_left, m_right, !m_invert, Line, End, Begin);
    }

    public override int GetRegister()
    {
        return -1;
    }

    public override Expression.Expression AsExpression(Registers registers)
    {
        var transpose = false;
        var op = m_invert ? "~=" : "==";

        return new BinaryExpression(op, registers.GetKExpression(!transpose ? m_left : m_right, Line),
            registers.GetKExpression(!transpose ? m_right : m_left, Line), Expression.Expression.PRECEDENCE_COMPARE,
            Expression.Expression.ASSOCIATIVITY_LEFT);
    }

    public override void UseExpression(Expression.Expression expression)
    {
        /* Do nothing */
    }
}
namespace UnluacNET.Core.Decompile.Expression;

public class UnaryExpression : Expression
{
    private readonly Expression m_expression;
    private readonly string m_op;

    public UnaryExpression(
        string op,
        Expression expression,
        int precedence
    ) : base(precedence)
    {
        m_op = op;
        m_expression = expression;
    }

    public override int ConstantIndex => m_expression.ConstantIndex;

    public override void Print(Output output)
    {
        var precedence = Precedence > m_expression.Precedence;

        output.Print(m_op);

        if (precedence)
            output.Print("(");

        m_expression.Print(output);

        if (precedence)
            output.Print(")");
    }
}
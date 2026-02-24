namespace UnluacNET.Core.Decompile.Expression;

public class FunctionCall : Expression
{
    private readonly Expression[] m_arguments;
    private readonly Expression m_function;

    private readonly bool m_multiple;

    public FunctionCall(
        Expression function,
        Expression[] arguments,
        bool multiple
    ) : base(PRECEDENCE_ATOMIC)
    {
        m_function = function;
        m_arguments = arguments;

        m_multiple = multiple;
    }

    private bool IsMethodCall =>
        m_function.IsMemberAccess && m_arguments.Length > 0 && m_function.GetTable() == m_arguments[0];

    public override bool BeginsWithParen
    {
        get
        {
            var obj = IsMethodCall ? m_function.GetTable() : m_function;

            return obj.IsClosure || obj.IsConstant || obj.BeginsWithParen;
        }
    }

    public override int ConstantIndex
    {
        get
        {
            var index = m_function.ConstantIndex;

            foreach (var argument in m_arguments)
                index = Math.Max(argument.ConstantIndex, index);

            return index;
        }
    }

    public override bool IsMultiple => m_multiple;

    public override void Print(Output output)
    {
        var args = new List<Expression>(m_arguments.Length);

        var obj = IsMethodCall ? m_function.GetTable() : m_function;

        if (obj.IsClosure || obj.IsConstant)
        {
            output.Print("(");
            obj.Print(output);
            output.Print(")");
        }
        else
        {
            obj.Print(output);
        }

        if (IsMethodCall)
        {
            output.Print(":");
            output.Print(m_function.GetField());
        }

        for (var i = IsMethodCall ? 1 : 0; i < m_arguments.Length; i++)
            args.Add(m_arguments[i]);

        output.Print("(");
        PrintSequence(output, args, false, true);
        output.Print(")");
    }

    public override void PrintMultiple(Output output)
    {
        if (!m_multiple)
            output.Print("(");

        Print(output);

        if (!m_multiple)
            output.Print(")");
    }
}
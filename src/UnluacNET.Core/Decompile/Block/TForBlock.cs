using UnluacNET.Core.Parse;

namespace UnluacNET.Core.Decompile.Block;

public class TForBlock : Block
{
    private readonly int m_length;
    private readonly Registers m_r;
    private readonly int m_register;
    private readonly List<Statement.Statement> m_statements;

    public TForBlock(
        LFunction function,
        int begin,
        int end,
        int register,
        int length,
        Registers r
    ) : base(function, begin, end)
    {
        m_register = register;
        m_length = length;
        m_r = r;

        m_statements = new List<Statement.Statement>(end - begin + 1);
    }

    public override bool Breakable => true;

    public override bool IsContainer => true;

    public override bool IsUnprotected => false;

    public override int ScopeEnd => End - 3;

    public override void AddStatement(Statement.Statement statement)
    {
        m_statements.Add(statement);
    }

    public override int GetLoopback()
    {
        throw new InvalidOperationException();
    }

    public override void Print(Output output)
    {
        output.Print("for ");

        m_r.GetTarget(m_register + 3, Begin - 1)
            .Print(output);

        var n = m_register + 2 + m_length;

        for (var r1 = m_register + 4; r1 <= n; r1++)
        {
            output.Print(", ");
            m_r.GetTarget(r1, Begin - 1)
                .Print(output);
        }

        output.Print(" in ");

        Expression.Expression value = null;

        value = m_r.GetValue(m_register, Begin - 1);
        value.Print(output);

        // TODO: Optimize code
        if (!value.IsMultiple)
        {
            output.Print(", ");

            value = m_r.GetValue(m_register + 1, Begin - 1);
            value.Print(output);

            if (!value.IsMultiple)
            {
                output.Print(", ");

                value = m_r.GetValue(m_register + 2, Begin - 1);
                value.Print(output);
            }
        }

        output.Print(" do");
        output.PrintLine();

        output.IncreaseIndent();

        PrintSequence(output, m_statements);

        output.DecreaseIndent();
        output.Print("end");
    }
}
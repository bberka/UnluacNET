using UnluacNET.Core.Parse;

namespace UnluacNET.Core.Decompile.Block;

public class DoEndBlock : Block
{
    private readonly List<Statement.Statement> m_statements;

    public DoEndBlock(
        LFunction function,
        int begin,
        int end
    ) : base(function, begin, end)
    {
        m_statements = new List<Statement.Statement>(end - begin + 1);
    }

    public override bool Breakable => false;

    public override bool IsContainer => true;

    public override bool IsUnprotected => false;

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
        output.PrintLine("do");
        output.IncreaseIndent();

        PrintSequence(output, m_statements);

        output.DecreaseIndent();
        output.Print("end");
    }
}
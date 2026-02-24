using UnluacNET.Core.Parse;

namespace UnluacNET.Core.Decompile.Block;

public class AlwaysLoop : Block
{
    private readonly List<Statement.Statement> m_statements;

    public AlwaysLoop(
        LFunction function,
        int begin,
        int end
    ) : base(function, begin, end)
    {
        m_statements = new List<Statement.Statement>();
    }

    public override bool Breakable => true;

    public override bool IsContainer => true;

    public override bool IsUnprotected => true;

    public override int ScopeEnd => End - 2;

    public override int GetLoopback()
    {
        return Begin;
    }

    public override void AddStatement(Statement.Statement statement)
    {
        m_statements.Add(statement);
    }

    public override void Print(Output output)
    {
        output.PrintLine("while true do");
        output.IncreaseIndent();

        PrintSequence(output, m_statements);

        output.DecreaseIndent();
        output.Print("end");
    }
}
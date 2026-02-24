using UnluacNET.Core.Decompile.Statement;
using UnluacNET.Core.Parse;

namespace UnluacNET.Core.Decompile.Block;

public class OuterBlock : Block
{
    private readonly List<Statement.Statement> m_statements;

    public OuterBlock(LFunction function, int length) : base(function, 0, length + 1)
    {
        m_statements = new List<Statement.Statement>(length);
    }

    public override bool Breakable => false;

    public override bool IsContainer => true;

    public override bool IsUnprotected => false;

    public override int ScopeEnd => End - 1 + Function.Header.LuaVersion.OuterBlockScopeAdjustment;

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
        /* extra return statement */
        var last = m_statements.Count - 1;

        if (last < 0 || !(m_statements[last] is Return))
            throw new InvalidOperationException(m_statements[last]
                .ToString());

        // this doesn't seem like appropriate behavior???
        m_statements.RemoveAt(last);

        PrintSequence(output, m_statements);
    }
}
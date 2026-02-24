using UnluacNET.Core.Parse;

namespace UnluacNET.Core.Decompile.Block;

public class IfThenElseBlock : Block
{
    private readonly Branch.Branch m_branch;
    private readonly bool m_emptyElse;
    private readonly int m_loopback;
    private readonly Registers m_r;
    private readonly List<Statement.Statement> m_statements;

    public IfThenElseBlock(
        LFunction function,
        Branch.Branch branch,
        int loopback,
        bool emptyElse,
        Registers r
    ) : base(function, branch.Begin, branch.End)
    {
        m_branch = branch;
        m_loopback = loopback;
        m_emptyElse = emptyElse;
        m_r = r;

        m_statements = new List<Statement.Statement>(branch.End - branch.Begin + 1);
    }

    public ElseEndBlock Partner { get; set; }

    public override bool Breakable => false;

    public override bool IsContainer => true;

    public override bool IsUnprotected => true;

    public override int ScopeEnd => End - 2;

    public override void AddStatement(Statement.Statement statement)
    {
        m_statements.Add(statement);
    }

    public override int CompareTo(Block block)
    {
        if (block == Partner)
            return -1;

        return base.CompareTo(block);
    }

    public override int GetLoopback()
    {
        return m_loopback;
    }

    public override void Print(Output output)
    {
        output.Print("if ");

        m_branch.AsExpression(m_r)
            .Print(output);

        output.Print(" then");
        output.PrintLine();

        output.IncreaseIndent();

        //Handle the case where the "then" is empty in if-then-else.
        //The jump over the else block is falsely detected as a break.
        if (m_statements.Count == 1 && m_statements[0] is Break)
        {
            var b = m_statements[0] as Break;

            if (b.Target == m_loopback)
            {
                output.DecreaseIndent();
                return;
            }
        }

        PrintSequence(output, m_statements);

        output.DecreaseIndent();

        if (m_emptyElse)
        {
            output.PrintLine("else");
            output.PrintLine("end");
        }
    }
}
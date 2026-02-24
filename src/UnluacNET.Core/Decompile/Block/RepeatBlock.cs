using UnluacNET.Core.Parse;

namespace UnluacNET.Core.Decompile.Block;

public class RepeatBlock : Block
{
    private readonly Branch.Branch m_branch;
    private readonly Registers m_r;
    private readonly List<Statement.Statement> m_statements;

    public RepeatBlock(
        LFunction function,
        Branch.Branch branch,
        Registers r
    ) : base(function, branch.End, branch.Begin)
    {
        m_branch = branch;
        m_r = r;

        m_statements = new List<Statement.Statement>(branch.Begin - branch.End + 1);
    }

    public override bool Breakable => true;

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
        output.Print("repeat");
        output.PrintLine();

        output.IncreaseIndent();

        PrintSequence(output, m_statements);

        output.DecreaseIndent();

        output.Print("until ");
        m_branch.AsExpression(m_r)
            .Print(output);
    }
}
using UnluacNET.Core.Parse;

namespace UnluacNET.Core.Decompile.Block;

public class WhileBlock : Block
{
    private readonly Branch.Branch m_branch;
    private readonly int m_loopback;
    private readonly Registers m_registers;
    private readonly List<Statement.Statement> m_statements;

    public WhileBlock(
        LFunction function,
        Branch.Branch branch,
        int loopback,
        Registers registers
    ) : base(function, branch.Begin, branch.End)
    {
        m_branch = branch;
        m_loopback = loopback;
        m_registers = registers;

        m_statements = new List<Statement.Statement>(branch.End - branch.Begin + 1);
    }

    public override int ScopeEnd => End - 2;

    public override bool Breakable => true;

    public override bool IsContainer => true;

    public override bool IsUnprotected => true;

    public override void AddStatement(Statement.Statement statement)
    {
        m_statements.Add(statement);
    }

    public override int GetLoopback()
    {
        return m_loopback;
    }

    public override void Print(Output output)
    {
        output.Print("while ");

        m_branch.AsExpression(m_registers)
            .Print(output);

        output.Print(" do");
        output.PrintLine();

        output.IncreaseIndent();

        PrintSequence(output, m_statements);

        output.DecreaseIndent();

        output.Print("end");
    }
}
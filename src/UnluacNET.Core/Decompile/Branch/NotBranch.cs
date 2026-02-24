namespace UnluacNET.Core.Decompile.Branch;

public class NotBranch : Branch
{
    private readonly Branch m_branch;

    public NotBranch(Branch branch) : base(branch.Line, branch.Begin, branch.End)
    {
        m_branch = branch;
    }

    public override Expression.Expression AsExpression(Registers registers)
    {
        return Expression.Expression.MakeNot(m_branch.AsExpression(registers));
    }

    public override int GetRegister()
    {
        return m_branch.GetRegister();
    }

    public override Branch Invert()
    {
        return m_branch;
    }

    public override void UseExpression(Expression.Expression expression)
    {
        // Do nothing
    }
}
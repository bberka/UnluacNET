namespace UnluacNET.Core.Decompile.Expression;

public class TableReference : Expression
{
    private readonly Expression m_index;
    private readonly Expression m_table;

    public TableReference(Expression table, Expression index) : base(PRECEDENCE_ATOMIC)
    {
        m_table = table;
        m_index = index;
    }

    public override int ConstantIndex => Math.Max(m_table.ConstantIndex, m_index.ConstantIndex);

    public override bool IsDotChain => m_index.IsIdentifier && m_table.IsDotChain;

    public override bool IsMemberAccess => m_index.IsIdentifier;

    public override string GetField()
    {
        return m_index.AsName();
    }

    public override Expression GetTable()
    {
        return m_table;
    }

    public override void Print(Output output)
    {
        m_table.Print(output);

        if (m_index.IsIdentifier)
        {
            output.Print(".");
            output.Print(m_index.AsName());
        }
        else
        {
            output.Print("[");
            m_index.Print(output);
            output.Print("]");
        }
    }
}
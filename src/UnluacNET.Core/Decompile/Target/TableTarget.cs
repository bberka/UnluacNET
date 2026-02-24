using UnluacNET.Core.Decompile.Expression;

namespace UnluacNET.Core.Decompile.Target;

public class TableTarget : Target
{
    private readonly Expression.Expression m_index;
    private readonly Expression.Expression m_table;

    public TableTarget(Expression.Expression table, Expression.Expression index)
    {
        m_table = table;
        m_index = index;
    }

    public override bool IsFunctionName
    {
        get
        {
            if (!m_index.IsIdentifier)
                return false;
            if (!m_table.IsDotChain)
                return false;

            return true;
        }
    }

    public override void Print(Output output)
    {
        new TableReference(m_table, m_index).Print(output);
    }

    public override void PrintMethod(Output output)
    {
        m_table.Print(output);
        output.Print(":");
        output.Print(m_index.AsName());
    }
}
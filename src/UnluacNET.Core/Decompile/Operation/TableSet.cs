using UnluacNET.Core.Decompile.Expression;
using UnluacNET.Core.Decompile.Statement;
using UnluacNET.Core.Decompile.Target;

namespace UnluacNET.Core.Decompile.Operation;

public class TableSet : Operation
{
    private readonly Expression.Expression m_index;

    private readonly bool m_isTable;
    private readonly Expression.Expression m_table;
    private readonly int m_timestamp;
    private readonly Expression.Expression m_value;

    public TableSet(
        int line,
        Expression.Expression table,
        Expression.Expression index,
        Expression.Expression value,
        bool isTable,
        int timestamp
    ) : base(line)
    {
        m_table = table;
        m_index = index;
        m_value = value;

        m_isTable = isTable;
        m_timestamp = timestamp;
    }

    public override Statement.Statement Process(Registers r, Block.Block block)
    {
        // .isTableLiteral() is sufficient when there is debugging info
        // TODO: Fix the commented out section screwing up tables
        if (m_table.IsTableLiteral /*&& (m_value.IsMultiple || m_table.IsNewEntryAllowed)*/)
        {
            m_table.AddEntry(new TableLiteral.Entry(m_index, m_value, !m_isTable, m_timestamp));
            return null;
        }

        return new Assignment(new TableTarget(m_table, m_index), m_value);
    }
}
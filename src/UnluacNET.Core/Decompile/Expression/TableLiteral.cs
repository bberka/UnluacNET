namespace UnluacNET.Core.Decompile.Expression;

public class TableLiteral : Expression
{
    private readonly int m_capacity;

    private readonly List<Entry> m_entries;
    private bool m_isList = true;

    private bool m_isObject = true;
    private int m_listLength = 1;

    public TableLiteral(int arraySize, int hashSize) : base(PRECEDENCE_ATOMIC)
    {
        m_capacity = arraySize + hashSize;
        m_entries = new List<Entry>(m_capacity);
    }

    public override int ConstantIndex
    {
        get
        {
            var index = -1;

            foreach (var entry in m_entries)
            {
                index = Math.Max(entry.Key.ConstantIndex, index);
                index = Math.Max(entry.Value.ConstantIndex, index);
            }

            return index;
        }
    }

    public override bool IsBrief => false;

    public override bool IsNewEntryAllowed => m_entries.Count < m_capacity;

    public override bool IsTableLiteral => true;

    private void PrintEntry(int index, Output output)
    {
        var entry = m_entries[index];

        var key = entry.Key;
        var value = entry.Value;

        var isList = entry.IsList;
        var multiple = index + 1 >= m_entries.Count || value.IsMultiple;

        if (isList && key.IsInteger && m_listLength == key.AsInteger())
        {
            if (multiple)
                value.PrintMultiple(output);
            else
                value.Print(output);

            m_listLength++;
        }
        else if (m_isObject && key.IsIdentifier)
        {
            output.Print(key.AsName());
            output.Print(" = ");

            value.Print(output);
        }
        else
        {
            output.Print("[");
            key.Print(output);
            output.Print("] = ");
            value.Print(output);
        }
    }

    public override void AddEntry(Entry entry)
    {
        m_entries.Add(entry);

        m_isObject = m_isObject && (entry.IsList || entry.Key.IsIdentifier);
        m_isList = m_isList && entry.IsList;
    }

    public override void Print(Output output)
    {
        m_entries.Sort();
        m_listLength = 1;

        if (m_entries.Count == 0)
        {
            output.Print("{}");
        }
        else
        {
            var lineBreak = (m_isList && m_entries.Count > 5) || (m_isObject && m_entries.Count > 2) || !m_isObject;

            if (!lineBreak)
                foreach (var entry in m_entries)
                {
                    var value = entry.Value;

                    if (!value.IsBrief)
                    {
                        lineBreak = true;
                        break;
                    }
                }

            output.Print("{");

            if (lineBreak)
            {
                output.PrintLine();
                output.IncreaseIndent();
            }

            PrintEntry(0, output);

            if (!m_entries[0].Value.IsMultiple)
                for (var i = 1; i < m_entries.Count; i++)
                {
                    output.Print(",");

                    if (lineBreak)
                        output.PrintLine();
                    else
                        output.Print(" ");

                    PrintEntry(i, output);

                    if (m_entries[i].Value.IsMultiple)
                        break;
                }

            if (lineBreak)
            {
                output.PrintLine();
                output.DecreaseIndent();
            }

            output.Print("}");
        }
    }

    public sealed class Entry : IComparable<Entry>
    {
        public Entry(
            Expression key,
            Expression value,
            bool isList,
            int timestamp
        )
        {
            Key = key;
            Value = value;
            IsList = isList;
            Timestamp = timestamp;
        }

        public Expression Key { get; }
        public Expression Value { get; }

        public bool IsList { get; }
        public int Timestamp { get; }

        public int CompareTo(Entry e)
        {
            return Timestamp.CompareTo(e.Timestamp);
        }
    }
}
using UnluacNET.Core.Decompile.Expression;
using UnluacNET.Core.Parse;

namespace UnluacNET.Core.Decompile;

public class Upvalues
{
    private readonly LUpvalue[] m_upvalues;

    public Upvalues(LUpvalue[] upvalues)
    {
        m_upvalues = upvalues;
    }

    public string GetName(int idx)
    {
        if (idx < m_upvalues.Length && m_upvalues[idx].Name != null)
            return m_upvalues[idx].Name;

        return string.Format("_UPVALUE{0}_", idx);
    }

    public UpvalueExpression GetExpression(int index)
    {
        return new UpvalueExpression(GetName(index));
    }
}
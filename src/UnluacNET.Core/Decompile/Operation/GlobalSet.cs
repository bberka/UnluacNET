using UnluacNET.Core.Decompile.Statement;
using UnluacNET.Core.Decompile.Target;

namespace UnluacNET.Core.Decompile.Operation;

public class GlobalSet : Operation
{
    private readonly string m_global;
    private readonly Expression.Expression m_value;

    public GlobalSet(
        int line,
        string global,
        Expression.Expression value
    ) : base(line)
    {
        m_global = global;
        m_value = value;
    }

    public override Statement.Statement Process(Registers r, Block.Block block)
    {
        return new Assignment(new GlobalTarget(m_global), m_value);
    }
}
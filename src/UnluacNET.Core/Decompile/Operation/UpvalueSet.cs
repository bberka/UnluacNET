using UnluacNET.Core.Decompile.Statement;
using UnluacNET.Core.Decompile.Target;

namespace UnluacNET.Core.Decompile.Operation;

public class UpvalueSet : Operation
{
    private readonly UpvalueTarget m_target;
    private readonly Expression.Expression m_value;

    public UpvalueSet(
        int line,
        string upvalue,
        Expression.Expression value
    ) : base(line)
    {
        m_target = new UpvalueTarget(upvalue);
        m_value = value;
    }

    public override Statement.Statement Process(Registers r, Block.Block block)
    {
        return new Assignment(m_target, m_value);
    }
}
using UnluacNET.Core.Decompile.Statement;

namespace UnluacNET.Core.Decompile.Operation;

public class ReturnOperation : Operation
{
    private readonly Expression.Expression[] m_values;

    public ReturnOperation(int line, Expression.Expression value) : base(line)
    {
        m_values = new Expression.Expression[1]
        {
            value
        };
    }

    public ReturnOperation(int line, Expression.Expression[] values) : base(line)
    {
        m_values = values;
    }

    public override Statement.Statement Process(Registers r, Block.Block block)
    {
        return new Return(m_values);
    }
}
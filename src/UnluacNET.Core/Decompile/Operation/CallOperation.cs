using UnluacNET.Core.Decompile.Expression;
using UnluacNET.Core.Decompile.Statement;

namespace UnluacNET.Core.Decompile.Operation;

public class CallOperation : Operation
{
    private readonly FunctionCall m_call;

    public CallOperation(int line, FunctionCall call) : base(line)
    {
        m_call = call;
    }

    public override Statement.Statement Process(Registers r, Block.Block block)
    {
        return new FunctionCallStatement(m_call);
    }
}
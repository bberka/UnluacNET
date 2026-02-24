using UnluacNET.Core.Decompile.Expression;

namespace UnluacNET.Core.Decompile.Statement;

public class FunctionCallStatement : Statement
{
    private readonly FunctionCall m_call;

    public FunctionCallStatement(FunctionCall call)
    {
        m_call = call;
    }

    public override bool BeginsWithParen => m_call.BeginsWithParen;

    public override void Print(Output output)
    {
        m_call.Print(output);
    }
}
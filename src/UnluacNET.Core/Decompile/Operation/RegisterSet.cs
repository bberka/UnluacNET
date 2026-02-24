using UnluacNET.Core.Decompile.Statement;

namespace UnluacNET.Core.Decompile.Operation;

public class RegisterSet : Operation
{
    public RegisterSet(
        int line,
        int register,
        Expression.Expression value
    ) : base(line)
    {
        Register = register;
        Value = value;
    }

    public int Register { get; }
    public Expression.Expression Value { get; }

    public override Statement.Statement Process(Registers r, Block.Block block)
    {
        r.SetValue(Register, Line, Value);

        if (r.IsAssignable(Register, Line))
            return new Assignment(r.GetTarget(Register, Line), Value);
        return null;
    }
}
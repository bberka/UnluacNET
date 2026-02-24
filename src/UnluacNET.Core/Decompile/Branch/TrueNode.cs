using UnluacNET.Core.Decompile.Expression;
using UnluacNET.Core.Parse;

namespace UnluacNET.Core.Decompile.Branch;

public class TrueNode : Branch
{
    public TrueNode(
        int register,
        bool inverted,
        int line,
        int begin,
        int end
    ) : base(line, begin, end)
    {
        Register = register;
        Inverted = inverted;
        SetTarget = register;

        //???
        //IsTest = true;
    }

    public int Register { get; }
    public bool Inverted { get; }

    public override Expression.Expression AsExpression(Registers registers)
    {
        return new ConstantExpression(new Constant(Inverted ? LBoolean.LTRUE : LBoolean.LFALSE), -1);
    }

    public override int GetRegister()
    {
        return Register;
    }

    public override Branch Invert()
    {
        return new TrueNode(Register, !Inverted, Line, End, Begin);
    }

    public override void UseExpression(Expression.Expression expression)
    {
        // Do nothing
    }

    public override string ToString()
    {
        return string.Format("TrueNode[register={0};inverted={1};line={2};begin={3};end={4}]", Register, Inverted, Line, Begin, End);
    }
}
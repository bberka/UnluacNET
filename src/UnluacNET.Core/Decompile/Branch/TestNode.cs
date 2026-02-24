namespace UnluacNET.Core.Decompile.Branch;

public class TestNode : Branch
{
    public TestNode(
        int test,
        bool inverted,
        int line,
        int begin,
        int end
    ) : base(line, begin, end)
    {
        Test = test;
        Inverted = inverted;

        IsTest = true;
    }

    public int Test { get; }
    public bool Inverted { get; }

    public override Expression.Expression AsExpression(Registers registers)
    {
        if (Inverted)
            return new NotBranch(Invert()).AsExpression(registers);

        return registers.GetExpression(Test, Line);
    }

    public override int GetRegister()
    {
        return Test;
    }

    public override Branch Invert()
    {
        return new TestNode(Test, !Inverted, Line, End, Begin);
    }

    public override void UseExpression(Expression.Expression expression)
    {
        // Do nothing
    }

    public override string ToString()
    {
        return string.Format("TestNode[test={0};inverted={1};line={2};begin={3};end={4}]", Test, Inverted, Line, Begin, End);
    }
}
namespace UnluacNET.Core.Decompile.Statement;

public class Return : Statement
{
    private readonly Expression.Expression[] values;

    public Return()
    {
        values = new Expression.Expression[0];
    }

    public Return(Expression.Expression value)
    {
        values = new Expression.Expression[1]
        {
            value
        };
    }

    public Return(Expression.Expression[] values)
    {
        this.values = values;
    }

    public override void Print(Output output)
    {
        output.Print("do ");
        PrintTail(output);
        output.Print(" end");
    }

    public override void PrintTail(Output output)
    {
        output.Print("return");

        if (values.Length > 0)
        {
            output.Print(" ");

            var rtns = new List<Expression.Expression>(values.Length);

            foreach (var value in values)
                rtns.Add(value);

            Expression.Expression.PrintSequence(output, rtns, false, true);
        }
    }
}
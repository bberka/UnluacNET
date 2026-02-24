namespace UnluacNET.Core.Decompile.Branch;

public abstract class Branch
{
    public Branch(
        int line,
        int begin,
        int end
    )
    {
        Line = line;
        Begin = begin;
        End = end;
    }

    public int Line { get; private set; }

    public int Begin { get; set; }
    public int End { get; set; } // Might be modified to undo redirect

    public bool IsSet { get; set; }
    public bool IsCompareSet { get; set; }
    public bool IsTest { get; set; }

    public int SetTarget { get; set; } = -1;

    public abstract Branch Invert();
    public abstract int GetRegister();
    public abstract Expression.Expression AsExpression(Registers registers);
    public abstract void UseExpression(Expression.Expression expression);
}
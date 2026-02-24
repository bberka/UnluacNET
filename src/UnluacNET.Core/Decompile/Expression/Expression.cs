using UnluacNET.Core.Parse;

namespace UnluacNET.Core.Decompile.Expression;

public abstract class Expression
{
    public static readonly int PRECEDENCE_OR = 1;
    public static readonly int PRECEDENCE_AND = 2;
    public static readonly int PRECEDENCE_COMPARE = 3;
    public static readonly int PRECEDENCE_CONCAT = 4;
    public static readonly int PRECEDENCE_ADD = 5;
    public static readonly int PRECEDENCE_MUL = 6;
    public static readonly int PRECEDENCE_UNARY = 7;
    public static readonly int PRECEDENCE_POW = 8;
    public static readonly int PRECEDENCE_ATOMIC = 9;

    public static readonly int ASSOCIATIVITY_NONE = 0;
    public static readonly int ASSOCIATIVITY_LEFT = 1;
    public static readonly int ASSOCIATIVITY_RIGHT = 2;

    public static readonly Expression NIL = new ConstantExpression(new Constant(LNil.NIL), -1);

    public Expression(int precedence)
    {
        Precedence = precedence;
    }

    public abstract int ConstantIndex { get; }

    public virtual int ClosureUpvalueLine => throw new InvalidOperationException();

    public virtual bool BeginsWithParen => false;

    public virtual bool IsBoolean => false;

    public virtual bool IsBrief => false;

    public virtual bool IsClosure => false;

    public virtual bool IsConstant => false;

    public virtual bool IsDotChain => false;

    public virtual bool IsIdentifier => false;

    public virtual bool IsInteger => false;

    public virtual bool IsMultiple => false;

    public virtual bool IsMemberAccess => false;

    public virtual bool IsNil => false;

    public virtual bool IsString => false;

    public virtual bool IsTableLiteral => false;

    public virtual bool IsNewEntryAllowed => false;

    public int Precedence { get; private set; }

    public static BinaryExpression MakeAdd(Expression left, Expression right)
    {
        return new BinaryExpression("+", left, right, PRECEDENCE_ADD, ASSOCIATIVITY_LEFT);
    }

    public static BinaryExpression MakeAnd(Expression left, Expression right)
    {
        return new BinaryExpression("and", left, right, PRECEDENCE_AND, ASSOCIATIVITY_NONE);
    }

    public static BinaryExpression MakeConcat(Expression left, Expression right)
    {
        return new BinaryExpression("..", left, right, PRECEDENCE_CONCAT, ASSOCIATIVITY_RIGHT);
    }

    public static BinaryExpression MakeDiv(Expression left, Expression right)
    {
        return new BinaryExpression("/", left, right, PRECEDENCE_MUL, ASSOCIATIVITY_LEFT);
    }

    public static BinaryExpression MakeMod(Expression left, Expression right)
    {
        return new BinaryExpression("%", left, right, PRECEDENCE_MUL, ASSOCIATIVITY_LEFT);
    }

    public static BinaryExpression MakeMul(Expression left, Expression right)
    {
        return new BinaryExpression("*", left, right, PRECEDENCE_MUL, ASSOCIATIVITY_LEFT);
    }

    public static BinaryExpression MakeOr(Expression left, Expression right)
    {
        return new BinaryExpression("or", left, right, PRECEDENCE_OR, ASSOCIATIVITY_NONE);
    }

    public static BinaryExpression MakePow(Expression left, Expression right)
    {
        return new BinaryExpression("^", left, right, PRECEDENCE_POW, ASSOCIATIVITY_RIGHT);
    }

    public static BinaryExpression MakeSub(Expression left, Expression right)
    {
        return new BinaryExpression("-", left, right, PRECEDENCE_ADD, ASSOCIATIVITY_LEFT);
    }

    public static UnaryExpression MakeUnm(Expression expression)
    {
        return new UnaryExpression("-", expression, PRECEDENCE_UNARY);
    }

    public static UnaryExpression MakeNot(Expression expression)
    {
        return new UnaryExpression("not ", expression, PRECEDENCE_UNARY);
    }

    public static UnaryExpression MakeLen(Expression expression)
    {
        return new UnaryExpression("#", expression, PRECEDENCE_UNARY);
    }

    public static void PrintSequence(
        Output output,
        List<Expression> exprs,
        bool lineBreak,
        bool multiple
    )
    {
        var n = exprs.Count;
        var i = 1;

        foreach (var expr in exprs)
        {
            var last = i == n;

            if (expr.IsMultiple)
                last = true;

            if (last)
            {
                if (multiple)
                    expr.PrintMultiple(output);
                else
                    expr.Print(output);

                break;
            }

            expr.Print(output);
            output.Print(",");

            if (lineBreak)
                output.PrintLine();
            else
                output.Print(" ");

            i++;
        }
    }

    protected static void PrintBinary(
        Output output,
        string op,
        Expression left,
        Expression right
    )
    {
        left.Print(output);
        output.Print(" ");
        output.Print(op);
        output.Print(" ");
        right.Print(output);
    }

    protected static void PrintUnary(
        Output output,
        string op,
        Expression expression
    )
    {
        output.Print(op);
        expression.Print(output);
    }

    public virtual bool IsUpvalueOf(int register)
    {
        throw new InvalidOperationException();
    }

    public virtual void AddEntry(TableLiteral.Entry entry)
    {
        throw new InvalidOperationException();
    }

    public virtual string GetField()
    {
        throw new InvalidOperationException();
    }

    public virtual Expression GetTable()
    {
        throw new InvalidOperationException();
    }

    public abstract void Print(Output output);

    public virtual void PrintClosure(Output output, Target.Target name)
    {
        throw new InvalidOperationException();
    }

    public virtual void PrintMultiple(Output output)
    {
        Print(output);
    }

    public virtual string AsName()
    {
        throw new InvalidOperationException();
    }

    public virtual int AsInteger()
    {
        throw new InvalidOperationException();
    }
}
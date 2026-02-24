using UnluacNET.Core.Decompile.Operation;
using UnluacNET.Core.Decompile.Statement;
using UnluacNET.Core.Parse;

namespace UnluacNET.Core.Decompile.Block;

public class SetBlock : Block
{
    private readonly bool m_empty;
    private readonly Registers m_r;
    private Assignment m_assign;
    private bool m_finalize;

    public SetBlock(
        LFunction function,
        Branch.Branch branch,
        int target,
        int line,
        int begin,
        int end,
        bool empty,
        Registers r
    ) : base(function, begin, end)
    {
        m_empty = empty;

        if (begin == end)
            Begin -= 1;

        Target = target;
        Branch = branch;

        m_r = r;
    }

    public int Target { get; }
    public Branch.Branch Branch { get; }

    public override bool Breakable => false;

    public override bool IsContainer => false;

    public override bool IsUnprotected => false;

    public override void AddStatement(Statement.Statement statement)
    {
        if (!m_finalize && statement is Assignment)
            m_assign = statement as Assignment;
        else if (statement is BooleanIndicator)
            m_finalize = true;
    }

    public override int GetLoopback()
    {
        throw new InvalidOperationException();
    }

    public Expression.Expression GetValue()
    {
        return Branch.AsExpression(m_r);
    }

    public override void Print(Output output)
    {
        if (m_assign != null)
        {
            var target = m_assign.GetFirstTarget();

            if (target != null)
            {
                new Assignment(target, GetValue()).Print(output);
            }
            else
            {
                output.Print("-- unhandled set block");
                output.PrintLine();
            }
        }
    }

    public override Operation.Operation Process(Decompiler d)
    {
        if (m_empty)
        {
            var expr = m_r.GetExpression(Branch.SetTarget, End);
            Branch.UseExpression(expr);

            return new RegisterSet(End - 1, Branch.SetTarget, Branch.AsExpression(m_r));
        }

        if (m_assign != null)
        {
            Branch.UseExpression(m_assign.GetFirstValue());

            var target = m_assign.GetFirstTarget();
            var value = GetValue();

            return new LambdaOperation(End - 1, (r, block) => { return new Assignment(target, value); });
        }

        return new LambdaOperation(End - 1, (r, block) =>
        {
            Expression.Expression expr = null;

            var register = 0;

            for (; register < r.NumRegisters; register++)
                if (r.GetUpdated(register, Branch.End - 1) == Branch.End - 1)
                {
                    expr = r.GetValue(register, Branch.End);
                    break;
                }

            if (d.Code.Op(Branch.End - 2) == Op.Loadbool && d.Code.C(Branch.End - 2) != 0)
            {
                var target = d.Code.A(Branch.End - 2);

                if (d.Code.Op(Branch.End - 3) == Op.Jmp && d.Code.SBx(Branch.End - 3) == 2)
                    expr = r.GetValue(target, Branch.End - 2);
                else
                    expr = r.GetValue(target, Branch.Begin);

                Branch.UseExpression(expr);

                if (r.IsLocal(target, Branch.End - 1))
                    return new Assignment(r.GetTarget(target, Branch.End - 1), Branch.AsExpression(r));

                r.SetValue(target, Branch.End - 1, Branch.AsExpression(r));
            }
            else if (expr != null && Target >= 0)
            {
                Branch.UseExpression(expr);

                if (r.IsLocal(Target, Branch.End - 1))
                    return new Assignment(r.GetTarget(Target, Branch.End - 1), Branch.AsExpression(r));

                r.SetValue(Target, Branch.End - 1, Branch.AsExpression(r));
            }
            else
            {
                Console.WriteLine("-- fail " + (Branch.End - 1));
                Console.WriteLine(expr);
                Console.WriteLine(Target);
            }

            return null;
        });
    }

    public void UseAssignment(Assignment assignment)
    {
        m_assign = assignment;
        Branch.UseExpression(assignment.GetFirstValue());
    }
}
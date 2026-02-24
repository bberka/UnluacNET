using UnluacNET.Core.Decompile.Operation;
using UnluacNET.Core.Parse;

namespace UnluacNET.Core.Decompile.Block;

public abstract class Block : Statement.Statement, IComparable<Block>
{
    public Block(
        LFunction function,
        int begin,
        int end
    )
    {
        Function = function;

        Begin = begin;
        End = end;
    }

    protected LFunction Function { get; private set; }

    public int Begin { get; set; }
    public int End { get; set; }

    public bool LoopRedirectAdjustment { get; set; }

    public virtual int ScopeEnd => End - 1;

    public abstract bool Breakable { get; }
    public abstract bool IsContainer { get; }
    public abstract bool IsUnprotected { get; }

    public virtual int CompareTo(Block block)
    {
        if (Begin < block.Begin) return -1;

        if (Begin == block.Begin)
        {
            if (End < block.End) return 1;

            if (End == block.End)
            {
                if (IsContainer && !block.IsContainer) return -1;

                if (!IsContainer && block.IsContainer) return 1;

                return 0;
            }

            return -1;
        }

        return 1;
    }

    public abstract void AddStatement(Statement.Statement statement);
    public abstract int GetLoopback();

    public virtual bool Contains(Block block)
    {
        return Begin <= block.Begin && End >= block.End;
    }

    public virtual bool Contains(int line)
    {
        return Begin <= line && line < End;
    }

    public virtual Operation.Operation Process(Decompiler d)
    {
        var statement = this;

        return new LambdaOperation(End - 1, (r, block) => { return statement; });
    }
}
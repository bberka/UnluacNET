using UnluacNET.Core.Decompile.Operation;
using UnluacNET.Core.Parse;

namespace UnluacNET.Core.Decompile.Block;

public class CompareBlock : Block
{
    public CompareBlock(
        LFunction function,
        int begin,
        int end,
        int target,
        Branch.Branch branch
    ) : base(function, begin, end)
    {
        Target = target;
        Branch = branch;
    }

    public int Target { get; set; }
    public Branch.Branch Branch { get; set; }

    public override bool Breakable => false;

    public override bool IsContainer => false;

    public override bool IsUnprotected => false;

    public override void AddStatement(Statement.Statement statement)
    {
        // Do nothing
    }

    public override int GetLoopback()
    {
        throw new InvalidOperationException();
    }

    public override void Print(Output output)
    {
        output.Print("-- unhandled compare assign");
    }

    public override Operation.Operation Process(Decompiler d)
    {
        return new LambdaOperation(End - 1, (r, block) => { return new RegisterSet(End - 1, Target, Branch.AsExpression(r)).Process(r, block); });
    }
}
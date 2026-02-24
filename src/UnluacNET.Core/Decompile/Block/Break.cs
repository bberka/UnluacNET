using UnluacNET.Core.Parse;

namespace UnluacNET.Core.Decompile.Block;

public class Break : Block
{
    public Break(
        LFunction function,
        int line,
        int target
    ) : base(function, line, line)
    {
        Target = target;
    }

    public int Target { get; private set; }

    public override bool Breakable => false;

    public override bool IsContainer => false;

    public override bool IsUnprotected =>
        //Actually, it is unprotected, but not really a block
        false;

    public override void AddStatement(Statement.Statement statement)
    {
        throw new InvalidOperationException();
    }

    public override int GetLoopback()
    {
        throw new InvalidOperationException();
    }

    public override void Print(Output output)
    {
        output.Print("do return end");
    }

    public override void PrintTail(Output output)
    {
        output.Print("break");
    }
}
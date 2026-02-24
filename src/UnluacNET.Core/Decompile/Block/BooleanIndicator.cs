using UnluacNET.Core.Parse;

namespace UnluacNET.Core.Decompile.Block;

public class BooleanIndicator : Block
{
    public BooleanIndicator(LFunction function, int line) : base(function, line, line)
    {
    }

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
        output.Print("-- unhandled boolean indicator");
    }
}
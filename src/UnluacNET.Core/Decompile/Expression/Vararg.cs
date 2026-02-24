namespace UnluacNET.Core.Decompile.Expression;

public class Vararg : Expression
{
    private readonly int m_length;
    private readonly bool m_multiple;

    public Vararg(int length, bool multiple) : base(PRECEDENCE_ATOMIC)
    {
        m_length = length;
        m_multiple = multiple;
    }

    public override int ConstantIndex => -1;

    public override bool IsMultiple => m_multiple;

    public override void Print(Output output)
    {
        //output.Print("...");
        output.Print(m_multiple ? "..." : "(...)");
    }

    public override void PrintMultiple(Output output)
    {
        output.Print(m_multiple ? "..." : "(...)");
    }
}
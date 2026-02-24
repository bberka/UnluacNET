namespace UnluacNET.Core.Decompile.Target;

public class UpvalueTarget : Target
{
    private readonly string m_name;

    public UpvalueTarget(string name)
    {
        m_name = name;
    }

    public override void Print(Output output)
    {
        output.Print(m_name);
    }

    public override void PrintMethod(Output output)
    {
        throw new InvalidOperationException();
    }
}
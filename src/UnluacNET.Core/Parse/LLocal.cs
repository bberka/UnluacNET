namespace UnluacNET.Core.Parse;

public class LLocal : BObject
{
    public LLocal(
        LString name,
        BInteger start,
        BInteger end
    )
    {
        Name = name;

        Start = start.AsInteger();
        End = end.AsInteger();
    }

    public LString Name { get; }

    public int Start { get; private set; }
    public int End { get; private set; }

    /* Used by the decompiler for annotation. */
    internal bool ForLoop { get; set; }

    public override string ToString()
    {
        return Name.DeRef();
    }
}
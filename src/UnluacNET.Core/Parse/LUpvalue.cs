namespace UnluacNET.Core.Parse;

public class LUpvalue : BObject
{
    public int Index { get; set; }

    public bool InStack { get; set; }

    public string Name { get; set; }

    public override bool Equals(object obj)
    {
        var upVal = obj as LUpvalue;

        if (upVal != null)
        {
            if (!(InStack == upVal.InStack && Index == upVal.Index)) return false;

            if (Name == upVal.Name) return true;

            return Name != null && Name == upVal.Name;
        }

        return false;
    }
}
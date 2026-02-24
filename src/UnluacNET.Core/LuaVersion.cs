using UnluacNET.Core.Decompile;
using UnluacNET.Core.Parse;

namespace UnluacNET.Core;

public abstract class LuaVersion(int versionNumber)
{
    public static readonly LuaVersion LUA51 = new LuaVersion51();
    public static readonly LuaVersion LUA52 = new LuaVersion52();

    protected int MVersionNumber = versionNumber;

    public abstract bool HasHeaderTail { get; }

    public abstract int OuterBlockScopeAdjustment { get; }

    public abstract Op ForTarget { get; }

    public abstract bool UsesInlineUpvalueDeclaritions { get; }
    public abstract bool UsesOldLoadNilEncoding { get; }

    public abstract LFunctionType GetLFunctionType();
    public abstract bool IsBreakableLoopEnd(Op op);

    public OpcodeMap GetOpcodeMap()
    {
        return new OpcodeMap(MVersionNumber);
    }
}
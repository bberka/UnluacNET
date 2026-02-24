using UnluacNET.Core.Decompile;
using UnluacNET.Core.Parse;

namespace UnluacNET.Core;

public sealed class LuaVersion52() : LuaVersion(0x52)
{
    public override bool HasHeaderTail => true;

    public override int OuterBlockScopeAdjustment => 0;

    public override Op ForTarget => Op.Tforcall;

    public override bool UsesInlineUpvalueDeclaritions => false;

    public override bool UsesOldLoadNilEncoding => false;

    public override LFunctionType GetLFunctionType()
    {
        return LFunctionType.TYPE52;
    }

    public override bool IsBreakableLoopEnd(Op op)
    {
        return op == Op.Jmp || op == Op.Forloop || op == Op.Tforloop;
    }
}
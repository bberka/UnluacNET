using UnluacNET.Core.Decompile;
using UnluacNET.Core.Parse;

namespace UnluacNET.Core;

public sealed class LuaVersion51() : LuaVersion(0x51)
{
    public override bool HasHeaderTail => false;

    public override int OuterBlockScopeAdjustment => -1;

    public override Op ForTarget => Op.Tforloop;

    public override bool UsesInlineUpvalueDeclaritions => true;

    public override bool UsesOldLoadNilEncoding => true;

    public override LFunctionType GetLFunctionType()
    {
        return LFunctionType.TYPE51;
    }

    public override bool IsBreakableLoopEnd(Op op)
    {
        return op == Op.Jmp || op == Op.Forloop;
    }
}
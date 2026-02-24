namespace UnluacNET.Core.Decompile;

public class OpcodeMap
{
    private readonly int[] luaP_opmodes =
    {
        /*       T  A    B                 C                 mode             opcode       */
        Opmode(0, 1, OpArgMask.OpArgR, OpArgMask.OpArgN, OpMode.IAbc) /* OP_MOVE */,
        Opmode(0, 1, OpArgMask.OpArgK, OpArgMask.OpArgN, OpMode.IABx) /* OP_LOADK */,
        Opmode(0, 1, OpArgMask.OpArgU, OpArgMask.OpArgU, OpMode.IAbc) /* OP_LOADBOOL */,
        Opmode(0, 1, OpArgMask.OpArgR, OpArgMask.OpArgN, OpMode.IAbc) /* OP_LOADNIL */,
        Opmode(0, 1, OpArgMask.OpArgU, OpArgMask.OpArgN, OpMode.IAbc) /* OP_GETUPVAL */,
        Opmode(0, 1, OpArgMask.OpArgK, OpArgMask.OpArgN, OpMode.IABx) /* OP_GETGLOBAL */,
        Opmode(0, 1, OpArgMask.OpArgR, OpArgMask.OpArgK, OpMode.IAbc) /* OP_GETTABLE */,
        Opmode(0, 0, OpArgMask.OpArgK, OpArgMask.OpArgN, OpMode.IABx) /* OP_SETGLOBAL */,
        Opmode(0, 0, OpArgMask.OpArgU, OpArgMask.OpArgN, OpMode.IAbc) /* OP_SETUPVAL */,
        Opmode(0, 0, OpArgMask.OpArgK, OpArgMask.OpArgK, OpMode.IAbc) /* OP_SETTABLE */,
        Opmode(0, 1, OpArgMask.OpArgU, OpArgMask.OpArgU, OpMode.IAbc) /* OP_NEWTABLE */,
        Opmode(0, 1, OpArgMask.OpArgR, OpArgMask.OpArgK, OpMode.IAbc) /* OP_SELF */,
        Opmode(0, 1, OpArgMask.OpArgK, OpArgMask.OpArgK, OpMode.IAbc) /* OP_ADD */,
        Opmode(0, 1, OpArgMask.OpArgK, OpArgMask.OpArgK, OpMode.IAbc) /* OP_SUB */,
        Opmode(0, 1, OpArgMask.OpArgK, OpArgMask.OpArgK, OpMode.IAbc) /* OP_MUL */,
        Opmode(0, 1, OpArgMask.OpArgK, OpArgMask.OpArgK, OpMode.IAbc) /* OP_DIV */,
        Opmode(0, 1, OpArgMask.OpArgK, OpArgMask.OpArgK, OpMode.IAbc) /* OP_MOD */,
        Opmode(0, 1, OpArgMask.OpArgK, OpArgMask.OpArgK, OpMode.IAbc) /* OP_POW */,
        Opmode(0, 1, OpArgMask.OpArgR, OpArgMask.OpArgN, OpMode.IAbc) /* OP_UNM */,
        Opmode(0, 1, OpArgMask.OpArgR, OpArgMask.OpArgN, OpMode.IAbc) /* OP_NOT */,
        Opmode(0, 1, OpArgMask.OpArgR, OpArgMask.OpArgN, OpMode.IAbc) /* OP_LEN */,
        Opmode(0, 1, OpArgMask.OpArgR, OpArgMask.OpArgR, OpMode.IAbc) /* OP_CONCAT */,
        Opmode(0, 0, OpArgMask.OpArgR, OpArgMask.OpArgN, OpMode.IAsBx) /* OP_JMP */,
        Opmode(1, 0, OpArgMask.OpArgK, OpArgMask.OpArgK, OpMode.IAbc) /* OP_EQ */,
        Opmode(1, 0, OpArgMask.OpArgK, OpArgMask.OpArgK, OpMode.IAbc) /* OP_LT */,
        Opmode(1, 0, OpArgMask.OpArgK, OpArgMask.OpArgK, OpMode.IAbc) /* OP_LE */,
        Opmode(1, 1, OpArgMask.OpArgR, OpArgMask.OpArgU, OpMode.IAbc) /* OP_TEST */,
        Opmode(1, 1, OpArgMask.OpArgR, OpArgMask.OpArgU, OpMode.IAbc) /* OP_TESTSET */,
        Opmode(0, 1, OpArgMask.OpArgU, OpArgMask.OpArgU, OpMode.IAbc) /* OP_CALL */,
        Opmode(0, 1, OpArgMask.OpArgU, OpArgMask.OpArgU, OpMode.IAbc) /* OP_TAILCALL */,
        Opmode(0, 0, OpArgMask.OpArgU, OpArgMask.OpArgN, OpMode.IAbc) /* OP_RETURN */,
        Opmode(0, 1, OpArgMask.OpArgR, OpArgMask.OpArgN, OpMode.IAsBx) /* OP_FORLOOP */,
        Opmode(0, 1, OpArgMask.OpArgR, OpArgMask.OpArgN, OpMode.IAsBx) /* OP_FORPREP */,
        Opmode(1, 0, OpArgMask.OpArgN, OpArgMask.OpArgU, OpMode.IAbc) /* OP_TFORLOOP */,
        Opmode(0, 0, OpArgMask.OpArgU, OpArgMask.OpArgU, OpMode.IAbc) /* OP_SETLIST */,
        Opmode(0, 0, OpArgMask.OpArgN, OpArgMask.OpArgN, OpMode.IAbc) /* OP_CLOSE */,
        Opmode(0, 1, OpArgMask.OpArgU, OpArgMask.OpArgN, OpMode.IABx) /* OP_CLOSURE */,
        Opmode(0, 1, OpArgMask.OpArgU, OpArgMask.OpArgN, OpMode.IAbc) /* OP_VARARG */
    };

    private readonly Op[] m_map;

    public OpcodeMap(int version)
    {
        if (version == 0x51)
            m_map = new Op[38]
            {
                Op.Move,
                Op.Loadk,
                Op.Loadbool,
                Op.Loadnil,
                Op.Getupval,
                Op.Getglobal,
                Op.Gettable,
                Op.Setglobal,
                Op.Setupval,
                Op.Settable,
                Op.Newtable,
                Op.Self,
                Op.Add,
                Op.Sub,
                Op.Mul,
                Op.Div,
                Op.Mod,
                Op.Pow,
                Op.Unm,
                Op.Not,
                Op.Len,
                Op.Concat,
                Op.Jmp,
                Op.Eq,
                Op.Lt,
                Op.Le,
                Op.Test,
                Op.Testset,
                Op.Call,
                Op.Tailcall,
                Op.Return,
                Op.Forloop,
                Op.Forprep,
                Op.Tforloop,
                Op.Setlist,
                Op.Close,
                Op.Closure,
                Op.Vararg
            };
        else
            m_map = new Op[40]
            {
                Op.Move,
                Op.Loadk,
                Op.Loadkx,
                Op.Loadbool,
                Op.Loadnil,
                Op.Getupval,
                Op.Gettabup,
                Op.Gettable,
                Op.Settabup,
                Op.Setupval,
                Op.Settable,
                Op.Newtable,
                Op.Self,
                Op.Add,
                Op.Sub,
                Op.Mul,
                Op.Div,
                Op.Mod,
                Op.Pow,
                Op.Unm,
                Op.Not,
                Op.Len,
                Op.Concat,
                Op.Jmp,
                Op.Eq,
                Op.Lt,
                Op.Le,
                Op.Test,
                Op.Testset,
                Op.Call,
                Op.Tailcall,
                Op.Return,
                Op.Forloop,
                Op.Forprep,
                Op.Tforcall,
                Op.Tforloop,
                Op.Setlist,
                Op.Closure,
                Op.Vararg,
                Op.Extraarg
            };
    }

    public Op this[int opcode] => GetOp(opcode);

    /*
     ** masks for instruction properties. The format is:
     ** bits 0-1: op mode
     ** bits 2-3: C arg mode
     ** bits 4-5: B arg mode
     ** bit 6: instruction set register A
     ** bit 7: operator is a test
     */
    private static int Opmode(
        byte T,
        byte a,
        OpArgMask b,
        OpArgMask c,
        OpMode m
    )
    {
        return (T << 7) | (a << 6) | ((byte)b << 4) | ((byte)c << 2) | (byte)m;
    }

    public Op GetOp(int opcode)
    {
        if (opcode >= 0 && opcode < m_map.Length) return m_map[opcode];

        throw new ArgumentOutOfRangeException("opcode", opcode, "The specified opcode exceeds the boundaries of valid opcodes.");
    }

    public OpMode GetOpMode(int m)
    {
        return (OpMode)(luaP_opmodes[m] & 3);
    }

    public OpArgMask GetBMode(int m)
    {
        return (OpArgMask)((luaP_opmodes[m] >> 4) & 3);
    }

    public OpArgMask GetCMode(int m)
    {
        return (OpArgMask)((luaP_opmodes[m] >> 2) & 3);
    }

    public bool TestAMode(int m)
    {
        return (luaP_opmodes[m] & (1 << 6)) == 1;
    }

    public bool TestTMode(int m)
    {
        return (luaP_opmodes[m] & (1 << 7)) == 1;
    }
}
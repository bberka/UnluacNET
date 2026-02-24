using UnluacNET.Core.Parse;

namespace UnluacNET.Core.Decompile;

public enum OpMode
{
    IAbc,
    IABx,
    IAsBx
}

public enum OpArgMask
{
    /// <summary>
    ///     Argument is not used.
    /// </summary>
    OpArgN,

    /// <summary>
    ///     Argument is used.
    /// </summary>
    OpArgU,

    /// <summary>
    ///     Argument is a register or a jump offset.
    /// </summary>
    OpArgR,

    /// <summary>
    ///     Argument is a constant or register/constant.
    /// </summary>
    OpArgK
}

public sealed class LInstruction
{
    /*
     ** Size and position of opcode arguments
     */
    private const int SizeC = 9;
    private const int SizeB = 9;
    private const int SizeBx = SizeC + SizeB;
    private const int SizeA = 8;

    private const int SizeOp = 6;

    private const int PosOp = 0;
    private const int PosA = PosOp + SizeOp;
    private const int PosC = PosA + SizeA;
    private const int PosB = PosC + SizeC;
    private const int PosBx = PosC;

    /*
     ** Limits for opcode arguments
     ** (signed) int used to manipulate most arguments
     */
    private const int MaxargBx = (1 << SizeBx) - 1;
    private const int MaxargSBx = MaxargBx >> 1;

    private const int MaxargA = (1 << SizeA) - 1;
    private const int MaxargB = (1 << SizeB) - 1;
    private const int MaxargC = (1 << SizeC) - 1;

    /*
     ** Macros to operate RK indices
     */

    /* this bit 1 means constant (0 means register) */
    private const int Bitrk = 1 << (SizeB - 1);
    private const int Maxindexrk = Bitrk - 1;

    private static readonly int MaskGetopcode = Mask1(SizeOp, 0);
    private static readonly int MaskSetopcode = Mask1(SizeOp, PosOp);

    private static readonly int MaskGeta = Mask1(SizeA, 0);
    private static readonly int MaskSeta = Mask1(SizeA, PosA);

    private static readonly int MaskGetb = Mask1(SizeB, 0);
    private static readonly int MaskSetb = Mask1(SizeB, PosB);

    private static readonly int MaskGetc = Mask1(SizeC, 0);
    private static readonly int MaskSetc = Mask1(SizeC, PosC);

    private static readonly int MaskGetBx = Mask1(SizeBx, 0);
    private static readonly int MaskSetBx = Mask1(SizeBx, PosBx);

    private int m_value;

    public LInstruction(int value)
    {
        m_value = value;
    }

    public LInstruction(
        Op op,
        int a,
        int bx
    )
    {
        m_value = ((int)op << PosOp) | (a << PosA) | (bx << PosBx);
    }

    public LInstruction(
        Op op,
        int a,
        int b,
        int c
    )
    {
        m_value = ((int)op << PosOp) | (a << PosA) | (b << PosB) | (c << PosC);
    }

    public Op Op
    {
        get => (Op)GetOpCode(m_value);
        set =>
            m_value = (m_value & ~MaskSetopcode) | (((int)value << PosOp) & MaskSetopcode);
    }

    public int A
    {
        get => GetArgA(m_value);
        set =>
            m_value = (m_value & ~MaskSeta) | ((value << PosA) & MaskSeta);
    }

    public int B
    {
        get => GetArgB(m_value);
        set =>
            m_value = (m_value & ~MaskSetb) | ((value << PosB) & MaskSetb);
    }

    public int C
    {
        get => GetArgC(m_value);
        set =>
            m_value = (m_value & ~MaskSetc) | ((value << PosC) & MaskSetc);
    }

    public int Bx
    {
        get => GetArgBx(m_value);
        set =>
            m_value = (m_value & ~MaskSetBx) | ((value << PosBx) & MaskSetBx);
    }

    public int SBx
    {
        get => GetArgsBx(m_value);
        set => Bx = value + MaxargSBx;
    }

    private static int Mask1(int n, int p)
    {
        return ~(~0 << n) << p;
    }

    private static int Mask0(int n, int p)
    {
        return ~Mask1(n, p);
    }

    /* test whether value is a constant */
    private static bool Isk(int x)
    {
        return (x & Bitrk) == 1;
    }

    /* gets the index of the constant */
    private static int Indexk(int r)
    {
        return r & ~Bitrk;
    }

    public static implicit operator LInstruction(int value)
    {
        return new LInstruction(value);
    }

    public static implicit operator int(LInstruction value)
    {
        return value.m_value;
    }

    public static LInstruction CreateAbc(
        Op op,
        int a,
        int b,
        int c
    )
    {
        return new LInstruction(op, a, b, c);
    }

    public static LInstruction CreateABx(
        Op op,
        int a,
        int bx
    )
    {
        return new LInstruction(op, a, bx);
    }

    public static int GetOpCode(int codePoint)
    {
        return (codePoint >> PosOp) & MaskGetopcode;
    }

    public static int GetArgA(int codePoint)
    {
        return (codePoint >> PosA) & MaskGeta;
    }

    public static int GetArgB(int codePoint)
    {
        return (codePoint >> PosB) & MaskGetb;
    }

    public static int GetArgC(int codePoint)
    {
        return (codePoint >> PosC) & MaskGetc;
    }

    public static int GetArgBx(int codePoint)
    {
        return (codePoint >> PosBx) & MaskGetBx;
    }

    public static int GetArgsBx(int codePoint)
    {
        return GetArgBx(codePoint) - MaxargSBx;
    }
}

public class Code
{
    /*
     ** Size and position of opcode arguments
     */
    private static readonly int SizeC = 9;
    private static readonly int SizeB = 9;
    private static readonly int SizeBx = SizeC + SizeB;
    private static readonly int SizeA = 8;

    private static readonly int SizeOp = 6;

    private static readonly int PosOp = 0;
    private static readonly int PosA = PosOp + SizeOp;
    private static readonly int PosC = PosA + SizeA;
    private static readonly int PosB = PosC + SizeC;
    private static readonly int PosBx = PosC;

    /*
     ** Limits for opcode arguments
     ** (signed) int used to manipulate most arguments
     */
    private static readonly int MaxargBx = (1 << SizeBx) - 1;
    private static readonly int MaxargSBx = MaxargBx >> 1;

    private static readonly int MaxargA = (1 << SizeA) - 1;
    private static readonly int MaxargB = (1 << SizeB) - 1;
    private static readonly int MaxargC = (1 << SizeC) - 1;

    /*
     ** Macros to operate RK indices
     */

    /* this bit 1 means constant (0 means register) */
    private static readonly int Bitrk = 1 << (SizeB - 1);
    private static readonly int Maxindexrk = Bitrk - 1;

    private static readonly int MaskOpcode = Mask1(SizeOp, 0);
    private static readonly int MaskA = Mask1(SizeA, 0);
    private static readonly int MaskB = Mask1(SizeB, 0);
    private static readonly int MaskC = Mask1(SizeC, 0);
    private static readonly int MaskBx = Mask1(SizeBx, 0);
    private readonly int[] code;
    private readonly OpcodeMap map;

    public Code(LFunction function)
    {
        code = function.Code;
        map = function.Header.LuaVersion.GetOpcodeMap();
    }

    private static int Mask1(int n, int p)
    {
        return ~(~0 << n) << p;
    }

    private static int Mask0(int n, int p)
    {
        return ~Mask1(n, p);
    }

    /* test whether value is a constant */
    private static bool Isk(int x)
    {
        return (x & Bitrk) == 1;
    }

    /* gets the index of the constant */
    private static int Indexk(int r)
    {
        return r & ~Bitrk;
    }

    //----------------------------------------------------\\

    public static int GetOpCode(int codePoint)
    {
        return (codePoint >> PosOp) & MaskOpcode;
    }

    public static int GetArgA(int codePoint)
    {
        return (codePoint >> PosA) & MaskA;
    }

    public static int GetArgB(int codePoint)
    {
        return (codePoint >> PosB) & MaskB;
    }

    public static int GetArgC(int codePoint)
    {
        return (codePoint >> PosC) & MaskC;
    }

    public static int GetArgBx(int codePoint)
    {
        return (codePoint >> PosBx) & MaskBx;
    }

    public static int GetArgsBx(int codePoint)
    {
        return GetArgBx(codePoint) - MaxargSBx;
    }

    public Op Op(int line)
    {
        return map.GetOp(GetOpCode(CodePoint(line)));
    }

    public int A(int line)
    {
        return GetArgA(CodePoint(line));
    }

    public int C(int line)
    {
        return GetArgC(CodePoint(line));
    }

    public int B(int line)
    {
        return GetArgB(CodePoint(line));
    }

    public int Bx(int line)
    {
        return GetArgBx(CodePoint(line));
    }

    public int SBx(int line)
    {
        return GetArgsBx(CodePoint(line));
    }

    public OpMode OpMode(int line)
    {
        return map.GetOpMode((int)Op(line));
    }

    public OpArgMask BMode(int line)
    {
        return map.GetBMode((int)Op(line));
    }

    public OpArgMask CMode(int line)
    {
        return map.GetCMode((int)Op(line));
    }

    public bool TestA(int line)
    {
        return map.TestAMode((int)Op(line));
    }

    public bool TestT(int line)
    {
        return map.TestTMode((int)Op(line));
    }

    public int CodePoint(int line)
    {
        return code[line - 1];
    }
}
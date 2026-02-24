namespace UnluacNET.Core.Decompile.Expression;

public class ConstantExpression : Expression
{
    private readonly Constant m_constant;
    private readonly int m_index;

    public ConstantExpression(Constant constant, int index) : base(PRECEDENCE_ATOMIC)
    {
        m_constant = constant;
        m_index = index;
    }

    public override int ConstantIndex => m_index;

    public override bool IsConstant => true;

    public override bool IsBoolean => m_constant.IsBoolean;

    public override bool IsBrief => !m_constant.IsString || m_constant.AsName()
        .Length <= 10;

    public override bool IsIdentifier => m_constant.IsIdentifier;

    public override bool IsInteger => m_constant.IsInteger;

    public override bool IsString => m_constant.IsString;

    public override bool IsNil => m_constant.IsNil;

    public override int AsInteger()
    {
        return m_constant.AsInteger();
    }

    public override string AsName()
    {
        return m_constant.AsName();
    }

    public override void Print(Output output)
    {
        m_constant.Print(output);
    }
}
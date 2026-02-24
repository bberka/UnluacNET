using UnluacNET.Core.Decompile.Expression;
using UnluacNET.Core.Parse;

namespace UnluacNET.Core.Decompile;

public class Function
{
    private readonly Constant[] m_constants;

    public Function(LFunction function)
    {
        m_constants = new Constant[function.Constants.Length];

        for (var i = 0; i < m_constants.Length; i++)
            m_constants[i] = new Constant(function.Constants[i]);
    }

    public string GetGlobalName(int constantIndex)
    {
        return m_constants[constantIndex]
            .AsName();
    }

    public ConstantExpression GetConstantExpression(int constantIndex)
    {
        return new ConstantExpression(m_constants[constantIndex], constantIndex);
    }

    public GlobalExpression GetGlobalExpression(int constantIndex)
    {
        return new GlobalExpression(GetGlobalName(constantIndex), constantIndex);
    }
}
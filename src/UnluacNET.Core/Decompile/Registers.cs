using UnluacNET.Core.Decompile.Expression;
using UnluacNET.Core.Decompile.Target;

namespace UnluacNET.Core.Decompile;

public class Registers
{
    private readonly Declaration[,] m_decls;
    private readonly Function m_func;

    private readonly bool[] m_startedLines;
    private readonly int[,] m_updated;
    private readonly Expression.Expression[,] m_values;

    public Registers(
        int registers,
        int length,
        Declaration[] declList,
        Function func
    )
    {
        NumRegisters = registers;
        Length = length;

        m_decls = new Declaration[registers, length + 1];

        for (var i = 0; i < declList.Length; i++)
        {
            var decl = declList[i];

            var register = 0;

            while (m_decls[register, decl.Begin] != null)
                register++;

            decl.Register = register;

            for (var line = decl.Begin; line <= decl.End; line++)
                m_decls[register, line] = decl;
        }

        m_values = new Expression.Expression[registers, length + 1];

        for (var register = 0; register < registers; register++)
            m_values[register, 0] = Expression.Expression.NIL;

        m_updated = new int[registers, length + 1];
        m_startedLines = new bool[length + 1];

        m_func = func;
    }

    public int NumRegisters { get; }
    public int Length { get; private set; }

    public bool IsAssignable(int register, int line)
    {
        return IsLocal(register, line) && !GetDeclaration(register, line)
            .ForLoop;
    }

    public bool IsLocal(int register, int line)
    {
        if (register < 0)
            return false;

        return GetDeclaration(register, line) != null;
    }

    public bool IsNewLocal(int register, int line)
    {
        var decl = GetDeclaration(register, line);

        return decl != null && decl.Begin == line && !decl.ForLoop;
    }

    public Declaration GetDeclaration(int register, int line)
    {
        return m_decls[register, line];
    }

    public Expression.Expression GetExpression(int register, int line)
    {
        if (IsLocal(register, line - 1))
            return new LocalVariable(GetDeclaration(register, line - 1));
        return GetValue(register, line);
    }

    public Expression.Expression GetKExpression(int register, int line)
    {
        if ((register & 0x100) != 0)
            return m_func.GetConstantExpression(register & 0xFF);
        return GetExpression(register, line);
    }

    public List<Declaration> GetNewLocals(int line)
    {
        var locals = new List<Declaration>(NumRegisters);

        for (var register = 0; register < NumRegisters; register++)
            if (IsNewLocal(register, line))
                locals.Add(GetDeclaration(register, line));

        return locals;
    }

    public Target.Target GetTarget(int register, int line)
    {
        if (!IsLocal(register, line))
            throw new InvalidOperationException("No declaration exists in register" + register + " at line " + line);

        return new VariableTarget(GetDeclaration(register, line));
    }

    public int GetUpdated(int register, int line)
    {
        return m_updated[register, line];
    }

    public Expression.Expression GetValue(int register, int line)
    {
        return m_values[register, line - 1];
    }

    private void NewDeclaration(
        Declaration decl,
        int register,
        int begin,
        int end
    )
    {
        for (var line = begin; line <= end; line++)
            m_decls[register, line] = decl;
    }

    public void SetValue(
        int register,
        int line,
        Expression.Expression expression
    )
    {
        m_values[register, line] = expression;
        m_updated[register, line] = line;
    }

    public void SetInternalLoopVariable(
        int register,
        int begin,
        int end
    )
    {
        var decl = GetDeclaration(register, begin);

        if (decl == null)
        {
            decl = new Declaration("_FOR_", begin, end);
            decl.Register = register;

            NewDeclaration(decl, register, begin, end);
        }

        decl.ForLoop = true;
    }

    public void SetExplicitLoopVariable(
        int register,
        int begin,
        int end
    )
    {
        var decl = GetDeclaration(register, begin);

        if (decl == null)
        {
            decl = new Declaration("_FORV_" + register + "_", begin, end);
            decl.Register = register;

            NewDeclaration(decl, register, begin, end);
        }

        decl.ForLoopExplicit = true;
    }

    public void StartLine(int line)
    {
        m_startedLines[line] = true;

        for (var register = 0; register < NumRegisters; register++)
        {
            m_values[register, line] = m_values[register, line - 1];
            m_updated[register, line] = m_updated[register, line - 1];
        }
    }
}
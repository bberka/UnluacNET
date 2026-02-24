namespace UnluacNET.Core.Decompile.Operation;

public abstract class Operation
{
    public Operation(int line)
    {
        Line = line;
    }

    public int Line { get; private set; }

    public abstract Statement.Statement Process(Registers r, Block.Block block);
}

public class GenericOperation : Operation
{
    private readonly Statement.Statement m_statement;

    public GenericOperation(int line, Statement.Statement statement) : base(line)
    {
        m_statement = statement;
    }

    public override Statement.Statement Process(Registers r, Block.Block block)
    {
        return m_statement;
    }
}

public class LambdaOperation : Operation
{
    private readonly Func<Registers, Block.Block, Statement.Statement> m_func;

    public LambdaOperation(int line, Func<Registers, Block.Block, Statement.Statement> func) : base(line)
    {
        m_func = func;
    }

    public override Statement.Statement Process(Registers r, Block.Block block)
    {
        return m_func.Invoke(r, block);
    }
}
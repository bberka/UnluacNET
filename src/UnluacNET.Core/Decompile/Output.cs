namespace UnluacNET.Core.Decompile;

public class Output
{
    private readonly TextWriter m_writer;

    public Output() : this(Console.Out)
    {
    }

    public Output(TextWriter writer)
    {
        m_writer = writer;
    }

    public int IndentationLevel { get; set; }

    public int Position { get; private set; }

    public void IncreaseIndent()
    {
        IndentationLevel += 2;
    }

    public void DecreaseIndent()
    {
        IndentationLevel -= 2;
    }

    private void Start()
    {
        if (Position == 0)
            for (var i = IndentationLevel; i != 0; i--)
            {
                m_writer.Write(" ");
                Position++;
            }
    }

    public void Print(string str)
    {
        Start();
        m_writer.Write(str);
        Position += str.Length;
    }

    public void PrintLine()
    {
        Start();
        m_writer.WriteLine();
        Position = 0;
    }

    public void PrintLine(string str)
    {
        Start();
        m_writer.WriteLine(str);
        Position = 0;
    }
}
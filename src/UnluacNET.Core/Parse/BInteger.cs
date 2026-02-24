namespace UnluacNET.Core.Parse;

public class BInteger : BObject
{
    private static readonly long MaxInt = int.MaxValue;

    private static readonly long MinInt = int.MinValue;

    // TODO: Why not just use a 'long' to hold both sizes? Doesn't make much of a difference IMO
    private readonly long m_big;
    private readonly int m_number;

    public BInteger(BInteger b)
    {
        m_big = b.m_big;
        m_number = b.m_number;
    }

    public BInteger(int number)
    {
        m_big = 0L;
        m_number = number;
    }

    public BInteger(long big)
    {
        m_big = big;
        m_number = 0;
    }

    public int AsInteger()
    {
        if (m_big == 0)
            return m_number;
        if (m_big.CompareTo(MaxInt) > 0 || m_big.CompareTo(MinInt) < 0)
            throw new InvalidOperationException("The size of an integer is outside the range that unluac can handle.");
        return (int)m_big;
    }

    public void Iterate(Action thunk)
    {
        // so what even is the difference between these two? they look exactly the same..
        if (m_big == 0)
        {
            var i = m_number;

            while (i-- != 0)
                thunk.Invoke();
        }
        else
        {
            var i = m_big;

            while (i > 0)
            {
                thunk.Invoke();
                i -= 1L;
            }
        }
    }
}
namespace UnluacNET.Core.Parse;

public abstract class LNumber : LObject
{
    // TODO: problem solution for this issue (???)
    public abstract double Value { get; }

    public static LNumber MakeInteger(int number)
    {
        return new LIntNumber(number);
    }

    public override bool Equals(object obj)
    {
        if (obj is LNumber)
            return Value == ((LNumber)obj).Value;

        return false;
    }
}

public class LFloatNumber : LNumber
{
    public LFloatNumber(float number)
    {
        Number = number;
    }

    public float Number { get; }

    public override double Value => Number;

    public override bool Equals(object obj)
    {
        if (obj is LFloatNumber)
            return Number == ((LFloatNumber)obj).Number;

        return base.Equals(obj);
    }

    public override string ToString()
    {
        if (Number == (float)Math.Round(Number))
            return ((int)Number).ToString();
        return Number.ToString();
    }
}

public class LDoubleNumber : LNumber
{
    public LDoubleNumber(double number)
    {
        Number = number;
    }

    public double Number { get; }

    public override double Value => Number;

    public override bool Equals(object obj)
    {
        if (obj is LDoubleNumber)
            return Number == ((LDoubleNumber)obj).Number;

        return base.Equals(obj);
    }

    public override string ToString()
    {
        if (Number == Math.Round(Number))
            return ((long)Number).ToString();
        return Number.ToString();
    }
}

public class LIntNumber : LNumber
{
    public LIntNumber(int number)
    {
        Number = number;
    }

    public int Number { get; }

    public override double Value => Number;

    public override bool Equals(object obj)
    {
        if (obj is LIntNumber)
            return Number == ((LIntNumber)obj).Number;

        return base.Equals(obj);
    }

    public override string ToString()
    {
        return Number.ToString();
    }
}

public class LLongNumber : LNumber
{
    public LLongNumber(long number)
    {
        Number = number;
    }

    public long Number { get; }

    public override double Value => Number;

    public override bool Equals(object obj)
    {
        if (obj is LLongNumber)
            return Number == ((LLongNumber)obj).Number;

        return base.Equals(obj);
    }

    public override string ToString()
    {
        return Number.ToString();
    }
}
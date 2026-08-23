namespace ApoFisher.Helpers;

public class StandardLinearFunction
{
    public double A { get; private set; } // the same as a in y = ax + b, but negative;
    public double B { get; private set; } // if is 0, then it's a vertical line;
    public double C { get; private set; } // the same as b in y = ax + b, but negative;

    public StandardLinearFunction(double A, double B, double C)
    {
        this.A = A;
        this.B = B;
        this.C = C;
    }

    public StandardLinearFunction(double a, double b)
    {
        this.A = a * -1;
        this.B = 1;
        this.C = b * -1;
    }

    public double? GetSlope()
    {
        if(B == 0) return null;
        return A*-1;
    }

    public double GetYInterception() => C * -1;

    public double? GetY(double x)
    {
        if(B == 0) return null;
        return (A*-1 * x) - C;
    }
}
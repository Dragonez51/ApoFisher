namespace ApoFisher.Helpers;

public static class LinearFunctionHelper
{
    public static StandardLinearFunction GetFunctionFromTwoPoints(Point p1, Point p2)
    {
        double? a = GetSlopeFromTwoPoints(p1, p2);
        if(a is null)
        {
            // if a is null, then x1 == x2, thus it is a vertical line
            return new StandardLinearFunction(1, 0, p1.X);
        }

        double b = GetYInterception((double)a, p1);

        return new StandardLinearFunction((double)a, b);
    }

    public static StandardLinearFunction GetPerpendicularLinearFunctionViaPoint(StandardLinearFunction func, Point p)
    {
        double? slope = func.GetSlope();
        if(slope is null)
            return new StandardLinearFunction(0, 1, p.Y);
        else if(slope == 0) 
            return new StandardLinearFunction(1, 0, p.Y);

        double perpendicularSlope = -1/((double)func.GetSlope());
        return new StandardLinearFunction(perpendicularSlope, GetYInterception(perpendicularSlope, p));
    }

    public static Point GetCrossPoint(StandardLinearFunction func1, StandardLinearFunction func2)
    {
        if(func1.B == func2.B && (func1.B != 0 || func2.B != 0))
        {
            double x = (func2.GetYInterception() - func1.GetYInterception()) / ((double)func1.GetSlope() - (double)func2.GetSlope());
            return new Point(x, (double)func1.GetY(x));
        }
        else if(func1.B != func2.B && (func1.B != 0 || func2.B != 0))
        {
            throw new System.Exception("[LinearFunctionHelper] GetCrossPoint() => I have no idea how to solve different (property)y = ax + b functions...");
        }
        else
        {
            // they are perpendicular
            if(func1.A == 0)
            {
                return new Point(func1.C, func2.C);
            }
            return new Point(func2.C, func1.C);
        }
    }

    public static double? GetSlopeFromTwoPoints(Point p1, Point p2)
    {
        if(p1.Y == p2.Y) return 1;
        if(p1.X == p2.X) return null;
        return (p1.Y - p2.Y) / (p1.X - p2.X);
    }

    public static double GetYInterception(double slope, Point p)
    {
        return p.Y - (slope * p.X);
    }
}
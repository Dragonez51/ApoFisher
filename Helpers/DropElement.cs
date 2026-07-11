using System;

public class DropElement<T>
{
    public T Drop { get; set; }
    public double chance { get; set; }
    private double min { get; set; }
    private double max { get; set; }

    public DropElement(T Drop, double chance)
    {
        this.Drop = Drop;
        this.chance = chance;
    }

    public void SetRange(double min, double max)
    {
        this.min = min;
        this.max = max;
    }

    public void SetRange(double min)
    {
        this.min = min;
        this.max = min+chance;
    }

    public double GetMax() => this.max;
    public double GetMin() => this.min;
}
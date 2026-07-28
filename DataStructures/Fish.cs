using System;

namespace ApoFisher.DataStructures;

public class Fish : Item
{
    private double _size = 0;
    private double _value = 0;

    public Fish(string name, double size) : base(name)
    {
        this._size = size;
        CalculateValue();
    }

    private void CalculateValue()
    {
        double roundPrecision = 10.0;
        _value = Math.Round(_size * GetItemShape().ItemSlots.Count * roundPrecision) / roundPrecision;
    }

    #region Get functions

    public double GetSize() => _size;
    public double GetValue() => _value;

    public override string ToString()
    {
        return base.ToString()+"[Fish: [size: "+_size+"][value: "+_value+"]]";
    }

    #endregion
}
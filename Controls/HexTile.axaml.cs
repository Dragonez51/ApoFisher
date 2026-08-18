using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace ApoFisher.Controls;

public partial class HexTile : TemplatedControl
{
    public static readonly StyledProperty<double> SizeProperty = AvaloniaProperty.Register<HexTile, double>(nameof(Size), defaultValue: 10.0);
    public static readonly StyledProperty<int> XProperty = AvaloniaProperty.Register<HexTile, int>(nameof(X), defaultValue: 0);
    public static readonly StyledProperty<int> YProperty = AvaloniaProperty.Register<HexTile, int>(nameof(Y), defaultValue: 0);
    
    public double Size { get => GetValue(SizeProperty); set => SetValue(SizeProperty, value); }
    public int X { get => GetValue(XProperty); set => SetValue(XProperty, value); }
    public int Y { get => GetValue(YProperty); set => SetValue(YProperty, value); }

    private ObservableCollection<Point> _points = new ();

    private double _hypotenuse;

    #region Calculations

    private void SetUp()
    {
        CalculateHypotenuse(); // depends on Size.
        CalculateDimensions(); // depends on hypotenuse.
        CalculateCanvasProperties(); // depends on dimensions.
        SetPoints(); // depends on Size and hypotenuse.
    }

    private void CalculateHypotenuse()
    {
        _hypotenuse = Math.Round(Math.Sqrt(2.0) * Size);
    }

    private void CalculateDimensions()
    {
        Height = Size + _hypotenuse + Size;
        Width = _hypotenuse * 2;
    }

    private void CalculateCanvasProperties()
    {
        this[Canvas.LeftProperty] = (Y % 2 == 0) ? Math.Round((X * Width) + (Width / 2.0)) : Math.Round(X * Width);
        this[Canvas.TopProperty] = Y * (Size + _hypotenuse);
    }

    private void SetPoints()
    {
        var heightOffset = Height/2.0;
        _points = new ()
        {
            new Point(0, -heightOffset),
            new Point(_hypotenuse, Size - heightOffset),
            new Point(_hypotenuse, Size + _hypotenuse - heightOffset),
            new Point(0, Size + _hypotenuse + Size - heightOffset),
            new Point(0 - _hypotenuse, Size + _hypotenuse - heightOffset),
            new Point(0 - _hypotenuse, Size - heightOffset),
            new Point(0, -heightOffset)
        };
    }

    #endregion

}
using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace ApoFisher.DataStructures;

public class Hexagon : Shape
{
    public static readonly StyledProperty<double> SizeProperty = AvaloniaProperty.Register<Hexagon, double>(nameof(Size), defaultValue: 10.0);
    public static readonly StyledProperty<int> XProperty = AvaloniaProperty.Register<Hexagon, int>(nameof(X), defaultValue: 0);
    public static readonly StyledProperty<int> YProperty = AvaloniaProperty.Register<Hexagon, int>(nameof(Y), defaultValue: 0);
    
    public double Size { get => GetValue(SizeProperty); set => SetValue(SizeProperty, value); }
    public int X { get => GetValue(XProperty); set => SetValue(XProperty, value); }
    public int Y { get => GetValue(YProperty); set => SetValue(YProperty, value); }

    private ObservableCollection<Point> _points = new ();

    private double _hypotenuse;

    #region Initialization
    static Hexagon()
    {
        AffectsGeometry<Hexagon>(
            SizeProperty
        );
    }

    public Hexagon() : base()
    {   
        InitDefault();
        SetUp();
    }

    public Hexagon(double size) : base()
    {
        Size = size;
        InitDefault();
    }
    
    public Hexagon(int x, int y) : base()
    {
        X = x;
        Y = y;
        InitDefault();
        SetUp();
    }

    public Hexagon(double size, int x, int y) : base()
    {
        Size = size;
        X = x;
        Y = y;
        InitDefault();
    }

    private void InitDefault()
    {
        StrokeThickness = 1;
        Stroke = Brush.Parse("#040");
        Fill = Brush.Parse("#060");
    }

    #endregion 

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

    #region Avalonia actions

    protected override Geometry? CreateDefiningGeometry()
    {
        return new PolylineGeometry(_points, !(Fill is null));
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if(change.Property == SizeProperty)
        {
            SetUp();
        }
    }

    #endregion
}
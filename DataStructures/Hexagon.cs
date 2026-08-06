using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        Stroke = Brush.Parse("#600");
        Fill = Brush.Parse("#a00");
        Initialize();
    }

    public Hexagon(double size) : base()
    {
        Size = size;
        Stroke = Brush.Parse("#060");
        Fill = Brush.Parse("#0a0");
        Initialize();
    }
    
    public Hexagon(int x, int y) : base()
    {
        X = x;
        Y = y;
        Stroke = Brush.Parse("#006");
        Fill = Brush.Parse("#00a");
        Initialize();
    }

    public Hexagon(double size, int x, int y) : base()
    {
        Size = size;
        X = x;
        Y = y;
        Stroke = Brush.Parse("#606");
        Fill = Brush.Parse("#a0a");
        Initialize();
    }

    private void Initialize()
    {
        StrokeThickness = 3;
        SetPoints();
    }

    #endregion 

    private void SetPoints()
    {
        CalculateHypotenuse();
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

    #region Calculations

    private void CalculateHypotenuse()
    {
        _hypotenuse = Math.Sqrt(2.0) * Size;
    }

    private void CalculateDimensions()
    {
        Height = Size + _hypotenuse + Size;
        Width = _hypotenuse * 2;
    }

    private void CalculateCanvasProperties()
    {
        this[Canvas.LeftProperty] = (Y % 2 == 0) ? (X * Width) + (Width / 2.0) : X * Width;
        this[Canvas.TopProperty] = Y * (Size + _hypotenuse);
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

        if(change.Property == SizeProperty
            || change.Property == XProperty
            || change.Property == YProperty
            || change.Property == WidthProperty
            || change.Property == HeightProperty)
        {
            CalculateDimensions();
            CalculateCanvasProperties();
            SetPoints();
        }
    }

    #endregion
}
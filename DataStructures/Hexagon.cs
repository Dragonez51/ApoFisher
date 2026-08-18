using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;

namespace ApoFisher.DataStructures;

public class Hexagon : Shape
{
    private static readonly IBrush SELECTED_FILL = Brush.Parse("#aa0");
    private static readonly IBrush SELECTED_STROKE = Brush.Parse("#dd0");
    private static readonly IBrush LAKE_FILL = Brush.Parse("#008aef");
    private static readonly IBrush LAKE_STROKE = Brush.Parse("#00aaff");
    private static readonly IBrush VILLAGE_FILL = Brush.Parse("#604400");
    private static readonly IBrush VILLAGE_STROKE = Brush.Parse("#705000");
    private static readonly IBrush WOODS_FILL = Brush.Parse("#004400");
    private static readonly IBrush WOODS_STROKE = Brush.Parse("#005000");
    private static readonly IBrush DEFAULT_FILL = Brush.Parse("#060");
    private static readonly IBrush DEFAULT_STROKE = Brush.Parse("#070");
    private static readonly int DEFAULT_STROKE_THICKNESS = 2;
    private static readonly int POI_STROKE_THICKNESS = 6;
    private static readonly int SELECTED_STROKE_THICKNESS = 4;
    private static readonly int SELECTED_Z_INDEX = 3;
    private static readonly int LAKE_Z_INDEX = 0;
    private static readonly int VILLAGE_Z_INDEX = 0;
    private static readonly int WOODS_Z_INDEX = 2;
    private static readonly int DEFAULT_Z_INDEX = 1;

    public static readonly StyledProperty<double>   SizeProperty        = AvaloniaProperty.Register<Hexagon, double>(nameof(Size), defaultValue: 10.0);
    public static readonly StyledProperty<int>      XProperty           = AvaloniaProperty.Register<Hexagon, int>(nameof(X), defaultValue: 0);
    public static readonly StyledProperty<int>      YProperty           = AvaloniaProperty.Register<Hexagon, int>(nameof(Y), defaultValue: 0);
    public static readonly StyledProperty<bool>     IsSelectedProperty  = AvaloniaProperty.Register<Hexagon, bool>(nameof(Y), defaultValue: false);
    
    public double   Size        { get => GetValue(SizeProperty);        set => SetValue(SizeProperty, value);           }
    public int      X           { get => GetValue(XProperty);           set => SetValue(XProperty, value);              }
    public int      Y           { get => GetValue(YProperty);           set => SetValue(YProperty, value);              }
    public bool     IsSelected  { get => GetValue(IsSelectedProperty);  set => SetValue(IsSelectedProperty, value);     }

    private ObservableCollection<Point> _points = new ();

    private double _hypotenuse;

    private string _tileType = "Default";

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
        SetUp();
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
        SetUp();
    }

    private void InitDefault()
    {
        Random rand = new Random();
        if(rand.NextDouble() > 0.4)
        {
            _tileType = "Woods"; 
            SetUpWoods();
        }
        else
        {
            _tileType = "Default";
            SetUpDefault();
        }
        StrokeThickness = DEFAULT_STROKE_THICKNESS;
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

    #region UI handle

    public void SetTileAsLake()
    {
        _tileType = "Lake";
        SetUpLake();
    }
    public void SetTileAsVillage()
    {
        _tileType = "Village";
        SetUpVillage();
    }

    private void SetUpVillage()
    {
        Fill = VILLAGE_FILL;
        Stroke = VILLAGE_STROKE;
        StrokeThickness = POI_STROKE_THICKNESS;
        ZIndex = VILLAGE_Z_INDEX;
    }
    private void SetUpLake()
    {
        Fill = LAKE_FILL;
        Stroke = LAKE_STROKE;
        StrokeThickness = POI_STROKE_THICKNESS;
        ZIndex = LAKE_Z_INDEX;
    }
    private void SetUpWoods()
    {
        Fill = WOODS_FILL;
        Stroke = WOODS_STROKE;
        StrokeThickness = DEFAULT_STROKE_THICKNESS;
        ZIndex = WOODS_Z_INDEX;
    }
    private void SetUpDefault()
    {
        Fill = DEFAULT_FILL;
        Stroke = DEFAULT_STROKE;
        StrokeThickness = DEFAULT_STROKE_THICKNESS;
        ZIndex = DEFAULT_Z_INDEX;
    }

    private void SwitchVariants()
    {
        if (IsSelected)
        {
            Fill = SELECTED_FILL;
            Stroke = SELECTED_STROKE;
            StrokeThickness = SELECTED_STROKE_THICKNESS;
            ZIndex = SELECTED_Z_INDEX;
            return;
        }
        if (_tileType.Equals("Village"))    SetUpVillage();
        if (_tileType.Equals("Lake"))       SetUpLake();
        if (_tileType.Equals("Woods"))      SetUpWoods();
        if (_tileType.Equals("Default"))    SetUpDefault();
    }

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
        if(change.Property == IsSelectedProperty)
        {
            SwitchVariants();
        }
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        IsSelected = !IsSelected;
    }

    #endregion

    public void SelectTile() => IsSelected = true;
    public string GetTileType() => _tileType;
}
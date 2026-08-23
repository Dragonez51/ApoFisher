using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using ApoFisher.DataStructures;
using ApoFisher.Helpers;
using ApoFisher.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;

namespace ApoFisher.Controls;

public partial class GameMapControl : Control
{
    // TODO:
    // 1. Camera,
    // 2. Rethink Tile selection design,
    // 3. Player Movement,
    // 4. Terrain rules

    public static readonly StyledProperty<int>   TileSizeProperty           = AvaloniaProperty.Register<GameMapControl, int>(nameof(TileSize),          defaultValue: 64);
    public static readonly StyledProperty<int>   RowsProperty               = AvaloniaProperty.Register<GameMapControl, int>(nameof(Rows),              defaultValue: 0);
    public static readonly StyledProperty<int>   ColumnsProperty            = AvaloniaProperty.Register<GameMapControl, int>(nameof(Columns),           defaultValue: 0);
    public static readonly StyledProperty<int>   ViewportWidthProperty      = AvaloniaProperty.Register<GameMapControl, int>(nameof(ViewportWidth),     defaultValue: 192);
    public static readonly StyledProperty<int>   ViewportHeightProperty     = AvaloniaProperty.Register<GameMapControl, int>(nameof(ViewportHeight),    defaultValue: 108);
    public int TileSize         { get => GetValue(TileSizeProperty);            set => SetValue(TileSizeProperty, value);           }            
    public int Rows             { get => GetValue(RowsProperty);                set => SetValue(RowsProperty, value);               }
    public int Columns          { get => GetValue(ColumnsProperty);             set => SetValue(ColumnsProperty, value);            }
    public int ViewportWidth    { get => GetValue(ViewportWidthProperty);       set => SetValue(ViewportWidthProperty, value);      }
    public int ViewportHeight   { get => GetValue(ViewportHeightProperty);      set => SetValue(ViewportHeightProperty, value);     }

    private static ObservableCollection<Tile>? Tiles;
    private static int _zoom = 2;

    static GameMapControl()
    {
        AffectsRender<GameMapControl>(
            TileSizeProperty,
            RowsProperty,
            ColumnsProperty,
            ViewportWidthProperty,
            ViewportHeightProperty
        );
    }

    public GameMapControl()
    {
        if(Tiles is null)
        {
            GenerateTiles();
        }
        UpdateTileSize();
    }
    public GameMapControl(int Rows, int Columns, int ViewportWidth, int ViewportHeight)
    {
        this.Rows           = Rows;
        this.Columns        = Columns;
        this.ViewportWidth  = ViewportWidth;
        this.ViewportHeight = ViewportHeight;
        
        if(Tiles is null) GenerateTiles();
        UpdateTileSize();
    }

    private void GenerateTiles()
    {
        if(Rows == 0 || Columns == 0) return;

        Tiles = new ();
        Debug.WriteLine($"[GameMapControl] GenerateTiles() => [Rows: {Rows}][Columns: {Columns}]");
        for(int y = 0; y < Rows; y++)
        {
            for(int x = 0; x < Columns; x++)
            {
                Tiles.Add(new Tile(x-1, y-1, TileSize));
            }
        }

        GenerateVillage();
        GenerateLakes(4);
    }
    private void GenerateVillage()
    {
        if(Tiles is null) return;

        Random rand = new Random();
        while (true)
        {
            int x = rand.Next(Rows - 2) + 1;
            int y = rand.Next(Columns - 2) + 1;

            foreach(var tile in Tiles)
            {
                if( tile.X == x && tile.Y == y &&
                    tile.TileType.Equals("Woods") || 
                    tile.TileType.Equals("Meadows"))
                { 
                    tile.SetTileType("Village");
                    return;
                }
            }
        }
    }
    private void GenerateLakes(int count)
    {
        if(Tiles is null) return;

        Random rand = new Random();
        for(; count > 0; count--)
        {  
            bool search = true;
            while (search)
            {
                int x = rand.Next(Rows - 2) + 1;
                int y = rand.Next(Columns - 2) + 1;

                foreach(var tile in Tiles)
                {
                    if( tile.X == x && tile.Y == y &&
                        tile.TileType.Equals("Woods") || 
                        tile.TileType.Equals("Meadows"))
                    {
                        tile.SetTileType("Lake");
                        search = false;
                        break;
                    }
                } 
            }
        }
    }

    public void ZoomIn()
    { 
        if(_zoom == 2) return;  
        _zoom++; UpdateTileSize(); 
    }
    public void ZoomOut()
    { 
        if(_zoom == 0) return; 
        _zoom--; 
        UpdateTileSize(); 
    }

    public override void Render(DrawingContext context)
    {
        if(Tiles is null) return;
        // var stopwatch = Stopwatch.StartNew();
        foreach(var tile in Tiles)
        {
            if(tile.IsVisible) 
            {
                context.DrawImage(tile.Image, new Rect(tile.CanvasLeft, tile.CanvasTop, tile.Size, tile.Size));
            }
        }
        // stopwatch.Stop();
        // Debug.WriteLine($"[GameMapControl] Render() => [Rendering took: {stopwatch.Elapsed.TotalMilliseconds}ms]");
    }
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if(change.Property == TileSizeProperty) UpdateTiles();
        else if(change.Property == ViewportWidthProperty || change.Property == ViewportHeightProperty) UpdateDimensions();
    }

    private void UpdateDimensions()
    {
        Width = ViewportWidth;
        Height = ViewportHeight;
    }
    private void UpdateTiles()
    {
        if(Tiles is null) return;
        foreach(var tile in Tiles)
        {
            tile.SetTileSize(TileSize);
        }
    }
    private void UpdateTileSize()
    {
        TileSize = 32 + (16 * _zoom);
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {    
        int pointerY = (int)e.GetPosition(this).Y;

        // hexagon grid is made up of two row types
        int halfTile = TileSize / 2; // type 0: rectangle that fits inside and touches the next hexagon side to side.
        int quarterTile = TileSize / 4; // type 1: four triangles within the width of the previous type.
        // since a hexagon should fit inside a square perfectly,
        // it can be divided into a grid that contains both types sizes.

        // This part is about searching which type the pointer is at (Triangles or Square)
        // it works by subtracting from pointer Y position: once triangle size, once square size,
        // until the pointer Y position is within any of the latest type and is unable to subtract from
        int type = 0;
        while (true)
        {
            if(type == 0)
            {
                if(pointerY - quarterTile < 0) break;
                pointerY -= quarterTile;
                type = 1;
            }
            else
            {
                if(pointerY - halfTile < 0) break;
                pointerY -= halfTile;
                type = 0;  
            }
        }

        // Now that we know which type we are in, we can calculate row and column.
        // Square type is easy to calculate,
        // as for the Triangle type, we need some linear function math.

        int row = (int)e.GetPosition(this).Y / (halfTile + quarterTile); // this gives a correct row when in Square type, and approximate when in Triangle type.

        // If the pointer is within Triangles type, we need to know if it's the previous row or the next row, 
        // and it can be calculated by knowing if the point is higher than the hypotenuse or not.
        if(type == 0)
        {
            // Here are calculations for the Triangle type.

            // Those variables are just for a more readable code.
            var colWidth = halfTile;
            var colHeight = quarterTile;

            double pointerX = e.GetPosition(this).X;

            // Later calculations require knowledge if this is column contains a:
            // 1.[Left bottom to Right Top] or a 2.[Left Top to Right Bottom] hypotenuse,
            // thus we have to divide the grid similarly as before.
            int colType = 0; // 0 => /  1 => \ 
            while (true)
            {
                if(pointerX - colWidth > 0)
                {
                    pointerX -= colWidth;
                    colType = colType == 0 ? 1 : 0;
                }
                else break;
            }

            if(colType == 0) colType = row % 2 == 0 ? 1 : 0;
            else colType = row % 2 == 0 ? 0 : 1;
            
            // Knowing the Slope of calculated hypotenuse, we can attach some points:
            var A = colType == 0 ? new Helpers.Point(0, colHeight) : new Helpers.Point(); // [Left top] or [Left Bottom]
            var B = colType == 0 ? new Helpers.Point(colWidth, 0) : new Helpers.Point(colWidth, colHeight); // [Right Bottom] or [Right Top]
            var P = new Helpers.Point(pointerX, pointerY); // this is the pointer click point, but brought down to be within the checking bounds
            
            // After the points are set, we need to calculate what is the difference  we can calculate the function that goes through point A and B,
            var func = LinearFunctionHelper.GetFunctionFromTwoPoints(A, B);
            
            // Then we need to get the perpendicular function that goes through the pointer location:
            var funcPerp = LinearFunctionHelper.GetPerpendicularLinearFunctionViaPoint(func, P);

            // Next we get the point where those two functions cross each other:
            var crossPoint = LinearFunctionHelper.GetCrossPoint(func, funcPerp);
            
            // The calculated cross point is the closest point from pointer to hypotenuse,
            // which can give us the information wether pointer is higher than it, or not.
            double yDiff = Math.Round(P.Y - crossPoint.Y, 2);

            // Then if the pointer is Higher (pointer.Y is lower than crossPoint.Y),
            // we know that it should be the previous row.
            // If it's not, then the approximate row is correct.
            if(yDiff < 0) row--;
        }

        // Now we just need to calculate which column is the pointer at.
        // It is done in two ways, since even (or odd) rows are offset by a half of the tile size.
        int col;
        if(row % 2 == 0) col = (int)(e.GetPosition(this).X - TileSize/2) / TileSize;
        else col = (int)e.GetPosition(this).X / TileSize;

        // Now that we have both row and column calculated, 
        // we can move on to handling the Tile selection.
        SelectTile(row, col);
    }

    private void SelectTile(int row, int column)
    {
        var tile = GetTile(column, row);
        if(tile is null) return;
        if(tile.TileType == "Lake")
        {
            MainViewModel.RouteLocation("Lake 1");
            return;
        }
        if(tile.IsSelected) tile.SetSelected(false);
        else tile.SetSelected(true);

        InvalidateVisual();
    }

    public Tile? GetTile(int X, int Y)
    {
        if(Tiles is null) return null;
        foreach(var tile in Tiles)
            if(tile.X == X && tile.Y == Y) return tile;
        return null;
    }
}
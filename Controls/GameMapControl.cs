using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using ApoFisher.DataStructures;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;

namespace ApoFisher.Controls;

public partial class GameMapControl : Control
{
    private static readonly StyledProperty<int>   TileSizeProperty           = AvaloniaProperty.Register<GameMapControl, int>(nameof(TileSize),          defaultValue: 64);
    public static readonly StyledProperty<int>   RowsProperty               = AvaloniaProperty.Register<GameMapControl, int>(nameof(Rows),              defaultValue: 0);
    public static readonly StyledProperty<int>   ColsProperty               = AvaloniaProperty.Register<GameMapControl, int>(nameof(Cols),              defaultValue: 0);
    public static readonly StyledProperty<int>   ViewportWidthProperty      = AvaloniaProperty.Register<GameMapControl, int>(nameof(ViewportWidth),     defaultValue: 192);
    public static readonly StyledProperty<int>   ViewportHeightProperty     = AvaloniaProperty.Register<GameMapControl, int>(nameof(ViewportHeight),    defaultValue: 108);
    public int TileSize         { get => GetValue(TileSizeProperty);            set => SetValue(TileSizeProperty, value);           }
    public int Rows             { get => GetValue(RowsProperty);                set => SetValue(RowsProperty, value);               }
    public int Cols             { get => GetValue(ColsProperty);                set => SetValue(ColsProperty, value);               }
    public int ViewportWidth    { get => GetValue(ViewportWidthProperty);       set => SetValue(ViewportWidthProperty, value);      }
    public int ViewportHeight   { get => GetValue(ViewportHeightProperty);      set => SetValue(ViewportHeightProperty, value);     }

    private ObservableCollection<Tile> Tiles = new ();

    private int _zoom = 2;

    static GameMapControl()
    {
        AffectsRender<GameMapControl>(
            TileSizeProperty,
            RowsProperty,
            ColsProperty,
            ViewportWidthProperty,
            ViewportHeightProperty
        );
    }

    public GameMapControl()
    {
        GenerateTiles();
    }

    private void GenerateTiles()
    {
        if(Rows == 0 || Cols == 0) return;

        Tiles = new ();
        Debug.WriteLine($"[GameMapControl] GenerateTiles() => [Rows: {Rows}][Cols: {Cols}]");
        for(int y = 0; y < Rows; y++)
        {
            for(int x = 0; x < Cols; x++)
            {
                Tiles.Add(new Tile(x-1, y-1, TileSize));
            }
        }

        GenerateVillage();
        GenerateLakes(4);
    }

    private void GenerateVillage()
    {
        Debug.Write($"[GameMapControl] GenerateVillage() => ");

        Random rand = new Random();
        while (true)
        {
            int x = rand.Next(Rows - 2) + 1;
            int y = rand.Next(Cols - 2) + 1;

            foreach(var tile in Tiles)
            {
                if( tile.X == x && tile.Y == y &&
                    tile.TileType.Equals("Woods") || 
                    tile.TileType.Equals("Meadows"))
                { 
                    tile.SetTileType("Village");
                    Debug.WriteLine($"Village spawned at [x: {x}][y: {y}]");
                    return;
                }
            }
        }
    }

    private void GenerateLakes(int count)
    {
        Debug.Write($"[GameMapControl] GenerateLakes() => [count: {4}] ");

        Random rand = new Random();
        for(; count > 0; count--)
        {  
            bool search = true;
            while (search)
            {
                int x = rand.Next(Rows - 2) + 1;
                int y = rand.Next(Cols - 2) + 1;

                foreach(var tile in Tiles)
                {
                    if( tile.X == x && tile.Y == y &&
                        tile.TileType.Equals("Woods") || 
                        tile.TileType.Equals("Meadows"))
                    {
                        tile.SetTileType("Lake");
                        search = false;
                        Debug.Write($"[Lake spawned at [x: {x}][y: {y}] ] ");
                        break;
                    }
                } 
            }
        }
        Debug.WriteLine($" [Done.]");
    }

    private void UpdateTiles()
    {
        foreach(var tile in Tiles)
        {
            tile.SetTileSize(TileSize);
        }
    }

    public void ZoomIn(){ if(_zoom == 2) return;  Debug.WriteLine("[GameMapControl] ZoomIn()"); TileSize = 32 + (++_zoom * 16); }
    public void ZoomOut(){ if(_zoom == 0) return; Debug.WriteLine("[GameMapControl] ZoomOut()"); TileSize = 32 + (--_zoom * 16); }

    private void SetDimensions()
    {
        Width = ViewportWidth;
        Height = ViewportHeight;
    }

    public override void Render(DrawingContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        foreach(var tile in Tiles)
        {
            if(tile.IsVisible) context.DrawImage(tile.Image, new Rect(tile.CanvasLeft, tile.CanvasTop, tile.Size, tile.Size));
        }
        stopwatch.Stop();
        Debug.WriteLine($"[GameMapControl] Render() => [Rendering took: {stopwatch.Elapsed.TotalMilliseconds}ms]");
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if(change.Property == RowsProperty || change.Property == ColsProperty) GenerateTiles();
        if(change.Property == TileSizeProperty) UpdateTiles();
        if(change.Property == ViewportWidthProperty || change.Property == ViewportHeightProperty) SetDimensions();
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        Debug.WriteLine($"[GameMapControl] OnPointerPressed() => [e.Source: {e.Source}]");
    }
}
using System;
using ApoFisher.DataBases;
using Avalonia.Media.Imaging;

namespace ApoFisher.DataStructures;

public class Tile
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public int Size { get; private set; }
    public int CanvasLeft { get; private set; }
    public int CanvasTop { get; private set; }
    public bool IsVisible { get; private set; } = true;
    public Bitmap Image { get => ImgDB.Get(TileType +""+ Size); }
    public string TileType { get; private set; } = "Woods";

    public Tile(int x, int y, int size)
    {
        X = x;
        Y = y;
        Size = size;
        CalculateCanvasOffset();
    }

    public Tile(int x, int y, int size, string tileType)
    {
        X = x;
        Y = y;
        Size = size;
        TileType = tileType;
        CalculateCanvasOffset();
    }

    private void CalculateCanvasOffset()
    {
        CanvasLeft = (Y % 2 == 0) ? (int)Math.Round((X * Size) + (Size / 2.0)) : (int)Math.Round((double)(X * Size));
        CanvasTop = (int)(Y * (Size/4)*3);
    }

    public void SetIsVisible(bool isVisible) => IsVisible = isVisible;
    public void SetTileType(string tileType) => TileType = tileType;
    public void SetTileSize(int size){ Size = size; CalculateCanvasOffset(); }
}
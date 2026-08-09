using System.Collections.ObjectModel;

namespace ApoFisher.DataStructures;

public class HexMap
{
    public ObservableCollection<Hexagon> Tiles { get; private set; }
    private double _tileSize = 5.0;
    private int _rows = 5;
    private int _cols = 5;

    public HexMap(double tileSize)
    {
        _tileSize = tileSize;
        Tiles = new ();
        GenerateTiles();
    }

    public HexMap(int rows, int cols)
    {
        _rows = rows;
        _cols = cols;
        Tiles = new ();
        GenerateTiles();
    }

    public HexMap(int rows, int cols, double tileSize)
    {
        _rows = rows;
        _cols = cols;
        _tileSize = tileSize;
        Tiles = new ();
        GenerateTiles();
    }

    public HexMap(Hexagon tile, int rows, int cols)
    {
        _rows = rows;
        _cols = cols;
        _tileSize = tile.Size;
        Tiles = new ();
        GenerateTiles(tile);
    }

    private void GenerateTiles()
    {
        for(int y = 0; y < _rows; y++)
        {
            for(int x = 0; x < _cols; x++)
            {
                Tiles.Add(new Hexagon(20.0, x, y));
            }
        }
    }

    private void GenerateTiles(Hexagon tile)
    {
        for(int y = 0; y < _rows; y++)
        {
            for(int x = 0; x < _cols; x++)
            {
                Hexagon tempTile = new Hexagon(x, y);
                tempTile.Stroke = tile.Stroke;
                tempTile.Fill = tile.Fill;
                tempTile.Size = tile.Size;
                tempTile.StrokeThickness = tile.StrokeThickness;
                Tiles.Add(tempTile);
            }
        }
    }
}
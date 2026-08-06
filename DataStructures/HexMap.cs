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
}
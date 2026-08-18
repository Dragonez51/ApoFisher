using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

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
        GenerateVillage();
        GenerateLakes(4);
    }

    public HexMap(int rows, int cols)
    {
        _rows = rows;
        _cols = cols;
        Tiles = new ();
        GenerateTiles();
        GenerateVillage();
        GenerateLakes(4);
    }

    public HexMap(int rows, int cols, double tileSize)
    {
        _rows = rows;
        _cols = cols;
        _tileSize = tileSize;
        Tiles = new ();
        GenerateTiles();
        GenerateVillage();
        GenerateLakes(4);
    }

    public HexMap(Hexagon tile, int rows, int cols)
    {
        _rows = rows;
        _cols = cols;
        _tileSize = tile.Size;
        Tiles = new ();
        GenerateTiles(tile);
        GenerateVillage();
        GenerateLakes(4);
    }

    // Generates a single Village tile on a random tile that is not a lake.
    private void GenerateVillage()
    {
        Random rand = new Random();
        while (true)
        {
            int x = rand.Next(_rows+1);
            int y = rand.Next(_cols+1);
            foreach(var tile in Tiles)
            {
                if(tile.X == x && tile.Y == y && !tile.GetTileType().Equals("Lake"))
                {
                    tile.SetTileAsVillage();
                    tile.SelectTile();
                    return;
                }
            }
        }
    }

    // Generates 'count' Lake tiles on tiles that are not lakes nor village
    private void GenerateLakes(int count)
    {
        Random rand = new Random();
        for(int i = 0; i<count; i++)
        {
            bool search = true;
            while (search)
            {
                int x = rand.Next(_rows+1);
                int y = rand.Next(_cols+1);
                foreach(var tile in Tiles)
                {
                    if( tile.X == x && tile.Y == y && 
                        !tile.GetTileType().Equals("Village") && 
                        !tile.GetTileType().Equals("Lake")
                    )
                    {
                        tile.SetTileAsLake();
                        search = false;
                        break;
                    }
                }
            }
        }
    }

    private void GenerateTiles()
    {
        for(int y = 0; y < _rows; y++)
        {
            for(int x = 0; x < _cols; x++)
            {
                Tiles.Add(new Hexagon(_tileSize, x, y));
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
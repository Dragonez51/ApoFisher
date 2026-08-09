using System.Collections.ObjectModel;
using ApoFisher.DataStructures;
using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.Input;

namespace ApoFisher.ViewModels;

public partial class TraverseViewModel : ViewModelBase
{
    private HexMap _map;

    public ObservableCollection<Hexagon> Tiles { get => _map.Tiles; }

    public Canvas Viewport {get; private set; }

    public TraverseViewModel()
    {
        Hexagon tempTile = new Hexagon(20.0);
        tempTile.Stroke = Brush.Parse("#004400");
        tempTile.Fill = Brush.Parse("#006600");
        tempTile.StrokeThickness = 1;

        _map = new HexMap(tempTile, 14, 19);
        Viewport = new Canvas();
        Viewport.Background = Brush.Parse("#777");
        Viewport.Width = 1000;
        Viewport.Height = 600;
        foreach(var tile in Tiles)
        {
            Viewport.Children.Add(tile);
        }
    }

    [RelayCommand] public void RouteVillage() => MainViewModel.RouteLocation("Village");
}
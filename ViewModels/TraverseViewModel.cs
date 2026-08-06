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
        _map = new HexMap(12, 10);
        Viewport = new Canvas();
        Viewport.Background = Brush.Parse("#777");
        Viewport.Width = 500;
        Viewport.Height = 500;
        foreach(var tile in Tiles)
        {
            Viewport.Children.Add(tile);
        }
    }

    [RelayCommand] public void RouteVillage() => MainViewModel.RouteLocation("Village");
}
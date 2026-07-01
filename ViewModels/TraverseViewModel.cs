using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;

namespace ApoFisher.ViewModels;

public partial class TraverseViewModel : ViewModelBase
{
    private Map _map;

    private Canvas? _viewport;
    public Canvas? Viewport { get => _viewport; set => SetProperty(ref _viewport, value); }
    
    public TraverseViewModel()
    {
        _map = new Map();
        Viewport = _map.GetCanvas();
    }

    [RelayCommand] public void RouteVillage() => MainViewModel.RouteLocation("Village");
}
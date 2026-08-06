using CommunityToolkit.Mvvm.Input;

namespace ApoFisher.ViewModels;

public partial class VillageViewModel : ViewModelBase
{
    // public Hexagon TestHexagon { get => new Hexagon(new Point(10.0, 0.0), 20.0, true); }

    [RelayCommand]
    public void RouteTraverse()
    {
        MainViewModel.RouteLocation("Traverse");
    }
}
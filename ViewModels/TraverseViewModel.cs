using ApoFisher.Controls;

namespace ApoFisher.ViewModels;

public partial class TraverseViewModel : ViewModelBase
{
    public GameMapControl Map { get; private set; } = new(24, 26, 800, 500);

    public void RouteVillage() => MainViewModel.RouteLocation("Village");
}
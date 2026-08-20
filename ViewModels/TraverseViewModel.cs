using CommunityToolkit.Mvvm.Input;

namespace ApoFisher.ViewModels;

public partial class TraverseViewModel : ViewModelBase
{
    [RelayCommand] public void RouteVillage() => MainViewModel.RouteLocation("Village");
}
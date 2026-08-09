using CommunityToolkit.Mvvm.Input;

namespace ApoFisher.ViewModels;

public partial class VillageViewModel : ViewModelBase
{
    [RelayCommand]
    public void RouteTraverse()
    {
        MainViewModel.RouteLocation("Traverse");
    }
}
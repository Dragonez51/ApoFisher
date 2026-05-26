using ApoFisher.DataBases;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ApoFisher.ViewModels;

public partial class VillageViewModel : ViewModelBase
{
    public Bitmap? MenuLogo { get => ImgDB.Get("Menu"); }

    private bool _leftPanelVisibility = false;
    public bool LeftPanelVisibility { get => _leftPanelVisibility; set => SetProperty(ref _leftPanelVisibility, value); }
    private bool _rightPanelVisibility = false;
    public bool RightPanelVisibility { get => _rightPanelVisibility; set => SetProperty(ref _rightPanelVisibility, value); }

    [RelayCommand]
    public void SwitchLeftPanel()
    {
        LeftPanelVisibility = !LeftPanelVisibility;
    }

    [RelayCommand]
    public void SwitchRightPanel()
    {
        RightPanelVisibility = !RightPanelVisibility;
    }
}
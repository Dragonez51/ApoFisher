using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.Input;
using ApoFisher.DataBases;
using ApoFisher.DataStructures;

namespace ApoFisher.ViewModels;

public partial class MainViewModel : ViewModelBase 
{
    private static MainViewModel? _self;
    public static Player Player = new Player();

    // Later on, move it to a different
    // view not to trash this view.
    public int HP { get => Player.GetHP(); }
    public int MaxHP { get => Player.GetMaxHP(); }
    public int Stamina { get => Player.GetStamina(); }
    public int MaxStamina { get => Player.GetMaxStamina(); }

    public Bitmap MenuLogo { get => ImgDB.Get("Menu"); }

    private ViewModelBase? _currentViewModel;
    public ViewModelBase? CurrentViewModel { get => _currentViewModel; set => SetProperty(ref _currentViewModel, value); }
    private ViewModelBase? _currentLocation;
    public ViewModelBase? CurrentLocation { get => _currentLocation; set => SetProperty(ref _currentLocation, value); }
    private bool? _settingsVisible = false;
    public bool? SettingsVisible { get => _settingsVisible; set => SetProperty(ref _settingsVisible, value); }

    private bool _statusVisibility;
    public bool StatusVisibility { get => _statusVisibility; set => SetProperty(ref _statusVisibility, value); }

    public MainViewModel()
    {
        _self = this;
        CurrentLocation = new VillageViewModel();
    }

    public static void RouteLocation(string locationName)
    {
        switch (locationName)
        {
            case "Traverse":
                _self?.RouteTraverse();
                break;
            case "Village":
                _self?.RouteVillage();
                break;
            case "Lake 1":
                _self?.RouteLake(1);
                break;
            case "Lake 2":
                _self?.RouteLake(2);
                break;
            case "Lake 3":
                _self?.RouteLake(3);
                break;
            case "Lake 4":
                _self?.RouteLake(4);
                break;
            default:
                throw new System.Exception("[MainViewModel](Route)(RouteLocation) -> Invalid argument => "+locationName);
        }
    }

    public static void Route(string pageName) 
    {
        switch (pageName) 
        {
            case "Inventory":
                _self?.RouteInventory();
                break;
            case "Glossary":
                _self?.RouteGlossary();
                break;
            case "Settings":
                _self?.RouteSettings();
                break;
            default:
            // This is done so that I can re-use control:RoutingButton
            // for both in-game locations and UI components.
                RouteLocation(pageName);
                break;
        }
    }

    [RelayCommand] public void RouteInventory()
    { 
        if(CurrentViewModel is InventoryViewModel) { CurrentViewModel = null; return; }
        CurrentViewModel = new InventoryViewModel();
    }
    [RelayCommand] public void RouteGlossary()
    { 
        if(CurrentViewModel is GlossaryViewModel) { CurrentViewModel = null; return; }
        CurrentViewModel = new GlossaryViewModel();    
    }
    [RelayCommand] public void RouteSettings() => SwitchSettingsVisibility();
    [RelayCommand] public void RouteTraverse() => CurrentLocation = new TraverseViewModel();
    [RelayCommand] public void RouteVillage() => CurrentLocation = new VillageViewModel();

    [RelayCommand] private void RouteLake(int lvl) => CurrentLocation = new LakeViewModel(lvl);

    [RelayCommand] public void SwitchStatusVisibility() => StatusVisibility = !StatusVisibility;
    [RelayCommand] public void SwitchSettingsVisibility() => SettingsVisible = !SettingsVisible;
}
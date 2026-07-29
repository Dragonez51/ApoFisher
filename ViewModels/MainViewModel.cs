using ApoFisher.DataStructures;
using Avalonia.Controls;

namespace ApoFisher.ViewModels;

public partial class MainViewModel : ViewModelBase 
{
    private static MainViewModel? _self;
    public static Player Player = new Player(5, 9);

    // public Bitmap MenuLogo { get => ImgDB.Get("Menu"); }

    private ViewModelBase? _currentViewModel;
    public ViewModelBase? CurrentViewModel { get => _currentViewModel; set => SetProperty(ref _currentViewModel, value); }
    private ViewModelBase? _currentLocation;
    public ViewModelBase? CurrentLocation { get => _currentLocation; set => SetProperty(ref _currentLocation, value); }
    private bool? _settingsVisible = false;
    public bool? SettingsVisible { get => _settingsVisible; set => SetProperty(ref _settingsVisible, value); }

    private bool _statusVisibility;
    public bool StatusVisibility { get => _statusVisibility; set => SetProperty(ref _statusVisibility, value); }

    private GridLength _playerViewCD = GridLength.Parse("0");
    public GridLength PlayerViewCD { get => _playerViewCD; set => SetProperty(ref _playerViewCD, value); }
    // private GridLength _workspaceViewCD = GridLength.Parse("0");
    // public GridLength WorkspaceViewCD { get => _workspaceViewCD; set => SetProperty(ref _workspaceViewCD, value); }

    private ViewModelBase? _workspaceContent;
    public ViewModelBase? WorkspaceContent { get => _workspaceContent; set => SetProperty(ref _workspaceContent, value); }

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
            case "Market":
                _self?.RouteMarket();
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
                throw new System.Exception("[MainViewModel] RouteLocation() -> Invalid argument => "+locationName);
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

    public void RouteInventory()
    { 
        if(CurrentViewModel is InventoryViewModel) { CurrentViewModel = null; PlayerViewCD = GridLength.Parse("0"); return; }
        CurrentViewModel = new InventoryViewModel(Player.GetInventory());
        PlayerViewCD = GridLength.Parse("350");
    }
    public void RouteGlossary()
    { 
        if(CurrentViewModel is GlossaryViewModel) { CurrentViewModel = null; return; }
        CurrentViewModel = new GlossaryViewModel();    
    }
    public void RouteSettings() => SwitchSettingsVisibility();
    public void RouteTraverse() => CurrentLocation = new TraverseViewModel();
    public void RouteVillage() 
    {
        CurrentLocation = new VillageViewModel();
        // WorkspaceViewCD = GridLength.Parse("0");
    }
    public void RouteMarket()
    { 
        WorkspaceContent = new InventoryViewModel(new Inventory());
        CurrentLocation = new MarketViewModel();
        // WorkspaceViewCD = GridLength.Parse("300");
    }

    private void RouteLake(int lvl) => CurrentLocation = new LakeViewModel(lvl);

    public void SwitchStatusVisibility() => StatusVisibility = !StatusVisibility;
    public void SwitchSettingsVisibility() => SettingsVisible = !SettingsVisible;
}
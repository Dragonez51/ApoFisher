using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.Input;
using ApoFisher.DataBases;
using ApoFisher.DataStructures;

namespace ApoFisher.ViewModels;

public partial class MainViewModel : ViewModelBase 
{
    private static MainViewModel? _self;
    public static Player Player = new Player(40);

    public int HP { get => Player.GetHP(); }
    public int MaxHP { get => Player.GetMaxHP(); }
    public int Stamina { get => Player.GetStamina(); }
    public int MaxStamina { get => Player.GetMaxStamina(); }

    public Bitmap AppLogo { get => ImgDB.Get("Logo"); }
    public Bitmap MapLogo { get => ImgDB.Get("MapLogo"); }
    public Bitmap InventoryLogo { get => ImgDB.Get("InventoryLogo"); }
    public Bitmap GlossaryLogo { get => ImgDB.Get("GlossaryLogo"); }
    public Bitmap MenuLogo { get => ImgDB.Get("Menu"); }

    private ViewModelBase? _currentViewModel;
    public ViewModelBase? CurrentViewModel { get => _currentViewModel; set => SetProperty(ref _currentViewModel, value); }
    private ViewModelBase? _currentLocation;
    public ViewModelBase? CurrentLocation { get => _currentLocation; set => SetProperty(ref _currentLocation, value); }

    public MainViewModel()
    {
        _self = this;
        // CurrentLocation = new VillageViewModel(); // That's the main location.
        // for testing we will set it to TraverseViewModel();
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
            default:
                throw new System.Exception("[MainViewModel] Invalid Route() argument => "+locationName);
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
                throw new System.Exception("[MainViewModel] Invalid Route() argument!");
        }
    }

    // [RelayCommand] public void RouteMap() => CurrentViewModel = new MapViewModel();
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
    [RelayCommand] public void RouteSettings() => CurrentViewModel = new SettingsViewModel();
    [RelayCommand] public void RouteTraverse() => CurrentLocation = new TraverseViewModel();
    [RelayCommand] public void RouteVillage() => CurrentLocation = new VillageViewModel();
}
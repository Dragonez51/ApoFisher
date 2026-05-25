using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ApoFisher.DataBases;
using ApoFisher.DataStructures;

namespace ApoFisher.ViewModels;

public partial class MainViewModel : ViewModelBase 
{
    public static Player Player = new Player(40);
    private static MainViewModel? _self;

    public Bitmap AppLogo { get => ImgDB.Get("Logo"); }
    public Bitmap MapLogo { get => ImgDB.Get("MapLogo"); }
    public Bitmap InventoryLogo { get => ImgDB.Get("InventoryLogo"); }
    public Bitmap GlossaryLogo { get => ImgDB.Get("GlossaryLogo"); }

    [ObservableProperty] private ViewModelBase _currentViewModel;

    public MainViewModel() 
    {
        CurrentViewModel = new MapViewModel();
        _self = this;
    }

    public static void Route(string pageName) 
    {
        switch (pageName) 
        {
            case "Map":
                _self?.RouteMap();
                break;
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
                throw new System.Exception("Invalid Route() argument!");
        }
    }

    [RelayCommand] public void RouteMap() => CurrentViewModel = new MapViewModel();
    [RelayCommand] public void RouteInventory() => CurrentViewModel = new InventoryViewModel();
    [RelayCommand] public void RouteGlossary() => CurrentViewModel = new GlossaryViewModel();
    [RelayCommand] public void RouteSettings() => CurrentViewModel = new SettingsViewModel();
}
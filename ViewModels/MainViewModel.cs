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

    private string? _bgtext;
    public string? Bgtext { get => _bgtext; set => SetProperty(ref _bgtext, value); }

    public MainViewModel() 
    {
        _self = this;
        string temp = "";
        // for(int i=0; i<600; i++)
        // {
            // temp+=".>.>.>.>.>.>.>.>.>.>.>.>.>.>.>.>.>.>";
        // }
        Bgtext = temp;
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
                throw new System.Exception("Invalid Route() argument!");
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
}
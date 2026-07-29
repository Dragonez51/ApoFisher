using ApoFisher.DataStructures;
using CommunityToolkit.Mvvm.Input;

namespace ApoFisher.ViewModels;

public partial class MarketViewModel : ViewModelBase
{
    // public Bitmap MarketBG { get => ImgDB.Get("MarketBG"); }
    public ViewModelBase? InventoryVM   { get; private set; }
    public Inventory MarketInventory    { get; init; }

    private double? _calculatedValue = 0.0;
    public double? CalculatedValue { get => _calculatedValue; set => SetProperty(ref _calculatedValue, value); }
    // public double? CalculatedValue       { get; private set; } = 0.0;

    public int InvWidth                 { get => 5;  }
    public int InvHeight                { get => 8;  }

    public MarketViewModel()
    {
        MarketInventory = new (InvWidth, InvHeight);
        InventoryVM = new InventoryViewModel(MarketInventory);
    }

    [RelayCommand] public void RouteVillage() => MainViewModel.RouteLocation("Village");
    [RelayCommand] public void CalculateValue() => CalculatedValue = MarketInventory.CalculateValue();
}
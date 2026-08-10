using ApoFisher.DataStructures;
using CommunityToolkit.Mvvm.Input;

namespace ApoFisher.ViewModels;

public partial class MarketViewModel : ViewModelBase
{
    // public Bitmap MarketBG { get => ImgDB.Get("MarketBG"); }
    public ViewModelBase? InventoryVM   { get; private set; }
    public Inventory MarketInventory    { get; init; }

    public int InvWidth                 { get => 5;  }
    public int InvHeight                { get => 8;  }

    public MarketViewModel()
    {
        MarketInventory = new (InvWidth, InvHeight);
        InventoryVM = new InventoryViewModel(MarketInventory);
    }

    [RelayCommand] public void RouteVillage() => MainViewModel.RouteLocation("Village");
    public void Sell(){ MainViewModel.Player.GetStatistics().Money += MarketInventory.Value; MarketInventory.ClearInventory(); }
}
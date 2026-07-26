using Avalonia.Media.Imaging;
using ApoFisher.DataBases;
using ApoFisher.DataStructures;
using Avalonia.Controls;
using Avalonia.Input;
using ApoFisher.Controls;
using System.Collections.ObjectModel;

namespace ApoFisher.ViewModels;

public class InventoryViewModel : ViewModelBase
{
    public ObservableCollection<PlayerInventorySlot> InventorySlots { get => MainViewModel.Player.GetInventory().InventorySlots; }
    public ObservableCollection<PlayerInventoryItem> InventoryItems { get => MainViewModel.Player.GetInventory().InventoryItems; }
    public int SlotSize             { get => PlayerInventory.SlotSize; }
    public int CanvasSize           { get => SlotSize * PlayerInventory.Size; }
    public int ContainerSize        { get => CanvasSize + (PlayerInventory.CanvasOffset*2); }
    public int CanvasOffset         { get => PlayerInventory.CanvasOffset; }
    public Bitmap SlotBackground    { get => ImgDB.Get("InventorySlot"); }

    private int _selectedItemID = -1;

    public void ClickHandler(object sender, PointerPressedEventArgs e)
    {
        if(!e.Properties.IsLeftButtonPressed) return;
        var slot = ((((e.Source as Image)?.Parent as Canvas)?.Parent as Border)?.Parent as InventorySlotControl)?.DataContext as PlayerInventorySlot;
        if(slot != null)
        { 
            if(_selectedItemID == -1)
            { 
                _selectedItemID = slot.ItemID;
            }
            else 
            { 
                MainViewModel.Player.GetInventory().MoveItem(_selectedItemID, slot.SlotX, slot.SlotY); 
                _selectedItemID = -1; 
            }
        }
    }
}
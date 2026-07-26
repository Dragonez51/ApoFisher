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
    public int CanvasWidth          { get => SlotSize * PlayerInventory.Width; }
    public int CanvasHeight         { get => SlotSize * PlayerInventory.Height; }
    public int CanvasOffset         { get => PlayerInventory.CanvasOffset; }
    public Bitmap SlotBackground    { get => ImgDB.Get("InventorySlot"); }

    private PlayerInventoryItem? _selectedItem = null;

    public void ClickHandler(object sender, PointerPressedEventArgs e)
    {
        if(!e.Properties.IsLeftButtonPressed) return;
        var slot = ((((e.Source as Image)?.Parent as Canvas)?.Parent as Border)?.Parent as InventorySlotControl)?.DataContext as PlayerInventorySlot;
        if(slot != null)
        { 
            if(_selectedItem is null)
            { 
                if(slot.ItemID == -1) return;
                _selectedItem = MainViewModel.Player.GetInventory().GetItem(slot.ItemID);
                _selectedItem.SelectItem();
            }
            else 
            { 
                MainViewModel.Player.GetInventory().MoveItem(_selectedItem.ItemID, slot.SlotX, slot.SlotY); 
                _selectedItem.DeSelectItem();
                _selectedItem = null;
            }
        }
    }
}
using Avalonia.Media.Imaging;
using ApoFisher.DataBases;
using ApoFisher.DataStructures;
using Avalonia.Controls;
using Avalonia.Input;
using ApoFisher.Controls;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ApoFisher.ViewModels;

public class InventoryViewModel : ViewModelBase
{
    public Inventory Inventory { get; private set; }
    public ObservableCollection<InventorySlot> InventorySlots { get => Inventory.InventorySlots; }
    public ObservableCollection<InventoryItem> InventoryItems { get => Inventory.InventoryItems; }
    public int SlotSize             { get => Inventory.SlotSize; }
    public int CanvasWidth          { get => SlotSize * Inventory.Width; }
    public int CanvasHeight         { get => SlotSize * Inventory.Height; }
    public int CanvasOffset         { get => Inventory.CanvasOffset; }
    public Bitmap SlotBackground    { get => ImgDB.Get("InventorySlot"); }

    private static InventoryItem? _selectedItem = null;
    private static Inventory? _selectedInventory = null;

    public InventoryViewModel(Inventory inventory)
    {
        Inventory = inventory;
    }

    public void ClickHandler(object sender, PointerPressedEventArgs e)
    {
        if(!e.Properties.IsLeftButtonPressed) return;
        var slot = ((((e.Source as Image)?.Parent as Canvas)?.Parent as Border)?.Parent as InventorySlotControl)?.DataContext as InventorySlot;
        if(slot is null) return;

        if(_selectedItem is null) FirstClick(slot);
        else SecondClick(slot);
    }

    private void FirstClick(InventorySlot slot)
    {
            if(slot.ItemID == -1) return;
            _selectedItem = Inventory.GetItem(slot.ItemID);
            _selectedItem.SelectItem();
            _selectedInventory = Inventory;
    }

    private void SecondClick(InventorySlot slot)
    {
        // yes, I'm aware that it can't be null, but VSCode is not and I want clean code without any warnings :)
        if(_selectedItem is null) throw new System.Exception("[InventoryViewModel] SecondClick() => _selectedItem is null!");
        if(_selectedInventory?.ID != Inventory.ID)
        {
            // Debug.WriteLine("[InventoryViewModel] SecondClick() => _selectedInventory.ID == ");
            _selectedItem.DeSelectItem();
            Inventory.MoveItemFrom(_selectedInventory, _selectedItem.ItemID, slot.SlotX, slot.SlotY);
            _selectedItem = null;
            return;
        }
        Inventory.MoveItem(_selectedItem.ItemID, slot.SlotX, slot.SlotY); 
        _selectedItem.DeSelectItem();
        _selectedItem = null;
    }
}
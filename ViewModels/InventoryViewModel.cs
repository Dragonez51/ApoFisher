using Avalonia.Media.Imaging;
using ApoFisher.DataBases;
using ApoFisher.DataStructures;
using Avalonia.Controls;
using Avalonia.Input;
using ApoFisher.Controls;
using System.Diagnostics;
using Avalonia.Controls.Shapes;
using System.Collections.Generic;

namespace ApoFisher.ViewModels;

public class InventoryViewModel : ViewModelBase
{
    public List<PlayerInventorySlot> InventorySlots { get => MainViewModel.Player.GetInventory().InventorySlots; }
    public List<PlayerInventoryItem> InventoryItems { get => MainViewModel.Player.GetInventory().InventoryItems; }
    // public PlayerInventorySlot[] InventorySlots { get => MainViewModel.Player.GetInventory().GetInventorySlots();}
    public int SlotSize             { get => PlayerInventory.SlotSize; }
    public int CanvasSize           { get => SlotSize * PlayerInventory.Size; }
    public int ContainerSize        { get => CanvasSize + (PlayerInventory.CanvasOffset*2); }
    public int CanvasOffset         { get => PlayerInventory.CanvasOffset; }
    public Bitmap SlotBackground    { get => ImgDB.Get("InventorySlot"); }

    // private int _selectedSlotID;

    public InventoryViewModel()
    {
        // _selectedSlotID = -1;
    }

    public void ClickHandler(object sender, PointerPressedEventArgs e)
    {
        // if we are not holding any item: click an item to hold it.
        if((e.Source as Image) != null) 
        {
            // Option 1: Empty slot;
            var slot = (((e.Source as Image)?.Parent as Canvas)?.Parent as Border)?.Parent as InventorySlotControl;
            if(slot != null)
            { 
                var slotDs = slot.DataContext as PlayerInventorySlot;
                Debug.WriteLine("[InventoryViewModel] ClickHandler() clicked on: "+slotDs);
            }
            else
            {
                var item = (e.Source as Image)?.DataContext as PlayerInventoryItem;
                Debug.WriteLine("[InventoryViewModel] ClickHandler() clicked "+item);
            }
        }
        
        // Below is a representation of the previous version.

        // Debug.WriteLine("Clicked Item slot, but didn't recoginze source... ("+e.Source+")");
        // int? SlotID = 0;
        // if ((e.Source as Border) != null) SlotID = ((e.Source as Border)?.Parent as InventorySlotControl)?.SlotID;
        // else if ((e.Source as Image) != null) SlotID = ((((e.Source as Image)?.Parent as Canvas)?.Parent as Border)?.Parent as InventorySlotControl)?.SlotID;
        // else if ((e.Source as TextBlock) != null) SlotID = ((((e.Source as TextBlock)?.Parent as Canvas)?.Parent as Border)?.Parent as InventorySlotControl)?.SlotID;

        // if(SlotID is null) throw new System.Exception("[InventoryView]=>OnPointerPressed() || SlotID is null");

        // if (_selectedSlotID == -1)
        // {
        //     if (e.Properties.IsRightButtonPressed) return;
        //     FirstClick((int)SlotID);
        // }
        // else
        // {
        //     if (e.Properties.IsRightButtonPressed)
        //     {
        //         InventoryView.UpdateCursor(-1); // this sends a signal to InventoryView to change the cursor type to default;
        //         _selectedSlotID = -1;
        //         return;
        //     }
        //     SecondClick((int)SlotID);
        // }
    }

    private void FirstClick(int X, int Y)
    {
        // if (SlotID < 0) return;

        // int itemID = MainViewModel.Player.GetInventory().GetInventorySlots()[SlotID].ItemID;

        // if (itemID == -1) return;

        // InventoryView.UpdateCursor(itemID);
        // _selectedSlotID = SlotID;
    }

    private void SecondClick(int X, int Y)
    {
        // InventoryView.UpdateCursor(-1);

        // if (_selectedSlotID == SlotID)
        // {
            // _selectedSlotID = -1;
            // return;
        // }

        // MainViewModel.Player.GetInventory().SwapItems(_selectedSlotID, SlotID);

        // Debug.WriteLine("F: " + _selectedSlotID + " S: " + SlotID);

        // _selectedSlotID = -1;
    }
}
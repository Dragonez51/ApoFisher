using Avalonia.Media.Imaging;
using ApoFisher.DataBases;
using System.Collections.ObjectModel;
using ApoFisher.DataStructures;
using ApoFisher.Views;
using Avalonia.Controls;
using Avalonia.Input;
using ApoFisher.Controls;
using System.Diagnostics;

namespace ApoFisher.ViewModels;

public class InventoryViewModel : ViewModelBase
{
    public ObservableCollection<PlayerInventorySlot> InventorySlots { get => MainViewModel.Player.GetInventory().GetInventorySlots(); }
    // public PlayerInventorySlot[] InventorySlots { get => MainViewModel.Player.GetInventory().GetInventorySlots();}
    public int SlotSize             { get => 96; }
    // public int ItemIconSize         { get => 64; }
    public int CanvasOffset         { get => 16; }
    public int GridSize             { get => SlotSize * MainViewModel.Player.GetInventory().GetSize(); }
    public Bitmap SlotBackground    { get => ImgDB.Get("InventorySlot"); }

    // private int _selectedSlotID;

    public InventoryViewModel()
    {
        // _selectedSlotID = -1;
    }

    public void ClickHandler(object sender, PointerPressedEventArgs e)
    {
        //NOTE: Clicking on main canvas sends the same signal....

        // if((e.Source as Border) != null) { Debug.WriteLine("(FROM BORDER) SlotX: "+(((e.Source) as Border)?.Parent as InventorySlotControl)?.SlotX+" | SlotY: "+(((e.Source) as Border)?.Parent as InventorySlotControl)?.SlotY); return; }
        if((e.Source as Image) != null) 
        {
            var slot = (((e.Source as Image)?.Parent as Canvas)?.Parent as Border)?.Parent as InventorySlotControl;
            bool? occupied = slot?.Occupied;
            if(occupied is null)
            {
                Debug.WriteLine("[InventoryViewModel](ClickHandler) occupied is null :D");
                return;
            }
            Debug.WriteLine("SlotX: "+slot?.SlotX+" | SlotY: "+slot?.SlotY+" | Occupied? "+occupied); 
        }
        
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
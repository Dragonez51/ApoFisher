using Avalonia.Controls;
using Avalonia.Input;
using ApoFisher.Controls;
using ApoFisher.DataBases;
using ApoFisher.ViewModels;
using System;

namespace ApoFisher.Views;

public partial class InventoryView : UserControl
{
    private int _selectedSlotID;

    public InventoryView()
    {
        InitializeComponent();
        _selectedSlotID = -1;
    }

    public void OnPointerPressed(object sender, PointerPressedEventArgs e)
    {
        int? SlotID = 0;
        if ((e.Source as Border) != null) SlotID = ((e.Source as Border)?.Parent as InventorySlotControl)?.SlotID;
        else if ((e.Source as Image) != null) SlotID = ((((e.Source as Image)?.Parent as Canvas)?.Parent as Border)?.Parent as InventorySlotControl)?.SlotID;
        else if ((e.Source as TextBlock) != null) SlotID = ((((e.Source as TextBlock)?.Parent as Canvas)?.Parent as Border)?.Parent as InventorySlotControl)?.SlotID;

        if(SlotID is null) throw new Exception("[InventoryView]=>OnPointerPressed() || SlotID is null");

        if (_selectedSlotID == -1)
        {
            if (e.Properties.IsRightButtonPressed) return;
            FirstClick((int)SlotID);
        }
        else
        {
            if (e.Properties.IsRightButtonPressed)
            {
                Cursor = new Cursor(StandardCursorType.Arrow);
                _selectedSlotID = -1;
                return;
            }
            SecondClick((int)SlotID);
        }
    }

    private void FirstClick(int SlotID)
    {
        if (SlotID < 0) return;

        int itemID = MainViewModel.Player.GetInventory().GetInventorySlots()[SlotID].ItemID;

        if (itemID == -1) return;

        Cursor = new Cursor(InventoryItemsDB.GetItemIcon(itemID), new Avalonia.PixelPoint(0, 0));
        _selectedSlotID = SlotID;
    }

    private void SecondClick(int SlotID)
    {
        Cursor = new Cursor(StandardCursorType.Arrow);

        if (_selectedSlotID == SlotID)
        {
            _selectedSlotID = -1;
            return;
        }

        MainViewModel.Player.GetInventory().SwapItems(_selectedSlotID, SlotID);

        // Debug.WriteLine("F: " + _selectedSlotID + " S: " + SlotID);

        _selectedSlotID = -1;
    }
}
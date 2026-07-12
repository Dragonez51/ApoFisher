using Avalonia.Controls;
using Avalonia.Input;
using ApoFisher.DataBases;
using ApoFisher.ViewModels;

namespace ApoFisher.Views;

public partial class InventoryView : UserControl
{
    private static InventoryView? _self;

    public InventoryView()
    {
        InitializeComponent();
        _self = this;
    }

    public static void UpdateCursor(int itemID) => _self?._updateCursor(itemID);

    private void _updateCursor(int itemID)
    {
        if(itemID < 0) Cursor = new Cursor(StandardCursorType.Arrow);
        else Cursor = new Cursor(InventoryItemsDB.GetItemIcon(itemID), new Avalonia.PixelPoint(0, 0));
    }

    public void OnPointerPressed(object sender, PointerPressedEventArgs e) => (DataContext as InventoryViewModel)?.ClickHandler(sender, e);
}
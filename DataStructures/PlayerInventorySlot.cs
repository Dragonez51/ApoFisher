using Avalonia.Media.Imaging;
using ApoFisher.DataBases;

namespace ApoFisher.DataStructures;

public class PlayerInventorySlot 
{
    protected Bitmap? _icon;
    public Bitmap? Icon { get => _icon; set => _icon = value; }
    public bool Visibility { get => Quantity != 0; }

    public int ItemID;
    public int SlotID { get; set; }
    public int Quantity { get; set; }

    public PlayerInventorySlot(int slotID) 
    {
        SlotID = slotID;
        ItemID = -1;
        Quantity = 0;
        setUpIcon();
    }

    public PlayerInventorySlot(int slotID, int itemID)
    {
        SlotID = slotID;
        ItemID = itemID;
        Quantity = itemID;
        setUpIcon();
    }

    private void setUpIcon() 
    {
        Icon = InventoryItemsDB.GetItemIcon(ItemID);
    }
}
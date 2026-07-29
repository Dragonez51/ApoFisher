using CommunityToolkit.Mvvm.ComponentModel;

namespace ApoFisher.DataStructures;

public partial class InventorySlot : ObservableObject
{
    // Slot properties
    [ObservableProperty]
    private bool _occupied;
    [ObservableProperty]
    private int _itemID = -1;

    // Slot virtual coordinates
    public int SlotX            { get; private set; }
    public int SlotY            { get; private set; }

    // Slot true coordinates (inventory canvas)
    public int LeftOffset       { get; private set; } 
    public int TopOffset        { get; private set; }

    public InventorySlot(int SlotX, int SlotY) 
    {
        this.SlotX = SlotX;
        this.SlotY = SlotY;
        Occupied = false;
        LeftOffset = SlotX * Inventory.SlotSize;
        TopOffset = SlotY * Inventory.SlotSize;
    }

    public void ToggleOccupied() => Occupied = !Occupied; 
    public void SetItemID(int itemID) => ItemID = itemID;
    public override string ToString() => "[PlayerInventorySlot][SlotX: "+SlotX+"][SlotY: "+SlotY+"][ItemID: "+ItemID+"][Occupied? "+Occupied+"]";
}
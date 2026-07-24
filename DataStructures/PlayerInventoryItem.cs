using System.Collections.Generic;
using ApoFisher.DataBases;
using Avalonia.Media.Imaging;

namespace ApoFisher.DataStructures;

public class PlayerInventoryItem
{
    // System elements:
    private List<PlayerInventorySlot> OccupiedSlots;
    private Item _item;
    public int RootX { get; private set; }
    public int RootY { get; private set; }

    // UI elements:
    public Bitmap Icon { get => ImgDB.Get(_item.GetName()); }
    public int IconPositionX { get; private set; }
    public int IconPositionY { get; private set; }
    public int IconWidth { get => _item.GetItemShape().Width * PlayerInventory.SlotSize; }
    public int IconHeight { get => _item.GetItemShape().Height * PlayerInventory.SlotSize; }

    public PlayerInventoryItem(Item item, int rootX, int rootY)
    {
        _item = item;
        OccupiedSlots = new ();
        RootX = rootX;
        RootY = rootY;

        // Set icon position with an offset to left if min X of item shape is < 0
        SetIconPosition(rootX+item.GetItemShape().MinX, rootY);
    }

    public void SetIconPosition(int x, int y)
    {
        IconPositionX = x * PlayerInventory.SlotSize;
        IconPositionY = y * PlayerInventory.SlotSize;
    }

    public Item GetItem() => _item;
    public List<PlayerInventorySlot> GetOccupiedSlots() => OccupiedSlots;

    public override string ToString()
    {
        return "[PlayerInventoryItem[Root=["+RootX+" "+RootY+"]][Item="+_item.ToString()+"]]";
    }
}
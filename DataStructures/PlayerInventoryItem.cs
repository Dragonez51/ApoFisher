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
    public int IconWidth { get => _item.GetItemShape().GetWidth() * PlayerInventory.SlotSize; }
    public int IconHeight { get => _item.GetItemShape().GetHeight() * PlayerInventory.SlotSize; }

    public PlayerInventoryItem(Item item, int rootX, int rootY)
    {
        _item = item;
        OccupiedSlots = new ();
        RootX = rootX;
        RootY = rootY;
        SetIconPosition(rootX, rootY);
    }

    public void SetIconPosition(int x, int y)
    {
        IconPositionX = x * PlayerInventory.SlotSize;
        IconPositionY = y * PlayerInventory.SlotSize;
    }

    public Item GetItem() => _item;
    public List<PlayerInventorySlot> GetOccupiedSlots() => OccupiedSlots;
}
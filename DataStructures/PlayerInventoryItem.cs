using ApoFisher.DataBases;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ApoFisher.DataStructures;

public partial class PlayerInventoryItem : ObservableObject
{
    public static int MinID { get; set; } = 0;

    // Properties:
    public Item Item    { get; private set; }
    public int RootX    { get; private set; }
    public int RootY    { get; private set; }
    public int ItemID   { get; private set;}

    // UI properties:
    public Bitmap Icon          { get => ImgDB.Get(Item.GetName()); }
    [ObservableProperty] private int _iconPositionX;
    [ObservableProperty] private int _iconPositionY;
    public int IconWidth        { get => Item.GetItemShape().Width * PlayerInventory.SlotSize; }
    public int IconHeight       { get => Item.GetItemShape().Height * PlayerInventory.SlotSize; }

    public PlayerInventoryItem(Item item, int rootX, int rootY)
    {
        Item = item;
        RootX = rootX;
        RootY = rootY;

        // Just to speed up things, I'll skip this part, but later on I have to change this.
        ItemID = MinID++;

        // Set icon position with an offset to left if min X of item shape is < 0
        SetIconPosition(rootX, rootY);
    }

    public void MoveItem(int rootX, int rootY)
    {
        RootX = rootX;
        RootY = rootY;

        SetIconPosition(rootX, rootY);
    }

    private void SetIconPosition(int x, int y)
    {
        IconPositionX = (x+Item.GetItemShape().MinX) * PlayerInventory.SlotSize;
        IconPositionY = y * PlayerInventory.SlotSize;
    }

    public override string ToString()
    {
        return "[PlayerInventoryItem[Root=["+RootX+" "+RootY+"]][Item="+Item.ToString()+"]]";
    }
}
using ApoFisher.DataBases;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ApoFisher.DataStructures;

public partial class InventoryItem : ObservableObject
{
    public static int MinID { get; set; } = 0;

    public static Avalonia.Thickness SelectedBorderBrushThickness = Avalonia.Thickness.Parse("3");

    // Properties:
    public Item Item        { get; private set; }
    public int RootX        { get; private set; }
    public int RootY        { get; private set; }
    public int ItemID       { get; private set; }

    // UI properties:
    [ObservableProperty] private int _iconPositionX;
    [ObservableProperty] private int _iconPositionY;
    [ObservableProperty] private Avalonia.Thickness _borderBrushThickness;
    public Bitmap Icon              { get => ImgDB.Get(Item.GetName()); }
    public int IconWidth            { get => Item.GetItemShape().Width * Inventory.SlotSize; }
    public int IconHeight           { get => Item.GetItemShape().Height * Inventory.SlotSize; }
    // public int BorderBrushThickness { get; private set; }

    public InventoryItem(Item item, int rootX, int rootY)
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
        IconPositionX = (x+Item.GetItemShape().MinX) * Inventory.SlotSize;
        IconPositionY = y * Inventory.SlotSize;
    }

    public void SelectItem()   => BorderBrushThickness = SelectedBorderBrushThickness;
    public void DeSelectItem() => BorderBrushThickness = Avalonia.Thickness.Parse("0");

    public override string ToString()
    {
        return "[PlayerInventoryItem[Root=["+RootX+" "+RootY+"]][Item="+Item.ToString()+"]]";
    }
}
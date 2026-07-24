namespace ApoFisher.DataStructures;

public class PlayerInventorySlot 
{
    public bool Occupied { get; private set; }
    
    // Slot virtual coordinates
    public int SlotX     { get; private set; }
    public int SlotY     { get; private set; }

    // Slot true coordinates (inventory canvas)
    public int LeftOffset   { get; private set; } 
    public int TopOffset    { get; private set; }

    public PlayerInventorySlot(int SlotX, int SlotY) 
    {
        this.SlotX = SlotX;
        this.SlotY = SlotY;
        Occupied = false;
        LeftOffset = SlotX * PlayerInventory.SlotSize;
        TopOffset = SlotY * PlayerInventory.SlotSize;
    }

    public void ToggleOccupied()
    {
        Occupied = !Occupied;
    }

    public override string ToString()
    {
        return "[PlayerInventorySlot][SlotX = "+SlotX+"][SlotY = "+SlotY+"][Occupied? "+Occupied+"]";
    }
}
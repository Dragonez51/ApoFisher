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

    public bool L { get; set; }
    public bool T { get; set; }
    public bool R { get; set; }
    public bool D { get; set; }

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
        string LTRD = "";
        if(L) LTRD+="L";
        if(T) LTRD+="T";
        if(R) LTRD+="R";
        if(D) LTRD+="D";
        return "[PlayerInventorySlot][SlotX = "+SlotX+"][SlotY = "+SlotY+"][Occupied? "+Occupied+"]["+LTRD+"]";
    }
}
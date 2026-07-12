namespace ApoFisher.DataStructures;

public class PlayerInventorySlot 
{
    public bool Occupied { get; private set; }
    public int SlotX     { get; private set; }
    public int SlotY     { get; private set; }

    public PlayerInventorySlot(int SlotX, int SlotY) 
    {
        this.SlotX = SlotX;
        this.SlotY = SlotY;
        Occupied = false;
    }

    public void ToggleOcupied()
    {
        Occupied = !Occupied;
    }
}
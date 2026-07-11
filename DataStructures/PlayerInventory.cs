using System.Collections.ObjectModel;

namespace ApoFisher.DataStructures;

public class PlayerInventory
{
    private ObservableCollection<PlayerInventorySlot> InventorySlots;

    public PlayerInventory() 
    {
        InventorySlots = new ObservableCollection<PlayerInventorySlot>();
        AddSlots(20);
    }

    public PlayerInventory(int slotsCount) 
    {
        InventorySlots = new ObservableCollection<PlayerInventorySlot>();
        AddSlots(slotsCount);
        AddTwoItems();
    }

    public void SwapItems(int FirstID, int SecondID) 
    {
        PlayerInventorySlot temp        = InventorySlots[FirstID];
        InventorySlots[FirstID]         = InventorySlots[SecondID];
        InventorySlots[SecondID]        = temp;
        InventorySlots[FirstID].SlotID  = FirstID;
        InventorySlots[SecondID].SlotID = SecondID;
    }

    private void AddTwoItems() { InventorySlots[0] = new PlayerInventorySlot(0, 1); InventorySlots[1] = new PlayerInventorySlot(1, 6); }
    private void AddSlots(int amount) { for (int i = 0; i < amount; i++) { InventorySlots.Add(new PlayerInventorySlot(i)); } }
    public ObservableCollection<PlayerInventorySlot> GetInventorySlots() => InventorySlots;
}
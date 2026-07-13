using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ApoFisher.DataStructures;

public class PlayerInventory
{
    // rn Inventory is a dynamic list of item slots.
    // We (me) need to change it to a static tab.
    // Thus I have to change the whole structure of InventoryView.
    // I don't need to fully change the InventorySlotControl since it works fine for now
    // I will just have to change it so that those slots point to the ??item id??.

    private int _size;

    private ObservableCollection<PlayerInventorySlot> InventorySlots;
    // private PlayerInventorySlot[] InventorySlots;

    public PlayerInventory() 
    {
        InventorySlots = new ObservableCollection<PlayerInventorySlot>();
        // InventorySlots = new PlayerInventorySlot[20];
        GenerateSlots(5);
        _size = 5;
    }

    public PlayerInventory(int size) 
    {
        InventorySlots = new ObservableCollection<PlayerInventorySlot>();
        // InventorySlots = new PlayerInventorySlot[slotsCount];
        GenerateSlots(size);
        _size = size;
        // AddSlots(slotsCount);
        // AddTwoItems();
    }

    // in this version we don't need this item swapping.
    public void SwapItems(int FirstID, int SecondID) 
    {
        Debug.WriteLine("[PlayerInventory](SwapItems) Swapping items is currently unavailable.");
        // throw new System.Exception("[PlayerInventory](SwapItems) Swapping items is currently unavailable.");
    //     PlayerInventorySlot temp        = InventorySlots[FirstID];
    //     InventorySlots[FirstID]         = InventorySlots[SecondID];
    //     InventorySlots[SecondID]        = temp;
    //     InventorySlots[FirstID].SlotID  = FirstID;
    //     InventorySlots[SecondID].SlotID = SecondID;
    }

    // in this version we also cannot add two items just like that for now.
    // private void AddTwoItems() { InventorySlots[0] = new PlayerInventorySlot(0, 1); InventorySlots[1] = new PlayerInventorySlot(1, 6); }
    // private void AddSlots(int amount) { for (int i = 0; i < amount; i++) { InventorySlots.Add(new PlayerInventorySlot(i)); } }
    
    public ObservableCollection<PlayerInventorySlot> GetInventorySlots() => InventorySlots;
    // public PlayerInventorySlot[] GetInventorySlots() => InventorySlots;
    // private void AddSlots(int amount) { for (int i = 0; i < amount; i++) { InventorySlots[i] = new PlayerInventorySlot(i); } }
    private void GenerateSlots(int size) 
    { 
        for (int x = 0; x < size; x++) 
        { 
            for(int y = 0; y < size; y++)
            {
                InventorySlots.Add(new PlayerInventorySlot(x, y)); 
                // Debug.WriteLine("x: "+x+" | y:" + y);
            }
        } 
    }

    public void AddItem(Item item)
    {
        ItemShape itemShape = item.GetItemShape();
        foreach(var itemSlot in itemShape.GetSlots())
        {
            foreach(var invSlot in InventorySlots)
            {
                if(itemSlot.SlotX == invSlot.SlotX && itemSlot.SlotY == invSlot.SlotY) invSlot.ToggleOcupied();
            }
        }
    }

    public int GetSize() => _size;
}
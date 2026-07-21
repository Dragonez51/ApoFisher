using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ApoFisher.DataStructures;

public class PlayerInventory
{
    private int _defaultSize = 6;
    public static int Size { get; private set; } // size of inventory (size x size)
    public static int SlotSize { get => 64; }
    public static int CanvasOffset { get => 16; }

    public List<PlayerInventorySlot> InventorySlots { get; private set; } // dynamic list of slots
    public List<PlayerInventoryItem> InventoryItems { get; private set; } // dynamic list of items

    public PlayerInventory() 
    {
        InventorySlots = new ();
        InventoryItems = new ();
        GenerateSlots(_defaultSize);
        Size = _defaultSize;
    }

    public PlayerInventory(int size) 
    {
        InventorySlots = new ();
        InventoryItems = new ();
        GenerateSlots(size);
        Size = size;
    }
    
    // Add size x size slots to InventorySlots list.
    private void GenerateSlots(int size) 
    { 
        for (int y = 0; y < size; y++)
            for(int x = 0; x < size; x++)
                InventorySlots.Add(new PlayerInventorySlot(x, y)); 
    }

    // Add an item to the InventoryItems
    public void AddItem(Item item)
    {
        ItemShape itemShape = item.GetItemShape();

        // Place dynamically
        foreach(var invSlot in InventorySlots)
        {
            if (!invSlot.Occupied)
            {
                //Go through item shape pattern and check if there are no obstructing slots.
                int startX = invSlot.SlotX;
                int startY = invSlot.SlotY;

                var itemSlots = itemShape.GetSlots();

                bool pathClear = true;
                for(int i=1; (i<itemSlots.Count) && pathClear; i++)
                {
                    try
                    {
                        if(GetSlotAt(startX+itemSlots[i].SlotX, startY + itemSlots[i].SlotY).Occupied)
                        {
                            pathClear = false;
                        } 
                    }
                    catch (Exception)
                    {
                        pathClear = false;
                    }
                }
                if (pathClear) // Add the item in this path.
                {
                    AddItemAt(startX, startY, itemShape);
                    // Add to items list.
                    var invItem = new PlayerInventoryItem(item, startX, startY);
                    // invItem.SetIconPosition(startX, startY);
                    InventoryItems.Add(invItem);
                    return;
                }
            }
        }

        // throw new Exception("[PlayerInventory] AddItem() -> could not find space for this item.");
        Debug.WriteLine("Could not find space for this item!");
    }

    private void AddItemAt(int x, int y, ItemShape itemShape)
    {
        foreach(var itemSlot in itemShape.GetSlots())
        {
            var invSlot = GetSlotAt(x + itemSlot.SlotX, y + itemSlot.SlotY);
            invSlot.ToggleOccupied();
        }
    }

    private PlayerInventorySlot GetSlotAt(int x, int y)
    {
        foreach(var slot in InventorySlots)
        {
            if(slot.SlotX == x && slot.SlotY == y) return slot;
        }
        throw new Exception("[PlayerInventory] GetSlotAt() Index out of range!");
    }
}
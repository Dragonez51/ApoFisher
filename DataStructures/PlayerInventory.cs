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
        ItemShapeData itemShape = item.GetItemShape();

        // Go through every slot in inventory
        foreach(var invSlot in InventorySlots)
        {
            // find an empty slot
            if (!invSlot.Occupied)
            {
                int startX = invSlot.SlotX;
                int startY = invSlot.SlotY;

                // dynamic list of PlayerInventorySlot that defines the item shape
                var itemSlots = itemShape.ItemSlots;

                // check value if checked offsets are not obstructed
                bool pathClear = true;

                // for every itemSlot (excluding the first one since we know it is not occupied) from item shape, 
                // check if (startX + item slot X) does not go off boundries (hence catch exception) and is not occupied.
                // Debug.WriteLine("[startX="+startX+"][startY="+startY+"]");
                for(int i=0; (i<itemSlots.Count) && pathClear; i++)
                {
                    try
                    {
                        var checkSlot = GetSlotAt(startX+itemSlots[i].SlotX, startY + itemSlots[i].SlotY);
                        // Debug.WriteLine("   [iteration: "+i+"][checkSlot.SlotX="+checkSlot.SlotX+"][checkSlot.SlotY="+checkSlot.SlotY+"]");
                        if(checkSlot.Occupied)
                        {
                            pathClear = false;
                        } 
                    }
                    catch (Exception)
                    {
                        pathClear = false;
                    }
                }
                // if the path is clear, add this item
                // starting from the previously checked slot
                if (pathClear)
                {
                    // Add to UI:
                    AddItemAt(startX, startY, itemShape);

                    // Add to items list:
                    InventoryItems.Add(new PlayerInventoryItem(item, startX, startY));
                    return;
                }
                // if path is not clear, look for another slot.
            }
        }

        // throw new Exception("[PlayerInventory] AddItem() -> could not find space for this item.");
        Debug.WriteLine("Could not find space for this item!");
    }

    private void AddItemAt(int x, int y, ItemShapeData itemShape)
    {
        // Debug.WriteLine("[PlayerInventory] AddItemAt(x:"+x+", y:"+y+", itemShape:"+itemShape+")");
        int i = 1;
        foreach(var itemSlot in itemShape.ItemSlots)
        {
            // Debug.WriteLine("   [iteration:"+i+++"][itemSlot.SlotX:"+itemSlot.SlotX+", itemSlot.SlotY:"+itemSlot.SlotY+"]");
            var slot = GetSlotAt(x + itemSlot.SlotX, y + itemSlot.SlotY);
            slot.ToggleOccupied();
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
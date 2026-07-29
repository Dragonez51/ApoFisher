using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ApoFisher.DataStructures;

public class Inventory
{
    #region Inventory Properties
    
    public static int NextID = 0;
    public int ID { get; private set; }

    private static int _defaultSize = 6;
    public static int SlotSize { get => 64; }
    public static int CanvasOffset { get => 16; }

    public int Width { get; private set; }
    public int Height { get; private set; }

    #endregion

    public ObservableCollection<InventorySlot> InventorySlots { get; private set; } // list of inventory slots
    public ObservableCollection<InventoryItem> InventoryItems { get; private set; } // list of items in inventory

    #region Initialization

    public Inventory() 
    {
        ID = NextID++;
        InventorySlots = new ();
        InventoryItems = new ();
        Width = _defaultSize;
        Height = _defaultSize;
        GenerateSlots(_defaultSize, _defaultSize);
    }

    public Inventory(int size) 
    {
        ID = NextID++;
        InventorySlots = new ();
        InventoryItems = new ();
        Width = size;
        Height = size;
        GenerateSlots(size, size);
    }
    public Inventory(int width, int height) 
    {
        ID = NextID++;
        InventorySlots = new ();
        InventoryItems = new ();
        Width = width;
        Height = height;
        GenerateSlots(width, height);
    }
    
    private void GenerateSlots(int width, int height) 
    { 
        for (int y = 0; y < height; y++)
            for(int x = 0; x < width; x++)
                InventorySlots.Add(new InventorySlot(x, y)); 
    }

    #endregion

    #region Item Addition

    // Add an item to the InventoryItems
    public void AddItem(Item item)
    {

        // Go through every slot in inventory
        foreach(var invSlot in InventorySlots)
        {
            // find an empty slot
            if (!invSlot.Occupied)
            {
                int startX = invSlot.SlotX;
                int startY = invSlot.SlotY;

                // if the path is clear, add this item
                // starting from the currently checked slot
                if (CheckPath(item.GetItemShape().ItemSlots, startX, startY))
                {
                    AddItemAt(item, startX, startY);
                    return;
                }
                // if path is not clear, look for another slot.
            }
        }

        // NOTE: Come up with a solution for an item adder blockage.
        Debug.WriteLine("Could not find space for this item!");
    }

    private void AddItemAt(Item item, int x, int y)
    {
        // Add to list:
        var newItem = new InventoryItem(item, x, y);
        InventoryItems.Add(newItem);

        FlushItemSlots(newItem, x, y);       
    }

    #endregion

    #region Item Deletion

    public void DeleteItem(int itemID)
    {
        DeleteItemUI(itemID);
        DeleteItemStructure(itemID);
    }

    private void DeleteItemUI(int itemID)
    {
        foreach(var invSlot in InventorySlots)
        {
            if (invSlot.Occupied && invSlot.ItemID == itemID)
            {
                invSlot.SetItemID(-1);
                invSlot.ToggleOccupied();
            }
        }
    }

    private void DeleteItemStructure(int itemID)
    {
        foreach(var item in InventoryItems)
        {
            if(item.ItemID == itemID)
            {
                InventoryItems.Remove(item);
                break;
            }
        }
    }

    #endregion

    #region Item Movement

    public void MoveItem(int itemID, int x, int y)
    {
        var listItem = GetItem(itemID);
        if(CheckPath(listItem.Item.GetItemShape().ItemSlots, itemID, x, y))
        {
            DeleteItemUI(itemID);
            listItem.MoveItem(x, y);
            FlushItemSlots(listItem, x, y);
        }
    }

    public void MoveItemFrom(Inventory inventory, int itemID, int x, int y)
    {
        var item = inventory.GetItem(itemID).Item;
        if (CheckPath(item.GetItemShape().ItemSlots, x, y))
        {
            AddItemAt(item, x, y);
            inventory.DeleteItem(itemID);
        }
    }

    #endregion

    #region Update functions

    private void FlushItemSlots(InventoryItem listItem, int x, int y)
    {
        foreach(var itemSlot in listItem.Item.GetItemShape().ItemSlots)
        {
            var slot = GetSlot(x + itemSlot.SlotX, y + itemSlot.SlotY);
            slot.SetItemID(listItem.ItemID);
            slot.ToggleOccupied();
        }
    }

    #endregion

    #region Check functions

    private bool CheckPath(List<InventorySlot> itemSlots, int startX, int startY)
    {
        for(int i=0; i<itemSlots.Count; i++)
        {
            try
            {
                if(GetSlot(startX+itemSlots[i].SlotX, startY + itemSlots[i].SlotY).Occupied) return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        return true;
    }

    // check item shape path (exclude slots taken by the same item)
    private bool CheckPath(List<InventorySlot> itemSlots, int itemID, int startX, int startY)
    {
        for(int i=0; i<itemSlots.Count; i++)
        {
            try
            {
                var slot = GetSlot(startX+itemSlots[i].SlotX, startY + itemSlots[i].SlotY);
                if(slot.ItemID == itemID) continue;
                if(slot.Occupied) return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        return true;
    }

    #endregion

    #region Get functions

    private InventorySlot GetSlot(int x, int y)
    {
        foreach(var slot in InventorySlots)
        {
            if(slot.SlotX == x && slot.SlotY == y) return slot;
        }
        throw new Exception("[Inventory] GetSlotAt() Index out of range!");
    }

    public InventoryItem GetItem(int itemID)
    {
        foreach(var item in InventoryItems)
        {
            if(item.ItemID == itemID) return item;
        }
        throw new Exception("[Inventory] GetItem() => itemID not found!");
    }

    public double? CalculateValue()
    {
        double? value = 0.0;
        foreach(var item in InventoryItems)
        {
            if(!(item.Item is Fish)) continue; 
            value += (item.Item as Fish)?.GetValue();
        }
        return value;
    }

    #endregion
}
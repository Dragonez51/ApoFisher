using System;
using System.Collections.ObjectModel;
using Avalonia.Media.Imaging;
using ApoFisher.DataStructures;

namespace ApoFisher.DataBases;

public class InventoryItemsDB
{
    public static readonly ObservableCollection<InventoryItemTemplate> Items = new ()
    {
        new (   "Wood"              ,   64  ),
        new (   "Stone"             ,   64  ),
        new (   "Wooden Sword"      ,   1   ),
        new (   "Iron Sword"        ,   1   ),
        new (   "Obsidian Sword"    ,   1   ),
        new (   "Bow"               ,   1   ),
        new (   "Crossbow"          ,   1   ),
        new (   "Slingshot"         ,   1   ),
        new (   "Buckler"           ,   1   ),
        new (   "Wooden Shield"     ,   1   ),
        new (   "Iron Shield"       ,   1   ),
    };

    public static Bitmap? GetItemIcon(int itemID) 
    {
        if (itemID == -1) return null;
        int id = 0;
        foreach (InventoryItemTemplate item in Items) 
        {
            if (id++ == itemID) return item.Icon;
        }
        throw new Exception("ItemID not found");
    }
}
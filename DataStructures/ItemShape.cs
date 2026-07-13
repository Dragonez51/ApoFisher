using System.Collections.ObjectModel;
using ApoFisher.DataBases;

namespace ApoFisher.DataStructures;

public class ItemShape
{
    private ObservableCollection<PlayerInventorySlot> _slots;

    public ItemShape(string name)
    {
        _slots = new ();
        GenerateSlots(ItemsDB.GetItemShape(name).shape);
    }

    private void GenerateSlots(bool[][] shape)
    {
        for(int x = 0; x < shape.Length; x++)
        {
            for(int y = 0; y < shape[x].Length; y++)
            {
                // we have to multiply the slots coordinates by -1
                // because the item is not spawned inside of inventory but a temporary holder.
                // but we can do so only when the temporary holder exists so uhm... screw that for now.
                // if (shape[x][y]) _slots.Add(new PlayerInventorySlot(x*-1, y*-1));
                if (shape[x][y]) _slots.Add(new PlayerInventorySlot(x, y));
            }
        }
    }
    // to determine the most left top slot, we should do that within the GenerateSlots itself or even in ItemShapeData.
    public ObservableCollection<PlayerInventorySlot> GetSlots() => _slots;
}
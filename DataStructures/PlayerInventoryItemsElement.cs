using System.Collections.ObjectModel;

namespace ApoFisher.DataStructures;

public class PlayerInventoryItemsElement
{
    // we need a reference here of the item.
    private ObservableCollection<PlayerInventorySlot> OccupiedSlots;
    
    public PlayerInventoryItemsElement(string fishName)
    {
        OccupiedSlots = new ();
    }
}
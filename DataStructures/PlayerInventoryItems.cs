using System.Collections.ObjectModel;

namespace ApoFisher.DataStructures;

public class PlayerInventoryItems
{
    private ObservableCollection<PlayerInventoryItemsElement> Items;

    public PlayerInventoryItems()
    {
        Items = new ();
    }
}
using ApoFisher.ViewModels;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ApoFisher.DataStructures;

public class PlayerInventory
{
    private ObservableCollection<PlayerInventorySlot> InventorySlots;
    private static PlayerInventorySlot HelmetSlot      = new PlayerInventorySlot(-2);
    private static PlayerInventorySlot GauntletLSlot   = new PlayerInventorySlot(-3);
    private static PlayerInventorySlot ChestplateSlot  = new PlayerInventorySlot(-4);
    private static PlayerInventorySlot GauntletRSlot   = new PlayerInventorySlot(-5);
    private static PlayerInventorySlot HandLSlot       = new PlayerInventorySlot(-6);
    private static PlayerInventorySlot BootsSlot       = new PlayerInventorySlot(-7);
    private static PlayerInventorySlot HandRSlot       = new PlayerInventorySlot(-8);

    public static PlayerInventorySlot[] ArmorSlots =
    {
        HelmetSlot,
        GauntletLSlot,
        ChestplateSlot,
        GauntletRSlot,
        HandLSlot,
        BootsSlot,
        HandRSlot
    };

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
        if (FirstID < -1) 
        {
            SwapArmorItem(FirstID, SecondID);
            return;
        }
        if (SecondID < -1) 
        {
            SwapArmorItem(SecondID, FirstID);
            return;
        }

        PlayerInventorySlot temp        = InventorySlots[FirstID];
        InventorySlots[FirstID]         = InventorySlots[SecondID];
        InventorySlots[SecondID]        = temp;
        InventorySlots[FirstID].SlotID  = FirstID;
        InventorySlots[SecondID].SlotID = SecondID;
    }

    public void SwapArmorItem(int firstID, int secondID) 
    {
        int armorID = (firstID + 2) * -1;

        //Debug.WriteLine("armorID: " + armorID + " ArmorSlots[armorID]");
        PlayerInventorySlot temp        = ArmorSlots[armorID];
        ArmorSlots[armorID]             = InventorySlots[secondID];
        InventorySlots[secondID]        = temp;
        ArmorSlots[armorID].SlotID      = firstID;
        InventorySlots[secondID].SlotID = secondID;

        //InventoryEquipmentViewModel.UpdateIcon(armorID);
    }

    private void AddTwoItems() { InventorySlots[0] = new PlayerInventorySlot(0, 1); InventorySlots[1] = new PlayerInventorySlot(1, 6); }
    private void AddSlots(int amount) { for (int i = 0; i < amount; i++) { InventorySlots.Add(new PlayerInventorySlot(i)); } }
    public ObservableCollection<PlayerInventorySlot> GetInventorySlots() => InventorySlots;
}
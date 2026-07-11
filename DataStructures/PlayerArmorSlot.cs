// unnecessary

using ApoFisher.DataBases;

namespace ApoFisher.DataStructures;

public partial class PlayerArmorSlot : PlayerInventorySlot 
{
    public PlayerArmorSlot(int slotID, string placeholderName) : base(slotID) 
    {
        Icon = ImgDB.Get(placeholderName);
    }
}
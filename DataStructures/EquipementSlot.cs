using Avalonia.Media.Imaging;
using ApoFisher.DataBases;

namespace ApoFisher.DataStructures;

public partial class EquipementSlot : PlayerInventorySlot 
{
    public int GridColumn { get => _gridColumn; }
    public int GridRow { get => _gridRow; }
    private int _gridColumn;
    private int _gridRow;

    public Bitmap? Placeholder { get => updatePlaceholder(); }
    private Bitmap? _placeholder;

    private Bitmap? updatePlaceholder() 
    {
        return ItemID < 0 ? _placeholder : Icon;
    }

    public EquipementSlot(int slotID, int gridColumn, int gridRow) : base(slotID) 
    {
        init(gridColumn, gridRow);
    }

    public EquipementSlot(int slotID, int itemID, int gridColumn, int gridRow) : base(slotID, itemID)
    {
        init(gridColumn, gridRow);
    }

    private void init(int gridColumn, int gridRow) 
    {
        _gridColumn = gridColumn;
        _gridRow = gridRow;
        initPlaceholder(SlotID);
    }

    private void initPlaceholder(int? slotID) 
    {
        switch (slotID) 
        {
            case -2:
                _placeholder = ImgDB.Get("Helmet");
                break;
            case -3:
                _placeholder = ImgDB.Get("GauntletL");
                break;
            case -4:
                _placeholder = ImgDB.Get("Chestplate");
                break;
            case -5:
                _placeholder = ImgDB.Get("GauntletR");
                break;
            case -6:
                _placeholder = ImgDB.Get("GauntletL");
                break;
            case -7:
                _placeholder = ImgDB.Get("Boots");
                break;
            case -8:
                _placeholder = ImgDB.Get("GauntletR");
                break;
            default:
                throw new System.Exception("[EqiupementSlot]=>initPlaceholder("+slotID+") argument slotID incorrect!");
        }
    }
}

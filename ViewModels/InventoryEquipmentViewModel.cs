using Avalonia.Media.Imaging;
using ApoFisher.DataStructures;
using System.Collections.ObjectModel;

namespace ApoFisher.ViewModels;

public partial class InventoryEquipmentViewModel : ViewModelBase 
{
    public int SlotSize             { get => InventoryViewModel.SlotSize;       }
    public int ItemIconSize         { get => InventoryViewModel.ItemIconSize;   }
    public int CanvasOffset         { get => InventoryViewModel.CanvasOffset;   }
    public Bitmap SlotBackground    { get => InventoryViewModel.SlotBackground; }

    public ObservableCollection<EquipementSlot> Slots { get => _slots; }
    private ObservableCollection<EquipementSlot> _slots = new ObservableCollection<EquipementSlot>()
    {
        new EquipementSlot(-2, 1, 0), 
        new EquipementSlot(-3, 0, 1), 
        new EquipementSlot(-4, 1, 1), 
        new EquipementSlot(-5, 2, 1),
        new EquipementSlot(-6, 0, 2),
        new EquipementSlot(-7, 1, 2),
        new EquipementSlot(-8, 2, 2),
    };
}
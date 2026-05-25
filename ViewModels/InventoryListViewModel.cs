using Avalonia.Media.Imaging;
using ApoFisher.DataBases;
using ApoFisher.DataStructures;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace ApoFisher.ViewModels;

public partial class InventoryListViewModel : ViewModelBase 
{
    public ObservableCollection<PlayerInventorySlot> InventorySlots { get => MainViewModel.Player.GetInventory().GetInventorySlots();}
    public Bitmap SlotBackground        { get => InventoryViewModel.SlotBackground;     }
    
    public int SlotBackgroundSize       { get => InventoryViewModel.SlotSize;           }
    public int ItemIconSize             { get => InventoryViewModel.ItemIconSize;       }
    public int CanvasOffset             { get => InventoryViewModel.CanvasOffset;       }

    public ICommand EquipCommand        { get; }
    public ICommand DiscardCommand      { get; }

    public InventoryListViewModel() 
    {
        EquipCommand = ReactiveCommand.Create(Equip);
        DiscardCommand = ReactiveCommand.Create(Discard);
    }

    public void Equip() 
    {
        Debug.WriteLine("Equip from InventoryListViewModel!");
    }

    public void Discard() 
    {
        Debug.WriteLine("Discard from InventoryListViewModel!");
    }
}
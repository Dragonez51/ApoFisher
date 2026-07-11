using Avalonia.Media.Imaging;
using ApoFisher.DataBases;
using System.Collections.ObjectModel;
using ApoFisher.DataStructures;

namespace ApoFisher.ViewModels;

public class InventoryViewModel : ViewModelBase
{
    public ObservableCollection<PlayerInventorySlot> InventorySlots { get => MainViewModel.Player.GetInventory().GetInventorySlots();}
    public int SlotSize             { get => 96; }
    public int ItemIconSize         { get => 64; }
    public int CanvasOffset         { get => 16; }
    public Bitmap SlotBackground    { get => ImgDB.Get("InventorySlot"); }
}
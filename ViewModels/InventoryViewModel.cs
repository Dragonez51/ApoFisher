using Avalonia.Media.Imaging;
using ApoFisher.DataBases;

namespace ApoFisher.ViewModels;

public class InventoryViewModel : ViewModelBase
{
    public readonly static int SlotSize             = 96;
    public readonly static int ItemIconSize         = 64;
    public readonly static int CanvasOffset         = 16;
    public readonly static Bitmap SlotBackground    = ImgDB.Get("EmptySlot");
}
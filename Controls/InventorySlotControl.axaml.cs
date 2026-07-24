using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media.Imaging;

namespace ApoFisher.Controls;

public partial class InventorySlotControl : TemplatedControl
{
    public static readonly StyledProperty<Bitmap>       SlotBackgroundProperty      = AvaloniaProperty.Register<InventorySlotControl, Bitmap>(nameof(SlotBackground));
    public static readonly StyledProperty<bool>         OccupiedProperty            = AvaloniaProperty.Register<InventorySlotControl, bool>(nameof(Occupied));
    public static readonly StyledProperty<int>          SlotBackgroundSizeProperty  = AvaloniaProperty.Register<InventorySlotControl, int>(nameof(SlotBackgroundSize));
    public static readonly StyledProperty<int>          SlotXProperty               = AvaloniaProperty.Register<InventorySlotControl, int>(nameof(SlotX));
    public static readonly StyledProperty<int>          SlotYProperty               = AvaloniaProperty.Register<InventorySlotControl, int>(nameof(SlotY));
    public Bitmap       SlotBackground          { get => GetValue(SlotBackgroundProperty);      }
    public bool         Occupied                { get => GetValue(OccupiedProperty);            }
    public int          SlotBackgroundSize      { get => GetValue(SlotBackgroundSizeProperty);  }
    public int          SlotX                   { get => GetValue(SlotXProperty);               }
    public int          SlotY                   { get => GetValue(SlotYProperty);               }



}
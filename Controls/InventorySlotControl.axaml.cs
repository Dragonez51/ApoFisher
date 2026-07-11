using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media.Imaging;
using System.Windows.Input;

namespace ApoFisher.Controls;

public partial class InventorySlotControl : TemplatedControl
{
    // public static readonly StyledProperty<ICommand>     EquipProperty               = AvaloniaProperty.Register<InventorySlotControl, ICommand>(nameof(Equip));
    // public static readonly StyledProperty<ICommand>     DiscardProperty             = AvaloniaProperty.Register<InventorySlotControl, ICommand>(nameof(Discard));
    public static readonly StyledProperty<Bitmap>       ItemIconProperty            = AvaloniaProperty.Register<InventorySlotControl, Bitmap>(nameof(ItemIcon));
    public static readonly StyledProperty<Bitmap>       SlotBackgroundProperty      = AvaloniaProperty.Register<InventorySlotControl, Bitmap>(nameof(SlotBackground));
    public static readonly StyledProperty<int>          ItemQuantityProperty        = AvaloniaProperty.Register<InventorySlotControl, int>(nameof(ItemQuantityProperty));
    public static readonly StyledProperty<int>          SlotIDProperty              = AvaloniaProperty.Register<InventorySlotControl, int>(nameof(SlotID));
    public static readonly StyledProperty<int>          ItemIconSizeProperty        = AvaloniaProperty.Register<InventorySlotControl, int>(nameof(ItemIconSize));
    public static readonly StyledProperty<int>          CanvasOffsetProperty        = AvaloniaProperty.Register<InventorySlotControl, int>(nameof(CanvasOffset));
    public static readonly StyledProperty<int>          SlotBackgroundSizeProperty  = AvaloniaProperty.Register<InventorySlotControl, int>(nameof(SlotBackgroundSize));
    public static readonly StyledProperty<bool>         VisibilityProperty          = AvaloniaProperty.Register<InventorySlotControl, bool>(nameof(Visibility));
    // public ICommand     Equip                   { get => GetValue(EquipProperty);                   set => SetValue(EquipProperty, value);                  } 
    // public ICommand     Discard                 { get => GetValue(DiscardProperty);                 set => SetValue(DiscardProperty, value);                } 
    public Bitmap       ItemIcon                { get => GetValue(ItemIconProperty);                set => SetValue(ItemIconProperty, value);               }
    public Bitmap       SlotBackground          { get => GetValue(SlotBackgroundProperty);          set => SetValue(SlotBackgroundProperty, value);         }
    public int          ItemQuantity            { get => GetValue(ItemQuantityProperty);            set => SetValue(ItemQuantityProperty, value);           }
    public int          SlotID                  { get => GetValue(SlotIDProperty);                  set => SetValue(SlotIDProperty, value);                 }
    public int          ItemIconSize            { get => GetValue(ItemIconSizeProperty);            set => SetValue(ItemIconSizeProperty, value);           }
    public int          CanvasOffset            { get => GetValue(CanvasOffsetProperty);            set => SetValue(CanvasOffsetProperty, value);           }
    public int          SlotBackgroundSize      { get => GetValue(SlotBackgroundSizeProperty);      set => SetValue(SlotBackgroundSizeProperty, value);     }
    public bool         Visibility              { get => GetValue(VisibilityProperty);}

}
using ApoFisher.DataStructures;
using ApoFisher.ViewModels;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media.Imaging;

namespace ApoFisher.Controls;

public partial class InventorySlotControl : TemplatedControl
{
    #region Control Properties

    public static readonly StyledProperty<Bitmap>       SlotBackgroundProperty      = AvaloniaProperty.Register<InventorySlotControl, Bitmap>(nameof(SlotBackground));
    public static readonly StyledProperty<bool>         OccupiedProperty            = AvaloniaProperty.Register<InventorySlotControl, bool>(nameof(Occupied));
    public static readonly StyledProperty<int>          SlotBackgroundSizeProperty  = AvaloniaProperty.Register<InventorySlotControl, int>(nameof(SlotBackgroundSize));
    public static readonly StyledProperty<int>          SlotXProperty               = AvaloniaProperty.Register<InventorySlotControl, int>(nameof(SlotX));
    public static readonly StyledProperty<int>          SlotYProperty               = AvaloniaProperty.Register<InventorySlotControl, int>(nameof(SlotY));
    public static readonly StyledProperty<int>          ItemIDProperty              = AvaloniaProperty.Register<InventorySlotControl, int>(nameof(ItemID));
    public Bitmap       SlotBackground          { get => GetValue(SlotBackgroundProperty);      }
    public bool         Occupied                { get => GetValue(OccupiedProperty);            }
    public int          SlotBackgroundSize      { get => GetValue(SlotBackgroundSizeProperty);  }
    public int          SlotX                   { get => GetValue(SlotXProperty);               }
    public int          SlotY                   { get => GetValue(SlotYProperty);               }
    public int          ItemID                  { get => GetValue(ItemIDProperty);              }

    #endregion

    #region Context Menu Data

    public Item?        Item            { get { try { return MainViewModel.Player.GetInventory().GetItem(ItemID)?.Item; } catch(System.Exception) {return null; } } }
    public bool         ItemExists      { get => !(Item is null);                               }
    public string?      ItemName        { get => "Name: " + Item?.GetName();                    }
    public bool         IsItemFish      { get => !(Item as Fish is null);                       }
    public string?      ItemSize        { get => "Size: " + (Item as Fish)?.GetSize();          }
    public string?      ItemValue       { get => "Value: " + (Item as Fish)?.GetValue();        }

    #endregion
}
using Avalonia;

namespace ApoFisher.Controls;

public partial class InventoryEquipmentSlotControl : InventorySlotControl
{
    public static readonly StyledProperty<int> GridColumnProperty = AvaloniaProperty.Register<InventoryEquipmentSlotControl, int>(nameof(GridColumn));
    public static readonly StyledProperty<int> GridRowProperty = AvaloniaProperty.Register<InventoryEquipmentSlotControl, int>(nameof(GridRow));
    public int GridColumn { get => GetValue(GridColumnProperty); set => SetValue(GridColumnProperty, value); }
    public int GridRow { get => GetValue(GridRowProperty); set => SetValue(GridRowProperty, value); }

}
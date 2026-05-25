using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using ApoFisher.DataBases;
using ApoFisher.DataStructures;
using ApoFisher.ViewModels;

namespace ApoFisher.Controls;

public class GlossaryCategoryControl : TemplatedControl
{
    public static readonly StyledProperty<string> CategoryNameProperty = AvaloniaProperty.Register<GlossaryCategoryControl, string>(nameof(CategoryName));
    public static readonly StyledProperty<Bitmap> StatusIconProperty = AvaloniaProperty.Register<GlossaryCategoryControl, Bitmap>(nameof(StatusIcon));
    public static readonly StyledProperty<ObservableCollection<GlossaryEntry>> EntriesProperty = AvaloniaProperty.Register<GlossaryCategoryControl, ObservableCollection<GlossaryEntry>>(nameof(Entries));
    public static readonly StyledProperty<bool> VisibleProperty = AvaloniaProperty.Register<GlossaryCategoryControl, bool>(nameof(Visible));
    public string CategoryName { get => GetValue(CategoryNameProperty); set => SetValue(CategoryNameProperty, value); }
    public ObservableCollection<GlossaryEntry> Entries { get => GetValue(EntriesProperty); set => SetValue(EntriesProperty, value); }
    public Bitmap StatusIcon { get => GetValue(StatusIconProperty); set => SetValue(StatusIconProperty, value); }
    public bool Visible { get => GetValue(VisibleProperty); set => SetValue(VisibleProperty, value); }

    private void OnPointerPressedCategory(object? sender, PointerPressedEventArgs e)
    {
        GlossaryDB.ToggleVisibility(GetValue(CategoryNameProperty));
    }

    private void OnPointerPressedItem(object? sender, PointerPressedEventArgs e)
    {
        string? text = "";
        if (e.Source is TextBlock) text = (e.Source as TextBlock)?.Text;
        if (e.Source is Grid) text = ((e.Source as Grid)?.Children[1] as TextBlock)?.Text;
        if (e.Source is Image) text = (((e.Source as Image)?.Parent as Grid)?.Children[1] as TextBlock)?.Text;
        if (e.Source is Border) return;

        GlossaryViewModel.SetUpViewer(GetValue(CategoryNameProperty), text is null ? "something went wrong" : text );
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        var CategoryBorder = e.NameScope.Find<Border>("Category");
        CategoryBorder?.PointerPressed += OnPointerPressedCategory;
        var ItemsControl = e.NameScope.Find<ItemsControl>("ItemsControl");
        ItemsControl?.PointerPressed += OnPointerPressedItem;

    }
}
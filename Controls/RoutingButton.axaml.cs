using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using ApoFisher.DataBases;
using ApoFisher.ViewModels;

namespace ApoFisher.Controls;

public partial class RoutingButton : TemplatedControl
{
    public static readonly StyledProperty<string> RoutingNameProperty = AvaloniaProperty.Register<RoutingButton, string>(nameof(RoutingName));

    public string RoutingName { get => GetValue(RoutingNameProperty); set => SetValue(RoutingNameProperty, value); }

    public void OnPointerPressed(object? sender, PointerPressedEventArgs e) 
    {
        MainViewModel.Route(RoutingName);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        var border = e.NameScope.Find<Border>("Border");
        border?.PointerPressed += OnPointerPressed;
        e.NameScope?.Find<Image>("Icon")?.Source = ImgDB.Get(GetValue(RoutingNameProperty)+"Logo");
    }
}
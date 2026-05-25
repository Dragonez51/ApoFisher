using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using ApoFisher.DataBases;
using ApoFisher.DataStructures;

namespace ApoFisher.ViewModels;

public partial class GlossaryViewModel : ViewModelBase
{
    [ObservableProperty] private static string _description = "Default Description";
    [ObservableProperty] private static string _title = "Default Title";
    [ObservableProperty] private static Bitmap? _itemIcon;

    private static GlossaryViewModel? _self;

    public GlossaryViewModel()
    {
        _self = this;
    }

    public static void SetUpViewer(string categoryName, string itemTitle)
    {
        GlossaryEntry entry = GlossaryDB.GetEntry(categoryName, itemTitle);
        _self?.Description = entry.Description;
        _self?.ItemIcon = entry.Icon;
        _self?.Title = itemTitle;
    }
}
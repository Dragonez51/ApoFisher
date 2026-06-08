using Avalonia.Media.Imaging;
using ApoFisher.DataBases;
using ApoFisher.DataStructures;

namespace ApoFisher.ViewModels;

public partial class GlossaryViewModel : ViewModelBase
{
    private static string _description = "Default Description";
    public string Description { get => _description; set => SetProperty(ref _description, value); }
    private static string _title = "Default Title";
    public string Title { get => _title; set => SetProperty(ref _title, value); }
    private static Bitmap? _itemIcon;
    public Bitmap? ItemIcon { get => _itemIcon; set => SetProperty(ref _itemIcon, value); }

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
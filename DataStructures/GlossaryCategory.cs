using System.Collections.ObjectModel;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using ApoFisher.DataBases;

namespace ApoFisher.DataStructures;

public partial class GlossaryCategory : ObservableObject
{
    public string Title { get; set; }
    public ObservableCollection<GlossaryEntry> Entries { get; }

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(CurrentStatusIcon))] private bool _visible = false;
    
    public Bitmap CurrentStatusIcon { get => Visible ? ColapseIcon : ExpandIcon; }
    private Bitmap ExpandIcon { get => ImgDB.Get("Expand"); }
    private Bitmap ColapseIcon { get => ImgDB.Get("Collapse"); }
    
    public GlossaryCategory(string title, ObservableCollection<GlossaryEntry> entries)
    {
        Title = title;
        Entries = entries;
        Visible = true;
    }

    public void ToggleVisibiliy()
    {
        Visible = !Visible;
    }
}
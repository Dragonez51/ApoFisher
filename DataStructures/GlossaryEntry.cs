using Avalonia.Media.Imaging;
using ApoFisher.DataBases;

namespace ApoFisher.DataStructures;

public partial class GlossaryEntry
{
    public string Title { get; private set; }
    public Bitmap Icon { get=>ImgDB.Get(Title); }
    public string Description { get; private set; }
    public bool Visible { get; set; }

    public GlossaryEntry(string title, string description)
    {
        Title = title;
        Description = description;
        Visible = false;
    }
}
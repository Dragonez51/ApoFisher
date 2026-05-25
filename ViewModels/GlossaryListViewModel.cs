using System.Collections.ObjectModel;
using ApoFisher.DataBases;
using ApoFisher.DataStructures;

namespace ApoFisher.ViewModels;

public class GlossaryListViewModel : ViewModelBase
{
    public ObservableCollection<GlossaryCategory> Glossary { get => GlossaryDB.Glossary; }
}
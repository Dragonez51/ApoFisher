using ApoFisher.DataStructures;

namespace ApoFisher.ViewModels;

public partial class StatisticsViewModel : ViewModelBase
{
    public Statistics PlayerStats { get => MainViewModel.Player.GetStatistics(); }
}
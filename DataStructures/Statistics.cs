using CommunityToolkit.Mvvm.ComponentModel;

namespace ApoFisher.DataStructures;

public partial class Statistics : ObservableObject
{
    private double _money;
    public double Money { get => _money; set => SetProperty(ref _money, value); }
    
    public Statistics() 
    {
        Money = 0;
    }

    public Statistics(double money)
    {
        Money = money;
    }
}
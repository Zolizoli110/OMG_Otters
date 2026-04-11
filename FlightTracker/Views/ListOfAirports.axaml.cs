using Avalonia.Controls;
using FlightTracker.ViewModels;
namespace FlightTracker.Views;

public partial class ListOfAirports : UserControl
{
    public ListOfAirports()
    {
        InitializeComponent();
        DataContext = new ListOfAirportsViewModel(); 
    }
}
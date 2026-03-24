using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OtterLibrary.Models;


namespace OtterLibrary.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public User user { get; }
    public LibraryViewModel Library { get; } = new LibraryViewModel();
    public MyLeasesViewModel MyLeases { get; } = new MyLeasesViewModel();
    public AllLeasesViewModel AllLeases { get; } = new AllLeasesViewModel();
}
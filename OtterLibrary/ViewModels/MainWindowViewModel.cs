using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OtterLibrary.Models;


namespace OtterLibrary.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public User user { get; }
    public bool SeeMemberStuff => user.role == UserRole.Admin || user.role == UserRole.Member;
    public bool SeeLibrarianStuff => user.role == UserRole.Admin || user.role == UserRole.Librarian;
    public LibraryViewModel Library { get; } = new LibraryViewModel(null);
    public MyLeasesViewModel MyLeases { get; } = new MyLeasesViewModel();
    public AllLeasesViewModel AllLeases { get; } = new AllLeasesViewModel();

    public MainWindowViewModel(User? user)
    {
        if (user == null)
        {
            user = new User()
            {
                userName = "test",
                role = UserRole.Admin
            };
            
        }
        this.user = user;
        Library = new LibraryViewModel(user);
        MyLeases = new MyLeasesViewModel();
        AllLeases = new AllLeasesViewModel();
    }
    public MainWindowViewModel()
    {
        User user = new User()
        {
            userName = "test",
            role = UserRole.Admin
        };
        this.user = user;
    }
}
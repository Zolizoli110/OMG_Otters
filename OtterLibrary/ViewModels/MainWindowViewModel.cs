using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OtterLibrary.Models;


namespace OtterLibrary.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public User user { get; }
    public bool SeeMemberStuff => user.Role == UserRole.Admin || user.Role == UserRole.Member;
    public bool SeeLibrarianStuff => user.Role == UserRole.Admin || user.Role == UserRole.Librarian;
    public LibraryViewModel Library { get; }
    public MyLeasesViewModel MyLeases { get; }
    public AllLeasesViewModel AllLeases { get; }

    public MainWindowViewModel(User? user)
    {
        if (user == null)
        {
            user = new User()
            {
                UserName = "test",
                Role = UserRole.Admin
            };
            
        }
        this.user = user;
        Library = new LibraryViewModel(user);
        MyLeases = new MyLeasesViewModel(user);
        AllLeases = new AllLeasesViewModel(Library.Books);
    }
    public MainWindowViewModel()
    {
        User user = new User()
        {
            UserName = "test",
            Role = UserRole.Admin
        };
        this.user = user;
    }
}
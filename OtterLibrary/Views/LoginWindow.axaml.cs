using Avalonia.Controls;
using OtterLibrary.ViewModels;

namespace OtterLibrary.Views;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
        DataContext = new LoginWindowViewModel();

    }
}

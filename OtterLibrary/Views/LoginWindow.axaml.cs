using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Security.Cryptography;
using OtterLibrary.ViewModels;
using OtterLibrary.Models;


namespace OtterLibrary.Views;


public partial class LoginWindow : Window
{
    public LoginWindow()
    {

        InitializeComponent();
        var vm = new LoginWindowViewModel();
        vm.LoginCallback = (user) =>
        {
            Login(user);
        };
        DataContext = vm;
    }

    public void Login(User user)
    {
        Window main = new MainWindow(user);
        main.Show();
        this.Close();
    }

}

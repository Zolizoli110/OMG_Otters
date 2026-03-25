using Avalonia.Controls;
using Avalonia.Interactivity;
using OtterLibrary.ViewModels;
using OtterLibrary.Models;
using System;
using System.Security.Cryptography;
namespace OtterLibrary.Views;
using OtterLibrary.ViewModels;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    public MainWindow(User? user)
    {
        if(user == null)
        {
            user = new User()
            {
                UserName = "test",
                Role = UserRole.Admin
            };
        }
        InitializeComponent();
        DataContext = new MainWindowViewModel(user);
    } 
}
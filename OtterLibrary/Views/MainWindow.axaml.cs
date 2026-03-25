using Avalonia.Controls;
using Avalonia.Interactivity;
using OtterLibrary.ViewModels;
using OtterLibrary.Models;
using System;
using System.Security.Cryptography;
namespace OtterLibrary.Views;

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
                userName = "test",
                role = UserRole.Admin
            };
        }
        InitializeComponent();
        DataContext = new MainWindowViewModel(user);
    } 
}
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Security.Cryptography;
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
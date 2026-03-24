using Avalonia.Controls;
using Avalonia.Interactivity;
using OtterLibrary.ViewModels;
using System;
using System.Security.Cryptography;
namespace OtterLibrary.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}
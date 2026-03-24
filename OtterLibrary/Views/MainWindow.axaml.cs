using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Security.Cryptography;
namespace OtterLibrary.Views;
using OtterLibrary.ViewModels;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}
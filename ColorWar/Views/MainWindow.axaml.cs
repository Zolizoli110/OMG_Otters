using Avalonia.Controls;

namespace ColorWar.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    void NewGameButtonClickHandler(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        
    }

    void LoadGameButtonClickHandler(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        
    }
    void QuitGameButtonClickHandler(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        this.Close();
    }
}
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
        var wnd = new NewGameWindow();
        wnd.Show();
    }

    void LoadGameButtonClickHandler(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var loadWindow = new Load();
        loadWindow.Show();
        this.Close();
    }
    void QuitGameButtonClickHandler(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        this.Close();
        
    }
}
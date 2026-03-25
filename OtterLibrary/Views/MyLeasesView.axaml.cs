using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using System.Threading.Tasks;

namespace OtterLibrary.Views;

public partial class MyLeasesView : UserControl
{
    public MyLeasesView()
    {
        InitializeComponent();
    }

    private void ReturnButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (sender is Button btn)
        {
            ReturnPopup.IsOpen = true;
        }
    }
}
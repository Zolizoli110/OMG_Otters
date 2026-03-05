using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System;

namespace ColorWar.Views;
public partial class NewGameWindow : Window
{
    private readonly RadioButton? _redButton1;
    private readonly RadioButton? _blueButton1;
    private readonly RadioButton? _redButton2;
    private readonly RadioButton? _blueButton2;
    private readonly TextBox? _playableMessageTextBox;
    private readonly TextBox? _gridSizeXTextBox;
    private readonly TextBox? _gridSizeYTextBox;
    private readonly TextBox? _player1NameTextBox;
    private readonly TextBox? _player2NameTextBox;

    public NewGameWindow()
    {
        AvaloniaXamlLoader.Load(this);
        _redButton1 = this.FindControl<RadioButton>("RedButton1");
        _blueButton1 = this.FindControl<RadioButton>("BlueButton1");
        _redButton2 = this.FindControl<RadioButton>("RedButton2");
        _blueButton2 = this.FindControl<RadioButton>("BlueButton2");
        _playableMessageTextBox = this.FindControl<TextBox>("PlayableMessageTextBox");
        _gridSizeXTextBox = this.FindControl<TextBox>("GridSizeXTextBox");
        _gridSizeYTextBox = this.FindControl<TextBox>("GridSizeYTextBox");
        _player1NameTextBox = this.FindControl<TextBox>("Player1NameTextBox");
        _player2NameTextBox = this.FindControl<TextBox>("Player2NameTextBox");
    }

    private void PlayButtonClickHandler(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var redButton1 = _redButton1;
        var blueButton1 = _blueButton1;
        var redButton2 = _redButton2;
        var blueButton2 = _blueButton2;
        var playableMessageTextBox = _playableMessageTextBox;
        var gridSizeXTextBox = _gridSizeXTextBox;
        var gridSizeYTextBox = _gridSizeYTextBox;
        var player1NameTextBox = _player1NameTextBox;
        var player2NameTextBox = _player2NameTextBox;
        bool playable = true;

        if (redButton1 is null || blueButton1 is null || redButton2 is null || blueButton2 is null || playableMessageTextBox is null || gridSizeXTextBox is null || gridSizeYTextBox is null || player1NameTextBox is null || player2NameTextBox is null)
        {
            return;
        }

        if (redButton1.IsChecked == true && redButton2.IsChecked == true)
        {
            playable = false;
        }
        else if (blueButton1.IsChecked == true && blueButton2.IsChecked == true)
        {
            playable = false;   
        }

        if (!int.TryParse(gridSizeXTextBox.Text, out var x) || !int.TryParse(gridSizeYTextBox.Text, out var y) || x <= 0 || y <= 0)
        {
            playableMessageTextBox.Text = "Please enter valid grid size";
            playableMessageTextBox.IsVisible = true;
            return;
        }

        if (!playable)
        {
            playableMessageTextBox.Text = "Please choose different colours";
            playableMessageTextBox.IsVisible = true;
            return;
        }

        playableMessageTextBox.Text = string.Empty;
        playableMessageTextBox.IsVisible = false;

        var player1Name = string.IsNullOrWhiteSpace(player1NameTextBox.Text) ? "Player 1" : player1NameTextBox.Text.Trim();
        var player2Name = string.IsNullOrWhiteSpace(player2NameTextBox.Text) ? "Player 2" : player2NameTextBox.Text.Trim();
        var player1Colour = redButton1.IsChecked == true ? Brushes.Red : blueButton1.IsChecked == true ? Brushes.Blue : Brushes.Yellow;
        var player2Colour = redButton2.IsChecked == true ? Brushes.Red : blueButton2.IsChecked == true ? Brushes.Blue : Brushes.Yellow;

        var gameWindow = new GameWindow(x, y, player1Name, player1Colour, player2Name, player2Colour,null);//int int string brushes string brushes
        gameWindow.Show();  
        Close();
    }
}
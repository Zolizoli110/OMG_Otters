using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia;

namespace ColorWar.Views;
public partial class GridWindow : Window
{
    private readonly Grid? _gameGrid;
    private readonly TextBlock? _player1InfoTextBlock;
    private readonly TextBlock? _player2InfoTextBlock;

    public GridWindow() : this(1, 1, "Player 1", "No colour", "Player 2", "No colour")
    {
    }

    public GridWindow(int x, int y) : this(x, y, "Player 1", "No colour", "Player 2", "No colour")
    {
    }

    public GridWindow(int x, int y, string player1Name, string player1Colour, string player2Name, string player2Colour)
    {
        AvaloniaXamlLoader.Load(this);
        _gameGrid = this.FindControl<Grid>("GameGrid");
        _player1InfoTextBlock = this.FindControl<TextBlock>("Player1InfoTextBlock");
        _player2InfoTextBlock = this.FindControl<TextBlock>("Player2InfoTextBlock");
        SetPlayerInfo(player1Name, player1Colour, player2Name, player2Colour);
        BuildGrid(x, y);
    }

    private void SetPlayerInfo(string player1Name, string player1Colour, string player2Name, string player2Colour)
    {
        var player1InfoTextBlock = _player1InfoTextBlock;
        var player2InfoTextBlock = _player2InfoTextBlock;

        if (player1InfoTextBlock is null || player2InfoTextBlock is null)
        {
            return;
        }

        player1InfoTextBlock.Text = $"Player1: {player1Name} ({player1Colour})";
        player2InfoTextBlock.Text = $"Player2: {player2Name} ({player2Colour})";
    }

    private void BuildGrid(int x, int y)
    {
        var gameGrid = _gameGrid;
        if (gameGrid is null)
        {
            return;
        }

        gameGrid.RowDefinitions.Clear();
        gameGrid.ColumnDefinitions.Clear();
        gameGrid.Children.Clear();

        for (var row = 0; row < y; row++)
        {
            gameGrid.RowDefinitions.Add(new RowDefinition(GridLength.Auto));
        }

        for (var column = 0; column < x; column++)
        {
            gameGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Auto));
        }

        for (var row = 0; row < y; row++)
        {
            for (var column = 0; column < x; column++)
            {
                var cell = new Border
                {
                    Width = 50,
                    Height = 50,
                    Margin = new Thickness(1),
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(1)
                };

                Grid.SetRow(cell, row);
                Grid.SetColumn(cell, column);
                gameGrid.Children.Add(cell);
            }
        }
    }
}
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using ColorWar.Views;
using System;
using System.Collections.Generic;
using ColorWar.Models;

namespace ColorWar;

public partial class GameWindow : Window
{
    Player one;
    Player two;
    int turn;
    int x_size;
    int y_size;
    List<List<int>> table;
    public GameWindow(int x_size, int y_size, string one_name, IBrush one_color, string two_name, IBrush two_color)
    {
        this.x_size = x_size;
        this.y_size = y_size;
        one = new Player(one_name,one_color);
        two = new Player(two_name,two_color);
        turn = 0;
        LoadTable();
        InitializeComponent();
        BuildGameGrid(x_size, y_size);
        TurnIndicator.Text = $"Player 1 \"{one.Name}\" turn";
        TurnIndicator.Foreground = one.PlayerColor;
    }
    public GameWindow(int x_size, int y_size, string one_name, IBrush one_color, string two_name, IBrush two_color, string[][] board)
    {
        this.x_size = x_size;
        this.y_size = y_size;
        one = new Player(one_name, one_color);
        two = new Player(two_name, two_color);
        turn = 0;
        LoadTable(board);
        InitializeComponent();
        BuildGameGrid(x_size, y_size);
        TurnIndicator.Text = $"Player 1 \"{one.Name}\" turn";
        TurnIndicator.Foreground = one.PlayerColor;
    }
    private void LoadTable()//creates a X+2xY+2 matrix and fills it up with 0
    {
        table = new List<List<int>>(x_size + 2);
        for (int i = 0; i < x_size + 2; i++)
        {
            List<int> list = new List<int>(y_size + 2);
            for (int j = 0; j < y_size + 2; j++)
            {
                if (j == 0 || j == y_size + 1)
                {
                    list.Add(-1);
                }
                else
                {
                    list.Add(0);
                }
            }
            table.Add(list);
        }
    }
    private void LoadTable(string[][] board)//creates a X+2xY+2 matrix and fills it up with 0
    {
        table = new List<List<int>>(x_size + 2);
        for (int i = 0; i < x_size + 2; i++)
        {
            List<int> list = new List<int>(y_size + 2);
            for (int j = 0; j < y_size + 2; j++)
            {
                if (j == 0 || j == y_size + 1 || i == 0 || i == x_size + 1)
                {
                    list.Add(-1);
                }
                else
                {
                    switch (board[i - 1][j-1])
                    {
                        case "0":
                            list.Add(0);
                            break;
                        case "1":
                            list.Add(1);
                            turn++;
                            break;
                        case "2":
                            list.Add(2);
                            turn++;
                            break;
                        default:
                            break;
                    }
                }
            }
            table.Add(list);
        }
    }
    private void SaveMove(int x, int y, int player)//saves moves from button click to table and converts them as table is larger than button grid
    {
        int x1 = x + 1; 
        int y1= y + 1;
        table[x1][y1] = player;
    }

    private bool CheckMove(int x, int y, int player)//checks if move is valid
    {
        int x1 = x + 1;
        int y1 = y + 1;
        for(int i=x1-1;i<x1+2;i++)
        {
            for(int j=y1-1;j<y1+2;j++)
            {
                if (table[i][j]==player)
                    return true;
            }
        }
        return false;
    }
    private bool CheckWin(int player)//checks if other player can make any moves 
    {
        for(int i=1;i<x_size;i++)
        {
            for (int j=1;j<y_size;j++)
            {
                //if(table[i][j]==0 && CheckMove(i,j,player)) return false;
                if (table[i][j] == 0)
                    if (CheckMove(i-1, j-1, player))
                        return false;
            }
        }
        return true;
    }

    private void GameWon(Player player)
    {
        TurnIndicator.Foreground = player.PlayerColor;
        TurnIndicator.Text = $"Player {player.Name} won ";
        foreach(var child in GameMap.Children)
        {
            if(child is Button button && button.Tag is int tag && tag==0)
            {
                button.IsEnabled = false;
            }
        }

    }
    private Button CreateCell()//instead of making each button in the xaml we can make this and call it
    {
        var cell = new Button
        {
            Tag = 0,
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,//stretches the thing to fill out the entire space in the grid horizontally
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,//same but vertically
            Background = Brushes.White,
            BorderBrush = Brushes.Black,
            BorderThickness = new Thickness(2),
            CornerRadius = new CornerRadius(0)//makes the edges sharp
        };
        cell.Click += SelectOnClick;
        return cell;
    }
    private void BuildGameGrid(int row, int column)//we build the game grid out of buttons
    {
        GameMap.RowDefinitions.Clear();
        GameMap.ColumnDefinitions.Clear();
        GameMap.Children.Clear();
        for (int i = 0; i < row; i++)
        {
            GameMap.RowDefinitions.Add(new RowDefinition(GridLength.Star));
        }
        for (int i = 0; i < column; i++)
        {
            GameMap.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
        }
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                var cell = CreateCell();
                Grid.SetRow(cell, i);
                Grid.SetColumn(cell, j);
                GameMap.Children.Add(cell);
            }
        }
    }

    private void SelectOnClick(object? sender, RoutedEventArgs e)//if someone clicked a button(out of the grid) we call this
    {
        if(sender is Button button)
        {
            if((int)button.Tag!=0)//if button was not clicked before
            {
                return;
            }
            int row = Grid.GetRow(button);
            int column = Grid.GetColumn(button);
            if (turn % 2 == 0)//check who's turn is it
            {
                
                if (turn != 0)//if not first turn must check adjacent blocks
                {
                    if(!CheckMove(row, column,1))
                    {
                        TurnIndicator.Text = $"Player \"{one.Name}\" turn - Invalid Move(Must place adjacent to an occupied cell.)";
                        return;
                    }
                }
                button.Background = one.PlayerColor;
                button.Tag = 1;//set button tag
                SaveMove(row, column,1);//save to matrix as well
                TurnIndicator.Text = $"Player \"{two.Name}\" turn";
                TurnIndicator.Foreground = two.PlayerColor;
            }
            else
            {
                if (turn != 1)//if not first turn must check adjacent blocks
                {
                    if (!CheckMove(row, column, 2))
                    {
                        TurnIndicator.Text = $"Player \"{two.Name}\" turn - Invalid Move(Must place adjacent to an occupied cell.)";
                        return;
                    }
                }
                button.Background = two.PlayerColor;
                button.Tag = 2;//set button
                SaveMove(row, column, 2);//save positon
                TurnIndicator.Text = $"Player \"{one.Name}\" turn";
                TurnIndicator.Foreground = one.PlayerColor;
            }
            turn++;
            if(turn == x_size*y_size)//if ran out of moves then its a draw
            {
                TurnIndicator.Foreground = Brushes.Black;
                TurnIndicator.Text = "Draw";
            }
            else if(turn>1 && CheckWin((turn%2)+1))//if not draw check if player can make moves
            {
                Player winner;
                if (((turn + 1) % 2) + 1 == 1)
                {
                    winner = one;
                }
                else
                {
                    winner = two;
                    GameWon(winner);
                }
            }
        }
    }
    private void QuitOnClick(object? sender, RoutedEventArgs e)//quits on clikc
    {
        Close();//closes this window 
    }
    private void NewOnClick(object? sender, RoutedEventArgs e)//createsd new game window 
    {
        var wnd = new NewGameWindow();
        wnd.Show();
    }
    private void SaveOnClick(object? sender, RoutedEventArgs e)//saves game on click
    {
        Board board = new Board(x_size, y_size, one, two);
        for (int i = 0; i < x_size; i++)
        {
            for (int j = 0; j < y_size; j++)
            {
                board.Cells[i, j] = table[i + 1][j + 1];
            }
        }
        board.SaveToFile();
    }
}
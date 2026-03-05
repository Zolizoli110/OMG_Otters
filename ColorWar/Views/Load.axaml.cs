using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using ColorWar.Data;
using ColorWar.Models;

namespace ColorWar.Views;

public partial class Load : Window
{
    public Load()
    {
        InitializeComponent();
        LoadSaves();
    }

    private void BackToMenuButtonClickHandler(object? sender, RoutedEventArgs e)
    {
        var mainWindow = new MainWindow();
        mainWindow.Show();
        this.Close();
    }

    private void LoadSaves()
    {
        string[] files = System.IO.Directory.GetFiles("Saves", "*.csv");
        
        ListBox? savesListBox = this.FindControl<ListBox>("SaveFilesListBox");
        if (savesListBox != null)
        {
            foreach (string file in files)
            {
                Button button = new Button();
                button.Content = System.IO.Path.GetFileNameWithoutExtension(file);
                button.Name = System.IO.Path.GetFileNameWithoutExtension(file);
                button.Click += LoadSaveFileButtonClickHandler;
                ListBoxItem item = new ListBoxItem();
                item.Content = button;
                savesListBox.Items.Add(item);
            }
        }
    }

    private void LoadSaveFileButtonClickHandler(object? sender, RoutedEventArgs e)
    {
        string? fileName = (sender as Button)?.Name + ".csv";
        FileNameParser.TryGetPlayers(fileName, out var p1, out var p2);
        if (fileName != null)
        {
            var board = CsvIO.ReadCsv(fileName);
            List<int> map = new List<int>();

            for (int i = 0; i < board.Count; i++)
            {
                for (int j = 0; j < board[i].Count; j++)
                {
                    map.Add(board[i][j]);
                }
            }

            var gameWindow = new GameWindow(board.Count, board[0].Count, p1, Brushes.Red, p2, Brushes.Blue, map);
            gameWindow.Show();
            this.Close();
        }
    }
}
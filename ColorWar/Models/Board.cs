using System;
using System.IO;
using System.Text.RegularExpressions;
using Avalonia.Media;
using ColorWar.Data;

namespace ColorWar.Models;

public class Board
{
    public int[,] Cells { get; set; }
    public string? FileName { get; set; }
    public Player Player1{ get; set; } = new Player("Player 1", Brushes.Blue);
    public Player Player2{ get; set; } = new Player("Player 2", Brushes.Red);

    public Board(int height, int width, Player? player1, Player? player2)
    {
        Cells = new int[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Cells[i, j] = 0;
            }
        }
        
        if (player1 != null)
        {
            Player1 = player1;
        }

        if (player2 != null)
        {
            Player2 = player2;
        }
    }

    public Board(string fileName)
    {
        string[][] board = CsvIO.ReadCsv(fileName);
        Cells = new int[board.Length, board[0].Length];
        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[0].Length; j++)
            {
                Cells[i, j] = int.Parse(board[i][j]);
            }
        } 
        FileName = fileName;
    }

    public void SaveToFile()
    {
        string[][] board = new string[Cells.GetLength(0)][];
        

        for (int i = 0; i < Cells.GetLength(0); i++)
        {
            board[i] = new string[Cells.GetLength(1)];
            for (int j = 0; j < Cells.GetLength(1); j++)
            {
                board[i][j] = Cells[i, j].ToString(); 
            }
        }
        CsvIO.WriteCsv(FileName ?? $"{Player1.Name}_vs_{Player2.Name}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.csv", board);
    }
    
}

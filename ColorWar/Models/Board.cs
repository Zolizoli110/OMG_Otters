using System;
using System.Collections.Generic;
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
        List<List<int>> board = CsvIO.ReadCsv(fileName);
        Cells = new int[board.Count, board[0].Count];
        for (int i = 0; i < board.Count; i++)
        {
            for (int j = 0; j < board[0].Count; j++)
            {
                Cells[i, j] = board[i][j];
            }
        } 
        FileName = fileName;
    }

    public void SaveToFile()
    {

        List<List<int>> board = new List<List<int>>();

        for (int i = 0; i < Cells.GetLength(0); i++)
        {
            // Create row first
            List<int> row = new List<int>();

            for (int j = 0; j < Cells.GetLength(1); j++)
            {
                row.Add(Cells[i, j]);
            }

            // Add row to board
            board.Add(row);
        }

        CsvIO.WriteCsv(FileName ?? $"{Player1.Name}_vs_{Player2.Name}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.csv", board);
    }
    
}

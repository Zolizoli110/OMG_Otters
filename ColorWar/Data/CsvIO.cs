using System;
using System.IO;

namespace ColorWar.Data;

public static class CsvIO
{
    
    public static void WriteCsv(string fileName, string[][] board)
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        using StreamWriter sw = new StreamWriter(filePath);
        sw.WriteLine($"{board.Length} {board[0].Length}");
        foreach (string[] line in board)
        {
            sw.WriteLine(string.Join(' ', line));
        }
        sw.Close();
    }
    
    public static string[][] ReadCsv(string fileName)
    {
        string filePath = GetFilePath(fileName);
        if (!File.Exists(filePath))
        {
            return Array.Empty<string[]>();
        };
        
        StreamReader sr = new StreamReader(filePath);
        string[] headers = sr.ReadLine()?.Split(' ') ?? Array.Empty<string>();
        string[][] board = new string[int.Parse(headers[0])][];
        int i = 0;
        while (!sr.EndOfStream)
        {
            string[] line = sr.ReadLine()?.Split(' ') ?? Array.Empty<string>();
            
            board[i] = line;
            i++;
        }
        sr.Close();
        return board;
    }
    
    private static string GetFilePath(string fileName)
    {
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
    }
}
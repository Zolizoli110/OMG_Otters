using System;
using System.Collections.Generic;
using System.IO;

namespace ColorWar.Data;

public static class CsvIO
{
    
    public static void WriteCsv(string fileName, List<List<int>> board)
    {
        string filePath = GetFilePath(fileName);
        using StreamWriter sw = new StreamWriter(filePath);
        sw.WriteLine($"{board.Count} {board[0].Count}");
        foreach (List<int> line in board)
        {
            sw.Write(string.Join(' ', line) + ' ');
        }
        sw.Flush();
        sw.Close();
    }
    
    public static List<List<int>>? ReadCsv(string fileName)
    {
        string filePath = GetFilePath(fileName);
        if (!File.Exists(filePath))
        {
            return  null;
        };
        
        List<List<int>> board = new List<List<int>>();
        using StreamReader sr = new StreamReader(filePath);
        string header = sr.ReadLine();
        string data = sr.ReadLine();
        sr.Close();
        
        string[] dimensions = header?.Split(' ') ?? Array.Empty<string>();
        string[] values = data?.Split(' ') ?? Array.Empty<string>();

        for (int i = 0; i < int.Parse(dimensions[0]) * int.Parse(dimensions[1]); i += int.Parse(dimensions[1]))
        {
            List<int> row = new List<int>();
            for (int j = 0; j < int.Parse(dimensions[1]); j++)
            {
                row.Add(int.Parse(values[i+j]));
            }
            board.Add(row);
        }
        return board;
    }
    
    private static string GetFilePath(string fileName)
    {
        string baseDir = AppContext.BaseDirectory;
        string savesDir = Path.Combine(baseDir, "Saves");
        Directory.CreateDirectory(savesDir);
        return Path.Combine(savesDir, fileName);
    }

    private static string GetLeaderboardFilePath()
    {
        string baseDir = AppContext.BaseDirectory;
        return Path.Combine(baseDir, "leaderboard.csv");
    }
    public static void WriteLeaderboard(Tuple<string, int> result)
    {
        string filePath = GetLeaderboardFilePath();
        List<Tuple<string, int>> leaderboard = new List<Tuple<string, int>>();
        if (File.Exists(filePath))
        {
            using StreamReader sr = new StreamReader(filePath);
            string header = sr.ReadLine();
            string data;
            while ((data = sr.ReadLine()) != null)
            {
                string[] parts = data.Split(' ');
                leaderboard.Add(new Tuple<string, int>(parts[0], int.Parse(parts[1])));
            }
            sr.Close();
        }
        leaderboard.Add(result);
        leaderboard.Sort((a, b) => a.Item2.CompareTo(b.Item2));
        
        using StreamWriter sw = new StreamWriter(filePath);
        foreach (Tuple<string, int> entry in leaderboard)
        {
            sw.WriteLine($"{entry.Item1} {entry.Item2}");
        }
        sw.Flush();
        sw.Close();
    }
    
    public static string ReadLeaderboard()
    {
        string filePath = GetLeaderboardFilePath();
        if (!File.Exists(filePath))
        {
            return "No leaderboard data available.";
        }
        
        string leaderboardData = "";
        using StreamReader sr = new StreamReader(filePath);
        string data;
        while ((data = sr.ReadLine()) != null)
        {
            leaderboardData += data + Environment.NewLine;
        }
        sr.Close();
        
        return leaderboardData;
    }
}
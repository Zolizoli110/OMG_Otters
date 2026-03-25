using OtterLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Reflection.Metadata.BlobBuilder;

namespace OtterLibrary.Data;

public class BookIO
{
    private string filePath;

    public BookIO(string filePath)
    {
        this.filePath = filePath;
    }

    public ObservableCollection<Book>? ReadBook()
    {
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close();
            return new ObservableCollection<Book>();
        }
        
        string json = File.ReadAllText(filePath); 
        
        if (string.IsNullOrWhiteSpace(json))
        {
            File.WriteAllText(filePath, "[]");
            return new ObservableCollection<Book>();
        }

        try
        {
            return JsonSerializer.Deserialize<ObservableCollection<Book>>(json)
                   ?? new ObservableCollection<Book>();
        }
        catch
        {
            // Recover from corrupted JSON
            File.WriteAllText(filePath, "[]");
            return new ObservableCollection<Book>();
        }
    }
    public void Save(ObservableCollection<Book> books)
    {
        StreamWriter sw = new StreamWriter(filePath);
        string json = JsonSerializer.Serialize(books);
        sw.Write(json);
        sw.Flush();
        sw.Close();
    }
}
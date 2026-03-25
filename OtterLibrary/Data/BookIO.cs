using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using OtterLibrary.Models;

namespace OtterLibrary.Data;

public class BookIO
{
    private string filePath;

    public BookIO(string filePath)
    {
        this.filePath = filePath;
    }

    public List<Book>? ReadBook()
    {
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close();
            return new List<Book>();
        }
        
        string json = File.ReadAllText(filePath); 
        
        if (string.IsNullOrWhiteSpace(json))
        {
            File.WriteAllText(filePath, "[]");
            return new List<Book>();
        }

        try
        {
            return JsonSerializer.Deserialize<List<Book>>(json)
                   ?? new List<Book>();
        }
        catch
        {
            // Recover from corrupted JSON
            File.WriteAllText(filePath, "[]");
            return new List<Book>();
        }
    }

    public void WriteBook(List<Book> books)
    {
        List<Book> bookList = ReadBook();
        StreamWriter sw = new StreamWriter(filePath);
        bookList.AddRange(books);
        string json = JsonSerializer.Serialize(bookList);
        sw.Write(json);
        sw.Flush();
        sw.Close();
    }
}
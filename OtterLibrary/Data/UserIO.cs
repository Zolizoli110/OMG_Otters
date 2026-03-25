using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using OtterLibrary.Models;

namespace OtterLibrary.Data;

public class UserIO
{
    private string filePath;

    public UserIO(string filePath)
    {
        this.filePath = filePath;
    }

    public User? CheckUser(string username)
    {
        StreamReader sr = new StreamReader(filePath);
        string json = sr.ReadToEnd();
        sr.Close();
        List<User> users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        return users.FirstOrDefault(u => u.UserName == username);
    }

    public void Write(string username, List<Book> borrowdBooks)
    {
        StreamReader sr = new StreamReader(filePath);
        string json = sr.ReadToEnd();
        sr.Close();
        List<User> users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        
        users.FirstOrDefault(u => u.UserName == username)?.LeasedBooks.AddRange(borrowdBooks);
        
        StreamWriter sw = new StreamWriter(filePath);
        string jsonString = JsonSerializer.Serialize(users);
        sw.Write(jsonString);
        sw.Flush();
        sw.Close();
    }
}

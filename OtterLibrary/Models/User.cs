using System.Collections.Generic;

namespace OtterLibrary.Models
{
    public enum UserRole { Member,Librarian,Admin}
    public class User
    {
        public string? userName {  get; set; }
        public UserRole role { get; set; }
        public List<Book> LeasedBooks = new List<Book>();
    }
}

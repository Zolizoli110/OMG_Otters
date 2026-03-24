using System.Collections.Generic;

namespace OtterLibrary.Models
{
    public enum UserRole { Member,Librarian}
    public class User
    {
        string userName {  get; set; }
        UserRole role { get; set; }
        List<Book> LeasedBooks;
    }
}

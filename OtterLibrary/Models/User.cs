using System.Collections.Generic;

namespace OtterLibrary.Models
{
    public enum UserRole { Member,Librarian}
    public class User
    {
        public string UserName {  get; set; }
        public UserRole Role { get; set; }
        public List<Book> LeasedBooks {  get; set; }
        
        public User(string userName, UserRole role, List<Book>? leasedBooks)
        {
            this.UserName = userName;
            this.Role = role;
            LeasedBooks = leasedBooks ?? [];
        }
    }
}

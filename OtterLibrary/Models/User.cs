using System.Collections.ObjectModel;

namespace OtterLibrary.Models
{
    public enum UserRole { Member,Librarian,Admin}
    public class User
    {
        public string? UserName {  get; set; }
        public UserRole Role { get; set; }
        public ObservableCollection<Book> LeasedBooks { get; } = new();

        public string Hash { get; set; }
        public string Salt { get; set; }
    }
}
using System.Collections.ObjectModel;

namespace OtterLibrary.Models
{
    public enum UserRole { Member,Librarian,Admin}
    public class User
    {
        public string? userName {  get; set; }
        public UserRole role { get; set; }
        public ObservableCollection<Book> LeasedBooks { get; } = new();
    }
}

using CommunityToolkit.Mvvm.Input;
using OtterLibrary.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace OtterLibrary.ViewModels
{
    public partial class MyLeasesViewModel : ViewModelBase
    {
        public User user{ get; }
        public ObservableCollection<Book> Books { get; set; }
        public IEnumerable<Book> MyLeasedBooks => Books.Where(b => b.LeasedTo == user.UserName);
        public string PageTitle { get; } = "All the books I borrowed";
        public ICommand Return { get; }
        public MyLeasesViewModel(User? user, ObservableCollection<Book> Books) 
        {
            this.user = user;
            Return = new RelayCommand<Book>(ReturnBook);
        }
        private void ReturnBook(Book? book)
        {
            user.LeasedBooks.Remove(book);
            book.LeasedTo = null;
            book.Leased = false;
            OnPropertyChanged(nameof(book));
        }
    }
}

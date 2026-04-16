using CommunityToolkit.Mvvm.Input;
using OtterLibrary.Data;
using OtterLibrary.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace OtterLibrary.ViewModels
{
    public partial class MyLeasesViewModel : ViewModelBase
    {
        public BookIO BookIO { get; set; }
        public User user{ get; }

        public ObservableCollection<Book> Books { get; set; }
        public string PageTitle { get; } = "All the books I borrowed";
        public ICommand Return { get; }
        public MyLeasesViewModel(User? user, ObservableCollection<Book> Books) 
        {
            this.user = user;
            this.Books = Books;
            Return = new RelayCommand<Book>(ReturnBook);
            BookIO = new BookIO("catalog.json");
        }
        private void ReturnBook(Book? book)
        {
            user.LeasedBooks.Remove(book);
            book.LeasedTo = "";
            book.Leased = false;
            OnPropertyChanged(nameof(book));
            BookIO.Save(Books);
            //userIO.Return(user.UserName, book);
        }
    }
}

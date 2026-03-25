using CommunityToolkit.Mvvm.Input;
using OtterLibrary.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace OtterLibrary.ViewModels
{
    public partial class MyLeasesViewModel : ViewModelBase
    {
        public User user{ get; }
        public string PageTitle { get; } = "All the books I borrowed";
        public ICommand Return { get; }
        public MyLeasesViewModel(User? user) 
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

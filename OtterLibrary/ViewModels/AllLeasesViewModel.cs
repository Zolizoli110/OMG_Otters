using OtterLibrary.Models;
using OtterLibrary.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace OtterLibrary.ViewModels
{
    public partial class AllLeasesViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public string PageTitle { get; } = "All Currently Leased Books";

        public ObservableCollection<Book> Books { get; set; }
        public IEnumerable<Book> BorrowedBooks => Books.Where(b => b.LeasedTo != null);
        public AllLeasesViewModel(ObservableCollection<Book> Books)
        {
            this.Books = Books;

            foreach(Book book in Books)
            {
                book.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(Book.LeasedTo)) OnPropertyChanged(nameof(BorrowedBooks));
                };
            }
        }
    }
}

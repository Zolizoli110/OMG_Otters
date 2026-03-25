using OtterLibrary.Models;
using OtterLibrary.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OtterLibrary.ViewModels
{
    public partial class AllLeasesViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public string PageTitle { get; } = "All Currently Leased Books";

        public ObservableCollection<Book> Books { get; set; }
        public AllLeasesViewModel(ObservableCollection<Book> Books)
        {
            this.Books = Books;
        }
    }
}

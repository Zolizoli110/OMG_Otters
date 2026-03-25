using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.Input;
using OtterLibrary.Data;
using OtterLibrary.Models;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace OtterLibrary.ViewModels
{
    public partial class LibraryViewModel : ViewModelBase , INotifyPropertyChanged
    {
        public BookIO bookIO {  get; set; }
        public UserIO userIO { get; set; }
        public User user { get; }
        public bool SeeMemberStuff => user.Role == UserRole.Admin || user.Role == UserRole.Member;
        public bool SeeLibrarianStuff => user.Role == UserRole.Admin || user.Role == UserRole.Librarian;
        public ObservableCollection<Book> Books { get; set; }
        public Book newBook { get; set; }
        public ICommand Lease {  get; }
        public ICommand Edit { get; }
        public ICommand Delete { get; }
        public ICommand Add {  get; }
        public LibraryViewModel(User? user)
        {
            this.user = user;
            bookIO = new BookIO("catalog.json");
            userIO = new UserIO("user_catalog.json");
            newBook = new Book()
            {
                Author = "",
                Title = "",
                ISBN = "",
                Description = "",
                Picture = "",
            };

            Books = new ObservableCollection<Book>();
            /*{
                new Book()
                {
                    Title = "1984",
                    Author = "George Orwell",
                    ISBN = "978-1847498571",
                    Description = "Nineteen Eight-Four is George Orwell's terrifying vision of a totalitarian future in which everything and everyone is slave to a tyrannical regime.",
                    Picture = "avares://OtterLibrary/Assets/1984.jpg"
                },
                new Book()
                {
                    Title = "Dune",
                    Author = "Frank Herbert",
                    ISBN = "978-0441172719",
                    Description = "Whoever controls Arrakis controls the spice. And whoever controls the spice controls the universe.",
                    Picture = "avares://OtterLibrary/Assets/Dune.jpg"
                },
                new Book()
                {
                    Title = "Fahrenheit 451",
                    Author = "Ray Bradbury",
                    ISBN = "978-0006546061",
                    Description = "The hauntingly prophetic classic novel set in a not-too-distant future where books are burned by a special task force of firemen.",
                    Picture = "avares://OtterLibrary/Assets/Fahrenheit 451.jpg"
                },
                new Book()
                {
                    Title = "Red, White & Royal Blue",
                    Author = "Casey McQuiston",
                    ISBN = "978-1529099461",
                    Description = "What happens when America's First Son falls in love with the Prince of Wales?",
                    Picture = "avares://OtterLibrary/Assets/Red, White & Royal Blue.jpg"
                },
                new Book()
                {
                    Title = "Animal Farm",
                    Author = "George Orwell",
                    ISBN = "978-1998114450",
                    Description = "Set in a rural landscape, this remarkable novel examines the nature of power, corruption, and the human condition.",
                    Picture = "avares://OtterLibrary/Assets/Animal Farm.jpg"
                },
                new Book()
                {
                    Title = "Dune Messiah",
                    Author = "Frank Herbert",
                    ISBN = "978-0593098233",
                    Description = "Dune Messiah continues the story of Paul Atreides, better known—and feared—as the man christened Muad’Dib.",
                    Picture = "avares://OtterLibrary/Assets/Dune Messiah.jpg"
                },
                new Book()
                {
                    Title = "Children of Dune",
                    Author = "Frank Herbert",
                    ISBN = "978-0593098240",
                    Description = "The Children of Dune are twin siblings Leto and Ghanima Atreides, whose father, the Emperor Paul Muad’Dib, disappeared in the desert wastelands of Arrakis nine years ago.",
                    Picture = "avares://OtterLibrary/Assets/Children of Dune.jpg"
                },
                new Book()
                {
                    Title = "God Emperor of Dune",
                    Author = "Frank Herbert",
                    ISBN = "978-0593098257",
                    Description = "Millennia have passed on Arrakis, and the once-desert planet is green with life.",
                    Picture = "avares://OtterLibrary/Assets/God Emperor of Dune.jpg"
                },
                new Book()
                {
                    Title = "Heretics of Dune",
                    Author = "Frank Herbert",
                    ISBN = "978-0593098264",
                    Description = "Leto Atreides, the God Emperor of Dune, is dead.",
                    Picture = "avares://OtterLibrary/Assets/Heretics of Dune.jpg"
                },
                new Book()
                {
                    Title = "Chapterhouse: Dune",
                    Author = "Frank Herbert",
                    ISBN = "978-0593098271",
                    Description = "The desert planet Arrakis, called Dune, has been destroyed.",
                    Picture = "avares://OtterLibrary/Assets/Chapterhouse Dune.jpg"
                },
                new Book()
                {
                    Title = "The Hunger Games",
                    Author = "Suzanne Collins",
                    ISBN = "978-1339030609",
                    Description = "One of the ways the Capitol keeps control is its annual Hunger Games, a televised fight to the death featuring two young tributes from each of Panem's twelve districts.",
                    Picture = "avares://OtterLibrary/Assets/The Hunger Games.jpg"
                },
                new Book()
                {
                    Title = "Catching Fire",
                    Author = "Suzanne Collins",
                    ISBN = "978-1546159544",
                    Description = "Against all odds, Katniss Everdeen has won the annual Hunger Games with fellow district tribute Peeta Mellark.",
                    Picture = "avares://OtterLibrary/Assets/Catching Fire.jpg"
                }
            };*/
            Books = bookIO.ReadBook();
            foreach(Book book in Books)
            {
                if(book.Picture!="")
                {
                    book.ImageFromBinding = ImageHelper.LoadFromResource(new Uri(book.Picture));
                }
                if(book.LeasedTo == user.UserName)
                {
                    user.LeasedBooks.Add(book);
                }
            }
            Lease = new RelayCommand<Book>(LeaseBook);
            Edit = new RelayCommand<Book>(EditBook);
            Delete = new RelayCommand<Book>(DeleteBook);
            Add = new RelayCommand(AddBook);
        }
        private void LeaseBook(Book? book)
        {
            book.ImageFromBinding = ImageHelper.LoadFromResource(new Uri(book.Picture));
            user.LeasedBooks.Add(book);
            book.LeasedTo = user.UserName;
            book.Leased = true;
            OnPropertyChanged(nameof(book));
            bookIO.Save(Books);
            //userIO.Borrow(user.UserName,Books);
        }
        private void EditBook(Book? book)
        {
            if (book == null) return;
            if(book.IsEditing)
            {
                book.IsEditing = false;
                bookIO.Save(Books);
            }
            else
            {
                book.IsEditing = true;
            }
        }
        private void DeleteBook(Book? book)
        {
            Books.Remove(book);
            bookIO.Save(Books);
        }
        private void AddBook()
        {
            if(newBook==null) return;
            Book BookToAdd = new Book()
            {
                Title = newBook.Title,
                Author = newBook.Author,
                ISBN = newBook.ISBN,
                Description = newBook.Description,
                Picture = newBook.Picture,
                Leased = false,
                LeasedTo = ""
            };
            if (BookToAdd.Picture != "" && BookToAdd.Picture!=null)
            {
                string newPicture = "avares://OtterLibrary/Assets/";
                newPicture += newBook.Picture;
                BookToAdd.Picture = newPicture;
                BookToAdd.ImageFromBinding = ImageHelper.LoadFromResource(new Uri(BookToAdd.Picture));
            }
            Books.Add(BookToAdd);
            newBook = new Book();
            OnPropertyChanged(nameof(newBook));
            bookIO.Save(Books);
        }

    }
}

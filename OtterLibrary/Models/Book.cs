namespace OtterLibrary.Models
{
    public class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public User? LeasedTo {  get; set; }
        
        public Book(string isbn, string title, string author, string description, User? leasedTo)
        {
            ISBN = isbn;
            Title = title;
            Author = author;
            Description = description;
            LeasedTo = leasedTo ?? null;
        }
    }
}

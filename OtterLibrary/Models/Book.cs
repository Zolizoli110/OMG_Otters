namespace OtterLibrary.Models
{
    public class Book
    {
        string ISBN { get; set; }
        string Title { get; set; }
        string Author { get; set; }
        string Description { get; set; }
        User leasedTo {  get; set; }
    }
}

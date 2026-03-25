namespace OtterLibrary.Models
{
    public class Book
    {
        public string? ISBN { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public string? Picture {  get; set; }
        public User? leasedTo {  get; set; }
    }
}

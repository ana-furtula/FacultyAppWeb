namespace FacultyAppWeb.Models
{
    public class BooksResponse
    {
        public string Error { get; set; }
        public string Total { get; set; }
        public ITBook[] Books { get; set; }
    }
}

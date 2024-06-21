namespace LibraryManagement.Web.Models.Entities
{
    public class Book
    {
        public Guid BookID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public bool Availability { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Web.Models
{
    public class AddBookvViewModel
    {
        public Guid BookID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Genre { get; set; }
        public bool Availability { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Web.Models.Entities
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password Should be a minimum of 8 Characters")]
        public string Password { get; set; }
    }
}

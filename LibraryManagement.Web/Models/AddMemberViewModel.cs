using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Web.Models
{
    public class AddMemberViewModel
    {
        public Guid MemberID { get; set; }

        [Required]
        public string Name { get; set; }
        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }
        public bool Status { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8,ErrorMessage ="Password Should be a minimum of 8 Characters")]
        public string Password { get; set; }
    }
}

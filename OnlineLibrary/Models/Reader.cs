using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary.Models
{
    public class Reader
    {
        [Required]
        public long ReaderId { get; set; }

        [Display(Name = "Login")]
        [Required]
        public string NickName { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Role { get; set; }
        public long RoleId { get; set; }

        public static Reader GetDefaultGuestReader()
        {
            Reader guest = new Reader();
            guest.RoleId = 3;
            return guest;
        }
    }
}
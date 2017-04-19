using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Nickname { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
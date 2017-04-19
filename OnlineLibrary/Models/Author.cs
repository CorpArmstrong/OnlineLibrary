using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary.Models
{
    public class Author 
    {
        [Required]
        public long AuthorID { get; set; }

        [Display(Name = "First name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Nick name")]
        public string NickName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birth date")]
        public DateTime BirthDate { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Photo url")]
        public string PhotoUrl { get; set; }
    }
}
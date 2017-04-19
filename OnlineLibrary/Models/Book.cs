using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary.Models
{
    public class Book
    {
        [Required]
        public long BookId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Publish date")]
        [Required]
        public DateTime PublishDate { get; set; }

        public string Genre { get; set; }

        [Display(Name = "Image url")]
        public string ImageUrl { get; set; }

        public bool IsBookTaken { get; set; }

        public int NormQuantity { get; set; }

        [Required]
        public int RealQuantity { get; set; }

        public List<Author> Authors { get; set; }

        [RegularExpression(@"^([a-zA-Z]+,?)+[a-zA-Z]$",
         ErrorMessage = "Characters are not allowed.")]
        [Required]
        [Display(Name = "Authors (Type authors id number from table separeted by commas)")]
        public string AuthorsStr { get; set; }
    }
}
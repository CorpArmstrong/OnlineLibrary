using System;

namespace OnlineLibrary.Models
{
    public class UserBook
    {
        public long BookId { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
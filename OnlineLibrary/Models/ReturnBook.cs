
namespace OnlineLibrary.Models
{
    public class ReturnBook
    {
        public long BookId { get; set; }
        public long ReaderId { get; set; }
        public int SelectedRow { get; set; }
    }
}
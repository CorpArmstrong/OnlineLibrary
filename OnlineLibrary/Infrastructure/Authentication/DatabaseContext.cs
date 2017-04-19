using OnlineLibrary.Infrastructure.DAL;

namespace OnlineLibrary.Infrastructure.Authentication
{
    public class DatabaseContext
    {
        public DatabaseContext()
        {
            Readers = new UserDAL();
        }

        public UserDAL Readers { get; set; }
    }
}
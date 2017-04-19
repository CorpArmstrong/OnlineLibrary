using OnlineLibrary.Models;

namespace OnlineLibrary.Infrastructure.Authentication
{
    public class ReaderAuthentication
    {
        private DatabaseContext _dbContext;

        public ReaderAuthentication()
        {
            _dbContext = new DatabaseContext();
        }

        public bool CheckLogin(string nickname, string password, out Reader reader)
        {
            return _dbContext.Readers.FindUser(nickname, password, out reader);
        }

        public bool CreateReader(Reader reader)
        {
            return _dbContext.Readers.AddUser(reader);
        }
    }
}

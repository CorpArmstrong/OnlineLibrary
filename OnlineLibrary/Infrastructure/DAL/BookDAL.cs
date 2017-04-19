using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using OnlineLibrary.Models;
using System.Configuration;

namespace OnlineLibrary.Infrastructure.DAL
{
    public class BookDAL
    {
        /// <summary>
        /// Gets available books
        /// </summary>
        /// <param name="viewAllBooks">If true - allows to get all books</param>
        /// <returns></returns>
        public List<Book> GetAvailableBooks(bool viewAllBooks)
        {
            List<Book> bookList = new List<Book>();

            SqlConnection dbConnection = null;
            SqlDataReader dbReader = null;

            try
            {
                dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString);
                dbConnection.Open();

                SqlCommand availableBooksCmd = new SqlCommand(
                    @"select t2.BookId, 
                            t2.ImageURL, 
                             t2.Name, 
                             t2.PublishDate, 
	                         t2.Genre, 
                             t1.NormQuantity,
	                         t1.RealQuantity
                      from BookRepository as t1
                      join Book           as t2 on t2.BookID = t1.BookID
                      where (@ShowAllBooks = '0' 
                      or (@ShowAllBooks  = '1' and t1.RealQuantity > 0))", dbConnection);

                char showAllBooks = '0';

                if (!viewAllBooks)
                {
                    showAllBooks = '1';
                }

                SqlParameter showAllParam = new SqlParameter();
                showAllParam.ParameterName = "@ShowAllBooks";
                showAllParam.Value = showAllBooks;

                availableBooksCmd.Parameters.Add(showAllParam);
                dbReader = availableBooksCmd.ExecuteReader();

                Book book = null;

                while (dbReader.Read())
                {
                    book = new Book();
                    book.BookId = (long)dbReader["BookId"];
                    book.Name = dbReader["Name"].ToString();
                    book.PublishDate = (DateTime)dbReader["PublishDate"];
                    book.Genre = dbReader["Genre"].ToString();
                    book.ImageUrl = dbReader["ImageUrl"].ToString();
                    book.NormQuantity = (int)dbReader["NormQuantity"];
                    book.RealQuantity = (int)dbReader["RealQuantity"];

                    bookList.Add(book);
                }
            }
            finally
            {
                if (dbReader != null)
                {
                    dbReader.Close();
                }

                if (dbConnection != null)
                {
                    dbConnection.Close();
                }
            }

            return bookList;
        }

        public bool RemoveBook(long bookId)
        {
            bool isDeleted = true;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
            {
                SqlCommand deleteBookCmd = new SqlCommand(
                        @"delete Book from Book where bookID = @bookID", connection);
                deleteBookCmd.Parameters.AddWithValue("@bookID", bookId);

                SqlCommand deleteBookFromRepoCmd = new SqlCommand(
                        @"delete BookRepository from BookRepository where bookID = @bookID", connection);
                deleteBookFromRepoCmd.Parameters.AddWithValue("@bookID", bookId);

                SqlCommand deleteBookFromAuthorsCmd = new SqlCommand(
                        @"delete BookAuthors from BookAuthors where bookID = @bookID", connection);
                deleteBookFromAuthorsCmd.Parameters.AddWithValue("@bookID", bookId);

                try
                {
                    connection.Open();
                    deleteBookCmd.ExecuteNonQuery();
                    deleteBookFromRepoCmd.ExecuteNonQuery();
                    deleteBookFromAuthorsCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    isDeleted = false;
                }
            }

            return isDeleted;
        }

        public bool TakeBook(long readerId, long bookId, out string bookName)
        {
            SqlDataReader dbReader = null;
            bookName = null;
            bool isBookTaken = false;
            int isTaken = 0;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
            {
                SqlCommand insertJournalCmd = new SqlCommand(
                        @"insert into IssuingJournal(BookID, ReaderID, IssueDate)
                          select @bookID, @readerID, getdate()", connection);

                insertJournalCmd.Parameters.AddWithValue("@readerID", readerId);
                insertJournalCmd.Parameters.AddWithValue("@bookID", bookId);

                SqlCommand updateBookRepoCmd = new SqlCommand(
                        @"update BookRepository
                          set RealQuantity = RealQuantity - 1
                          where bookID = @bookID", connection);

                updateBookRepoCmd.Parameters.AddWithValue("@bookID", bookId);

                SqlCommand checkBookTakenCmd = new SqlCommand(
                    @"select 1 as 'isTaken'
                      from IssuingJournal 
                      where readerID = @readerID
                        and bookID = @bookID
                        and ReturnDate is null", connection);

                checkBookTakenCmd.Parameters.AddWithValue("@readerID", readerId);
                checkBookTakenCmd.Parameters.AddWithValue("@bookID", bookId);

                SqlCommand getBookNameCmd = new SqlCommand(
                        @"select Name as 'bookName' 
                          from Book
                          where bookID = @bookID", connection);

                getBookNameCmd.Parameters.AddWithValue("@bookID", bookId);

                try
                {
                    connection.Open();
                    dbReader = checkBookTakenCmd.ExecuteReader();

                    while (dbReader.Read())
                    {
                        isTaken = ((int)dbReader["isTaken"]);
                        isBookTaken = isTaken > 0 ? true : false;
                    }

                    if (dbReader != null)
                    {
                        dbReader.Close();
                    }

                    if (!isBookTaken)
                    {
                        insertJournalCmd.ExecuteNonQuery();
                        updateBookRepoCmd.ExecuteNonQuery();
                    }

                    dbReader = getBookNameCmd.ExecuteReader();

                    while (dbReader.Read())
                    {
                        bookName = dbReader["bookName"].ToString();
                    }

                    if (dbReader != null)
                    {
                        dbReader.Close();
                    }
                }
                catch (Exception ex)
                {
                }
            }

            return isBookTaken;
        }

        public DateTime? ReturnBook(long readerId, long bookId)
        {
            SqlConnection dbConnection = null;
            DateTime returnDate = DateTime.Now;

            try
            {
                dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString);
                dbConnection.Open();

                SqlCommand updateJournalCmd = new SqlCommand(
                        @"update IssuingJournal
                          set ReturnDate = @returnDate
                          where readerID = @readerID
                          and bookID = @bookID
                          and ReturnDate is null", dbConnection);

                updateJournalCmd.Parameters.AddWithValue("@readerID", readerId);
                updateJournalCmd.Parameters.AddWithValue("@bookID", bookId);
                updateJournalCmd.Parameters.AddWithValue("@returnDate", returnDate);

                SqlCommand updateBookRepoCmd = new SqlCommand(
                        @"update BookRepository
                          set RealQuantity = RealQuantity + 1
                          where bookID = @bookID", dbConnection);

                updateBookRepoCmd.Parameters.AddWithValue("@bookID", bookId);

                updateJournalCmd.ExecuteNonQuery();
                updateBookRepoCmd.ExecuteNonQuery();
            }
            finally
            {
                if (dbConnection != null)
                {
                    dbConnection.Close();
                }
            }

            return returnDate;
        }

        public int ChangeBookQuantity(long bookId, int newQuantity)
        {
            SqlDataReader dbReader = null;
            int currentQuantity = 0;
            int returnQuantity = 0;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
            {
                SqlCommand getCurrentQuantityCmd = new SqlCommand(
                        @"select NormQuantity
                          from BookRepository
                          where bookID = @bookID", connection);

                getCurrentQuantityCmd.Parameters.AddWithValue("@bookID", bookId);


                SqlCommand changeBookQuantityCmd = new SqlCommand(
                        @"update BookRepository
                          set RealQuantity = RealQuantity + (@newQuantity - @currentQuantity), 
                              NormQuantity = @newQuantity
                          where bookID = @bookID", connection);

                changeBookQuantityCmd.Parameters.AddWithValue("@bookID", bookId);
                changeBookQuantityCmd.Parameters.AddWithValue("@newQuantity", newQuantity);

                SqlCommand getRealQuantityCmd = new SqlCommand(
                        @"select RealQuantity
                          from BookRepository
                          where bookID = @bookID", connection);

                getRealQuantityCmd.Parameters.AddWithValue("@bookID", bookId);

                try
                {
                    connection.Open();
                    getCurrentQuantityCmd.ExecuteNonQuery();

                    dbReader = getCurrentQuantityCmd.ExecuteReader();

                    while (dbReader.Read())
                    {
                        currentQuantity = ((int)dbReader["NormQuantity"]);
                    }

                    if (dbReader != null)
                    {
                        dbReader.Close();
                    }

                    changeBookQuantityCmd.Parameters.AddWithValue("@currentQuantity", currentQuantity);
                    changeBookQuantityCmd.ExecuteNonQuery();

                    dbReader = getRealQuantityCmd.ExecuteReader();

                    while (dbReader.Read())
                    {
                        returnQuantity = ((int)dbReader["RealQuantity"]);
                    }

                    if (dbReader != null)
                    {
                        dbReader.Close();
                    }
                }
                catch (Exception ex)
                {
                }
            }

            return returnQuantity;
        }
        
        public bool AddBook(Book book)
        {
            bool isAdded = true;
            SqlDataReader dbReader = null;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
            {
                SqlCommand getMaxBookIdCmd = new SqlCommand(
                        @"select max(bookid) as 'maxID'
                          from Book", connection);

                SqlCommand insertBookCmd = new SqlCommand(
                        @"SET IDENTITY_INSERT Book ON;
                          insert into Book(BookId, Name, PublishDate, Genre, ImageUrl)
                          values
                          (@bookId, @name, @publishDate, @genre, @imageUrl);

                          SET IDENTITY_INSERT Book ON;", connection);

                SqlCommand insertBookRepositoryCmd = new SqlCommand(
                        @"insert into BookRepository(BookID, NormQuantity, RealQuantity)
                          values
                          (@bookID, @quantity, @quantity)", connection);

                SqlCommand insertBookAuthorCmd = new SqlCommand(
                        @"insert into BookAuthors(BookID, AuthorID)
                          values
                          (@bookID, @authorID)", connection);

                string[] authorIdsString = book.AuthorsStr.Split(',');
                int[] authorIds = new int[authorIdsString.Length];

                for (int i = 0; i < authorIdsString.Length; i++)
                {
                    int.TryParse(authorIdsString[i], out authorIds[i]);
                }
               
                try
                {
                    connection.Open();
                    dbReader = getMaxBookIdCmd.ExecuteReader();

                    while (dbReader.Read())
                    {
                        book.BookId = ((long)dbReader["maxID"]) + 1;
                    }

                    if (dbReader != null)
                    {
                        dbReader.Close();
                    }

                    insertBookCmd.Parameters.AddWithValue("@bookID", book.BookId);
                    insertBookCmd.Parameters.AddWithValue("@name", book.Name);
                    insertBookCmd.Parameters.AddWithValue("@publishDate", book.PublishDate);
                    insertBookCmd.Parameters.AddWithValue("@genre", book.Genre);
                    insertBookCmd.Parameters.AddWithValue("@imageUrl", book.ImageUrl);

                    insertBookRepositoryCmd.Parameters.AddWithValue("@bookID", book.BookId);
                    insertBookRepositoryCmd.Parameters.AddWithValue("@quantity", book.NormQuantity);

                    insertBookAuthorCmd.Parameters.AddWithValue("@bookID", book.BookId);

                    insertBookCmd.ExecuteNonQuery();
                    insertBookRepositoryCmd.ExecuteNonQuery();

                    for (int i = 0; i < authorIds.Length; i++)
                    {
                        insertBookAuthorCmd.Parameters.AddWithValue("@authorID", authorIds[i]);
                        insertBookAuthorCmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    isAdded = false;
                }
            }

            return isAdded;
        }
    }
}
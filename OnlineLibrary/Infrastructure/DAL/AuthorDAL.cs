using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using OnlineLibrary.Models;

namespace OnlineLibrary.Infrastructure.DAL
{
    public class AuthorDAL
    {
        public bool AddAuthor(Author author)
        {
            bool isAdded = true;
            SqlDataReader dbReader = null;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
            {
                SqlCommand getMaxAuthorIdCmd = new SqlCommand(
                       @"select max(AuthorId) as 'maxID'
                          from Author", connection);

                SqlCommand insertAuthorCmd = new SqlCommand(
                        @"SET IDENTITY_INSERT Author ON;
                          insert into Author(AuthorID, FirstName, LastName, NickName, BirthDate, PhotoURL)
                          values
                          (@authorid, @firstName, @lastName, @nickName, @birthDate, @photoURL);
                          SET IDENTITY_INSERT Author OFF;", connection);

                try
                {
                    connection.Open();
                    dbReader = getMaxAuthorIdCmd.ExecuteReader();

                    while (dbReader.Read())
                    {
                        author.AuthorID = ((long)dbReader["maxID"]) + 1;
                    }

                    if (dbReader != null)
                    {
                        dbReader.Close();
                    }

                    insertAuthorCmd.Parameters.AddWithValue("@authorid", author.AuthorID);
                    insertAuthorCmd.Parameters.AddWithValue("@firstName", author.FirstName);
                    insertAuthorCmd.Parameters.AddWithValue("@lastName", author.LastName);
                    insertAuthorCmd.Parameters.AddWithValue("@nickName", author.NickName);
                    insertAuthorCmd.Parameters.AddWithValue("@birthDate", author.BirthDate);
                    insertAuthorCmd.Parameters.AddWithValue("@photoURL", author.PhotoUrl);

                    insertAuthorCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    isAdded = false;
                }
            }

            return isAdded;
        }

        public List<Author> GetAuthors()
        {
            List<Author> authorsList = new List<Author>();

            SqlConnection dbConnection = null;
            SqlDataReader dbReader = null;

            try
            {
                dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString);
                dbConnection.Open();

                SqlCommand getAuthorsCmd = new SqlCommand(
                    @"select AuthorId, 
                             FirstName, 
                             LastName
                      from Author", dbConnection);

                dbReader = getAuthorsCmd.ExecuteReader();

                Author author = null;

                while (dbReader.Read())
                {
                    author = new Author();
                    author.AuthorID = (long)dbReader["AuthorID"];
                    author.FirstName = dbReader["FirstName"].ToString();
                    author.LastName = dbReader["LastName"].ToString();

                    authorsList.Add(author);
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

            return authorsList;
        }
    }
}
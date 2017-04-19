using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using OnlineLibrary.Models;

namespace OnlineLibrary.Infrastructure.DAL
{
    public class UserDAL
    {
        public static List<UserBook> GetBooksTakenByUser(long readerId)
        {
            List<UserBook> userBooks = new List<UserBook>();

            SqlConnection dbConnection = null;
            SqlDataReader dbReader = null;

            try
            {
                dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString);
                dbConnection.Open();

                SqlCommand booksTakenByUserCmd = new SqlCommand(
                    @"select t2.BookID, 
                             t2.ImageURL, 
                             t2.Name, 
                             t1.IssueDate, 
	                         t1.ReturnDate
                      from IssuingJournal as t1 
                      join Book         as t2 on t2.BookID = t1.BookID
                      where ReaderID = @ReaderId", dbConnection);

                SqlParameter readerIdParam = new SqlParameter();
                readerIdParam.ParameterName = "@ReaderId";
                readerIdParam.Value = readerId;

                booksTakenByUserCmd.Parameters.Add(readerIdParam);
                dbReader = booksTakenByUserCmd.ExecuteReader();

                UserBook userBook = null;

                while (dbReader.Read())
                {
                    userBook = new UserBook();
                    userBook.ImageUrl = dbReader["ImageUrl"].ToString();
                    userBook.Name = dbReader["Name"].ToString();
                    userBook.BookId = (long)dbReader["BookId"];

                    userBook.IssueDate = ((DateTime)dbReader["IssueDate"]);

                    // Return date:
                    if (!dbReader.IsDBNull(4))
                    {
                        userBook.ReturnDate = (DateTime?)dbReader["ReturnDate"];
                    }
                    else
                    {
                        userBook.ReturnDate = null;
                    }

                    userBooks.Add(userBook);
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

            return userBooks;
        }

        public bool FindUser(string nickname, string password, out Reader reader)
        {
            reader = null;
            bool result = false;
            SqlDataReader dbReader = null;
            int resultReader = 0;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
            {
                SqlCommand getReaderCredentialsCmd = new SqlCommand(
                        @"select 1 as 'Result'
                          from Reader
                          where NickName = @nickName
                            and [Password] = @password", connection);

                getReaderCredentialsCmd.Parameters.AddWithValue("@nickName", nickname);
                getReaderCredentialsCmd.Parameters.AddWithValue("@password", password);

                SqlCommand getReaderCmd = new SqlCommand(
                        @"select t1.ReaderId, 
                                 t1.FirstName, 
                                 t1.LastName, 
                                 t1.NickName, 
                                 t1.Email, 
                                 t1.Password, 
                                 t3.RoleId, 
                                 t3.RoleName
                          from Reader      as t1
                          join ReaderRoles as t2 on t2.readerid = t1.readerid
                          join Roles       as t3 on t3.roleid = t2.roleid
                          where t1.NickName = @nickName", connection);

                getReaderCmd.Parameters.AddWithValue("@nickName", nickname);

                try
                {
                    connection.Open();
                    dbReader = getReaderCredentialsCmd.ExecuteReader();

                    while (dbReader.Read())
                    {
                        resultReader = ((int)dbReader["Result"]);
                        result = resultReader > 0 ? true : false;
                    }

                    if (dbReader != null)
                    {
                        dbReader.Close();
                    }

                    // If we have found a reader:
                    if (result)
                    {
                        reader = new Reader();
                        dbReader = getReaderCmd.ExecuteReader();

                        while (dbReader.Read())
                        {
                            reader.ReaderId = ((long)dbReader["ReaderId"]);
                            reader.FirstName = dbReader["FirstName"].ToString();
                            reader.LastName = dbReader["LastName"].ToString();
                            reader.NickName = nickname;
                            reader.Password = password;
                            reader.Email = dbReader["Email"].ToString();
                            reader.RoleId = ((long)dbReader["RoleId"]);
                            //reader.Role = dbReader["RoleName"].ToString();
                        }

                        if (dbReader != null)
                        {
                            dbReader.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    result = false;
                }
            }

            return result;
        }

        public bool AddUser(Reader reader)
        {
            bool result = true;
            SqlDataReader dbReader = null;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
            {
                SqlCommand getMaxReaderIdCmd = new SqlCommand(
                        @"select (max(ReaderId) + 1) as 'newReaderId'
                          from Reader", connection);
                
                SqlCommand insertReaderValuesCmd = new SqlCommand(
                        @"set identity_insert Reader ON;
                          insert into Reader (ReaderId, FirstName, LastName, NickName, Email, Password)
                          values
                          (@readerId, @firstName, @lastName, @nickName, @email, @password);
                          set identity_insert Reader OFF;", connection);

                SqlCommand insertReaderRoleCmd = new SqlCommand(
                    @"insert into ReaderRoles (ReaderId, RoleId)
                      values
                      (@readerId, @roleId)", connection);
                
                try
                {
                    connection.Open();
                    dbReader = getMaxReaderIdCmd.ExecuteReader();

                    while (dbReader.Read())
                    {
                        reader.ReaderId = ((long)dbReader["newReaderId"]) + 1;
                    }

                    if (dbReader != null)
                    {
                        dbReader.Close();
                    }

                    insertReaderValuesCmd.Parameters.AddWithValue("@readerId", reader.ReaderId);
                    insertReaderValuesCmd.Parameters.AddWithValue("@firstName", reader.FirstName);
                    insertReaderValuesCmd.Parameters.AddWithValue("@lastName", reader.LastName);
                    insertReaderValuesCmd.Parameters.AddWithValue("@nickName", reader.NickName);
                    insertReaderValuesCmd.Parameters.AddWithValue("@email", reader.Email);
                    insertReaderValuesCmd.Parameters.AddWithValue("@password", reader.Password);

                    insertReaderRoleCmd.Parameters.AddWithValue("@readerId", reader.ReaderId);
                    insertReaderRoleCmd.Parameters.AddWithValue("@roleId", reader.RoleId);

                    insertReaderValuesCmd.ExecuteNonQuery();
                    insertReaderRoleCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    result = false;
                }
            }

            return result;
        }

        public bool UserIsInRole(int readerId, int roleId)
        {
            bool result = true;
            SqlDataReader dbReader = null;
            int resultRoleId = 0;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString))
            {
                SqlCommand getReaderRoleCmd = new SqlCommand(
                        @"select 1 as 'resultRoleId'
                          from ReaderRoles
                          where ReaderId = @readerId
                            and RoleId = @roleId", connection);

                getReaderRoleCmd.Parameters.AddWithValue("@readerId", readerId);
                getReaderRoleCmd.Parameters.AddWithValue("@roleId", roleId);

                try
                {
                    connection.Open();
                    dbReader = getReaderRoleCmd.ExecuteReader();

                    while (dbReader.Read())
                    {
                        resultRoleId = ((int)dbReader["resultRoleId"]);
                        result = resultRoleId > 0 ? true : false;
                    }

                    if (dbReader != null)
                    {
                        dbReader.Close();
                    }
                }
                catch (Exception ex)
                {
                    result = false;
                }
            }

            return result;
        }
    }
}

using Data.Interfaces;
using Data.Models.Accounts;
using Domain.Extensions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AccountManagers
{
    internal class AccountManager
    {
        #region Fields
        private IDatabase m_database;
        #endregion

        #region Properties

        #endregion

        #region Ctor

        public AccountManager(IDatabase database)
        {
            m_database = database;
        }

        #endregion

        #region Functions

        public Account Login(string login, string password, out string error)
        {        
            string current_pass = string.Empty;
            int current_id = -1;

            error = string.Empty;

            using (var con = m_database.Open())
            {
                using (var command = m_database.BuildCommand(con,
                    "SELECT Account_Id, Pass FROM TanksDb.Account a WHERE a.Login = @login",
                (config) => {
                    var parametrs = (config as MySqlParameterCollection);
                    parametrs.AddWithValue("@login", login);
                }))
                {
                    try
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            reader.Read();
                            current_id = reader.GetInt32(0);
                            current_pass = reader.GetString(1);
                        }                                                                      
                    }
                    catch (Exception ex)
                    {
                        error = "Incorrect Login or/and Password";
                    }                    
                }
            }

            bool passCorrect = SecurePasswordHasher.Verify(password, current_pass);

            if (!passCorrect)
            {
                error = "Incorrect Login or/and Password";
                return null;
            }

            // Find Account info in Case Login is Successfull

            Account account = new Account();

            using (var connection = m_database.Open())
            {
                using (var command = m_database.BuildCommand(connection,
                    "SELECT a.Account_Id, Login, Email, Surename, Name, Gender, Birthday, " +
                    "ur.RoleName FROM TanksDb.Account a" +
                    "JOIN TanksDb.Account_UserRole aur ON aur.Account_Id = a.Account_Id " +
                    "JOIN TanksDb.UserRole ur ON ur.UserRole_Id = aur.UserRole_Id " +
                    "WHERE a.Account_Id = @current_id", 
                    config => 
                    {
                        var parametrs = (config as MySqlParameterCollection);
                        parametrs.AddWithValue("@current_id", current_id);
                    }))
                {
                    try
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            reader.Read();
                            account.AccountId = reader.GetInt32(0);
                            account.Login = reader.GetString(1);
                            account.Password = current_pass;
                            account.Email = reader.GetString(2);
                            account.Surename = reader.GetString(3);
                            account.Name = reader.GetString(4);
                            account.Gender = reader.GetBoolean(5);
                            account.Birthday = reader.GetDateTime(6);
                            account.Role.RoleName = reader.GetString(7);
                        }
                    }
                    catch (Exception e)
                    {
                        error = e.Message;
                    }
                }
            }

            return account;
            
        }

        public bool Register(Account account)
        {
            return true;
        }

        public bool EmailExists(string email)
        {
            using (var con = m_database.Open())
            {
                using (var command = m_database.BuildCommand(con,
                    "SELECT * FROM TanksDb.Account a WHERE a.Email = @email",
                    config => 
                    {
                        var p = (config as MySqlParameterCollection);

                        p.AddWithValue("@email", email);
                    }))
                {
                    using (var r = command.ExecuteReader())
                    {
                        return r.FieldCount > 0;
                    }                                        
                }
            }
        }

        public bool LoginExists(string login)
        {
            using (var con = m_database.Open())
            {
                using (var command = m_database.BuildCommand(con,
                    "SELECT * FROM TanksDb.Account a WHERE a.Email = @email",
                    config =>
                    {
                        var p = (config as MySqlParameterCollection);

                        p.AddWithValue("@email", login);
                    }))
                {
                    using (var r = command.ExecuteReader())
                    {
                        return r.FieldCount > 0;
                    }
                }
            }
        }

        #endregion
    }
}

using Data.Interfaces;
using Data.Models.Accounts;
using Domain.Extensions;
using MySql.Data.MySqlClient;
using SPH = Domain.Extensions.SecurePasswordHasher;

namespace Domain.AccountManagers
{
    public class AccountManager
    {
        #region Fields
        private IDatabase m_database;

        private int m_iterations;
        #endregion

        #region Properties

        #endregion

        #region Ctor

        public AccountManager(IDatabase database)
        {
            m_iterations = 2000;

            m_database = database;
        }

        #endregion

        #region Functions

        public Account Login(string login, string password, out string error)
        {
            string current_pass = string.Empty;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                throw new ArgumentNullException("Arguments login or password were null!");
            
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

            bool passCorrect = SPH.Verify(password, current_pass);

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
                    "SELECT Login, Email, Surename, Name, Gender, Birthday, " +
                    "ur.RoleName FROM TanksDb.Account a " +
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
                            account.AccountId = current_id;
                            account.Login = reader.GetString(0);
                            account.Password = current_pass;
                            account.Email = reader.GetString(1);
                            account.Surename = reader.GetString(2);
                            account.Name = reader.GetString(3);
                            account.Gender = reader.GetBoolean(4);
                            account.Birthday = reader.GetDateTime(5);
                            account.Role.RoleName = reader.GetString(6);
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

        public bool Register(Account account, out string error)
        {
            error = string.Empty;

            if(account is null)
                throw new ArgumentNullException(nameof(account));

            //1 - Verify Login

            if (EmailExists(account.Email))
            {
                error = "Email already Exists, Please choose another one!";
                return false;
            }

            if (LoginExists(account.Login))
            {
                error = "Login already exists, Please choose another one!";
                return false;
            }

            //All Correct add New User to Database

            using (var con = m_database.Open())
            {
                var hashedPass = SPH.Hash(account.Password, m_iterations);

                using (var com = m_database.BuildCommand(con,
                    "INSERT INTO TanksDb.Account (Login, Pass, Email, Surename, Name, Gender, Birthday) VALUES" +
                    "(@login, @pass, @email, @surename, @name, @gender, @birth);",
                    config =>
                    {
                        var p = (config as MySqlParameterCollection);
                        p.AddWithValue("@login", account.Login);
                        p.AddWithValue("@pass", hashedPass);
                        p.AddWithValue("@email", account.Email);
                        p.AddWithValue("@surename", account.Surename);
                        p.AddWithValue("@name", account.Name);
                        p.AddWithValue("@gender", account.Gender);
                        p.AddWithValue("@birth", account.Birthday);

                    }
                    ))
                {
                    try
                    {
                        if (com.ExecuteNonQuery() <= 0)
                            throw new Exception("An Error occured on adding new Account to DataBase");
                    }
                    catch (Exception e)
                    {
                        error = e.Message;
                        return false;
                    }
                }

                using (var com = m_database.BuildCommand(con,
                    "INSERT INTO Account_UserRole (Account_Id, UserRole_Id) " +
                    "VALUES ((SELECT a.Account_Id " +
                    "FROM TanksDb.Account a " +
                    "WHERE a.Login = @login)," +
                    "(SELECT ur.UserRole_Id " +
                    "FROM TanksDb.UserRole ur WHERE ur.RoleName = 'User'))", config =>
                    {
                        var p = (config as MySqlParameterCollection);
                        p.AddWithValue("@login", account.Login);
                    }))
                {
                    try
                    {
                        if (com.ExecuteNonQuery() > 0)
                            return true;
                    }
                    catch (Exception e)
                    {
                        error = e.Message;
                        return false;
                    }
                }
            }

            return false;
        }

        public bool EmailExists(string email)
        {
            using (var con = m_database.Open())
            {
                using (var command = m_database.BuildCommand(con,
                    "SELECT COUNT(a.Email) FROM TanksDb.Account a WHERE a.Email = @email",
                    config => 
                    {
                        var p = (config as MySqlParameterCollection);

                        p.AddWithValue("@email", email);
                    }))
                {
                    using (var r = command.ExecuteReader())
                    {
                        r.Read();

                        return r.GetInt32(0) > 0;
                    }                                        
                }
            }
        }

        public bool LoginExists(string login)
        {
            using (var con = m_database.Open())
            {
                using (var command = m_database.BuildCommand(con,
                    "SELECT COUNT(a.Login) FROM TanksDb.Account a WHERE a.Login = @login",
                    config =>
                    {
                        var p = (config as MySqlParameterCollection);

                        p.AddWithValue("@login", login);
                    }))
                {
                    using (var r = command.ExecuteReader())
                    {
                        r.Read();

                        return r.GetInt32(0) > 0;
                    }
                }
            }
        }

        #endregion
    }
}

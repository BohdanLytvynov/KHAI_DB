using Data.Database;
using Data.Interfaces;
using Data.Models.Accounts;
using Data.Realizations;
using DB_Lab7.ViewModels.Pages;
using DB_Lab7.Views;
using DB_Lab7.Views.Pages;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Windows;
using ViewModelBaseLibDotNetCore.VM;
using CM = System.Configuration.ConfigurationManager;

namespace DB_Lab7.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region Pages
        LoginRegisterPage m_loginRegisterPage;

        AdminPage m_adminPage;

        MainPage m_mainPage;
        #endregion

        #region Fields

        private Account m_current;

        private Database m_database;

        private IDbConnectionBuilder m_dbConnectionBuilder;

        private ISQLCommandBuilder m_sqlCommandBuilder;

        private string m_title;

        private object m_frame;

        private string m_UserName;

        private string m_UserRole;
        #endregion

        #region Properties

        public object Frame { get => m_frame; set => Set(ref m_frame, value); }

        public string Title { get => m_title; set => Set(ref m_title, value); }

        public string UserName { get => m_UserName; set => Set(ref m_UserName, value); }

        public string UserRole { get => m_UserRole; set => Set(ref m_UserRole, value); }

        #endregion

        #region Ctor
        public MainWindowViewModel()
        {
            #region Field Init

            m_UserRole = string.Empty;

            m_UserName = string.Empty;

            m_current = new Account();

            m_title = "Lab Work 7";

            string conStr = null;
           
            conStr = GetConnectionString();
            
            m_dbConnectionBuilder = new MySqlConnectionBuilder();

            m_sqlCommandBuilder = new Data.Realizations.MySqlCommandBuilder();

            m_database = new Database(conStr, m_dbConnectionBuilder, m_sqlCommandBuilder);

            m_frame = new object();

            m_loginRegisterPage = new LoginRegisterPage(m_database);

            m_mainPage = new MainPage(m_database);

            m_adminPage = new AdminPage(m_database);

            m_loginRegisterPage.OnLoginFinished += OnLoginFinished;

            Frame = m_loginRegisterPage;

            #endregion        
        }
        #endregion

        #region Functions

        private void OnLoginFinished(object o, Account account)
        {
            if (account is not null)
            {
                m_current = account;
                UserName = account.Login;
                UserRole = account.Role.RoleName;

                if (account.Role.RoleName is null)
                    return;

                if (account.Role.RoleName.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    Frame = m_adminPage;
                }
                else
                    Frame = m_mainPage;
            }
        }

        protected virtual string GetConnectionString()
        {
            MySqlConnectionStringBuilder connectionStringBuilder = new();

            dynamic con = CM.GetSection("connectionString");

            if (con is null) throw new Exception("Error on getting connection string section!");

            string server = con["Server"];
            uint port = uint.Parse(con["Port"]);
            string user = con["User"];
            string password = con["Pass"];
            string db = con["Database"];

            connectionStringBuilder.Server = server;
            connectionStringBuilder.Port = port;
            connectionStringBuilder.UserID = user;
            connectionStringBuilder.Password = password;
            connectionStringBuilder.Database = db;

            return connectionStringBuilder.ConnectionString;
        }
        #endregion
    }
}

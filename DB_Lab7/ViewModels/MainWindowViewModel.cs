using Data.Database;
using Data.Interfaces;
using Data.Realizations;
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
        #region Windows
        LoginRegisterPage m_loginRegisterPage;
        #endregion

        #region Fields
        private Database database;

        private IDbConnectionBuilder m_dbConnectionBuilder;

        private string m_title;

        private Visibility m_MainVisibility;

        private object m_frame;
        #endregion

        #region Properties

        public object Frame { get => m_frame; set=> Set(ref m_frame, value); }

        public string Title { get => m_title; set => Set(ref m_title, value); }

        #endregion

        #region Ctor
        public MainWindowViewModel()
        {
            #region Field Init

            m_MainVisibility = Visibility.Hidden;

            m_title = "Lab Work 7";

            string conStr = GetConnectionString();

            m_dbConnectionBuilder = new MySqlConnectionBuilder();

            database = new Database(conStr, m_dbConnectionBuilder);

            m_frame = new object(); 

            m_loginRegisterPage = new LoginRegisterPage();

            Frame = m_loginRegisterPage;

            #endregion        
        }
        #endregion

        #region Functions
        protected string GetConnectionString()
        { 
            MySqlConnectionStringBuilder connectionStringBuilder = new();

            dynamic con = CM.GetSection("connectionString");

            if (con is null) throw new Exception("Error on getting connection string section!");

            try
            {
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
            }
            catch (Exception e)
            {
                throw;
            }
            
            return connectionStringBuilder.ConnectionString;
        }
        #endregion
    }
}

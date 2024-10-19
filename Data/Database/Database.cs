using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Database
{
    public class Database
    {
        #region Properties

        private string m_host;
        private string m_port;
        private string m_username;
        private string m_password;
        private string m_databaseName;

        #endregion

        #region Ctor

        public Database(string host, string port, string username, string password, string databaseName)
        {
            m_host = host;
            m_port = port;
            m_username = username;
            m_password = password;
            m_databaseName = databaseName;
        }

        #endregion

        #region Functions

        public IDbConnection Open()
        { 
            IDbConnection connection = null;

            return connection;
        }

        #region Static

        public static IDbCommand BuildCommand()
        { 
            
        }

        #endregion

        #endregion
    }
}

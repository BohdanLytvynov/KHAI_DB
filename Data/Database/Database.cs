using Data.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Database
{
    public class Database : IDatabase
    {
        #region Properties

        private string m_conStr;

        IDbConnectionBuilder m_connBuilder;

        ISQLCommandBuilder m_sqlCommandBuilder;

        #endregion

        #region Ctor

        public Database(string conStr, 
            IDbConnectionBuilder conBuilder,
            ISQLCommandBuilder sQLCommandBuilder)
        {
            if (string.IsNullOrEmpty(conStr))
                throw new ArgumentNullException(nameof(conStr));

            if(conBuilder is null)
                throw new ArgumentNullException(nameof(conBuilder));

            if (sQLCommandBuilder is null)
                throw new ArgumentNullException(nameof(sQLCommandBuilder));

            m_conStr = conStr;

            m_connBuilder = conBuilder;

            m_sqlCommandBuilder = sQLCommandBuilder;
        }
        
        #endregion

        #region Functions

        public IDbConnection Open()
        {
           var b = m_connBuilder.Buid(m_conStr);

           if(b.State == ConnectionState.Closed)
                b.Open();

           return b;
        }
        
        public IDbCommand BuildCommand(IDbConnection dbConnection, string sql,
            Action<IDataParameterCollection> configureParams)
        {
            if (dbConnection is null)
                throw new ArgumentNullException(nameof(dbConnection));

            if (sql is null)
                throw new ArgumentNullException(nameof(sql));

            if (string.IsNullOrEmpty(sql))
                throw new Exception("Sql command wasn't set!");

            IDbCommand SqlCommand = m_sqlCommandBuilder.Build();
            SqlCommand.Connection = dbConnection;
            SqlCommand.CommandText = sql;

            if (configureParams is not null)
                configureParams.Invoke(SqlCommand.Parameters);

            return SqlCommand;

        }

        #endregion
    }
}

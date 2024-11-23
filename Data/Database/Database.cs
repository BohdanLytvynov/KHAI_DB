using Data.Base;
using Data.Interfaces;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace Data.Database
{    
    #region Delegates

    public delegate void ParametersConfiguratorDelegate(DbParameterCollection paramsCollection, params (string, object)[] Parameters);

    #endregion

    public class Database : OnErrorHappened, IDatabase
    {      
        #region Events
        
        private ParametersConfiguratorDelegate m_ParametersConfigurator;

        #endregion

        #region Fields

        private bool m_useDataTableDiscovery;

        private string m_conStr;

        private IDbConnectionBuilder m_connBuilder;

        private ISQLCommandBuilder m_sqlCommandBuilder;

        private IDataAdapterBuilder m_dataAdapterBuilder;

        private IDataTableDiscoverer m_tableDiscoverer;

        protected ParametersConfiguratorDelegate ParametersConfigurator => m_ParametersConfigurator;

        #endregion
        
        #region Ctor

        public Database(string conStr, 
            IDbConnectionBuilder conBuilder,
            ISQLCommandBuilder sQLCommandBuilder,
            IDataAdapterBuilder dataAdapterBuilder,
            ParametersConfiguratorDelegate ParametersConfigurator,
            IDataTableDiscoverer? dataTableDiscoverer = null)
        {
            if (string.IsNullOrEmpty(conStr))
                throw new ArgumentNullException(nameof(conStr));

            if(conBuilder is null)
                throw new ArgumentNullException(nameof(conBuilder));

            if (sQLCommandBuilder is null)
                throw new ArgumentNullException(nameof(sQLCommandBuilder));
            
            if(dataAdapterBuilder is null)
                throw new ArgumentNullException(nameof(dataAdapterBuilder));

            if(ParametersConfigurator is null)
                throw new ArgumentNullException(nameof(ParametersConfigurator));

            if(string.IsNullOrEmpty(conStr))
                throw new ArgumentNullException(nameof(conStr));

            m_conStr = conStr;

            m_connBuilder = conBuilder;

            m_sqlCommandBuilder = sQLCommandBuilder;   
            
            m_dataAdapterBuilder = dataAdapterBuilder;

            m_ParametersConfigurator = ParametersConfigurator;

            m_tableDiscoverer = dataTableDiscoverer;

            m_useDataTableDiscovery = !(m_tableDiscoverer is null);
        }
        
        #endregion

        #region Functions

        public IDbConnection Open()
        {
           var b = m_connBuilder.Buid(m_conStr);

            if (b.State == ConnectionState.Closed)
            {
                try
                {
                    b.Open();
                }
                catch (Exception e)
                {
                    OnExceptionHappened?.Invoke(this, new DataBaseExceptionEventArgs(e));                            
                }                
            }
                

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

        public DbDataAdapter CreateDataAdapter()
        {
            return m_dataAdapterBuilder.Build();
        }

        public void ConfigureParameters(DbParameterCollection paramCollection, params (string, object)[] Parameters)
        {
            m_ParametersConfigurator.Invoke(paramCollection, Parameters);
        }

        #endregion
    }
}

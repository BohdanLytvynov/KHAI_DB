using Data.Interfaces;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace Data.Database
{
    public class DataBaseExceptionEventArgs : EventArgs
    {
        public Exception Exception { get; }

        public DataBaseExceptionEventArgs(Exception ex)
        {
            Exception = ex;
        }
    }

    #region Delegates

    public delegate void ParametersConfiguratorDelegate(DbParameterCollection paramsCollection, params (string, object)[] Parameters);

    #endregion

    public class Database : IDatabase
    {      
        #region Events

        private EventHandler<DataBaseExceptionEventArgs>? m_onExceptionHappened;

        private ParametersConfiguratorDelegate m_ParametersConfigurator;

        #endregion

        #region Properties

        private string m_conStr;

        IDbConnectionBuilder m_connBuilder;

        ISQLCommandBuilder m_sqlCommandBuilder;

        IDataAdapterBuilder m_dataAdapterBuilder;

        public EventHandler<DataBaseExceptionEventArgs>? OnExceptionHappened 
        { get => m_onExceptionHappened; set => m_onExceptionHappened = value; }

        protected ParametersConfiguratorDelegate ParametersConfigurator => m_ParametersConfigurator;

        #endregion

        #region Ctor

        public Database(string conStr, 
            IDbConnectionBuilder conBuilder,
            ISQLCommandBuilder sQLCommandBuilder,
            IDataAdapterBuilder dataAdapterBuilder,
            ParametersConfiguratorDelegate ParametersConfigurator)
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
                    m_onExceptionHappened?.Invoke(this, new DataBaseExceptionEventArgs(e));                            
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

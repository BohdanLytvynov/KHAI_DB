using Data.Base;
using Data.Interfaces;
using System.Data;
using System.Data.Common;

namespace Data.DataControllers
{
    public class DataController : OnErrorHappened, IDataController
    {
        #region Fields

        IDatabase m_database;

        #endregion

        #region Properties

        protected IDatabase Database { get => m_database; }

        #endregion

        #region Ctor

        public DataController(IDatabase db)
        {
            if(db is null)
                throw new ArgumentNullException(nameof(db));

            m_database = db;
        }

        #endregion

        #region Functions

        public DataTable ExecuteQueryCommand(string sqlCommand, params (string, object)[] Parametrs)
        {
            DataTable dt = new DataTable();
                        
            try
            {
                using (var con = m_database.Open())
                {
                    using (var command = m_database.BuildCommand(con,
                        sqlCommand,
                        (config) =>
                        {
                            if (Parametrs is not null)
                            {
                                m_database.ConfigureParameters((DbParameterCollection)config, Parametrs);
                            }
                        }))
                    {
                        using (DbDataAdapter da = m_database.CreateDataAdapter())
                        {
                            da.SelectCommand = (DbCommand)command;

                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                OnExceptionHappened?.Invoke(this, new DataBaseExceptionEventArgs(e));
            }
            
            return dt;
        }

        public int ExecuteCommand(string sqlCommand, params (string, object)[] Parametrs)
        {                       
            int rows = -1;

            try
            {
                using (var con = m_database.Open())
                {
                    using (var command = m_database.BuildCommand(con,
                        sqlCommand,
                        (config) =>
                        {
                            if (Parametrs is not null)
                            {
                                m_database.ConfigureParameters((DbParameterCollection)config, Parametrs);
                            }

                        }))
                    {
                        rows = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                OnExceptionHappened?.Invoke(this, new DataBaseExceptionEventArgs(e));
            }
            
            return rows;
        }

        #endregion        
    }

    public class DataController<TSqlCommandType> : DataController, IDataController<TSqlCommandType>
    {
        #region Fields
        
        Dictionary<TSqlCommandType, string> m_SqlCommandsStorage;

        #endregion

        #region Properties
        
        protected Dictionary<TSqlCommandType, string> SqlCommandsStorage { get => m_SqlCommandsStorage; }

        #endregion

        #region Ctor

        public DataController(IDatabase db) : base(db)
        {
            m_SqlCommandsStorage = new();
        }

        #endregion

        #region Functions

        public DataTable ExecuteQueryCommand(TSqlCommandType sqlCommandType, params (string, object)[] Parametrs)
        {
            DataTable dt = new DataTable();
            
            try
            {
                using (var con = Database.Open())
                {
                    using (var command = Database.BuildCommand(con,
                        m_SqlCommandsStorage[sqlCommandType],
                        (config) =>
                        {
                            if (Parametrs is not null)
                            {
                                Database.ConfigureParameters((DbParameterCollection)config, Parametrs);
                            }
                        }))
                    {
                        using (DbDataAdapter da = Database.CreateDataAdapter())
                        {
                            da.SelectCommand = (DbCommand)command;

                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                OnExceptionHappened?.Invoke(this, new DataBaseExceptionEventArgs(e));
            }
            
            return dt;
        }

        public int ExecuteCommand(TSqlCommandType sqlCommandType, params (string, object)[] Parametrs)
        {                        
            int rows = -1;

            try
            {
                using (var con = Database.Open())
                {
                    using (var command = Database.BuildCommand(con,
                        m_SqlCommandsStorage[sqlCommandType],
                        (config) =>
                        {
                            if (Parametrs is not null)
                            {
                                Database.ConfigureParameters((DbParameterCollection)config, Parametrs);
                            }

                        }))
                    {
                        rows = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                OnExceptionHappened?.Invoke(this, new DataBaseExceptionEventArgs(e));
            }           

            return rows;
        }

        public void RegisterSqlScript(params (TSqlCommandType, string)[] sqlScripts)
        {
            foreach (var script in sqlScripts)
            {
                m_SqlCommandsStorage.Add(script.Item1, script.Item2);
            }
        }

        #endregion        
    }
}

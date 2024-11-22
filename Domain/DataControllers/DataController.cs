using Data.Interfaces;
using Domain.Interfaces;
using Domain.OperationResults;
using System.Data;
using System.Data.Common;

namespace Domain.DataControllers
{
    public class DataController<TSqlCommandType> : IDataController<TSqlCommandType>
    {
        #region Fields

        IDatabase m_database;

        Dictionary<TSqlCommandType, string> m_SqlCommandsStorage;

        #endregion

        public DataController(IDatabase db)
        {
            m_database = db;

            m_SqlCommandsStorage = new();
        }

        public IOperResult<DataTable> ExecuteQueryCommand(TSqlCommandType sqlCommandType, params (string, object)[] Parametrs)
        {
            DataTable dt = new DataTable();

            Exception ex = null;

            IOperResult<DataTable> res = null;

            try
            {
                using (var con = m_database.Open())
                {
                    using (var command = m_database.BuildCommand(con,
                        m_SqlCommandsStorage[sqlCommandType],
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
                ex = e;
            }
            finally
            {
                res = new OperationResult<DataTable>(dt, ex);
            }

            return res;
        }

        public IOperResult<DataTable> ExecuteQueryCommand(string sqlCommand, params (string, object)[] Parametrs)
        {
            DataTable dt = new DataTable();

            Exception? ex = null;

            IOperResult<DataTable> res = null;

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
                ex = e;
            }
            finally
            {
                res = new OperationResult<DataTable>(dt, ex);
            }

            return res;
        }

        public IOperResult<int> ExecuteCommand(TSqlCommandType sqlCommandType, params (string, object)[] Parametrs)
        {
            DataTable dt = new DataTable();

            Exception? ex = null;

            IOperResult<int> res = null;

            int rows = -1;

            try
            {
                using (var con = m_database.Open())
                {
                    using (var command = m_database.BuildCommand(con,
                        m_SqlCommandsStorage[sqlCommandType],
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
                ex = e;
            }
            finally
            {
                res = new OperationResult<int>(rows, ex);
            }

            return res;
        }

        public IOperResult<int> ExecuteCommand(string sqlCommand, params (string, object)[] Parametrs)
        {
            DataTable dt = new DataTable();

            Exception ex = null;

            IOperResult<int> res = null;

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
                ex = e;
            }
            finally
            {
                res = new OperationResult<int>(rows, ex);
            }

            return res;
        }

        public void RegisterSqlScript(params (TSqlCommandType, string)[] sqlScripts)
        {
            foreach (var script in sqlScripts)
            {
                m_SqlCommandsStorage.Add(script.Item1, script.Item2);
            }
        }
    }
}

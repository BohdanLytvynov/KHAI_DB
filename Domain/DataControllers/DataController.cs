using Data.Interfaces;
using Data.Models.Tanks;
using Data.SqlScripts;
using Domain.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;
using ZstdSharp.Unsafe;

namespace Domain.DataControllers
{
    public enum SqlCommands : byte 
    {
        GetAll = 1
    }

    public class DataController : IDataController
    {
        #region Fields

        IDatabase m_database;

        Dictionary<string, SqlScript> m_SqlCommands;

        #endregion

        public DataController(IDatabase db)
        {
            m_database = db;

            m_SqlCommands = new();
        }

        public DataTable ExecuteQueryCommand(int UserId, SqlCommands sqlCommand)
        {
            DataTable dt = new DataTable();

            using (var con = m_database.Open())
            {
                using (var command = m_database.BuildCommand(con,
                    m_SqlCommands[sqlCommand.ToString()].CommandText,
                    (config) => 
                    { 
                        var param = m_SqlCommands[sqlCommand.ToString()].Params;

                        var comParams = (config as MySqlParameterCollection);

                        foreach (var p in param)
                        {
                            comParams.AddWithValue(p.Item1, p.Item2);
                        }
                    }))
                {
                    using (MySqlDataAdapter da = new MySqlDataAdapter(command as MySqlCommand))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public void RegisterSqlScript(params (string, SqlScript)[] sqlScripts)
        {
            foreach (var script in sqlScripts)
            {
                m_SqlCommands.Add(script.Item1, script.Item2);
            }
        }
    }
}

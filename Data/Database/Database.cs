using Data.Attributes.Tables;
using Data.Exceptions;
using Data.Interfaces;
using Data.Models.Base;
using System.Data;
using System.Reflection;
using RU = Data.Utilities.ReflexionUtility;

namespace Data.Database
{
    public enum ScriptAction : byte 
    {
        Create = 1, Read, Update, Delete
    }

    public struct ScriptDescription
    {
        public string Entity { get; }
        public ScriptAction Action { get; }

        public ScriptDescription(string entity, ScriptAction action)
        {
            Entity = entity;
            Action = action;
        }
    }

    public class Database : IDatabase
    {
        #region Properties

        public Dictionary<ScriptDescription, string> SqlScripts { get; set; }

        private string m_conStr;

        IDbConnectionBuilder m_connBuilder;

        ISQLCommandBuilder m_sqlCommandBuilder;

        ISqlScriptBuilder m_sqlScriptBuilder;

        #endregion

        #region Ctor

        public Database(string conStr, 
            IDbConnectionBuilder conBuilder,
            ISQLCommandBuilder sQLCommandBuilder,
            ISqlScriptBuilder sqlScriptBuilder)
        {
            if (string.IsNullOrEmpty(conStr))
                throw new ArgumentNullException(nameof(conStr));

            if(conBuilder is null)
                throw new ArgumentNullException(nameof(conBuilder));

            if (sQLCommandBuilder is null)
                throw new ArgumentNullException(nameof(sQLCommandBuilder));

            if(sqlScriptBuilder is null)
                throw new ArgumentNullException(nameof(sqlScriptBuilder));

            m_conStr = conStr;

            m_connBuilder = conBuilder;

            m_sqlCommandBuilder = sQLCommandBuilder;

            m_sqlScriptBuilder = sqlScriptBuilder;

            SqlScripts = new();

            BuildSqlScripts();
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

        private void BuildSqlScripts()
        {
            //Get Assembly
            var assembly = RU.GetAssembly(new Table("").GetType());

            //Get All model types
            var models = assembly.GetTypes().Where(t => t.BaseType.Name.Equals("DataBaseEntity"));

            if (models.Count() == 0)
                throw new FailToFindDataBaseModels();

            //Build SQL Scripts for each Entity
            foreach (var model in models)
            {
                var script = m_sqlScriptBuilder.BuildScript(model, models, ScriptAction.Read);

                SqlScripts.Add(new ScriptDescription(model.Name, ScriptAction.Read), script);
            }
        }

        #endregion
    }
}

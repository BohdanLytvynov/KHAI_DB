using Data.DataControllers;
using Data.Interfaces;
using System.Data;

namespace Data.Realizations
{
    public class MySQLDataTableDiscoverer : IDataTableDiscoverer
    {
        #region Fields

        private const string SQL_USE_DATABASE = "USE {0}";

        private const string SQL_DESCRIBE_DB = "SHOW TABLES";

        private const string SQL_ANALIZE_TABLE = "DESCRIBE {0}";

        private string m_databaseName;

        private string[] m_IgnoreList;

        private IDatabase m_Database;

        private IDataController m_DataController;

        #endregion

        #region Properties

        public string[] IgnoreTablesList { get => m_IgnoreList; set => m_IgnoreList = value; }

        #endregion

        #region Ctor

        public MySQLDataTableDiscoverer(IDatabase database, string databaseName, string[] IgnoreTablesList = default)
        {
            if (database is null)
                throw new ArgumentNullException(nameof(database));

            if(databaseName is null)
                throw new ArgumentNullException(nameof(databaseName));

            m_Database = database;
            
            m_databaseName = databaseName;

            m_DataController = new DataController(m_Database);

            m_IgnoreList = IgnoreTablesList;
        }

        #endregion

        #region Functions

        public DataTable DiscoverDataBase()
        {
            m_DataController.ExecuteCommand(string.Format(SQL_USE_DATABASE, m_databaseName));

            return m_DataController.ExecuteQueryCommand(SQL_DESCRIBE_DB);
        }
            
        public DataTable DiscoverTable(string tableName) =>                    
            m_DataController.ExecuteQueryCommand(string.Format(SQL_ANALIZE_TABLE, tableName));            
        
        public IEnumerable<string> GetTableFields(DataTable dataTable)
        {
            List<string> operResult = new();

            foreach (DataRow row in dataTable.Rows)
            {
                var v = row.Field<string>(0);

                if(!m_IgnoreList.Contains(v))
                    operResult.Add(v);
            }

            return operResult;  
        }

        #endregion
    }
}

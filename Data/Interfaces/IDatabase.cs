using Data.Database;
using System.Data;
using System.Data.Common;

namespace Data.Interfaces
{
    public interface IDatabase
    {        
        IDbConnection Open();

        IDbCommand BuildCommand(IDbConnection dbConnection, string sql,
            Action<IDataParameterCollection> configureParams = default);

        DbDataAdapter CreateDataAdapter();

        public void ConfigureParameters(DbParameterCollection paramCollection, params (string, object)[] Parameters);
    }
}

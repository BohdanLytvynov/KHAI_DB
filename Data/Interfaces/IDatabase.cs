using Data.Database;
using System.Data;

namespace Data.Interfaces
{
    public interface IDatabase
    {
        public EventHandler<DataBaseExceptionEventArgs>? OnExceptionHappened { get; set; }

        IDbConnection Open();

        IDbCommand BuildCommand(IDbConnection dbConnection, string sql,
            Action<IDataParameterCollection> configureParams = default);
    }
}

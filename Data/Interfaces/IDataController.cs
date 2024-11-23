using System.Data;

namespace Data.Interfaces
{
    public interface IDataController
    {
        DataTable ExecuteQueryCommand(string sqlCommand, params (string, object)[] Parametrs);

        int ExecuteCommand(string sqlCommand, params (string, object)[] Parametrs);
    }

    public interface IDataController<TSqlCommandType> : IDataController
    {
        void RegisterSqlScript(params (TSqlCommandType, string)[] sqlScripts);

        DataTable ExecuteQueryCommand(TSqlCommandType sqlCommandType, params (string, object)[] Parametrs);
        
        int ExecuteCommand(TSqlCommandType sqlCommandType, params (string, object)[] Parametrs);        
    }
}

using System.Data;

namespace Domain.Interfaces
{
    public interface IDataController<TSqlCommandType>
    {
        void RegisterSqlScript(params (TSqlCommandType, string)[] sqlScripts);

        IOperResult<DataTable> ExecuteQueryCommand(TSqlCommandType sqlCommandType, params (string, object)[] Parametrs);

        IOperResult<DataTable> ExecuteQueryCommand(string sqlCommand, params (string, object)[] Parametrs);

        IOperResult<int> ExecuteCommand(TSqlCommandType sqlCommandType, params (string, object)[] Parametrs);

        IOperResult<int> ExecuteCommand(string sqlCommand, params (string, object)[] Parametrs);
    }
}

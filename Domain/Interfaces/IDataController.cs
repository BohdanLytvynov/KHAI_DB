using System.Data;

namespace Domain.Interfaces
{
    public interface IDataController<TSqlCommandType>
    {
        void RegisterSqlScript(params (TSqlCommandType, string)[] sqlScripts);

        IOperResult<DataTable> ExecuteQueryCommand(TSqlCommandType sqlCommand, params (string, object)[] Parametrs);

        IOperResult<int> ExecuteCommand(TSqlCommandType sqlCommand, params (string, object)[] Parametrs);
    }
}

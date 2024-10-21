using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{    
    public interface IDatabase
    {
        IDbConnection Open();

        IDbCommand BuildCommand(IDbConnection dbConnection, string sql,
            Action<IDataParameterCollection> configureParams);
    }
}

using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Realizations
{
    public class MySqlConnectionBuilder : IDbConnectionBuilder
    {
        public IDbConnection Buid(string conString)
        {
            throw new NotImplementedException();
        }
    }
}

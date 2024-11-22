using Data.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Realizations
{
    public class MySqlDataAdapterBuilder : IDataAdapterBuilder
    {
        public DbDataAdapter Build()
        {
            return new MySqlDataAdapter();
        }
    }
}

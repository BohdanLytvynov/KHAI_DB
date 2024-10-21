using Data.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Realizations
{
    public class MySqlCommandBuilder : ISQLCommandBuilder
    {
        public IDbCommand Build()
        {
            return new MySqlCommand();
        }
    }
}

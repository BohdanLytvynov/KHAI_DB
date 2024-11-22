using Data.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;

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

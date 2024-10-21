using Data.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;

namespace Data.Realizations
{
    public class MySqlConnectionBuilder : IDbConnectionBuilder
    {
        public IDbConnection Buid(string conString)
        {
            return new MySqlConnection(conString);      
        }
    }
}

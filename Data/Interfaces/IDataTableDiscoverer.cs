using System.Data;

namespace Data.Interfaces
{
    public interface IDataTableDiscoverer
    {
        public string[] IgnoreTablesList { get; set; }

        DataTable DiscoverTable(string tableName);

        DataTable DiscoverDataBase();
    }
}

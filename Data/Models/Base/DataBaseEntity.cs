using Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Base
{
    public class DataBaseEntity<TKey> : IDataBaseEntity<TKey>
    {
        public TKey Entity_Id { get; set; }
    }
}

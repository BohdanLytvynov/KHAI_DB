using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Interfaces
{
    public interface IDataBaseEntity<TKey>
    {
        public TKey Entity_Id { get; set; }
    }
}

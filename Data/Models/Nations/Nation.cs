using Data.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Nations
{
    public class Nation : DataBaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
    }
}

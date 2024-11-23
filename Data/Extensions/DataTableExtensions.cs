using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Extensions
{
    public static class DataTableExtensions 
    {
        public static bool IsEmpty(this DataTable table)
        { 
            return table.Rows.Count == 0;
        }
    }
}

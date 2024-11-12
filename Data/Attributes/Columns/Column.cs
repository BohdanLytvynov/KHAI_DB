using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Attributes.Columns
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Column : Attribute
    {
        #region Fields
        public string ColumnName { get; set; }
        #endregion

        #region ctor
        public Column(string columnName)
        {
            ColumnName = columnName;
        }
        #endregion
    }
}

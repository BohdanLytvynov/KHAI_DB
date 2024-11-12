using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Attributes.Bindings
{
    [AttributeUsage(AttributeTargets.Property)]
    public class OneToMany : Attribute
    {
        #region Properties

        public string AdjTable { get; set; }

        public string ForeignKey { get; set; }        

        #endregion
    }
}

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
        
        public string ForeignKey { get; set; }

        #endregion

        #region Ctor
        public OneToMany(string foreignKey)
        {
            ForeignKey = foreignKey;
        }
        #endregion
    }
}

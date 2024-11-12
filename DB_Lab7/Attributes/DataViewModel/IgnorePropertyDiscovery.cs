using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Lab7.Attributes.DataViewModel
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class IgnorePropertyDiscovery : Attribute
    {
       
    }
}

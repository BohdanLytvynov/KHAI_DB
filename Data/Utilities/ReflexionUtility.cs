using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data.Utilities
{
    public static class ReflexionUtility
    {
        /// <summary>
        /// Gets Assembly where type is Present 
        /// If type is null gets current assembly
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Assembly GetAssembly(Type type = null)
        {
            if (type is null)
                return Assembly.GetExecutingAssembly();
            else
                return Assembly.GetAssembly(type);
        }
    }
}

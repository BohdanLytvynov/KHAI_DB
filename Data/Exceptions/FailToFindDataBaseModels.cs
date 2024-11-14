using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Exceptions
{
    internal class FailToFindDataBaseModels : Exception
    {
        public FailToFindDataBaseModels() : 
            base("Fail to find some DataBase Models!")
        {
            
        }
    }
}

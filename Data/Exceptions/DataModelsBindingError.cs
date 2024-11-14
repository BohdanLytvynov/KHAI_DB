using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Exceptions
{
    public class DataModelsBindingError : Exception
    {
        public DataModelsBindingError(string error) : base(error) 
        {
            
        }
    }
}

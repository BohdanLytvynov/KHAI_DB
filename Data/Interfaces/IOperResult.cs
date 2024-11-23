using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IOperResult
    {
        public bool HasError { get; }

        public Exception Error { get; }
    }

    public interface IOperResult<TResult> : IOperResult
    {
        public TResult Result { get; }        
    } 
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IOperResult<TResult>
    {
        public TResult Result { get; }

        public bool HasError { get; }

        public Exception Error { get; }
    }
}

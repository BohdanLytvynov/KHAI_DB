using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OperationResults
{
    internal class OperationResult<TResult> : IOperResult<TResult>
    {
        public TResult Result { get; }

        public bool HasError { get; }

        public Exception Error { get; }

        public OperationResult(TResult result, Exception error)
        {
            Result = result;
            HasError = error is null? false : true;
            Error = error;
        }

        public OperationResult(TResult result) : this(result, null)
        {
            
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Base
{
    public class DataBaseExceptionEventArgs : EventArgs
    {
        public Exception Exception { get; }

        public DataBaseExceptionEventArgs(Exception ex)
        {
            Exception = ex;
        }
    }

    public abstract class OnErrorHappened
    {
        private EventHandler<DataBaseExceptionEventArgs>? m_onExceptionHappened;

        #region Properties

        public EventHandler<DataBaseExceptionEventArgs>? OnExceptionHappened
        { get => m_onExceptionHappened; set => m_onExceptionHappened = value; }

        #endregion
    }
}

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;


namespace ViewModelBaseLibDotNetCore.VM
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDataErrorInfo
    {
        private Dispatcher? m_dispatcher;

        private bool [] m_validArray;

        public virtual Dispatcher Dispatcher { set => m_dispatcher = value; }

        public virtual string Error => throw new NotImplementedException();

        public virtual string this[string columnName] => throw new NotImplementedException();

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string PropName)
        { 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropName));
        }

        protected bool SetIfNull<T>(ref T field, T value, [CallerMemberName] string PropName = null)
        {            
            if (field is not null && field.Equals(value))
            {
                return false;
            }
            else
            {
                field = value;

                OnPropertyChanged(PropName);

                return true;
            }
        }

        protected bool Set<T>(ref T field, T value, [CallerMemberName] string PropName = null)
        {            
            if (field == null)
            { 
                throw new ArgumentNullException(string.Format("Property: {0}", PropName));
            }

            if (field.Equals(value))
            {
                return false;
            }
            else
            {
                field = value;

                OnPropertyChanged(PropName);

                return true;
            }
        }

        protected virtual void QueueWorkToDispatcher(Action work)
        {
            if (m_dispatcher is null)
                throw new Exception("Dispatcher is not initialized!");

            m_dispatcher?.Invoke(work);
        }

        protected virtual void InitValidArray(int count)
        { 
            m_validArray = new bool[count];
        }

        protected virtual bool Validate(int startInsex, int endIndex)
        {
            for(int i = startInsex; i <= endIndex; ++i)            
                if (!m_validArray[i])
                    return false;                            

            return true;
        }

        protected virtual void SetValidArray(int index, bool value)
        { 
            m_validArray[index] = value;
        }
    }
}

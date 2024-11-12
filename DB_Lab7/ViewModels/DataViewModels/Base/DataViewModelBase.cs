using DB_Lab7.Attributes.DataViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Lab7.ViewModels.DataViewModels.Base
{
    internal class DataViewModelBase<TId> : ViewModelBaseLibDotNetCore.VM.ViewModelBase
    {
        public TId Id { get; set; }

        [IgnorePropertyDiscovery]
        public uint Number { get; set; }

        public DataViewModelBase(TId Id)
        {
            this.Id = Id;
        }
    }
}

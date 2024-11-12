using DB_Lab7.ViewModels.DataViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Lab7.ViewModels.DataViewModels
{
    internal class Nation_ViewModel : DataViewModelBase<int>
    {
        #region Fields

        private string m_Name;

        #endregion

        #region Properties

        public string Name { get => m_Name; set => Set(ref m_Name, value); }

        #endregion

        #region Ctor
        public Nation_ViewModel(int id) : base(id) 
        {
            
        }
        #endregion
    }
}

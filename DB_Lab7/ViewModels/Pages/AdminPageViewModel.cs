using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBaseLibDotNetCore.VM;

namespace DB_Lab7.ViewModels.Pages
{
    internal class AdminPageViewModel : ViewModelBase
    {
        #region Fields
        IDatabase m_database;
        #endregion

        #region Properties

        #endregion

        #region Ctor
        public AdminPageViewModel(IDatabase database)
        {
            m_database = database;  
        }
        #endregion

        #region Functions

        #endregion
    }
}

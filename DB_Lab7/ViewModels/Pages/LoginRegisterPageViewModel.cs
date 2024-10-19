using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBaseLibDotNetCore.VM;

namespace DB_Lab7.ViewModels.Pages
{
    internal class LoginRegisterPageViewModel : ViewModelBase
    {
        #region Fields
        private string m_title;
        #endregion

        #region Properties
        public string Title { get => m_title; set => Set(ref m_title, value); }
        #endregion

        #region Ctor
        public LoginRegisterPageViewModel()
        {
            m_title = "Login/Register";
        }
        #endregion

        #region Functions

        #endregion
    }
}

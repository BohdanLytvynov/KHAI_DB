using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModelBaseLibDotNetCore.VM;

namespace DB_Lab7.ViewModels.Pages
{
    internal class MainPageViewModel : ViewModelBase
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Commands

        public ICommand OnGetTanksButtonPressed { get;}

        #endregion

        #region Ctor
        public MainPageViewModel(IDatabase database)
        {
            
        }
        #endregion

        #region Functions

        #region On Get Tanks Button Pressed

        private bool CanOnGetTanksButtonPressedExecute(object p) => true;

        private void OnGetTanksButtonPressedExecute(object p)
        { 
            
        }

        #endregion

        #endregion
    }
}

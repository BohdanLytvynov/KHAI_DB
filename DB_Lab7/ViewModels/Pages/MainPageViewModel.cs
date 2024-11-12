using Data.Interfaces;
using DB_Lab7.ViewModels.DataViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<Tank_ViewModel> m_tanks;

        private int m_SelectedTankIndex;
        #endregion

        #region Properties
        public ObservableCollection<Tank_ViewModel> Tanks { get => m_tanks; set => m_tanks = value; }

        public int SelectedTankIndex { get=> m_SelectedTankIndex; set => Set(ref m_SelectedTankIndex, value); }
        #endregion

        #region Commands

        public ICommand OnGetTanksButtonPressed { get;}

        #endregion

        #region Ctor
        public MainPageViewModel(IDatabase database)
        {
            #region Init Fields
            m_tanks = new();

            m_SelectedTankIndex = -1;
            #endregion

            #region Init Commands

            #endregion
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

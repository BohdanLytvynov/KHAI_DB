using Data.Enums;
using Data.Interfaces;
using System.Windows.Input;
using ViewModelBaseLibDotNetCore.VM;

namespace DB_Lab7.ViewModels.Pages
{
    internal class AdminPageViewModel : ViewModelBase
    {
        #region Fields
        IDatabase m_database;

        EntityType m_entityType;

        #endregion

        #region Properties
        public EntityType EntityType { get => m_entityType; set => Set(ref m_entityType, value); }
        #endregion

        public ICommand OnGetAllButtonPressed { get; }

        public ICommand OnEditButtonPressed { get; }

        public ICommand OnDeleteButtonPressed { get; }

        public ICommand OnSearchButtonPressed { get; }

        #region Ctor
        public AdminPageViewModel(IDatabase database)
        {
            #region Init Fields

            m_database = database;

            #endregion

        }
        #endregion

        #region Functions

        #endregion
    }
}

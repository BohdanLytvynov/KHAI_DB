using Data.Interfaces;
using Data.Models.Accounts;
using DB_Lab7.ViewModels.Pages;
using System.Security;
using System.Windows;
using System.Windows.Controls;

namespace DB_Lab7.Views.Pages
{
    /// <summary>
    /// Interaction logic for LoginRegisterPage.xaml
    /// </summary>
    public partial class LoginRegisterPage : Page
    {
        #region Events

        public EventHandler<Account> OnLoginFinished { get=> m_viewModel.OnLoginFinished; 
            set => m_viewModel.OnLoginFinished = value; }
        
        #endregion

        LoginRegisterPageViewModel m_viewModel;

        public LoginRegisterPage(IDatabase database)
        {
            m_viewModel = new LoginRegisterPageViewModel(database);

            InitializeComponent();   
            
            this.DataContext = m_viewModel;

            Login_PasswordChanged += m_viewModel.OnLoginPasswordChanged;

            Register_Password1Changed += m_viewModel.OnRegisterPassword1Changed;

            Register_Password2Changed += m_viewModel.OnRegisterPassword2Changed;
        }

        #region Events

        public EventHandler<SecureString> Login_PasswordChanged;

        public EventHandler<SecureString> Register_Password1Changed;

        public EventHandler<SecureString> Register_Password2Changed;

        #endregion

        private void pass1_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Login_PasswordChanged?.Invoke(sender, pass1.SecurePassword);
        }

        private void pass2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Register_Password1Changed?.Invoke(sender, pass2.SecurePassword);
        }

        private void pass3_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Register_Password2Changed?.Invoke(sender, pass3.SecurePassword);
        }
    }
}

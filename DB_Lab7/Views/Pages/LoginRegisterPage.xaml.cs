using DB_Lab7.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DB_Lab7.Views.Pages
{
    /// <summary>
    /// Interaction logic for LoginRegisterPage.xaml
    /// </summary>
    public partial class LoginRegisterPage : Page
    {
        LoginRegisterPageViewModel m_viewModel;

        public LoginRegisterPage()
        {
            m_viewModel = new LoginRegisterPageViewModel();

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

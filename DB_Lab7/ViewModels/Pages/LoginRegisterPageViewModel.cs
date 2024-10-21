using SecureStringExtensions_DotNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ViewModelBaseLibDotNetCore.Commands;
using ViewModelBaseLibDotNetCore.VM;

namespace DB_Lab7.ViewModels.Pages
{
    internal class LoginRegisterPageViewModel : ViewModelBase
    {
        #region Fields
        private string m_title;
        
        #region Login

        private string m_login;

        private SecureString m_password;

        #endregion

        #region Register

        private string m_login_register;

        private SecureString m_password2;

        private string m_name;

        private string m_surename;

        private string m_email;

        private int m_gender;

        private DateTime m_birthday;

        #endregion

        #endregion

        #region Properties
        public string Title { get => m_title; set => Set(ref m_title, value); }

        #region Login

        public string Login { get => m_login; set => Set(ref m_login, value); }

        #endregion

        #region Register

        public string LoginRegister { get => m_login_register; set => Set(ref m_login_register, value); }

        public string Name { get => m_name; set => Set(ref m_name, value); }

        public string Surename { get => m_surename; set => Set(ref m_surename, value); }

        public string Email { get => m_email; set => Set(ref m_email, value); }

        public int Gender { get => m_gender; set => Set(ref m_gender, value); }

        public DateTime Birthday { get => m_birthday; set => Set(ref m_birthday, value); }

        #endregion

        #endregion

        #region Commands

        public ICommand OnLoginButtonPressed { get; }

        #endregion

        #region Ctor
        public LoginRegisterPageViewModel()
        {
            #region Init Fields

            m_password = new();

            m_password2 = new();

            m_title = "Login/Register";

            m_login = string.Empty;

            m_name = string.Empty;

            m_surename = string.Empty;

            m_email = string.Empty;

            m_gender = 0;

            #endregion

            #region Init Commands

            OnLoginButtonPressed = new Command
                (
                    OnLoginButtonPressedExecute,
                    CanOnLoginButtonPressedExecute
                );

            #endregion

        }
        #endregion

        #region Functions

        #region Secure String Bindings

        public void OnLoginPasswordChanged(object o, SecureString s)
        {
            m_password = s;        
        }

        public void OnRegisterPassword1Changed(object o, SecureString s)
        { 
            m_password = s;
        }

        public void OnRegisterPassword2Changed(object o, SecureString s)
        {
            m_password2 = s;
        }

        #endregion

        #region On Login Button Pressed

        private bool CanOnLoginButtonPressedExecute(object p)
        {
            return !string.IsNullOrEmpty(m_login) && m_password?.Length > 0;
        }

        private void OnLoginButtonPressedExecute(object p)
        {
            MessageBox.Show($"Login: {m_login}, Password: {m_password.GetString()}", "", 
                MessageBoxButton.OK);
        }

        #endregion

        #endregion
    }
}

using Data.Interfaces;
using Data.Models;
using Data.Models.Accounts;
using Domain.AccountManagers;
using SecureStringExtensions_DotNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security;
using System.Security.Cryptography.Xml;
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
        #region Events

        public EventHandler<Account> OnLoginFinished;
 
        #endregion

        #region Fields
        private string m_title;

        AccountManager m_accountManager;

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

        public ICommand OnRegisterButtonPressed { get; }

        public ICommand OnClearButtonPressed { get; }

        #endregion

        #region Ctor
        public LoginRegisterPageViewModel(IDatabase database)
        {
            #region Init Fields

            m_accountManager = new AccountManager(database);

            m_password = new();

            m_password2 = new();

            m_title = "Login/Register";

            m_login = string.Empty;

            m_login_register = string.Empty;

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

            OnRegisterButtonPressed = new Command
                (
                    OnRegisterButtonPressedExecute,
                    CanOnRegisterButtonPressedExecute
                );

            OnClearButtonPressed = new Command
                (
                    OnClearButtonPressedExecute,
                    CanOnClearButtonPressedExecute
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
            string error = string.Empty;

            Account? account = m_accountManager.Login(m_login, m_password.GetString(), out error);

            MessageBox.Show(string.IsNullOrEmpty(error)? "Login Successful!" : error, "Login/Register",
                MessageBoxButton.OK, string.IsNullOrEmpty(error)? MessageBoxImage.Information : MessageBoxImage.Error);

            OnLoginFinished?.Invoke(this, account);
        }

        #endregion

        #region On Register Button Pressed Execute

        private bool CanOnRegisterButtonPressedExecute(object p)
        {
            return true;
        }

        private void OnRegisterButtonPressedExecute(object p)
        {
            Role role = new Role() { Id = -1, RoleName = "User" };

            Account account = new Account(-1, LoginRegister, m_password2.GetString(), Name, Surename, Email, 
                Gender == 0? true : false, Birthday, role);

            string error = string.Empty;

            var r = m_accountManager.Register(account, out error);         

            MessageBox.Show(string.IsNullOrEmpty(error) ? "Registration Completed. Now you can login." : $"Error on register atempt! Error: {error}!", "Login/Register",
                    MessageBoxButton.OK, string.IsNullOrEmpty(error) ? MessageBoxImage.Information : MessageBoxImage.Error);            
        }

        #endregion

        #region On Clear Button Pressed

        private bool CanOnClearButtonPressedExecute(object p)
        {
            return true;
        }

        private void OnClearButtonPressedExecute(object p)
        { 
            LoginRegister = string.Empty;
            Name = string.Empty;
            Surename = string.Empty;
            Email = string.Empty;
            Gender = 0;
            Birthday = DateTime.Now;
        }

        #endregion

        #endregion
    }
}

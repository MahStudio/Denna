using Core.Data;
using Core.Service.Users;
using Core.Utils;
using Denna.Classes;
using Denna.Views;
using Microsoft.AppCenter.Analytics;
using System;
using System.ComponentModel;

namespace Denna.ViewModels
{
    public class SignInViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public MyCommand SignInCommand
        {
            get;
            set;
        }
        public MyCommand SignUpCommand
        {
            get;
            set;
        }

        UserService _usrsvc;

        public SignInViewModel()
        {
            SignInCommand = new MyCommand();
            SignInCommand.CanExecuteFunc = obj => true;
            SignInCommand.ExecuteFunc = SignIn;
            SignUpCommand = new MyCommand();
            SignUpCommand.CanExecuteFunc = obj => true;
            SignUpCommand.ExecuteFunc = SignUp;
            _usrsvc = new UserService();
        }

        void SignUp(object obj) => Welcome.current.opensignup();

        async void SignIn(object obj)
        {
            try
            {
                if (username == null && password == null)
                    throw new Exception("Please fill blank fields");

                IsLogging = true;
                await _usrsvc.Login(UserName.Replace(" ", ""), Password);
                Analytics.TrackEvent("User signed in");
                IsLogging = false;
                Welcome.current.Frame.Navigate(typeof(PageMaster));
            }
            catch (Exception ex)
            {
                IsLogging = false;
                "Something went wrong".ShowMessage(ex.Message);
            }

        }

        string username, password;
        bool islogging;
        public string UserName
        {
            get
            {
                return username;
            }
            set
            {
                if (username != value)
                {
                    username = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("UserName"));
                    }
                }
            }
        }

        public bool IsLogging
        {
            set
            {
                islogging = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsLogging"));

            }
            get { return islogging; }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (password != value)
                {
                    password = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("Password"));
                    }
                }
            }
        }
    }
}
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
        public SignInViewModel()
        {
            SignInCommand = new MyCommand();
            SignInCommand.CanExecuteFunc = obj => true;
            SignInCommand.ExecuteFunc = SignIn;
            SignUpCommand = new MyCommand();
            SignUpCommand.CanExecuteFunc = obj => true;
            SignUpCommand.ExecuteFunc = SignUp;
        }

        void SignUp(object obj) => Welcome.current.opensignup();

        async void SignIn(object obj)
        {
            try
            {
                await UserService.Login(UserName, Password);
                Analytics.TrackEvent("User signed in");
                Welcome.current.Frame.Navigate(typeof(PageMaster));
            }
            catch (Exception ex)
            {
                "SomethingwentWrong".ShowMessage(ex.Message);
            }
            
        }

        string username, password;
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
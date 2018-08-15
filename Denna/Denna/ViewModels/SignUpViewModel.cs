using Core.Service.Users;
using Denna.Classes;
using Core.Utils;
using Denna.Views;
using Microsoft.AppCenter.Analytics;
using System;
using System.ComponentModel;

namespace Denna.ViewModels
{
    class SignUpViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string username, password, rpassword, email, _name, name, filename;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("Name"));
                    }
                }
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if (email != value)
                {
                    email = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("Email"));
                    }
                }
            }
        }
        public string RPassword
        {
            get
            {
                return rpassword;
            }
            set
            {
                if (rpassword != value)
                {
                    rpassword = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("RPassword"));
                    }
                }
            }
        }
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
        public SignUpViewModel()
        {
            SignInCommand = new MyCommand();
            SignInCommand.CanExecuteFunc = obj => true;
            SignInCommand.ExecuteFunc = SignIn;
            SignUpCommand = new MyCommand();
            SignUpCommand.CanExecuteFunc = obj => true;
            SignUpCommand.ExecuteFunc = SignUp;
        }
        async void SignUp(object obj)
        {
            try
            {
                if (Password != RPassword)
                {
                    "Retype password".ShowMessage("Passwords not maching");
                    return;
                }

                await UserService.Register(UserName, Password, Name, Email);
                Analytics.TrackEvent("User signed up");
                Welcome.current.Frame.Navigate(typeof(PageMaster));
            }
            catch (Exception ex)
            {
                "SomethingwentWrong".ShowMessage(ex.Message);
            }

        }

        void SignIn(object obj) => Welcome.current.opensignin();
    }
}
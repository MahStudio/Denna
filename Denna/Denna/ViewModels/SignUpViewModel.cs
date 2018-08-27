using Core.Service.Users;
using Denna.Classes;
using Core.Utils;
using Denna.Views;
using Microsoft.AppCenter.Analytics;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Denna.ViewModels
{
    class SignUpViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        UserService _usrsvc;
        string username, password, rpassword, email, _name;
        bool issigning = false;
        public bool IsSigning
        {
            set
            {
                issigning = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsSigning"));
            }
            get
            {
                return issigning;

            }
        }
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
            Name = AppSettings.Get<string>("Username");
            _usrsvc = new UserService();
        }
        async void SignUp(object obj)
        {
            try
            {
                var emailrgx = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.Compiled);
                var usrrgx = new Regex(@"^(?=[a-zA-Z])[-\w.]{0,23}([a-zA-Z\d]|(?<![-.])_)$", RegexOptions.Compiled);

                if (Password != RPassword)
                {
                    "Retype password".ShowMessage("Passwords not maching");
                    return;
                }
                if (Password.Length < 6)
                {
                    "Passwork too short".ShowMessage("Passwords must have more than 6 chars");
                    return;
                }
                if (!emailrgx.IsMatch(Email))
                {
                    "Email not vaid".ShowMessage("make sure you enter a valid email");
                    return;
                }
                if (!usrrgx.IsMatch(UserName))
                {
                    "Username not valid".ShowMessage("Username must be less than 24 chars, can include numbers, alphabets, _ and -");
                    return;
                }
                IsSigning = true;
                await _usrsvc.Register(UserName, Password, Name, Email);

                Analytics.TrackEvent("User signed up");
                IsSigning = false;
                Welcome.current.Frame.Navigate(typeof(PageMaster));

            }
            catch (Exception ex)
            {
                IsSigning = false;
                "Something went wrong".ShowMessage(ex.Message);
            }

        }

        void SignIn(object obj) => Welcome.current.opensignin();
    }
}
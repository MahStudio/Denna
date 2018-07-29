using Denna.Classes;
using Denna.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;
using Core.Service.Users;
using Core.Data;

namespace Denna.ViewModels
{
    class SignUpViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _username;
        private string _password;
        private string _rpassword;
        private string _email;
        private string _name;
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
                return _email;

            }
            set
            {

                if (_email != value)
                {
                    _email = value;
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
                return _rpassword;

            }
            set
            {

                if (_rpassword != value)
                {
                    _rpassword = value;
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
                return _username;

            }
            set
            {

                if (_username != value)
                {
                    _username = value;
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
                return _password;

            }
            set
            {

                if (_password != value)
                {
                    _password = value;
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



        private string name;
        private string filename;
        private async void SignUp(object obj)
        {
            if (Password != RPassword)
            {
                "Retype password".ShowMessage("Passwords not maching");
                return;
            }

            await UserService.Register(UserName, Password,Name,Email);
            Welcome.current.Frame.Navigate(typeof(PageMaster));
        }

        private void SignIn(object obj)
        {
            Welcome.current.opensignin();
        }
    }
}

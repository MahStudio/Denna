using Denna.Classes;
using Denna.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private void SignUp(object obj)
        {
            Welcome.current.opensignup();
        }
        private void SignIn(object obj)
        {
            
            Welcome.current.Frame.Navigate(typeof(PageMaster));
        }
        private string _username;
        private string _password;
        public string UserName
        {
            get
            {
                return _username;

            } set
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

    }
}

using Core.Utils;
using Denna.Classes;
using System;
using System.ComponentModel;

namespace Denna.ViewModels
{
    class UnitTestViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public MyCommand Click
        {
            get;
            set;
        }
        public UnitTestViewModel()
        {
            Click = new MyCommand();
            Click.CanExecuteFunc = obj => true;
            Click.ExecuteFunc = SignIn;
        }

        void SignIn(object obj)
        {

        }

        double per = 10;
        int value = 2;
        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("Value"));
                    }
                }
            }
        }
        public double Per
        {
            get => per;
            set
            {
                if (per != value)
                {
                    per = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("Per"));
                    }
                }
            }
        }
    }
}
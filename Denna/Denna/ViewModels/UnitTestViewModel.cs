using Denna.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private void SignIn(object obj)
        {
            Value++;
            var a = new Core.Service.TestSvc();
            a.addsth();
            Per = Per + 1;
        }
        private Double _per = 10;
        private int _value = 2;
        public int Value
        {
            get
            {
                return _value;

            }
            set
            {

                if (_value != value)
                {
                    _value = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("Value"));
                    }
                }
            }
        }
        public Double Per
        {
            get
            {
                return _per;

            }
            set
            {

                if (_per != value)
                {
                    _per = value;
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

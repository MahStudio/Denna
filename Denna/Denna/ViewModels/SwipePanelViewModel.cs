using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
namespace Denna.ViewModels
{
    class SwipePanelViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _greet;
        private string _todate = DateTime.Now.ToLocalTime().ToString("D");
        private string _picture= "ms-appx:///Assets/Mockups/usrimg.jpg";
        private int _counter = 2;
        public SwipePanelViewModel()
        {
            Greet = "Dear Insider"; /*+ Core.Models.DBH.Query();*/
        }
        public int Counter
        {
            get
            {

                return _counter;

            }
            set
            {
                if (_counter != value)
                {
                    _counter = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("Counter"));
                    }
                }

            }
        }
        public string Picture
        {
            get
            {

                return _picture;

            }
            set
            {
                if (_picture != value)
                {
                    _picture = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("Picture"));
                    }
                }

            }
        }
        public string Greet {
            get
            {

                return _greet;

            }
            set
            {
                if (_greet != value)
                {
                    _greet = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("Greet"));
                    }
                }

            }
        }
        public string Todate
        {
            get
            {

                return _todate;

            }
            set
            {
                if (_todate != value)
                {
                    _todate = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("Todate"));
                    }
                }

            }
        }
    }
}

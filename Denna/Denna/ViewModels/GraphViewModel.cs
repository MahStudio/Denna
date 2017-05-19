using Denna.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denna.ViewModels
{
    public class GraphViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Random _random = new Random();
        public ObservableCollection<NameValueItem> items { get; set; }
        public ObservableCollection<NameValueItem> week { get; set; }
        public GraphViewModel()
        {
            items = new ObservableCollection<NameValueItem>();
            week = new ObservableCollection<NameValueItem>();
            for (int i = 0; i < 7; i++)
            {
                week.Add(new NameValueItem { Name = "Test" + i, Value = _random.Next(10, 100) });
            }
            for (int i = 0; i < 30; i++)
            {
                items.Add(new NameValueItem { Name = "Test" + i, Value = _random.Next(10, 100) });
            }
            Pending = 3;
            Done = 4;
        }
        private int _done;
        public int Done
        {
            get
            {
                return _done;

            }
            set
            {

                if (_done != value)
                {
                    _done = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("Done"));
                    }
                }
            }
        }
        private int _pending;
        public int Pending
        {
            get
            {
                return _pending;

            }
            set
            {

                if (_pending != value)
                {
                    _pending = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("Pending"));
                    }
                }
            }
        }
    }
}

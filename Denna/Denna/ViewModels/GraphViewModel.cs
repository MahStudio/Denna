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
        public GraphViewModel()
        {
            items = new ObservableCollection<NameValueItem>();

            for (int i = 0; i < 10; i++)
            {
                items.Add(new NameValueItem { Name = "Test" + i, Value = _random.Next(10, 100) });
            }
        }
    }
}

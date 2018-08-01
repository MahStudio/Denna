using Core.Domain;
using Core.Todos.Tasks;
using Denna.Classes;
using Realms;
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
        public IRealmCollection<Todo> Today { get; set; }
        public IRealmCollection<Todo> Yesterday { get; set; }
        public IRealmCollection<Todo> ThisWeek { get; set; }
        public IRealmCollection<Todo> LastWeek { get; set; }
        public IRealmCollection<Todo> LastMonth { get; set; }
        public GraphViewModel()
        {
            Today = TodoService.GetTodayList();
            Yesterday = TodoService.GetYesterdayList();
            ThisWeek = TodoService.GetThisWeekList();
            LastWeek = TodoService.GetLastWeekList();
            LastMonth = TodoService.GetLastMonthList();
            //for (int i = 0; i < 7; i++)
            //{
            //    week.Add(new NameValueItem { Name = "Test" + i, Value = _random.Next(10, 100) });
            //}
            //for (int i = 0; i < 30; i++)
            //{
            //    items.Add(new NameValueItem { Name = "Test" + i, Value = _random.Next(10, 100) });
            //}
        }
    }
}

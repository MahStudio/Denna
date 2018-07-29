using Core.Domain;
using Core.Todos.Tasks;
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
    public class CalendarViewModel : INotifyPropertyChanged
    {
        private DateTimeOffset _selecteddate;
        public event PropertyChangedEventHandler PropertyChanged;
        public IRealmCollection<Todo> _todayList;
        public IRealmCollection<Todo> TodayList
        {
            get => _todayList;
            set
            {
                if (_todayList != value)
                {
                    _todayList = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("TodayList"));
                    }
                }
            }
        }
        public CalendarViewModel()
        {
            TodayList = TodoService.GetTodoListForDate( DateTime.Today);

        }
    }
}

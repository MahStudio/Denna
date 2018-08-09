using Core.Domain;
using Core.Todos.Tasks;
using Realms;
using System;
using System.ComponentModel;

namespace Denna.ViewModels
{
    public class CalendarViewModel : INotifyPropertyChanged
    {
        DateTimeOffset selecteddate;
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
            TodayList = TodoService.GetTodoListForDate(DateTime.Today);
        }
    }
}
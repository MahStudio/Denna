using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Data;
using Core.Domain;
using Core.Todos.Tasks;
using Realms;
using System.Collections.Specialized;
using System.Diagnostics;

namespace Denna.ViewModels
{
    public class TimeLineViewModel : INotifyPropertyChanged
    {

        public TimeLineViewModel()
        {
            TodayList = TodoService.GetTodayList();

            Attention = TodoService.GetPostponedList();



        }

        public IRealmCollection<Todo> TodayList { get; set; }
        public IRealmCollection<Todo> Attention { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}

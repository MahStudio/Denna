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
        private IRealmCollection<Todo> _value;
        public IRealmCollection<Todo> SearchResults
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
                            new PropertyChangedEventArgs("SearchResults"));
                    }
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
    }
}

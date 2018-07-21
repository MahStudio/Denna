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
            TodayList = RealmContext.Instance.All<Todo>().AsRealmCollection();
            TodayList.CollectionChanged += (s, e) =>
            {
                foreach (var item in TodayList)
                {
                    Debug.WriteLine(item.Subject);
                }
            };

            Attention = new ObservableCollection<Todo>();

            for (int i = 0; i < 5; i++)
            {
                Attention.Add(new Core.Domain.Todo()
                {
                    Detail = "Lurem IPsum Very cool app is under dev to be abnormal and very secret " + i,
                    Subject = "This is POSTPONED of " + i
                    ,
                    Imprtance = 2,
                    Status = 1,
                    Notify = 1,
                    StartTime = DateTime.Now.AddHours(2)
                });

            }
        }

        public IRealmCollection<Core.Domain.Todo> TodayList { get; set; }
        public ObservableCollection<Core.Domain.Todo> Attention { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}

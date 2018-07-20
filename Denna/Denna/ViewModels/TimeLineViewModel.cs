using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;

namespace Denna.ViewModels
{
    public class TimeLineViewModel : INotifyPropertyChanged
    {

        public TimeLineViewModel()
        {
            TodayList = new ObservableCollection<TaskItem>();
            Attention = new ObservableCollection<TaskItem>();


            for (int i = 0; i < 5; i++)
            {
                TodayList.Add(new TaskItem()
                {
                    Id = i,
                    Detail = "Lurem IPsum Very cool app is under dev to be abnormal and very secret " + i,
                    Subject = "This is Title of " + i
                    ,
                    Imprtance = Importance.High,
                    Isdone = 2,
                    Notify = 1,
                    StartTime = DateTime.Now.AddHours(2)
                });

            }

            for (int i = 0; i < 5; i++)
            {
                Attention.Add(new TaskItem()
                {
                    Id = i,
                    Detail = "Lurem IPsum Very cool app is under dev to be abnormal and very secret " + i,
                    Subject = "This is POSTPONED of " + i
                    ,
                    Imprtance = Importance.High,
                    Isdone = 1,
                    Notify = 1,
                    StartTime = DateTime.Now.AddHours(2)
                });

            }
        }

        public ObservableCollection<TaskItem> TodayList { get; set; }
        public ObservableCollection<TaskItem> Attention { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}

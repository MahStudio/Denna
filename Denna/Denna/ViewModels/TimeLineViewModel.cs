using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Types;

namespace Denna.ViewModels
{
    public class TimeLineViewModel:INotifyPropertyChanged
    {
        
        public TimeLineViewModel()
        {
            TodayList = new ObservableCollection<TaskItem>();
            TodayList.Add(new TaskItem()
            {
                ID = 33,
                Detail = "Lurem IPsum Very cool app is under dev to be abnormal and very secret " + 33,
                Subject = "This is Title of " + 33
                   ,
                Imprtance = 1,
                Isdone = 0,
                Notify = 1,
                StartTime = DateTime.Now.AddHours(2),
                
            });
            TodayList.Add(new TaskItem()
            {
                ID = 66,
                Detail = "Lurem IPsum Very cool app is under dev to be abnormal and very secret " + 66,
                Subject = "This is Title of " + 66
                   ,
                Imprtance = 1,
                Isdone = 1,
                Notify = 1,
                StartTime = DateTime.Now.AddHours(2),
                Tags = new ObservableCollection<string>() { "Tagone", "tagtwo", "tagThree", "TagFour" }
            });
            for (int i = 0; i < 20; i++)
            {
                TodayList.Add(new TaskItem() { ID = i, Detail = "Lurem IPsum Very cool app is under dev to be abnormal and very secret " + i, Subject = "This is Title of " + i
                    , Imprtance = 1, Isdone = 2, Notify = 1, StartTime = DateTime.Now.AddHours(2), Tags= new ObservableCollection<string>() {"Tagone" , "tagtwo","tagThree","TagFour"}
                });

            }
        }

        public ObservableCollection<TaskItem> TodayList { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

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
    class TimeLineViewModel:INotifyPropertyChanged
    {
        
        public TimeLineViewModel()
        {
            TodayList = new ObservableCollection<TaskItem>();
            for (int i = 0; i < 10; i++)
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

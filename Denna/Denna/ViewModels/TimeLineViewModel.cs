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
            TodayList = new ObservableCollection<TaskItem>(Core.Models.TasksModel.Tasks());
            Attention = new ObservableCollection<TaskItem>();


            
        }

        public ObservableCollection<TaskItem> TodayList { get; set; }
        public ObservableCollection<TaskItem> Attention { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}

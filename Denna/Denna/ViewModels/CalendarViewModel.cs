using Core.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denna.ViewModels
{
    class CalendarViewModel : INotifyPropertyChanged
    {
        private DateTimeOffset _selecteddate;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<TaskItem> TodayList { get; set; }
        public DateTimeOffset SelectedDate
        {
            get
            {
                return _selecteddate;

            }
            set
            {

                if (_selecteddate != value)
                {
                    _selecteddate = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("SelectedDate"));
                    }
                }
            }
        }
        public CalendarViewModel()
        {

            SelectedDate = DateTimeOffset.Now;
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
            for (int i = 0; i < 3; i++)
            {
                TodayList.Add(new TaskItem()
                {
                    ID = i,
                    Detail = "Lurem IPsum Very cool app is under dev to be abnormal and very secret " + i,
                    Subject = "This is Title of " + i
                    ,
                    Imprtance = 1,
                    Isdone = 2,
                    Notify = 1,
                    StartTime = DateTime.Now.AddHours(2),
                    Tags = new ObservableCollection<string>() { "Tagone", "tagtwo", "tagThree", "TagFour" }
                });

            }

        }
    }
}

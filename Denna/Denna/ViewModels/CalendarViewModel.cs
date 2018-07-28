using Core.Domain;
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
        public ObservableCollection<Core.Domain.Todo> TodayList { get; set; }
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

            SelectedDate = DateTime.Today;
            TodayList = new ObservableCollection<Core.Domain.Todo>();
            TodayList.Add(new Core.Domain.Todo()
            {
                Detail = "Lurem IPsum Very cool app is under dev to be abnormal and very secret " + 33,
                Subject = "This is Title of " + 33
                   ,
                Status = 2,
                Notify = 1,
                StartTime = DateTime.Now.AddHours(2),

            });
            TodayList.Add(new Core.Domain.Todo()
            {
                Detail = "Lurem IPsum Very cool app is under dev to be abnormal and very secret " + 66,
                Subject = "This is Title of " + 66
                   ,
                Status = 0,
                Notify = 1,
                StartTime = DateTime.Now.AddHours(2)
            });
            for (int i = 0; i < 3; i++)
            {
                TodayList.Add(new Core.Domain.Todo()
                {
                    Detail = "Lurem IPsum Very cool app is under dev to be abnormal and very secret " + i,
                    Subject = "This is Title of " + i
                    ,
                    Status = 1,
                    Notify = 1,
                    StartTime = DateTime.Now.AddHours(2)
                });

            }

        }
    }
}

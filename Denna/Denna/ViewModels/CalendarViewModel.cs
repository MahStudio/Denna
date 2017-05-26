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
    public class CalendarViewModel : INotifyPropertyChanged
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

            SelectedDate = DateTime.Today;
            TodayList = new ObservableCollection<TaskItem>();
           

        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;
using Core.Todos.Tasks;

namespace Denna.ViewModels
{
    public class TimeLineViewModel : INotifyPropertyChanged
    {

        public TimeLineViewModel()
        {
            TodayList = new ObservableCollection<Todo>(TodoService.GetAllTodos());
            Attention = new ObservableCollection<Todo>();

            for (int i = 0; i < 5; i++)
            {
                Attention.Add(new Core.Domain.Todo()
                {
                    Id = i,
                    Detail = "Lurem IPsum Very cool app is under dev to be abnormal and very secret " + i,
                    Subject = "This is POSTPONED of " + i
                    ,
                    Imprtance = Importance.High,
                    Status = Status.Suspended,
                    Notify = 1,
                    StartTime = DateTime.Now.AddHours(2)
                });

            }
        }

        public ObservableCollection<Core.Domain.Todo> TodayList { get; set; }
        public ObservableCollection<Core.Domain.Todo> Attention { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}

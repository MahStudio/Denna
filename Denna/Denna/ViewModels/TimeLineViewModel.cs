using Core.Domain;
using Core.Todos.Tasks;
using Realms;
using System.ComponentModel;

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
        IRealmCollection<Todo> value;
        public IRealmCollection<Todo> SearchResults
        {
            get
            {
                return value;
            }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
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
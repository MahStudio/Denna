using Core.Domain;
using Core.Todos.Tasks;
using Realms;
using System.ComponentModel;

namespace Denna.ViewModels
{
    public class GraphViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public IRealmCollection<Todo> Today { get; set; }
        public IRealmCollection<Todo> Yesterday { get; set; }
        public IRealmCollection<Todo> ThisWeek { get; set; }
        public IRealmCollection<Todo> LastWeek { get; set; }
        public IRealmCollection<Todo> LastMonth { get; set; }
        public GraphViewModel()
        {
            Today = TodoService.GetTodayList();
            Yesterday = TodoService.GetYesterdayList();
            ThisWeek = TodoService.GetThisWeekList();
            LastWeek = TodoService.GetLastWeekList();
            LastMonth = TodoService.GetLastMonthList();
        }
    }
}

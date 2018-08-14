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
        TodoService _service;
        public GraphViewModel()
        {
            _service = new TodoService();
            Today = _service.GetTodayList();
            Yesterday = _service.GetYesterdayList();
            ThisWeek = _service.GetThisWeekList();
            LastWeek = _service.GetLastWeekList();
            LastMonth = _service.GetLastMonthList();
        }
    }
}

using Autofac;
using Core.Data;
using Core.Domain;
using Realms;
using System;
using System.Linq;
using Core.Service.Notifications;

namespace Core.Todos.Tasks
{
    public class TodoService
    {
        IGenericRepository<Todo> repo;
        Realm _instance;
        public TodoService()
        {
            _instance = RealmContext.GetInstance();
            repo = new GenericRepository<Todo>(_instance);

        }
        #region CRUD
        public void AddTodo(Todo task)
        {
            task.Id = repo.CreateId();
            repo.Create(task);
            if (task.Notify == 1)
                task.CreateNotification();
            if (task.Notify == 2)
                task.CreateAlarm();
        }

        public void AddSilentTodo(Todo task)
        {
            task.Id = repo.CreateId();
            repo.Create(task);
        }

        public IRealmCollection<Todo> GetAllTodos() => repo.GetAll();

        public Todo GetById(string id) => repo.GetById(id);

        public void Edit(Todo oldTask, Todo newTask)
        {
            newTask.UpdateNotification();
            repo.UpdatePrimary(newTask, newTask.Id);
        }


        public void Delete(string id)
        {
            GetById(id).DeleteNotification();
            repo.Delete(id);

        }

        public void Delete(Todo item)
        {
            item.DeleteNotification();
            repo.Delete(item.Id);

        }


        public void Done(Todo task)
        {
            task.DeleteNotification();
            using (var trans = _instance.BeginWrite())
            {
                task.Status = 0;
                trans.Commit();
            }

        }
        #endregion
        #region actions

        public void Undone(Todo task)
        {
            using (var trans = _instance.BeginWrite())
            {
                task.Status = 2;
                trans.Commit();
            }

            if (task.StartTime > DateTimeOffset.Now)   //If else... the user should edit the time
            {
                if (task.Status != 2 || task.Status != 1)
                {

                    if (task.Notify == 1)
                        task.CreateNotification();
                    if (task.Notify == 2)
                        task.CreateAlarm();
                }
            }

        }

        public void Postpone(Todo task)
        {
            using (var trans = _instance.BeginWrite())
            {
                task.Status = 1;
                trans.Commit();
            }

            if (task.StartTime > DateTimeOffset.Now)
                if (task.Status != 1 || task.Status != 2)
                {
                    if (task.Notify == 1)
                        task.CreateNotification();
                    if (task.Notify == 2)
                        task.CreateAlarm();
                }
        }
        #endregion
        #region Queries
        public IRealmCollection<Todo> GetTodayList()
        {
            var today = new DateTimeOffset(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0));
            return _instance.All<Todo>().Where(offset => offset.StartTime >= today).OrderBy(x => x.StartTime).AsRealmCollection();
        }

        public IRealmCollection<Todo> GetYesterdayList()
        {
            var yesterdayMorning = new DateTimeOffset(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-1).Day, 0, 0, 0));
            var yesterdayEnd = new DateTimeOffset(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-1).Day, 23, 59, 59));
            return _instance.All<Todo>().Where(offset => offset.StartTime >= yesterdayMorning && offset.StartTime <= yesterdayEnd).OrderBy(x => x.StartTime).AsRealmCollection();
        }

        public IRealmCollection<Todo> GetThisWeekList()
        {
            var today = new DateTimeOffset(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59));
            var lastWeek = new DateTimeOffset(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-7).Day, 0, 0, 0));
            return _instance.All<Todo>().Where(offset => offset.StartTime >= lastWeek && offset.StartTime <= today).OrderBy(x => x.StartTime).AsRealmCollection();
        }

        public IRealmCollection<Todo> GetLastWeekList()
        {
            var lastWeekMorning = new DateTimeOffset(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-7).Day, 0, 0, 0));
            var twoWeeksAgoMorning = new DateTimeOffset(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-14).Day, 0, 0, 0));
            return _instance.All<Todo>().Where(offset => offset.StartTime >= lastWeekMorning && offset.StartTime <= twoWeeksAgoMorning).OrderBy(x => x.StartTime).AsRealmCollection();
        }

        public IRealmCollection<Todo> GetLastMonthList()
        {
            var lastMonthMorning = new DateTimeOffset(new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, DateTime.Now.Day, 0, 0, 0));
            var endOfToday = new DateTimeOffset(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59));
            var y = _instance.All<Todo>().Where(offset => offset.StartTime >= lastMonthMorning && offset.StartTime <= endOfToday).OrderBy(x => x.StartTime).AsRealmCollection();
            return y;
        }

        public IRealmCollection<Todo> GetPostponedList()
        {
            var today = new DateTimeOffset(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0));
            return _instance.All<Todo>().Where(s => (s.Status == 1 || s.Status == 2) && s.StartTime < today).OrderBy(x => x.StartTime).AsRealmCollection();
        }

        public IRealmCollection<Todo> GetMustDoList() => _instance.All<Todo>().Where(s => s.Status == 1 || s.Status == 2).OrderBy(x => x.StartTime).AsRealmCollection();

        public IRealmCollection<Todo> GetTodoListForDate(DateTimeOffset date)
        {
            var startDate = new DateTimeOffset(new DateTime(date.Year, date.Month, date.Day, 0, 0, 0));
            var endDate = new DateTimeOffset(new DateTime(date.Year, date.Month, date.Day, 23, 59, 59));
            return _instance.All<Todo>().Where(s => s.StartTime >= startDate && s.StartTime <= endDate).OrderBy(x => x.StartTime).AsRealmCollection();
        }

        public IRealmCollection<Todo> FullTextSearch(string term)
        {
            var itmz = _instance.All<Todo>().Where(s => s.Detail.Contains(term) || s.Subject.Contains(term)).OrderBy(x => x.StartTime).AsRealmCollection();
            return itmz;
        }
        #endregion
    }
}
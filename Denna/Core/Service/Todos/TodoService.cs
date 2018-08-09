using Autofac;
using Core.Data;
using Core.Domain;
using Core.Infrastructure;
using Realms;
using System;
using System.Linq;

namespace Core.Todos.Tasks
{
    public static class TodoService
    {
        static IGenericRepository<Todo> repo;
        static TodoService() => repo = DI.Container.Resolve<IGenericRepository<Todo>>();
        public static void AddTodo(Todo task)
        {
            task.Id = repo.CreateId();
            repo.Create(task);
        }

        public static IRealmCollection<Todo> GetAllTodos() => repo.GetAll();

        public static Todo GetById(string id) => repo.GetById(id);

        public static void Edit(Todo oldTask, Todo newTask) => repo.UpdateManaged(oldTask, newTask);

        public static void Delete(string id) => repo.Delete(id);

        public static void Delete(Todo item) => repo.Delete(item.Id);

        public static void Done(Todo task)
        {
            using (var trans = RealmContext.Instance.BeginWrite())
            {
                task.Status = 0;
                trans.Commit();
            }
        }

        public static void Undone(Todo task)
        {
            using (var trans = RealmContext.Instance.BeginWrite())
            {
                task.Status = 2;
                trans.Commit();
            }
        }

        public static void Postpone(Todo task)
        {
            using (var trans = RealmContext.Instance.BeginWrite())
            {
                task.Status = 1;
                trans.Commit();
            }
        }

        public static IRealmCollection<Todo> GetTodayList()
        {
            var today = new DateTimeOffset(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0));
            return RealmContext.Instance.All<Todo>().Where(offset => offset.StartTime >= today).OrderBy(x => x.StartTime).AsRealmCollection();
        }

        public static IRealmCollection<Todo> GetYesterdayList()
        {
            var yesterdayMorning = new DateTimeOffset(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-1).Day, 0, 0, 0));
            var yesterdayEnd = new DateTimeOffset(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-1).Day, 23, 59, 59));
            return RealmContext.Instance.All<Todo>().Where(offset => offset.StartTime >= yesterdayMorning && offset.StartTime <= yesterdayEnd).OrderBy(x => x.StartTime).AsRealmCollection();
        }

        public static IRealmCollection<Todo> GetThisWeekList()
        {
            var today = new DateTimeOffset(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59));
            var lastWeek = new DateTimeOffset(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-7).Day, 0, 0, 0));
            return RealmContext.Instance.All<Todo>().Where(offset => offset.StartTime >= lastWeek && offset.StartTime <= today).OrderBy(x => x.StartTime).AsRealmCollection();
        }

        public static IRealmCollection<Todo> GetLastWeekList()
        {
            var lastWeekMorning = new DateTimeOffset(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-7).Day, 0, 0, 0));
            var twoWeeksAgoMorning = new DateTimeOffset(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-14).Day, 0, 0, 0));
            return RealmContext.Instance.All<Todo>().Where(offset => offset.StartTime >= lastWeekMorning && offset.StartTime <= twoWeeksAgoMorning).OrderBy(x => x.StartTime).AsRealmCollection();
        }

        public static IRealmCollection<Todo> GetLastMonthList()
        {
            var lastMonthMorning = new DateTimeOffset(new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, DateTime.Now.Day, 0, 0, 0));
            var endOfToday = new DateTimeOffset(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59));
            var y = RealmContext.Instance.All<Todo>().Where(offset => offset.StartTime >= lastMonthMorning && offset.StartTime <= endOfToday).OrderBy(x => x.StartTime).AsRealmCollection();
            return y;
        }

        public static IRealmCollection<Todo> GetPostponedList()
        {
            var today = new DateTimeOffset(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0));
            return RealmContext.Instance.All<Todo>().Where(s => (s.Status == 1 || s.Status == 2) && s.StartTime < today).OrderBy(x => x.StartTime).AsRealmCollection();
        }

        public static IRealmCollection<Todo> GetMustDoList() => RealmContext.Instance.All<Todo>().Where(s => s.Status == 1 || s.Status == 2).OrderBy(x => x.StartTime).AsRealmCollection();

        public static IRealmCollection<Todo> GetTodoListForDate(DateTimeOffset date)
        {
            var startDate = new DateTimeOffset(new DateTime(date.Year, date.Month, date.Day, 0, 0, 0));
            var endDate = new DateTimeOffset(new DateTime(date.Year, date.Month, date.Day, 23, 59, 59));
            return RealmContext.Instance.All<Todo>().Where(s => s.StartTime >= startDate && s.StartTime <= endDate).OrderBy(x => x.StartTime).AsRealmCollection();
        }

        public static IRealmCollection<Todo> FullTextSearch(string term)
        {
            var itmz = RealmContext.Instance.All<Todo>().Where(s => s.Detail.Contains(term) || s.Subject.Contains(term)).OrderBy(x => x.StartTime).AsRealmCollection();
            return itmz;
        }
    }
}
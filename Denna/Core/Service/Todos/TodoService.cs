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
        private static IGenericRepository<Todo> _repo;
        static TodoService() => _repo = DI.Container.Resolve<IGenericRepository<Todo>>();
        public static void AddTodo(Todo task)
        {
            task.Id = _repo.CreateId();
            _repo.Create(task);
        }
        public static IRealmCollection<Todo> GetAllTodos() => _repo.GetAll();
        public static Todo GetById(string id) => _repo.GetById(id);
        public static void Edit(Todo oldTask, Todo newTask) => _repo.UpdateManaged(oldTask, newTask);
        public static void Delete(string id) => _repo.Delete(id);
        public static void Delete(Todo item) => _repo.Delete(item.Id);
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
            var today = new DateTimeOffset(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0, new TimeSpan());
            return RealmContext.Instance.All<Todo>().Where(offset => offset.StartTime > today).OrderBy(x => x.StartTime).AsRealmCollection();
        }

        public static IRealmCollection<Todo> GetPostponedList()
        {
            var today = new DateTimeOffset(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0, new TimeSpan());
            return RealmContext.Instance.All<Todo>().Where(s => (s.Status == 1 || s.Status == 2) && s.StartTime < today).OrderBy(x => x.StartTime).AsRealmCollection();
        }


    }
}

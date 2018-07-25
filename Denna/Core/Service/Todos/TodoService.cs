using Autofac;
using Core.Data;
using Core.Domain;
using Core.Infrastructure;
using Core.Utils;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static void Edit(Todo oldTask,Todo newTask) => _repo.UpdateManaged(oldTask,newTask);
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
        public static IRealmCollection<Todo> GetTodayList() => RealmContext.Instance.All<Todo>().OrderBy(x => x.StartTime).AsRealmCollection();

    }
}

using Autofac;
using Core.Data;
using Core.Domain;
using Core.Infrastructure;
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
        public static void AddTodo(Todo task) => _repo.Create(task);
        public static List<Todo> GetAllTodos() => _repo.GetAll().ToList();
        public static Todo GetById(string id) => _repo.GetById(id);
        public static void Edit(Todo task) => _repo.Update(task);
        public static void Delete(string id) => _repo.Delete(id);

    }
}

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
        public static async Task AddTodo(Todo task) => await _repo.Create(task);
        public static List<Todo> GetAllTodos() => _repo.GetAll().ToList();
        public static async Task<Todo> GetById(int id) => await _repo.GetById(id);
        public static async Task Edit(int id, Todo task) => await _repo.Update(id, task);
        public static async Task Delete(int id) => await _repo.Delete(id);

    }
}

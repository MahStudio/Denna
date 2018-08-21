using Core.Domain;
using Core.Todos.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service.Backwards
{
    public class BackwardsService
    {
        TodoService _svc;
        string SQLPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");
        public BackwardsService()
        {
            _svc = new TodoService();
        }
        public bool IsBacwardsPresent() => File.Exists(SQLPath);

        IEnumerable<todo> GetTodos()
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), SQLPath))
            {
                var query = conn.Table<todo>();
                foreach (var message in query)
                    yield return message;

            }
        }
        public void MigrateTodos()
        {
            var todos = GetTodos();
            foreach (var todo in todos)
            {
                _svc.AddTodo(new Todo()
                {
                    Detail = todo.detail,
                    Notify = todo.notify,
                    StartTime = todo.time,
                    Status = ConvertToNewIsdone(todo.isdone),
                    Subject = todo.title
                });
            }


            File.Delete(SQLPath);
        }
        int ConvertToNewIsdone(int num)
        {
            switch (num)
            {
                case 0:
                    return 2;
                case 2:
                    return 0;
                default:
                    return 1;
            }
        }
    }
}

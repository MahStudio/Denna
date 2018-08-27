using Core.Domain;
using Core.Todos.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

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
            PushToDB(todos);
            File.Delete(SQLPath);
        }
        public async void RestoreBakcup(IActivatedEventArgs args)
        {
            if (args != null)
            {
                if (args.Kind == Windows.ApplicationModel.Activation.ActivationKind.File)
                {
                    var fileArgs = args as Windows.ApplicationModel.Activation.FileActivatedEventArgs;
                    var strFilePath = fileArgs.Files[0].Path;
                    var file = (StorageFile)fileArgs.Files[0];
                    var json = await FileIO.ReadTextAsync(file);
                    var toadd = JsonConvert.DeserializeObject<IEnumerable<todo>>(json);
                    var msg = new MessageDialog("Do you want to restore this backup?");
                    msg.Commands.Add(new UICommand("Yes", async delegate
                    {
                        PushToDB(toadd);

                        var noWifiDialog = new ContentDialog()
                        {
                            Title = "Success! :)",
                            Content = "Backup had been restored.",
                            PrimaryButtonText = "Nice!"
                        };
                        await noWifiDialog.ShowAsync();
                    }));
                    msg.Commands.Add(new UICommand("Nope"));
                    await msg.ShowAsync();
                }
            }
        }

        void PushToDB(IEnumerable<todo> todos)
        {
            foreach (var todo in todos)
            {
                _svc.AddSilentTodo(new Todo()
                {
                    Detail = todo.detail,
                    Notify = todo.notify,
                    StartTime = todo.time,
                    Status = ConvertToNewIsdone(todo.isdone),
                    Subject = todo.title
                });
            }
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

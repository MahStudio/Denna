using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Windows.Storage;

namespace Planel.Models
{
    class Localdb
    {
        //for database creation
        public static void CreateDatabase()
        {
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                conn.CreateTable<todo>();

            }


        }
        
        //save user name
        public static void Iuser(string name)
        {
            ApplicationData.Current.LocalSettings.Values["Username"] = name;



        }
        //logout proceed
        public static void Logout(string name)
        {
            ApplicationData.Current.LocalSettings.Values["Username"] = null;
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                conn.DropTable<todo>();
                ApplicationData.Current.LocalSettings.Values["Username"] = null;
            }



        }
        // add a todo list
        public static void Addtodo(string titl, string describe, DateTime date)
        {


            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                conn.Insert(new todo()
                {
                    title = titl,
                    detail = describe,
                    time = date,
                    isdone = false

                });

            }
        }
        
        // get all list
        public static ObservableCollection<Models.todo> getlist()
        {
            ObservableCollection<Models.todo> todos = new ObservableCollection<todo>();
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                var query = conn.Table<todo>();
                foreach (var message in query)
                {
                    todos.Add(new todo() { detail = message.detail, isdone = message.isdone, time = message.time, title = message.title, Id = message.Id });
                }

            }
            return todos;


        }
        //get stuff for today
        public static ObservableCollection<Models.todo> Getfordoday(DateTime now)
        {
            ObservableCollection < Models.todo > todos = getlist();


            DateTime starttoday = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            DateTime endtoday = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
            ObservableCollection<Models.todo> todoss = new ObservableCollection<Models.todo>();
            foreach (var item in todos)
            {
                if (item.time >= starttoday && item.time <= endtoday)
                {
                    todoss.Add(item);
                }
            }
            return todoss;



        }
        //get all todo list with order
        public static ObservableCollection<Models.todo> getall(DateTime now)
        {
            ObservableCollection<Models.todo> todos = getlist();
            

            DateTime starttoday = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            DateTime endtoday = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
            ObservableCollection<Models.todo> todoss = new ObservableCollection<Models.todo>();
            foreach (var item in todos)
            {
                if (item.time >= starttoday && item.time <= endtoday)
                {
                    todoss.Add(item);
                }
            }
            foreach (var item in todos)
            {
                if (item.time >= endtoday)
                {
                    todoss.Add(item);
                }
            }


            return todoss;


        }
        public static int counter()
        {
            List<todo> todos = new List<todo>();
            List<Classes.NameValueItem> weeker = new List<Classes.NameValueItem>();
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                var query = conn.Table<todo>();
                foreach (var message in query)
                {
                    todos.Add(new todo() { detail = message.detail, isdone = message.isdone, time = message.time, title = message.title, Id = message.Id });
                }

            }
            DateTime now = DateTime.Now;
            DateTime starttoday = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            List<todo> todoss = new List<todo>();
            foreach (var item in todos)
            {
                if (starttoday <= item.time && item.isdone == false)
                {
                    todoss.Add(item);
                }
                

            }
            
            int a = todoss.Count();

            return a ;




        }
        //return undone list for live tile
       
        public static Classes.mpercent percentage()
        {
            ObservableCollection<Models.todo> todos = getlist();
            
            Classes.mpercent percentage = new Classes.mpercent();
            //calculate for today
            DateTime now = DateTime.Now;
            DateTime starttoday = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            DateTime endtoday = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
            ObservableCollection<Models.todo> todoss = new ObservableCollection<Models.todo>();
            foreach (var item in todos)
            {
                if (item.time >= starttoday && item.time <= endtoday)
                {
                    todoss.Add(item);
                }
            }
            int all = todoss.Count;
            int done = 0;
            foreach (var item in todoss)
            {
                if (item.isdone == true)
                {
                    done++;
                }

            }
            int percentComplete;

            if (all != 0)
                percentComplete = (int)Math.Round((double)(100 * done) / all);
            else
                percentComplete = 0;


            percentage.firstpercentage = percentComplete;

            //calculate for yesterday
            DateTime startyesterday = new DateTime(now.Year, now.Month, now.Day , 0, 0, 0);
            startyesterday=startyesterday.AddDays(-1);
            DateTime endyesterday = new DateTime(now.Year, now.Month, now.Day , 23, 59, 59);
            endyesterday= endyesterday.AddDays(-1);
            ObservableCollection<Models.todo> todosss = new ObservableCollection<Models.todo>();
            foreach (var item in todos)
            {
                if (item.time >= startyesterday && item.time <= endyesterday)
                {
                    todosss.Add(item);
                }
            }
            int alll = todosss.Count;
            int donee = 0;
            foreach (var item in todosss)
            {
                if (item.isdone == true)
                {
                    donee++;
                }

            }
            int percentCompletee;
            if (alll != 0)
                percentCompletee = (int)Math.Round((double)(100 * donee) / alll);
            else
                percentCompletee = 0;
            percentage.secondpercentage = percentCompletee;


            return percentage;






        }

        public static List<Classes.NameValueItem> Wgraph()
        {
            List<todo> todos = new List<todo>();
            List<Classes.NameValueItem> weeker = new List<Classes.NameValueItem>();
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                var query = conn.Table<todo>();
                foreach (var message in query)
                {
                    todos.Add(new todo() { detail = message.detail, isdone = message.isdone, time = message.time, title = message.title, Id = message.Id });
                }

            }
            DateTime now = DateTime.Now;
            int done = 0;
            int alll = todos.Count;

            for (int i = 0; i < 6; i++)
            {
                DateTime starttoday = new DateTime(now.Year, now.Month, now.Day , 0, 0, 0);
                starttoday=starttoday.AddDays(-i);
                DateTime endtoday = new DateTime(now.Year, now.Month, now.Day , 23, 59, 59);
                endtoday=endtoday.AddDays(-i);
                foreach (var item in todos)
                {
                    if (item.isdone == true)
                    {
                        done++;
                    }

                }
                int percentCompletee;
                if (alll != 0)
                    percentCompletee = (int)Math.Round((double)(100 * done) / alll);
                else
                    percentCompletee = 0;
                weeker.Add(new Classes.NameValueItem() { Name = i.ToString(), Value = percentCompletee });




            }


            return weeker;

        }
        public static List<Classes.NameValueItem> Mgraph()
        {
            List<todo> todos = new List<todo>();
            List<Classes.NameValueItem> weeker = new List<Classes.NameValueItem>();
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                var query = conn.Table<todo>();
                foreach (var message in query)
                {
                    todos.Add(new todo() { detail = message.detail, isdone = message.isdone, time = message.time, title = message.title, Id = message.Id });
                }

            }
            DateTime now = DateTime.Now;
            int done = 0;
            int alll = todos.Count;

            for (int i = 0; i < 6; i++)
            {
                DateTime starttoday = new DateTime(now.Year, now.Month, now.Day , 0, 0, 0);
                starttoday=starttoday.AddDays(-i * 4);
                DateTime endtoday = new DateTime(now.Year, now.Month, now.Day , 23, 59, 59);
                endtoday=endtoday.AddDays(-i * 4);
                foreach (var item in todos)
                {
                    if (item.isdone == true)
                    {
                        done++;
                    }

                }
                int percentCompletee;
                if (alll != 0)
                    percentCompletee = (int)Math.Round((double)(100 * done) / alll);
                else
                    percentCompletee = 0;
                weeker.Add(new Classes.NameValueItem() { Name = i.ToString(), Value = percentCompletee });




            }


            return weeker;

        }





        public static void Deletetodo(int id)
        {
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                conn.Execute("DELETE FROM todo WHERE Id = ?", id);

            }
            Classes.worker.refresher();
        }

        public static void Done(todo item)
        {
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                item.isdone = true;
                conn.Update(item);


            }
            Classes.worker.refresher();
        }
    }
}


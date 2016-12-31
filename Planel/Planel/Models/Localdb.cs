using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Planel.Models
{
    class Localdb
    {
        public static void CreateDatabase()
        {
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                conn.CreateTable<todo>();
                
            }


        }
        public static void Iuser( string name)
        {
            ApplicationData.Current.LocalSettings.Values["Username"] = name;



        }
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
        public static void Addtodo (string titl, string describe, DateTime date)
        {

            
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                conn.Insert(new todo()
                {
                   title = titl , detail=describe , time=date, isdone=false 
                
                });

            }
        }


            public static List< todo> Getfordoday(DateTime now)
            {
            List<todo> todos = new List<todo>();
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                var query = conn.Table<todo>();
                foreach (var message in query)
            {   
                    todos.Add(new todo() { detail = message.detail,  isdone=message.isdone , time=message.time , title=message.title, Id = message.Id });
                }

            }
            
            DateTime starttoday = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            DateTime endtoday = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
            List<todo> todoss = new List<todo>();
            foreach (var item in todos)
            {
                if (item.time >= starttoday && item.time <= endtoday)
                {
                    todoss.Add(item);
                }
            }
            return todoss;
           


        }
        public static List<todo> getall(DateTime now)
        {
            List<todo> todos = new List<todo>();
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                var query = conn.Table<todo>();
                foreach (var message in query)
            {   
                    todos.Add(new todo() { detail = message.detail,  isdone=message.isdone , time=message.time , title=message.title, Id = message.Id });
                }

            }
            
            DateTime starttoday = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            DateTime endtoday = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
            List<todo> todoss = new List<todo>();
            foreach (var item in todos)
            {
                if (item.time >= starttoday && item.time <= endtoday)
                {
                    todoss.Add(item);
                }
            }
            foreach (var item in todos)
            {
                if (item.time >= starttoday && item.time <= endtoday)
                {
                    todoss.Remove(item);
                }
            }
            todoss.AddRange(todos);
            
            return todoss;
           

        }
        public static int counter()
        {
            List<todo> todos = new List<todo>();
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                var query = conn.Table<todo>();
                foreach (var message in query)
                {
                    todos.Add(new todo() { detail = message.detail, isdone = message.isdone, time = message.time, title = message.title,Id=message.Id});
                }

            }

           return todos.Count();
            



        }
        public static Classes.mpercent percentage()
        {
            List<todo> todos = new List<todo>();
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                var query = conn.Table<todo>();
                foreach (var message in query)
                {
                    todos.Add(new todo() { detail = message.detail, isdone = message.isdone, time = message.time, title = message.title, Id = message.Id });
                }

            }
            Classes.mpercent percentage = new Classes.mpercent();
            //calculate for today
            DateTime now = DateTime.Now;
            DateTime starttoday = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            DateTime endtoday = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
            List<todo> todoss = new List<todo>();
            foreach (var item in todos)
            {
                if (item.time >= starttoday && item.time <= endtoday)
                {
                    todoss.Add(item);
                }
            }
            int all=todoss.Count;
            int done = 0;
            foreach (var item in todos)
            {
                if (item.isdone==true)
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
            DateTime startyesterday = new DateTime(now.Year, now.Month, now.Day-1, 0, 0, 0);
            DateTime endyesterday = new DateTime(now.Year, now.Month, now.Day-1, 23, 59, 59);
            List<todo> todosss = new List<todo>();
            foreach (var item in todos)
            {
                if (item.time >= startyesterday && item.time <= endyesterday)
                {
                    todosss.Add(item);
                }
            }
            int alll = todoss.Count;
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


        public static void Deletetodo(int id)
        {
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                conn.Execute("DELETE FROM todo WHERE Id = ?",id);
                
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

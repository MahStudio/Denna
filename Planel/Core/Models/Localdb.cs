using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.UI.Notifications;

namespace Core.Models
{
    public class Localdb
    {
        //for database creation
        public static void CreateDatabase()
        {
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                conn.CreateTable<todo>();

            }
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                conn.CreateTable<Hobby>();

            }


        }
        
        //save user name
        public static void Iuser(string name)
        {
            ApplicationData.Current.LocalSettings.Values["Username"] = name;



        }
        //logout proceed
        public static async Task Logout()
        {
            
            ApplicationData.Current.LocalSettings.Values["Username"] = null;
            ApplicationData.Current.LocalSettings.Values["Firstrun"] = null; 
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                conn.DropTable<todo>();
                
            }
            CoreApplication.Exit();


        }
        // add a todo list
        
        public static async Task Addtodo(todo item)
        {


            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                conn.Insert(new todo()
                { 
                    notify = item.notify,
                    title = item.title,
                    detail = item.detail,
                    time = item.time.ToLocalTime(),
                    isdone = item.isdone


                });
                if (item.notify==1)
                 createnotify(item);
                else if (item.notify == 2)
                    createalarm(item);

            }
        }
        //Add a hobbie 
        public static async Task Addhobby(Hobby item)
        {


            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                conn.Insert(new Hobby()
                {
                    notify = item.notify,
                    title = item.title,
                    detail = item.detail,
                    time = item.time,
                    Days = item.Days


                });
                

            }
        }
        //Get hobbies as List
        public static ObservableCollection<Hobby> Gethobbies()
        {
            ObservableCollection<Hobby> hobbies = new ObservableCollection<Hobby>();
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                var query = conn.Table<Hobby>();
                foreach (var message in query)
                {
                    hobbies.Add(message);
                }

            }
            
            return hobbies;
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
                    todos.Add(message);
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
            ObservableCollection<Hobby> Hobbies = Gethobbies();

            DateTime starttoday = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            DateTime endtoday = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
            ObservableCollection<Models.todo> todoss = new ObservableCollection<Models.todo>();
            foreach (var item in todos)
            {
                if (item.time >= starttoday && item.time <= endtoday || item.isdone==1)
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
            TimeSpan starttodayi = new TimeSpan(0, 0, 0);
            TimeSpan endtodayi = new TimeSpan(23,59, 59);
            
            foreach (var item in Hobbies)
            {
                string json = item.Days.ToString();
                var toadd = JsonConvert.DeserializeObject<IList<DayOfWeek>>(json);
                List<DayOfWeek> adder = new List<DayOfWeek>();
                adder = toadd.ToList();
                bool iscontain = _containstoday(adder , starttoday.DayOfWeek);
                if (item.time >= starttodayi && item.time <= endtodayi)
                {
                    DateTime today = DateTime.Now;
                    DateTime Todate = new DateTime(today.Year,today.Month,today.Day,item.time.Hours,item.time.Minutes,item.time.Seconds);
                   
                    
                    
                    todo hobbytodo = new todo() { notify = item.notify, time = Todate, title = item.title, detail = item.detail };
                    todoss.Add(hobbytodo);
                }
            }


            return todoss;


        }
        private static bool _containstoday(List<DayOfWeek>list , DayOfWeek day)
        {
            bool result = false;

            foreach (var item in list)
            {
                if (item == day)
                    result = true;
            }

            return result;
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
                if (starttoday <= item.time && (item.isdone == 0 || item.isdone==1))
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
                if (item.isdone == 2)
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

            int suspend = 0;
            foreach (var item in todoss)
            {
                if (item.isdone == 1)
                {
                    suspend++;
                }

            }
            int percentsuspend;

            if (all != 0)
                percentsuspend = (int)Math.Round((double)(100 * suspend) / all);
            else
                percentsuspend = 0;

            percentage.firstsuspend = percentsuspend;

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
                if (item.isdone == 2)
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

            int ysuspend = 0;
            foreach (var item in todosss)
            {
                if (item.isdone == 1)
                {
                    ysuspend++;
                }

            }
            int percentysuspend;

            if (all != 0)
                percentysuspend = (int)Math.Round((double)(100 * ysuspend) / all);
            else
                percentysuspend = 0;

            percentage.secondsuspend = percentysuspend;

            return percentage;






        }
        private static async Task createalarm(todo item)
        {
            var xmlString = @"<toast launch='args' scenario='alarm'>
    <visual>
        <binding template='ToastGeneric'>
            <text>Header</text>
            <text>Detail</text>
        </binding>
    </visual>
    <actions>

       

        <action arguments = 'dismiss'
                content = 'OK' />

    </actions>
</toast>";
            var doc = new Windows.Data.Xml.Dom.XmlDocument();
            doc.LoadXml(xmlString);
            doc.LoadXml(doc.GetXml().Replace("Header", item.title));
            doc.LoadXml(doc.GetXml().Replace("Detail", item.detail + " " + item.time));
            DateTimeOffset offset = item.time;
            ScheduledToastNotification toast = new ScheduledToastNotification(doc, offset);
            ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);
        }
        private static async Task createnotify(todo item)
        {

            string xml = @"<toast>
            <visual>
            <binding template=""ToastGeneric"">
                <text>Header</text>
                <text>Detail</text>
            </binding>
            </visual>
        </toast>";

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(xml);
            doc.LoadXml(doc.GetXml().Replace("Header", item.title));
            doc.LoadXml(doc.GetXml().Replace("Detail", item.detail + " " + item.time));
            DateTimeOffset offset = item.time;
            ScheduledToastNotification toast = new ScheduledToastNotification(doc, offset);
            ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);
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
                    if (item.isdone == 2)
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
                    if (item.isdone == 2)
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

        //Deletes a hobby
        public static async Task DeleteHobby(int id)
        {
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                conn.Execute("DELETE FROM Hobby WHERE Id = ?", id);

            }
            
        }



        public static async Task Deletetodo(int id)
        {
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                conn.Execute("DELETE FROM todo WHERE Id = ?", id);

            }
          //  Classes.worker.refresher();
        }

        public static async Task Done(todo item)
        {
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                item.isdone = 2;
                conn.Update(item);


            }
           // Classes.worker.refresher();
        }
        public static async Task Suspend(todo item)
        {
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                item.isdone = 1;
                conn.Update(item);


            }
           // Classes.worker.refresher();
        }
    }
}


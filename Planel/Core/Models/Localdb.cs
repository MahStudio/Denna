﻿using Newtonsoft.Json;
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
            if (ApplicationData.Current.LocalSettings.Values["Showtoast"] == null)
                ApplicationData.Current.LocalSettings.Values["Showtoast"] = true;

            ApplicationData.Current.LocalSettings.Values["Firstrun"] = "1";
            ApplicationData.Current.LocalSettings.Values["RunTime"] = 1;



            //Create Tips
            tipper();


        }
        //Create TIPs
        private static void tipper()
        {

            List<Tips> tipsl = new List<Tips>();
            tipsl.Add(new Tips() { Title="Rate us!" , detail="Rate and review denna" ,Days=5 });
            tipsl.Add(new Tips() { Title = "Hobbies!", detail = "Add your habits and stuff you do frequently", Days = 2 });
            tipsl.Add(new Tips() { Title = "Add your tasks.", detail = "Add your task and stuff that you are planning to do.", Days = 1 });
            tipsl.Add(new Tips() { Title = "Pin !", detail = "Pin DENNA to your start menu to see more !", Days = 3 });
            tipsl.Add(new Tips() { Title = "Lockscreen", detail = "Add DENNA to your lockscreen.", Days = 4 });
            tipsl.Add(new Tips() { Title = "Cortana", detail = "DENNA and Cortana are friends. simply say 'hay,what to do today?' ", Days =6 });
            tipsl.Add(new Tips() { Title = "Quick Actions", detail = "DENNA can be on your action center! Go to settings and enable this feature and see more personalization settings.", Days = 7 });
            tipsl.Add(new Tips() { Title = "Feedback", detail = "Please tell us your ideas about DENNA in feedback hub", Days = 17 });

            string xml = @"<toast>
            <visual>
            <binding template=""ToastGeneric"">
                <text>Header</text>
                <text>Detail</text>
                <text placement='attribution'>Tips</text>
            </binding>
            </visual>
        </toast>";
            

            DateTimeOffset todate = DateTimeOffset.Now;
            foreach (var item in tipsl)
            {
                XmlDocument doc = new XmlDocument();

                doc.LoadXml(xml);
                doc.LoadXml(doc.GetXml().Replace("Header", item.Title));
                doc.LoadXml(doc.GetXml().Replace("Detail", item.detail));
                DateTimeOffset offset = todate.AddDays(item.Days);
                ScheduledToastNotification toast = new ScheduledToastNotification(doc, offset);
                ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);
            }
            

        }
        //logout proceed
        public static async Task Logout()
        {
            ApplicationData.Current.LocalSettings.Values["Showtoast"] = null;
            ApplicationData.Current.LocalSettings.Values["Username"] = null;
            ApplicationData.Current.LocalSettings.Values["Firstrun"] = null; 

            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                conn.DropTable<todo>();
                
            }
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                conn.DropTable<Hobby>();

            }
            try
            {
                Windows.Storage.StorageFolder storageFolder =
Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFile sampleFile =
                    await storageFolder.GetFileAsync("avatar.jpg");
                sampleFile.DeleteAsync(StorageDeleteOption.PermanentDelete);

            }
            catch { }
            var tup = TileUpdateManager.CreateTileUpdaterForApplication();
            tup.Clear();
            var updator = BadgeUpdateManager.CreateBadgeUpdaterForApplication();
            updator.Clear();

            CoreApplication.Exit();


        }
        // add a todo list
        
        public static async Task Addtodo(todo item)
        {


            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                var si =new todo()
                { 
                    notify = item.notify,
                    title = item.title,
                    detail = item.detail,
                    time = item.time.ToLocalTime(),
                    isdone = item.isdone


                };

                conn.Insert(si);

                item.Id = si.Id;
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
                if ( item.time<starttoday && item.isdone == 1)
                {
                    todoss.Add(item);
                }
            }
            //TimeSpan starttodayi = new TimeSpan(0, 0, 0);
            //TimeSpan endtodayi = new TimeSpan(23, 59, 59);

            //foreach (var item in Hobbies)
            //{
            //    string json = item.Days.ToString();
            //    var toadd = JsonConvert.DeserializeObject<IList<DayOfWeek>>(json);
            //    List<DayOfWeek> adder = new List<DayOfWeek>();
            //    adder = toadd.ToList();
            //    bool iscontain = _containstoday(adder, starttoday.DayOfWeek);
            //    if (item.time >= starttodayi && item.time <= endtodayi)
            //    {
            //        DateTime today = DateTime.Now;
            //        DateTime Todate = new DateTime(today.Year, today.Month, today.Day, item.time.Hours, item.time.Minutes, item.time.Seconds);



            //        todo hobbytodo = new todo() { notify = item.notify, time = Todate, title = item.title, detail = item.detail };
            //        todoss.Add(hobbytodo);
            //    }
            //}
            foreach (var item in todos)
            {
                if (item.time >= starttoday && item.time <= endtoday )
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
            ObservableCollection<Hobby> Hobbies = Gethobbies();
            TimeSpan starttodayi = new TimeSpan(0, 0, 0);
            TimeSpan endtodayi = new TimeSpan(23, 59, 59);

            foreach (var item in Hobbies)
            {
                string json = item.Days.ToString();
                var toadd = JsonConvert.DeserializeObject<IList<DayOfWeek>>(json);
                List<DayOfWeek> adder = new List<DayOfWeek>();
                adder = toadd.ToList();
                bool iscontain = _containstoday(adder, starttoday.DayOfWeek);
                if (item.time >= starttodayi && item.time <= endtodayi)
                {
                    DateTime today = DateTime.Now;
                    DateTime Todate = new DateTime(today.Year, today.Month, today.Day, item.time.Hours, item.time.Minutes, item.time.Seconds);



                    todo hobbytodo = new todo() { notify = item.notify, time = Todate, title = item.title, detail = item.detail };
                    todoss.Add(hobbytodo);
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
            
            var doc = new Windows.Data.Xml.Dom.XmlDocument();
            doc.LoadXml(await FileIO.ReadTextAsync(await StorageFile.GetFileFromApplicationUriAsync(
                        new Uri("ms-appx:///AlarmToast.xml"))));
            doc.LoadXml(doc.GetXml().Replace("Header", item.title));
            doc.LoadXml(doc.GetXml().Replace("Detail", item.detail ));
            DateTimeOffset offset = item.time;
            ScheduledToastNotification toast = new ScheduledToastNotification(doc, offset);
            toast.Id = item.Id.ToString();
            ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);
        }
        
        private static async Task createnotify(todo item)
        {
           
            string xml = @"<toast>
            <visual>
            <binding template=""ToastGeneric"">
                <text>Header</text>
                <text>Detail</text>
                <text placement='attribution'>Right now</text>
            </binding>
            </visual>
        </toast>";

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(xml);
            doc.LoadXml(doc.GetXml().Replace("Header", item.title));
            doc.LoadXml(doc.GetXml().Replace("Detail", item.detail ));
            DateTimeOffset offset = item.time;
            ScheduledToastNotification toast = new ScheduledToastNotification(doc, offset);
            toast.Id = item.Id.ToString();
            
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
                    todos.Add(message);
                }

            }
            DateTime today = DateTime.Now.ToLocalTime();
            var starttoday = new DateTime(today.Year,today.Month,today.Day,0,0,0);
            var endtoday = new DateTime(today.Year, today.Month, today.Day, 23, 59, 59);
            for (int i = 0; i < 7; i++)
            {
                var allitems = 0;
                var doneitems = 0;
                var starter = starttoday.AddDays(-i);
                var ender = endtoday.AddDays(-i);
                foreach (var item in todos)
                {
                    if (item.time<=ender && item.time >= starter)
                    {
                        allitems++;
                        if (item.isdone == 2)
                        {
                            doneitems++;
                        }
                    }
                }
                int percentCompletee;
                if (allitems != 0)
                    percentCompletee = (int)Math.Round((double)(100 * doneitems) / allitems);
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
            DateTime today = DateTime.Now.ToLocalTime();
            var starttoday = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);
            var endtoday = new DateTime(today.Year, today.Month, today.Day, 23, 59, 59);
            for (int i = 0; i < 29; i+=2)
            {
                var allitems = 0;
                var doneitems = 0;
                var starter = starttoday.AddDays(-i);
                var ender = endtoday.AddDays(-(i-1));
                foreach (var item in todos)
                {
                    if (item.time <= ender && item.time >= starter)
                    {
                        allitems++;
                        if (item.isdone == 2)
                        {
                            doneitems++;
                        }
                    }
                }
                int percentCompletee;
                if (allitems != 0)
                    percentCompletee = (int)Math.Round((double)(100 * doneitems) / allitems);
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

        public static async Task UpdateTask(Models.todo item)
        {
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                conn.Update(new todo() {
                    Id = item.Id,
                    isdone = item.isdone,
                    time = item.time.ToLocalTime(),
                    detail=item.detail,
                    notify=item.notify,
                    title=item.title
                



                });

            }
            
            var toastnotifier = ToastNotificationManager.CreateToastNotifier();

            foreach (var scheduledToastNotification in toastnotifier.GetScheduledToastNotifications())
            {
                if (scheduledToastNotification.Id == item.Id.ToString())
                {

                    toastnotifier.RemoveFromSchedule(scheduledToastNotification);
                }
            }
            if (item.notify == 1)
                createnotify(item);
            else if (item.notify == 2)
                createalarm(item);


        }

        public static async Task Deletetodo(int id)
        {
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                conn.Execute("DELETE FROM todo WHERE Id = ?", id);

            }
            var toastnotifier = ToastNotificationManager.CreateToastNotifier();

            foreach (var scheduledToastNotification in toastnotifier.GetScheduledToastNotifications())
            {
                if (scheduledToastNotification.Id == id.ToString())
                {

                    toastnotifier.RemoveFromSchedule(scheduledToastNotification);
                }
            }
        }

        public static async Task Done(todo item)
        {
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                item.isdone = 2;
                conn.Update(item);


            }
            var toastnotifier = ToastNotificationManager.CreateToastNotifier();

            foreach (var scheduledToastNotification in toastnotifier.GetScheduledToastNotifications())
            {
                if (scheduledToastNotification.Id == item.Id.ToString())
                {

                    toastnotifier.RemoveFromSchedule(scheduledToastNotification);
                }
            }
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


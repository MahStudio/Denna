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

            }



        }
        public static void Addtodo (string titl, string describe, DateTime date)
        {

            ApplicationData.Current.LocalSettings.Values["Username"] = null;
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                conn.Insert(new todo()
                {
                   title = titl , detail=describe , time=date, isdone=false 
                
                });

            }
        }


            public static List< todo> Getfordoday()
            {
            List<todo> todos = new List<todo>();
            var sqlpath = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Contactdb.sqlite");

            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), sqlpath))
            {
                var query = conn.Table<todo>();
                foreach (var message in query)
            {
                    todos.Add(new todo() { detail = message.detail,  isdone=message.isdone , time=message.time , title=message.title });
                }

            }

            return todos;
           


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
                    todos.Add(new todo() { detail = message.detail, isdone = message.isdone, time = message.time, title = message.title });
                }

            }
           return todos.Count();
            



        }
    }




    
}

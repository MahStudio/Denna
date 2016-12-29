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
    }
}

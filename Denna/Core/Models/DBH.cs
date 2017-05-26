using Couchbase.Lite;
using Couchbase.Lite.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public static class DBH
    {
        static Database DB;
        public static void CreateDB()
        {
            Couchbase.Lite.Support.UWP.Activate();
            DatabaseOptions options = new DatabaseOptions();
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            options.Directory = storageFolder.Path;

            var database = new Database("Xugros", options);
            DB = database;



        }

        public static void MakeDoc(IDictionary<string,object> Dic)
        {
            CreateDB();
            var _doc = new Document(Dic);
            DB.Save(_doc);

        }
        public static string Query()
        {

            CreateDB();

            var a = DB.GetDocument("-UHbc-Y_ty0ugUnpQeFGNSA");
            var x =a.ToDictionary();

            return (string)x["FirstName"];

        }

        
    }
}

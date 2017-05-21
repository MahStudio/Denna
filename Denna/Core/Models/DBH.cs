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

        public static void MakeDoc(Dictionary<string,object> Dic)
        {
            CreateDB();
            var _doc = new Document(Dic);
            DB.Save(_doc);

        }
        public static string Query()
        {

            CreateDB();
            var query = QueryFactory.Select()
        .From(DataSourceFactory.Database(DB))
        .Where(
            ExpressionFactory.Property("Type").EqualTo("user")
        );

            var rows = query.Run();
            return rows.FirstOrDefault().ToString();
        }

        
    }
}

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
        static IDatabase _database;
        public static void CreateDB()
        {
            Couchbase.Lite.Support.UWP.Activate();
            DatabaseOptions options = new DatabaseOptions();
            options.EncryptionKey = EncryptionKeyFactory.Create("PassHash");
            Windows.Storage.StorageFolder storageFolder =
            Windows.Storage.ApplicationData.Current.LocalFolder;
            options.Directory = storageFolder.Path;
            var database = DatabaseFactory.Create("Xugros", options);
            
            _database = database;
            
        }

        public static void MakeDoc(Dictionary<string,object> Dic)
        {
            Couchbase.Lite.Support.UWP.Activate();
            var database = DatabaseFactory.Create("Xugros");
            var doc = database.CreateDocument();
            doc.Properties = Dic;
            doc.Save();

        }
        public static string Query()
        {
            Couchbase.Lite.Support.UWP.Activate();
            var database = DatabaseFactory.Create("Xugros");

            var query = QueryFactory.Select()
        .From(DataSourceFactory.Database(database))
        .Where(
            ExpressionFactory.Property("Type").EqualTo("user")
        );

            var rows = query.Run();
            return rows.FirstOrDefault().Document.Id;
        }

        
    }
}

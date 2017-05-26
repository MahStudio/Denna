using Couchbase.Lite;
using Couchbase.Lite.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

            //var a = DB.GetDocument("-QmBJfl3Stk-ZYbINAux-vg");
            //var x =a.ToDictionary();
            //var test = GetObject<Types.Person>((Dictionary<string,object>)x);


            var query = QueryFactory.Select()
        .From(DataSourceFactory.Database(DB))
        .Where(
            ExpressionFactory.Property("Type").EqualTo("User")
        );

            var rows = query.Run();
            var a = rows.FirstOrDefault();
            var ab = a.Document;
            var sx = ab.ToDictionary();
            var test = GetObject<Types.Person>((Dictionary<string,object>)sx);
            return test.LastName;

        }
        static T GetObject<T>(Dictionary<string, object> dict)
        {
            Type type = typeof(T);
            var obj = Activator.CreateInstance(type);

            foreach (var kv in dict)
            {
                type.GetProperty(kv.Key).SetValue(obj, kv.Value);
            }
            return (T)obj;
        }

    }
}

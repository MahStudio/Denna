using Core.Types;
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
           

            var database = new Database("Xugros");
            DB = database;



        }

        public static void MakeDoc(IDictionary<string,object> Dic)
        {
            CreateDB();
            var _doc = new Document(Dic);
            DB.Save(_doc);

        }
        public static List<TaskItem> QueryW()
        {

            CreateDB();
            

           


            var query = QueryFactory.Select()
        .From(DataSourceFactory.Database(DB))
        .Where(
            ExpressionFactory.Property("Type").EqualTo("Task")
        );

            var rows = query.Run();
            var sdsd = new List<TaskItem>();
            foreach (var row in rows)
            {
                var ab = row.Document;
                var sx = ab.ToDictionary();
                var test = GetObject<Types.TaskItem>((Dictionary<string, object>)sx);
                sdsd.Add(test);
            }
            
            
            return sdsd;

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
            string name = "";
            try
            {

 
                var test = GetObject<Person>(
                  (Dictionary<string,object>)(rows.FirstOrDefault().Document
                    ).ToDictionary()
                    );
                name = test.FirstName;
            }
            catch
            {
                name = "Unidentified";
            }

            return name;

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

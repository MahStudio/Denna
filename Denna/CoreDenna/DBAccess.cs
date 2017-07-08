using System;
using System.Collections.Generic;
using System.Text;
using Couchbase.Lite;
using System.Diagnostics;
using Couchbase.Lite.Query;

namespace CoreDenna
{
    
    public static class DBAccess
    {
        public static Database CreateDB()
        {

            return new Database("Xugros");


        }
        public static void CreateDoc(Dictionary<string,object> Doc)
        {
            var _doc = new Document(Doc);
            _doc.Set("ID", _doc.Id);
            CreateDB().Save(_doc);
            
        }
        public static List<Document> QueryMaker(string Tag, string Value)
        {
            var query = QueryFactory.Select()
        .From(DataSourceFactory.Database(CreateDB()))
        .Where(
            ExpressionFactory.Property(Tag).EqualTo(Value)
        );
            var output = new List<Document>();
            var rows = query.Run();

            foreach (var row in rows)
            {
                var ab = row.Document;
                output.Add(ab);
            }

            return output;
        }
        private static DatabaseConfiguration GetConfig()
        {


            var config = new DatabaseConfiguration()
            {
                EncryptionKey = EncryptionKeyFactory.Create("VeryHaedPAss")
                ,
                ConflictResolver = new Fuclift()
            };

            return config;

        }

    }
    class Fuclift : IConflictResolver
    {
        public ReadOnlyDocument Resolve(Conflict conflict)
        {
            return conflict.Target;
        }
    }
}

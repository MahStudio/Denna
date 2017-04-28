using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Couchbase.Lite;
using System.IO;

namespace Core.Models
{
    class TasksModel
    {
        public void createDB()
        {
            Couchbase.Lite.Support.UWP.Activate();
            DatabaseOptions options = new DatabaseOptions();
            options.EncryptionKey = EncryptionKeyFactory.Create("PassHash");
            var database = DatabaseFactory.Create("Xugros",options);
            var doc = database.CreateDocument();
            doc.Properties = new Dictionary<string, object>
            {
                ["type"] = "user",
                ["admin"] = false,
                ["address"] = new Dictionary<string, object>
                {
                    ["street"] = "1 park street",
                    ["zip"] = 123456
                }
            };
            doc.Save();


        }

    }
}

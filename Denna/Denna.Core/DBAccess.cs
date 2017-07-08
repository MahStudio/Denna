using System;
using System.Collections.Generic;
using System.Text;
using Couchbase.Lite;

namespace Denna.Core
{
    internal class DBAccess
    {
        internal void CreateDB()
        {
            var database = new Database("Xugros");
            DB = database;

        }
    }
}

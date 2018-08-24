using Realms;
using Realms.Sync;
using System;

namespace Core.Data
{
    public static class RealmContext
    {
        public static Realm GetInstance()
        {
            var configuration = new SyncConfiguration(User.Current, new Uri("~/myRealm", UriKind.Relative)) { SchemaVersion = 1 };
            return Realm.GetInstance(configuration);
        }
    }
}
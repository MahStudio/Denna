using Realms;
using Realms.Sync;
using System;

namespace Core.Data
{
    public static class RealmContext
    {
        public static Realm GetInstance()
        {
            var configuration = new FullSyncConfiguration(new Uri("~/myRealm", UriKind.Relative), User.Current)
            {
                SchemaVersion=1
            };
            return Realm.GetInstance(configuration);
        }
    }
}
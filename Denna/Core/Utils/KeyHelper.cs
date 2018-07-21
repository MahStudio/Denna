using Autofac;
using Core.Data;
using Core.Domain;
using Core.Infrastructure;
using Realms;
using Realms.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils
{
    public static class KeyHelper
    {
        private static readonly Realm _instance;
        static KeyHelper() => _instance = Realm.GetInstance();
        public static string CreateId()
        {
            if (_instance.All<Count>().Count() == 0)
            {
                _instance.Write(() =>
                {
                    _instance.Add(new Count());
                });
            }
            var credentials = Credentials.UsernamePassword("", "", createUser: false);
            var counter = _instance.All<Count>().FirstOrDefault();
            using (var trans = _instance.BeginWrite())
            {
                counter.Counter.Increment();
                trans.Commit();
                int a = counter.Counter;
                return $"{a}_{DateTime.UtcNow.Ticks}";
            }
        }
    }
}

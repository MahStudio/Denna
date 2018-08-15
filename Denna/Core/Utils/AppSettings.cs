using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Core.Utils
{
    public static class AppSettings
    {
        public static T Get<T>(string name)
        {
            var item = ApplicationData.Current.LocalSettings.Values[name];
            if (item == null)
                return default(T);
            else return (T)item;
        }
        public static object OpenGet(string name) => ApplicationData.Current.LocalSettings.Values[name];
        public static void Set(string name, object item) => ApplicationData.Current.LocalSettings.Values[name] = item;
    }
}

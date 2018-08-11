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
        public static T Get<T>(string name) => (T)ApplicationData.Current.LocalSettings.Values[name];
        public static object OpenGet(string name) => ApplicationData.Current.LocalSettings.Values[name];
        public static void Set(string name, object item) => ApplicationData.Current.LocalSettings.Values[name] = item;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Planel.Classes
{
    class Themesetter
    {
        public static void SetApplicationTheme(string Theme = "System")
        {
            ApplicationData.Current.LocalSettings.Values["ApplicationTheme"] = Theme;
        }

        public static string GetApplicationTheme()
        {
            try
            {
                var res = ApplicationData.Current.LocalSettings.Values["ApplicationTheme"].ToString();
                return res;
            }
            catch (Exception)
            {
                SetApplicationTheme();
                return "System";
            }
        }
    }
}

using System;
using Windows.Storage;

namespace Planel.Classes
{
    class Themesetter
    {
        public static void SetApplicationTheme(string theme = "System")
        {
            ApplicationData.Current.LocalSettings.Values["ApplicationTheme"] = theme;
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

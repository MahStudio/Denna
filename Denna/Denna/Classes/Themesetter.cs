using Core.Utils;
using System;

namespace Denna.Classes
{
    class Themesetter
    {
        public static void SetApplicationTheme(string theme = "System") => AppSettings.Set("ApplicationTheme", theme);

        public static string GetApplicationTheme()
        {
            try
            {
                var res = AppSettings.OpenGet("ApplicationTheme").ToString();
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

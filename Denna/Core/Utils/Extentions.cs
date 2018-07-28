using System;
namespace Core.Utils
{
    public static class Extentions
    {
        public static bool IsToday(this DateTimeOffset offset) => offset > new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
    }
}

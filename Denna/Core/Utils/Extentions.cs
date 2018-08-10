using System;
namespace Core.Utils
{
    public static class Extentions
    {
        public static int GetUnixTimeNow() => (Int32) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
    }
}

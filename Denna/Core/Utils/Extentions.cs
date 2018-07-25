using Autofac;
using Core.Data;
using Core.Domain;
using Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils
{
    public static class Extentions
    {
        public static bool IsToday(this DateTimeOffset offset) => offset > new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
    }
}

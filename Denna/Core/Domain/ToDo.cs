using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Todo : RealmObject
    {
        public string Subject { get; set; }
        public string Detail { get; set; }
        public int Notify { get; set; }
        public int Status { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public int Imprtance { get; set; }

    }
}

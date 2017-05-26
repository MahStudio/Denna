using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Types
{
    public class TaskItem
    {

        public string Type { get; set; }
        public string Subject { get; set; }
        public string Detail { get; set; }
        public Int64 Notify { get; set; }
        public Int64 Isdone { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        //public List<string> Tags { get; set; }

    }
}

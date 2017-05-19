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
        public int ID { get; set; }
        public string Subject { get; set; }
        public string Detail { get; set; }
        public int Notify { get; set; }
        public int Isdone { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Imprtance { get; set; }
        public ObservableCollection<string> Tags { get; set; }

    }
}

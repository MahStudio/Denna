using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Todo:BaseModel
    {
        public string Subject { get; set; }
        public string Detail { get; set; }
        public int Notify { get; set; }
        public int Isdone { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Importance Imprtance { get; set; }

    }
}

using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planel.Models
{
    class todo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string title { get; set; }
        public string detail { get; set; }
        public DateTime time { get; set; }

        public bool isdone { get; set; }

    }
}

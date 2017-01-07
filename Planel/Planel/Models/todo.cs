using SQLite.Net.Attributes;
using System;


namespace Planel.Models
{
    class todo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public string title { get; set; }
        public string detail { get; set; }
        public DateTime time { get; set; }
        //0 stands for no notify, 1 stands for toast notify and 2 stands for alarm
        public byte notify { get; set; }
        // 0 stands for undone, 1 stands for suspend(snooze) and 2 stands for done
        public byte isdone { get; set; }

    }
}

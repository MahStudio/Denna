using System;
using Realms;
namespace Core.Domain
{
    public class Todo : RealmObject
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Detail { get; set; }
        public int Notify { get; set; }
        public int Status { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }

    }
}

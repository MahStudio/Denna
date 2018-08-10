using Realms;
using System;
namespace Core.Domain
{
    public class Todo : RealmObject
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Detail { get; set; }
        //refer to NitificationStatus.cs
        public int Notify { get; set; }
        //Refer to Status.cs
        public int Status { get; set; }
        public DateTimeOffset StartTime { get; set; }
    }
}
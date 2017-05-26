using Core.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class TasksModel
    {
        public static void create(string subject, string detail, DateTime? start, DateTime? end,int notify)
        {
            //isdone=0
            var task = new TaskItem
            {
                Type="Task",
                Isdone=0,
                Detail=detail,
                Subject=subject,
                StartTime = start.ToString(),
                EndTime = end.ToString(),
                Notify= notify

            };
            var mydic = task.ToDictionary();
            IDictionary<string, object> an = mydic;
            DBH.MakeDoc(an);





        }
        public static List<TaskItem> Tasks()
        {

            return DBH.QueryW();
        }
    }
}

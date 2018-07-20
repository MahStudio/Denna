using Core.Data;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public class TestSvc
    {
        private IGenericRepository<TaskItem> _repo;
        public void addsth()
        {
            _repo = new GenericRepository<TaskItem>(new DataContext());
            _repo.Create(new TaskItem()
            {
                Detail = "Lurem IPsum #Very cool app is under dev to be abnormal and very secret ",
                Subject = "This is Title of "
                    ,
                Imprtance = Importance.Medium,
                Isdone = 2,
                Notify = 1,
                StartTime = DateTime.Now.AddHours(2),
            });
            var a = _repo.GetAll();
        }
    }
}

using Autofac;
using Core.Data;
using Core.Domain;
using Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils
{
    static class Extentions
    {
        private static IGenericRepository<Count> _repo;
        static Extentions() => _repo = DI.Container.Resolve<IGenericRepository<Count>>();
        public static string createId()
        {
            var counter = _repo.GetAll().FirstOrDefault();
            counter.Counter.Increment();
            _repo.Update(counter);
            return string.Format("{0}_{1)", counter, DateTime.UtcNow.Ticks);
        }
    }
}

using Autofac;
using Core.Data;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure
{
    public static class DI
    {
        public static IContainer Container;
        public static void Build()
        {
            var builder = new ContainerBuilder();
            builder.Register(p => new GenericRepository<Todo>()).As<IGenericRepository<Todo>>();
            builder.Register(p => new GenericRepository<Count>()).As<IGenericRepository<Count>>();
            var container = builder.Build();
            Container = container;
        }
    }
}

using Autofac;
using Core.Data;
using Core.Domain;
using Realms;

namespace Core.Infrastructure
{
    public static class DI
    {
        public static IContainer Container;
        public static void Build()
        {
            var builder = new ContainerBuilder();
            builder.Register(p => new GenericRepository<Todo>()).As<IGenericRepository<Todo>>();
            var container = builder.Build();
            Container = container;
        }
    }
}

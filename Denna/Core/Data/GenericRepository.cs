using Autofac;
using Core.Domain;
using Core.Utils;
using Realms;
using Realms.Sync;
using System;
using System.Linq;

namespace Core.Data
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : RealmObject
    {
        readonly Realm instance;
        public GenericRepository() => instance = RealmContext.GetInstance();
        public GenericRepository(Realm instance) => this.instance = instance;

        public IRealmCollection<TEntity> GetAll() => instance.All<TEntity>().AsRealmCollection();

        public TEntity GetById(string id) => instance.Find<TEntity>(id);

        public void Create(TEntity entity)
        {
            instance.Write(() =>
            {
                instance.Add(entity);
            });
        }

        public void UpdateManaged(TEntity oldEntity, TEntity newEntity)
        {
            using (var trans = instance.BeginWrite())
            {
                oldEntity = newEntity;
                trans.Commit();
            }
        }

        public void UpdatePrimary(TEntity entity, string id) => instance.Write(() => instance.Add(entity, update: true));

        public void Delete(TEntity entity)
        {
            using (var trans = instance.BeginWrite())
            {
                instance.Remove(entity);
                trans.Commit();
            }
        }

        public void Delete(string id)
        {
            using (var trans = instance.BeginWrite())
            {
                var entity = GetById(id);
                instance.Remove(entity);
                trans.Commit();
            }
        }

        public string CreateId()
        {
            if (instance.All<Count>().Count() == 0)
            {
                instance.Write(() =>
                {
                    instance.Add(new Count());
                });
            }

            var credentials = Credentials.UsernamePassword("", "", createUser: false);
            var counter = instance.All<Count>().FirstOrDefault();
            using (var trans = instance.BeginWrite())
            {
                counter.Counter.Increment();
                trans.Commit();
                int a = counter.Counter;
                return $"{a}{Extentions.GetUnixTimeNow()}";
            }
        }
    }
}
using Core.Domain;
using Realms;
using Realms.Sync;
using System;
using System.Linq;

namespace Core.Data
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : RealmObject
    {
        private readonly Realm _instance;
        public GenericRepository() => _instance = RealmContext.Instance;
        public GenericRepository(Realm instance) => _instance = instance;

        public IRealmCollection<TEntity> GetAll() => _instance.All<TEntity>().AsRealmCollection();

        public TEntity GetById(string id) => _instance.Find<TEntity>(id);
        public void Create(TEntity entity)
        {
            _instance.Write(() =>
            {
                _instance.Add(entity);
            });
        }

        public void UpdateManaged(TEntity oldEntity, TEntity newEntity)
        {
            using (var trans = _instance.BeginWrite())
            {
                oldEntity = newEntity;
                trans.Commit();
            }
        }

        public void UpdatePrimary(TEntity entity, string id) => _instance.Write(() => _instance.Add(entity, update: true));
        public void Delete(TEntity entity)
        {
            using (var trans = _instance.BeginWrite())
            {
                _instance.Remove(entity);
                trans.Commit();
            }
        }

        public void Delete(string id)
        {
            using (var trans = _instance.BeginWrite())
            {
                var entity = GetById(id);
                _instance.Remove(entity);
                trans.Commit();
            }
        }
        public string CreateId()
        {
            if (_instance.All<Count>().Count() == 0)
            {
                _instance.Write(() =>
                {
                    _instance.Add(new Count());
                });
            }
            var credentials = Credentials.UsernamePassword("", "", createUser: false);
            var counter = _instance.All<Count>().FirstOrDefault();
            using (var trans = _instance.BeginWrite())
            {
                counter.Counter.Increment();
                trans.Commit();
                int a = counter.Counter;
                return $"{a}_{DateTime.UtcNow.Ticks}";
            }
        }
    }
}

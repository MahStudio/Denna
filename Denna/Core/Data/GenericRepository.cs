using Core.Domain;
using Realms;
using Realms.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : RealmObject
    {
        private readonly Realm _instance;
        public GenericRepository()
        {
            var serverURL = new Uri("/some-realm", UriKind.Relative);
            var configuration = new FullSyncConfiguration(serverURL, User.Current);
            _instance = Realm.GetInstance(configuration);
        }
        public GenericRepository(Realm instance) => _instance = instance;

        public IQueryable<TEntity> GetAll() => _instance.All<TEntity>();
        public TEntity GetById(string id) => _instance.Find<TEntity>(id);
        public void Create(TEntity entity)
        {
            _instance.Write(() =>
            {
                _instance.Add(entity);
            });
        }

        public void Update(TEntity entity) => _instance.Write(() => _instance.Add(entity, update: true));

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
    }
}

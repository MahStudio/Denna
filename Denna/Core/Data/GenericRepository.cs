using Core.Domain;
using Realms;
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
        public GenericRepository() => _instance = Realm.GetInstance();
        public GenericRepository(Realm instance) => _instance = instance;

        public IQueryable<TEntity> GetAll() => _instance.All<TEntity>();
        public async Task<TEntity> GetById(int id)
        {
            throw new NotImplementedException();
        }
        public async Task Create(TEntity entity)
        {
            _instance.Write(() =>
            {
                _instance.Add(entity);
            });
        }

        public async Task Update(int id, TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}

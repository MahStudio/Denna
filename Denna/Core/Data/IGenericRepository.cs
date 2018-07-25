using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IRealmCollection<TEntity> GetAll();
        TEntity GetById(string id);
        void Create(TEntity entity);
        void UpdateManaged(TEntity oldEntity, TEntity newEntity);
        void UpdatePrimary(TEntity entity, string id);
        void Delete(TEntity entity);
        void Delete(string id);
        string CreateId();
    }
}

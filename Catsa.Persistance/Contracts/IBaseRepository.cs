using Catsa.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Catsa.Persistance.Contracts
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity<int>
    {
        IEnumerable<TEntity> GetAll(bool trackChanges = false);

        TEntity Get(int id, bool trackChanges = false);

        IQueryable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges = false);        

        void Add(TEntity entity);

        void Update(TEntity entity);
        void Delete(int entityId);
    }
}

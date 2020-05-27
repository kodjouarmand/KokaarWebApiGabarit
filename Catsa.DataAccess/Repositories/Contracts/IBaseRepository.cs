using Catsa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Catsa.DataAccess.Repositories.Contracts
{
    public interface IBaseRepository<TEntity, TEntityKey> where TEntity : BaseEntity<TEntityKey>
    {
        IEnumerable<TEntity> GetAll(bool trackChanges = true);

        TEntity GetById(TEntityKey entityId, bool trackChanges = true);

        IQueryable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges = true);        

        void Add(TEntity entity);

        void Update(TEntity entity);
        void Delete(TEntityKey entityId);
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using Catsa.DataAccess.Contexts;
using Catsa.Domain.Entities;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using Catsa.DataAccess.Repositories.Contracts;

namespace Catsa.DataAccess.Repositories
{
    public class BaseRepository<TEntity, TEntityKey> : IBaseRepository<TEntity, TEntityKey> where TEntity : BaseEntity<TEntityKey>
    {
        private readonly CatsaDbContext _catsaDbContext;
        internal DbSet<TEntity> dbSet;

        public BaseRepository(CatsaDbContext catsaDbContext)
        {
            _catsaDbContext = catsaDbContext ?? throw new ArgumentNullException(nameof(catsaDbContext));
            this.dbSet = _catsaDbContext.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll(bool trackChanges = true) =>
            trackChanges ? dbSet.ToList() : dbSet.AsNoTracking().ToList();

        public TEntity GetById(TEntityKey entityId, bool trackChanges = true) =>
             GetByCondition(c => c.Id.Equals(entityId), trackChanges).SingleOrDefault();

        public IQueryable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges = true) =>
            !trackChanges ? dbSet.Where(expression).AsNoTracking() : dbSet.Where(expression);

        public void Add(TEntity entity) => dbSet.Add(entity);

        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }

        public void Delete(TEntityKey entityId)
        {
            TEntity entity = dbSet.Find(entityId);
            dbSet.Remove(entity);
        }
    }

}

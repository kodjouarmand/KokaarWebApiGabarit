using KokaarWebApiGabarit.Persistance.Contracts;
using KokaarWebApiGabarit.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using KokaarWebApiGabarit.Persistance.Data;
using KokaarWebApiGabarit.Model.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;

namespace KokaarWebApiGabarit.Persistance.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity<int>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        internal DbSet<TEntity> dbSet;

        public BaseRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            this.dbSet = _applicationDbContext.Set<TEntity>();
        }

        public  IEnumerable<TEntity> GetAll(bool trackChanges = false) =>
            trackChanges ?  dbSet.ToList() :  dbSet.AsNoTracking().ToList();

        public  TEntity Get(int companyId, bool trackChanges = false) =>
             GetByCondition(c => c.Id.Equals(companyId), trackChanges).SingleOrDefault();

        public IQueryable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges = false) =>
            !trackChanges ? dbSet.Where(expression).AsNoTracking() : dbSet.Where(expression);

        public  void Add(TEntity entity) =>  dbSet.Add(entity);

        public void Update(TEntity entity) => dbSet.Update(entity);

        public void Delete(int entityId)
        {
            TEntity entity = dbSet.Find(entityId);
            dbSet.Remove(entity);
        }
    }

}

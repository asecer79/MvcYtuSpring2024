using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Repositories.CommonInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories.Ef.DataAccess
{
    public class EfRepositoryBase<TEntity, TDbContext> : IRepositoryBase<TEntity> where TEntity : class, IEntityBase, new()
    where TDbContext : DbContext, new()
    {
        public bool Any(Expression<Func<TEntity, bool>> filter)
        {
            using (var dbContext = new TDbContext())
            {
                var result = dbContext.Set<TEntity>().Any(filter);

                return result;
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var dbContext = new TDbContext())
            {
                var result = dbContext.Set<TEntity>().FirstOrDefault(filter);

                return result;
            }
        }

        public TEntity Add(TEntity entity)
        {
            using (var dbContext = new TDbContext())
            {
                var result = dbContext.Set<TEntity>().Add(entity);
                dbContext.SaveChanges();

                return result.Entity;
            }
        }

        public TEntity Update(TEntity entity)
        {
            using (var dbContext = new TDbContext())
            {
                var result = dbContext.Set<TEntity>().Update(entity);
                dbContext.SaveChanges();

                return result.Entity;
            }
        }

        public bool Remove(TEntity entity)
        {
            using (var dbContext = new TDbContext())
            {
                var result = dbContext.Set<TEntity>().Remove(entity);
                dbContext.SaveChanges();

                return true;
            }
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>>? filter = null)
        {
            using (var dbContext = new TDbContext())
            {
                if (filter != null)
                {
                    return dbContext.Set<TEntity>().Where(filter).ToList();
                }
                else
                {
                    return dbContext.Set<TEntity>().ToList();
                }

            }
        }
    }
}

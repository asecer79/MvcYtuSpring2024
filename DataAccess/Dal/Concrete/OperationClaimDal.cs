using DataAccess.Dal.Abstract;
using DataAccess.EfDbContext.Obs;
using Entities.ObsEntities;
using System.Linq.Expressions;
using Entities.CommonEntities;

namespace DataAccess.Dal.Concrete
{
    public class OperationClaimDal : IOperationClaimDal
    {
        public bool Any(Expression<Func<OperationClaim, bool>> filter)
        {
            using (YtuSchoolDbContext context = new YtuSchoolDbContext())
            {
                return context.OperationClaims.Any(filter);
            }
        }

        public OperationClaim Get(Expression<Func<OperationClaim, bool>> filter)
        {
            using (YtuSchoolDbContext context = new YtuSchoolDbContext())
            {
                return context.OperationClaims.FirstOrDefault(filter);
            }
        }

        public OperationClaim Add(OperationClaim entity)
        {
            using (YtuSchoolDbContext context = new YtuSchoolDbContext())
            {
                context.OperationClaims.Add(entity);
                context.SaveChanges();

                return entity;
            }
        }

        public OperationClaim Update(OperationClaim entity)
        {
            using (YtuSchoolDbContext context = new YtuSchoolDbContext())
            {
                context.OperationClaims.Update(entity);
                context.SaveChanges();

                return entity;
            }
        }

        public bool Remove(OperationClaim entity)
        {
            using (YtuSchoolDbContext context = new YtuSchoolDbContext())
            {
                context.OperationClaims.Remove(entity);
                context.SaveChanges();

                return true;
            }
        }

        public List<OperationClaim> GetList(Expression<Func<OperationClaim, bool>>? filter = null)
        {
            using (YtuSchoolDbContext context = new YtuSchoolDbContext())
            {
                if (filter==null)
                {
                    return context.OperationClaims.ToList();
                }
                else
                {
                    return context.OperationClaims.Where(filter).ToList();
                }
            }
        }
    }
}

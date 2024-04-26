using DataAccess.Dal.Abstract;
using DataAccess.EfDbContext.Obs;
using Entities.ObsEntities;
using System.Linq.Expressions;
using Entities.CommonEntities;

namespace DataAccess.Dal.Concrete
{
    public class UserOperationClaimDal : IUserOperationClaimDal
    {
        public bool Any(Expression<Func<UserOperationClaim, bool>> filter)
        {
            using (YtuSchoolDbContext context = new YtuSchoolDbContext())
            {
                return context.UserOperationClaims.Any(filter);
            }
        }

        public UserOperationClaim Get(Expression<Func<UserOperationClaim, bool>> filter)
        {
            using (YtuSchoolDbContext context = new YtuSchoolDbContext())
            {
                return context.UserOperationClaims.FirstOrDefault(filter);
            }
        }

        public UserOperationClaim Add(UserOperationClaim entity)
        {
            using (YtuSchoolDbContext context = new YtuSchoolDbContext())
            {
                context.UserOperationClaims.Add(entity);
                context.SaveChanges();

                return entity;
            }
        }

        public UserOperationClaim Update(UserOperationClaim entity)
        {
            using (YtuSchoolDbContext context = new YtuSchoolDbContext())
            {
                context.UserOperationClaims.Update(entity);
                context.SaveChanges();

                return entity;
            }
        }

        public bool Remove(UserOperationClaim entity)
        {
            using (YtuSchoolDbContext context = new YtuSchoolDbContext())
            {
                context.UserOperationClaims.Remove(entity);
                context.SaveChanges();

                return true;
            }
        }

        public List<UserOperationClaim> GetList(Expression<Func<UserOperationClaim, bool>>? filter = null)
        {
            using (YtuSchoolDbContext context = new YtuSchoolDbContext())
            {
                if (filter==null)
                {
                    return context.UserOperationClaims.ToList();
                }
                else
                {
                    return context.UserOperationClaims.Where(filter).ToList();
                }
            }
        }
    }
}

using DataAccess.Dal.Abstract;
using DataAccess.EfDbContext.Obs;
using Entities.ObsEntities;
using System.Linq.Expressions;
using Entities.CommonEntities;

namespace DataAccess.Dal.Concrete
{
    public class UserDal : IUserDal
    {
        public bool Any(Expression<Func<User, bool>> filter)
        {
            using (YtuSchoolDbContext context = new YtuSchoolDbContext())
            {
                return context.Users.Any(filter);
            }
        }

        public User Get(Expression<Func<User, bool>> filter)
        {
            using (YtuSchoolDbContext context = new YtuSchoolDbContext())
            {
                return context.Users.FirstOrDefault(filter);
            }
        }

        public User Add(User entity)
        {
            using (YtuSchoolDbContext context = new YtuSchoolDbContext())
            {
                context.Users.Add(entity);
                context.SaveChanges();

                return entity;
            }
        }

        public User Update(User entity)
        {
            using (YtuSchoolDbContext context = new YtuSchoolDbContext())
            {
                context.Users.Update(entity);
                context.SaveChanges();

                return entity;
            }
        }

        public bool Remove(User entity)
        {
            using (YtuSchoolDbContext context = new YtuSchoolDbContext())
            {
                context.Users.Remove(entity);
                context.SaveChanges();

                return true;
            }
        }

        public List<User> GetList(Expression<Func<User, bool>>? filter = null)
        {
            using (YtuSchoolDbContext context = new YtuSchoolDbContext())
            {
                if (filter==null)
                {
                    return context.Users.ToList();
                }
                else
                {
                    return context.Users.Where(filter).ToList();
                }
            }
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            using (YtuSchoolDbContext context = new YtuSchoolDbContext())
            {
                return context.Users!.FirstOrDefault(p=>p.EMail==email && p.Password ==password)!;
            }
        }

        public List<OperationClaim> GetUserOperationClaims(int userId)
        {
            using (YtuSchoolDbContext context = new YtuSchoolDbContext())
            {
                return context.UserOperationClaims!
                    .Where(p => p.UserId == userId)
                    .Select(p=>p.OperationClaim)
                    .ToList()!;
            }
        }
    }
}

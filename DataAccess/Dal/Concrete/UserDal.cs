using DataAccess.Dal.Abstract;
using DataAccess.EfDbContext.Obs;
using Entities.ObsEntities;
using System.Linq.Expressions;
using Entities.CommonEntities;
using Core.Repositories.Ef.DataAccess;

namespace DataAccess.Dal.Concrete
{
    public class UserDal : EfRepositoryBase<User, YtuSchoolDbContext>, IUserDal
    {
        public User GetUserByEmailAndPassword(string email, string password)
        {
            using (YtuSchoolDbContext context = new YtuSchoolDbContext())
            {
                return context.Users!.FirstOrDefault(p => p.EMail == email && p.Password == password)!;
            }
        }

        public List<OperationClaim> GetUserOperationClaims(int userId)
        {
            using (YtuSchoolDbContext context = new YtuSchoolDbContext())
            {
                return context.UserOperationClaims!
                    .Where(p => p.UserId == userId)
                    .Select(p => p.OperationClaim)
                    .ToList()!;
            }
        }
    }
}

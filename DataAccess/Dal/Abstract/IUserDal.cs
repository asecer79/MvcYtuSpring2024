using Core.Repositories.CommonInterfaces;
using Entities.CommonEntities;

namespace DataAccess.Dal.Abstract
{
    public interface IUserDal: IRepositoryBase<User>
    {
        //...special methods for User.

        User GetUserByEmailAndPassword(string email, string password);
        List<OperationClaim> GetUserOperationClaims(int userId);
    }
}

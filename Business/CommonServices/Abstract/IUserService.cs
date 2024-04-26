using Business.Services.Obs.Abstract.CommonInterfaces;
using Entities.CommonEntities;
using Entities.ObsEntities;

namespace Business.CommonServices.Abstract
{
    public interface IUserService:ICommonDbOperations<User>
    {
        User GetUserByEmailAndPassword(string email, string password);
        List<OperationClaim> GetUserOperationClaims(int userId);

    }
}

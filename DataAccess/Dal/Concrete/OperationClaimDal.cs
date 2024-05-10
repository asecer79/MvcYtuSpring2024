using DataAccess.Dal.Abstract;
using DataAccess.EfDbContext.Obs;
using Entities.ObsEntities;
using System.Linq.Expressions;
using Entities.CommonEntities;
using Core.Repositories.Ef.DataAccess;

namespace DataAccess.Dal.Concrete
{
    public class OperationClaimDal : EfRepositoryBase<OperationClaim, YtuSchoolDbContext>, IOperationClaimDal
    {
 
    }
}

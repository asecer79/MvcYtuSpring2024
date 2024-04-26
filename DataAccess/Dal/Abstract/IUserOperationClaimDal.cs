using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dal.CommonOperations;
using Entities.CommonEntities;
using Entities.ObsEntities;

namespace DataAccess.Dal.Abstract
{
    public interface IUserOperationClaimDal:ICommonDal<UserOperationClaim>
    {
        //...special methods for UserOperationClaim.
    }
}

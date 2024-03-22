using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dal.CommonOperations;
using Entities.ObsEntities;

namespace DataAccess.Dal.Abstract
{
    public interface IFacultyDal:ICommonDal<Faculty>
    {
        //...special methods for faculty.
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Services.Obs.Abstract.CommonInterfaces;
using Entities.ObsEntities;

namespace Business.Services.Obs.Abstract
{
    public interface IDepartmentService:ICommonDbOperations<Department>
    {
    }
}

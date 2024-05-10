using DataAccess.Dal.Abstract;
using DataAccess.EfDbContext.Obs;
using Entities.ObsEntities;
using System.Linq.Expressions;
using Core.Repositories.Ef.DataAccess;

namespace DataAccess.Dal.Concrete
{
    public class FacultyDal:EfRepositoryBase<Faculty,YtuSchoolDbContext>, IFacultyDal
    {
       
    }
}

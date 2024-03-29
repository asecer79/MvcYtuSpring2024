using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Business.Services.Obs.Abstract;
using DataAccess.Dal.Abstract;
using Entities.ObsEntities;

namespace Business.Services.Obs.Concrete
{
    public class DepartmentService:IDepartmentService
    {
        private readonly IDepartmentDal _departmentDal;

        public DepartmentService(IDepartmentDal departmentDal)
        {
            _departmentDal = departmentDal;
        }

        public bool Any(Expression<Func<Department, bool>> filter)
        {
            return _departmentDal.Any(filter);
        }

        public Department Get(Expression<Func<Department, bool>> filter)
        {
            return _departmentDal.Get(filter);
        }

        public Department Add(Department entity)
        {
            return _departmentDal.Add(entity);
        }

        public Department Update(Department entity)
        {
            return _departmentDal.Update(entity);
        }

        public bool Remove(Department entity)
        {
            return _departmentDal.Remove(entity);
        }

        public List<Department> GetList(Expression<Func<Department, bool>>? filter = null)
        {
            return _departmentDal.GetList(filter);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dal.Abstract;
using DataAccess.EfDbContext.Obs;
using Entities.ObsEntities;

namespace DataAccess.Dal.Concrete
{
    public class DepartmentDal : IDepartmentDal
    {
        public bool Any(Expression<Func<Department, bool>> filter)
        {
            using (YtuSchoolDbContext context = new YtuSchoolDbContext())
            {
                return context.Departments.Any(filter);
            }
        }

        public Department Get(Expression<Func<Department, bool>> filter )
        {
            using (YtuSchoolDbContext context = new YtuSchoolDbContext())
            {
                return context.Departments.FirstOrDefault(filter);
            }
        }

        public Department Add(Department entity)
        {
            using (YtuSchoolDbContext context = new YtuSchoolDbContext())
            {
                context.Departments.Add(entity);
                context.SaveChanges();

                return entity;
            }
        }

        public Department Update(Department entity)
        {
            using (YtuSchoolDbContext context = new YtuSchoolDbContext())
            {
                context.Departments.Update(entity);
                context.SaveChanges();

                return entity;
            }
        }

        public bool Remove(Department entity)
        {
            using (YtuSchoolDbContext context = new YtuSchoolDbContext())
            {
                context.Departments.Remove(entity);
                context.SaveChanges();

                return true;
            }
        }

        public List<Department> GetList(Expression<Func<Department, bool>>? filter = null)
        {
            using (YtuSchoolDbContext context = new YtuSchoolDbContext())
            {
                if (filter == null)
                {
                    return context.Departments.ToList();
                }
                else
                {
                    return context.Departments.Where(filter).ToList();
                }

            }
        }
    }
}

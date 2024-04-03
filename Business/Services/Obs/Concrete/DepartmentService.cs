using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Business.Services.Obs.Abstract;
using Caching.Abstract;
using DataAccess.Dal.Abstract;
using DataAccess.Dal.Concrete;
using Entities.ObsEntities;

namespace Business.Services.Obs.Concrete
{
    public class DepartmentService:IDepartmentService
    {
        private readonly IDepartmentDal _departmentDal;

        private ICacheProvider cacheProvider;

        private HashSet<string> keys = new HashSet<string>();

        public DepartmentService(IDepartmentDal departmentDal, ICacheProvider cacheProvider)
        {
            _departmentDal = departmentDal;
            this.cacheProvider = cacheProvider;
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
            foreach (var key in keys)
            {
                cacheProvider.Remove(key);
            }

            return _departmentDal.Add(entity);
        }

        public Department Update(Department entity)
        {
            foreach (var key in keys)
            {
                cacheProvider.Remove(key);
            }
            return _departmentDal.Update(entity);
        }

        public bool Remove(Department entity)
        {
            foreach (var key in keys)
            {
                cacheProvider.Remove(key);
            }
            return _departmentDal.Remove(entity);
        }

        public List<Department> GetList(Expression<Func<Department, bool>>? filter = null)
        {
            var baseKey = "GetDepartmentList";


            if (filter != null)
            {
                var filterString = filter.ToString();

                // SHA256 kullanarak bir hash oluştur
                using (var sha256 = SHA256.Create())
                {
                    var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(filterString));
                    var hashString = BitConverter.ToString(hashBytes).Replace("-", "");

                    baseKey = $"{baseKey}_{hashString}";

                    keys.Add(baseKey);

                }
            }

            if (!cacheProvider.Any(baseKey))
            {
                var result = _departmentDal.GetList(filter);
                cacheProvider.Set(baseKey, result, TimeSpan.FromSeconds(6000));
                return result;
            }

            return cacheProvider.Get<List<Department>>(baseKey)!;

        }
    }
}

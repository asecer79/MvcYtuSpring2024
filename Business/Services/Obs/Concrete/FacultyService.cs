using Business.Services.Obs.Abstract;
using DataAccess.Dal.Abstract;
using Entities.ObsEntities;
using System.Linq.Expressions;
using System.Security.Cryptography;
using Caching.Abstract;
using System.Text;

namespace Business.Services.Obs.Concrete
{
    public class FacultyService(IFacultyDal facultyDal, ICacheProvider cacheProvider) : IFacultyService
    {
        private HashSet<string> keys = new HashSet<string>();

        public bool Any(Expression<Func<Faculty, bool>> filter)
        {
            return facultyDal.Any(filter);
        }

        public Faculty Get(Expression<Func<Faculty, bool>> filter)
        {
            return facultyDal.Get(filter);
        }

        public Faculty Add(Faculty entity)
        {
            
            foreach (var key in keys)
            {
                cacheProvider.Remove(key);
            }
            return facultyDal.Add(entity);
        }

        public Faculty Update(Faculty entity)
        {
            foreach (var key in keys)
            {
                cacheProvider.Remove(key);
            }
            return facultyDal.Update(entity);
        }

        public bool Remove(Faculty entity)
        {
              foreach (var key in keys)
            {
                cacheProvider.Remove(key);
            }
            return facultyDal.Remove(entity);
        }

        public List<Faculty> GetList(Expression<Func<Faculty, bool>>? filter = null)
        {
            var baseKey = "GetFacultyList";

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
                var result = facultyDal.GetList(filter);
                cacheProvider.Set(baseKey, result, TimeSpan.FromSeconds(6000));
                return result;
            }

            return cacheProvider.Get<List<Faculty>>(baseKey)!;
        }
    }
}

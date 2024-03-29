using Business.Services.Obs.Abstract;
using DataAccess.Dal.Abstract;
using Entities.ObsEntities;
using System.Linq.Expressions;
using Caching.Abstract;

namespace Business.Services.Obs.Concrete
{
    public class FacultyService(IFacultyDal facultyDal, ICacheProvider cacheProvider) : IFacultyService
    {

        public string GetListKey { get; set; } = "";

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
            cacheProvider.Remove(GetListKey);
            return facultyDal.Add(entity);
        }

        public Faculty Update(Faculty entity)
        {
            cacheProvider.Remove(GetListKey);
            return facultyDal.Update(entity);
        }

        public bool Remove(Faculty entity)
        {
            cacheProvider.Remove(GetListKey);
            return facultyDal.Remove(entity);
        }

        public List<Faculty> GetList(Expression<Func<Faculty, bool>>? filter = null)
        {
            GetListKey = $"GetFacultyList";

            if (!cacheProvider.Any(GetListKey))
            {
                var result= facultyDal.GetList(filter);
                cacheProvider.Set(GetListKey,result,TimeSpan.FromSeconds(6000));

                return result;
            }

            return cacheProvider.Get<List<Faculty>>(GetListKey)!;
        }
    }
}

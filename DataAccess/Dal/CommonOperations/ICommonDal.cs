using Entities.ObsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dal.CommonOperations
{
    public interface ICommonDal<T> where T : class, new()
    {
        bool Any(Expression<Func<T, bool>> filter );
        T Get(Expression<Func<T, bool>> filter );
        T Add(T entity);
        T Update(T entity);
        bool Remove(T entity);
        List<T> GetList(Expression<Func<T,bool>>? filter = null);
    }
}

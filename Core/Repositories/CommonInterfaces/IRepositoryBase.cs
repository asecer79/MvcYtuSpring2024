using System.Linq.Expressions;

namespace Core.Repositories.CommonInterfaces
{
    public interface IRepositoryBase<T> where T : class,IEntityBase, new()
    {
        public bool Any(Expression<Func<T, bool>> filter);

        public T Get(Expression<Func<T, bool>> filter);

        public T Add(T entity);

        public T Update(T entity);

        public bool Remove(T entity);

        public List<T> GetList(Expression<Func<T, bool>>? filter = null);
    }
}

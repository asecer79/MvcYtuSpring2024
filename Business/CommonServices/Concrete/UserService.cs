using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using Business.CommonServices.Abstract;
using Caching.Abstract;
using DataAccess.Dal.Abstract;
using Entities.CommonEntities;

namespace Business.CommonServices.Concrete
{
    public class UserService(IUserDal userDal, ICacheProvider cacheProvider) : IUserService
    {
        private HashSet<string> keys = new HashSet<string>();

        public bool Any(Expression<Func<User, bool>> filter)
        {
            return userDal.Any(filter);
        }

        public User Get(Expression<Func<User, bool>> filter)
        {
            return userDal.Get(filter);
        }

        public User Add(User entity)
        {

            foreach (var key in keys)
            {
                cacheProvider.Remove(key);
            }
            return userDal.Add(entity);
        }

        public User Update(User entity)
        {
            foreach (var key in keys)
            {
                cacheProvider.Remove(key);
            }
            return userDal.Update(entity);
        }

        public bool Remove(User entity)
        {
            foreach (var key in keys)
            {
                cacheProvider.Remove(key);
            }
            return userDal.Remove(entity);
        }

        public List<User> GetList(Expression<Func<User, bool>>? filter = null)
        {
            var baseKey = "GetUserList";

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
                var result = userDal.GetList(filter);
                cacheProvider.Set(baseKey, result, TimeSpan.FromSeconds(6000));
                return result;
            }

            return cacheProvider.Get<List<User>>(baseKey)!;
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            return userDal.GetUserByEmailAndPassword(email, password);
        }

        public List<OperationClaim> GetUserOperationClaims(int userId)
        {
            return userDal.GetUserOperationClaims(userId);
        }
    }
}

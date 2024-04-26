using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using Business.CommonServices.Abstract;
using Caching.Abstract;
using DataAccess.Dal.Abstract;
using Entities.CommonEntities;

namespace Business.CommonServices.Concrete
{
    public class UserOperationClaimService(IUserOperationClaimDal userOperationClaimDal, ICacheProvider cacheProvider) : IUserOperationClaimService
    {
        private HashSet<string> keys = new HashSet<string>();

        public bool Any(Expression<Func<UserOperationClaim, bool>> filter)
        {
            return userOperationClaimDal.Any(filter);
        }

        public UserOperationClaim Get(Expression<Func<UserOperationClaim, bool>> filter)
        {
            return userOperationClaimDal.Get(filter);
        }

        public UserOperationClaim Add(UserOperationClaim entity)
        {
            
            foreach (var key in keys)
            {
                cacheProvider.Remove(key);
            }
            return userOperationClaimDal.Add(entity);
        }

        public UserOperationClaim Update(UserOperationClaim entity)
        {
            foreach (var key in keys)
            {
                cacheProvider.Remove(key);
            }
            return userOperationClaimDal.Update(entity);
        }

        public bool Remove(UserOperationClaim entity)
        {
              foreach (var key in keys)
            {
                cacheProvider.Remove(key);
            }
            return userOperationClaimDal.Remove(entity);
        }

        public List<UserOperationClaim> GetList(Expression<Func<UserOperationClaim, bool>>? filter = null)
        {
            var baseKey = "GetUserOperationClaimList";

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
                var result = userOperationClaimDal.GetList(filter);
                cacheProvider.Set(baseKey, result, TimeSpan.FromSeconds(6000));
                return result;
            }

            return cacheProvider.Get<List<UserOperationClaim>>(baseKey)!;
        }
    }
}

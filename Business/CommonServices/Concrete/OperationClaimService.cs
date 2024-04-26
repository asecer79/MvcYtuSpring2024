using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using Business.CommonServices.Abstract;
using Caching.Abstract;
using DataAccess.Dal.Abstract;
using Entities.CommonEntities;

namespace Business.CommonServices.Concrete
{
    public class OperationClaimService(IOperationClaimDal operationClaimDal, ICacheProvider cacheProvider) : IOperationClaimService
    {
        private HashSet<string> keys = new HashSet<string>();

        public bool Any(Expression<Func<OperationClaim, bool>> filter)
        {
            return operationClaimDal.Any(filter);
        }

        public OperationClaim Get(Expression<Func<OperationClaim, bool>> filter)
        {
            return operationClaimDal.Get(filter);
        }

        public OperationClaim Add(OperationClaim entity)
        {
            
            foreach (var key in keys)
            {
                cacheProvider.Remove(key);
            }
            return operationClaimDal.Add(entity);
        }

        public OperationClaim Update(OperationClaim entity)
        {
            foreach (var key in keys)
            {
                cacheProvider.Remove(key);
            }
            return operationClaimDal.Update(entity);
        }

        public bool Remove(OperationClaim entity)
        {
              foreach (var key in keys)
            {
                cacheProvider.Remove(key);
            }
            return operationClaimDal.Remove(entity);
        }

        public List<OperationClaim> GetList(Expression<Func<OperationClaim, bool>>? filter = null)
        {
            var baseKey = "GetOperationClaimList";

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
                var result = operationClaimDal.GetList(filter);
                cacheProvider.Set(baseKey, result, TimeSpan.FromSeconds(6000));
                return result;
            }

            return cacheProvider.Get<List<OperationClaim>>(baseKey)!;
        }
    }
}

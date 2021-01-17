using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CteleportTechAssignment.Cache.Factory
{
    public interface ICacheManager
    {
        T Get<T>(string cacheKey) where T : class;
        bool Add<T>(string cacheKey, T data) where T : class;
        Task<T> GetAsync<T>(string cacheKey) where T : class;
        Task<bool> AddAsync<T>(string cacheKey, T data) where T : class;
        bool Remove(string cacheKey);
        bool Clear();
    }
}

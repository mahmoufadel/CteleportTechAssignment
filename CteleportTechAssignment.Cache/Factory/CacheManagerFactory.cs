using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CteleportTechAssignment.Cache.Factory
{
   
    public class CacheManagerFactory
    {
        private static IConfiguration _config;
        private static CacheType CacheType
        {
            get
            {
                return _config["CacheType"] != null ? (CacheType)Enum.Parse(typeof(CacheType), _config["CacheType"]) : CacheType.Memory;
            }
        }

        public static ICacheManager GetCacheManager(IConfiguration config) 
        {
            _config = config;
            return GetCacheManager();
        }

        public static ICacheManager GetCacheManager() => CacheType switch
        {
            CacheType.Redis => new RedisCacheManager(_config),
            CacheType.Memory => new MemoryCacheManager(),
            _ => null
        };

    }
}

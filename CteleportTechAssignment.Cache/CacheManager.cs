using CteleportTechAssignment.Cache.Factory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace CteleportTechAssignment.Cache
{
    public interface ICacheService
    {
        public abstract ICacheManager Current();
    }
    public class CacheManager: ICacheService
    {
        private readonly IConfiguration _config;
        public CacheManager(IConfiguration config) 
        {
            _config = config;
        }
        private static ICacheManager cacheManager = null;
        private static object _lockObj = new object();

        public  ICacheManager Current()
        {

            lock (_lockObj)
            {
                cacheManager = cacheManager != null ? cacheManager : CacheManagerFactory.GetCacheManager(_config);
                return cacheManager;
            }

        }		
	}
}

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CteleportTechAssignment.Cache.Factory
{
    public class RedisCacheManager : ICacheManager
    {
        private  readonly Lazy<ConnectionMultiplexer> redisConnectionsMaster;
        public  ConnectionMultiplexer ConnectionMaster => redisConnectionsMaster.Value;
        public  IDatabase RedisCacheDatabaseMaster => ConnectionMaster.GetDatabase();


        private  readonly Lazy<ConnectionMultiplexer> redisConnectionsSlave;
        public  ConnectionMultiplexer ConnectionSlave => redisConnectionsSlave.Value;
        public  IDatabase RedisCacheDatabaseSlave => ConnectionSlave.GetDatabase();
        IConfiguration _config;
        public RedisCacheManager(IConfiguration config)
        {
            _config = config;
            var configurationOptionsMaster = new ConfigurationOptions
            {
                EndPoints = { config["RedisMaster"] },
                Password = config["RedisMasterPassword"],
                Ssl = bool.Parse(config["RedisIsSSL"]),
                ConnectTimeout = 10000,
                SyncTimeout = 10000,
                ResponseTimeout = 10000,
                AllowAdmin = true
            };

            var configurationOptionsSlave = new ConfigurationOptions
            {
                EndPoints = { config["RedisSlave"] },
                Password = config["RedisSlavePassword"],
                Ssl = bool.Parse(config["RedisIsSSL"]),
                ConnectTimeout = 10000,
                SyncTimeout = 10000,
                ResponseTimeout = 10000,
                AllowAdmin = true
            };

            redisConnectionsMaster = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configurationOptionsMaster));
            redisConnectionsSlave = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configurationOptionsSlave));
        }

        public T Get<T>(string cacheKey) where T : class
        {
            if (RedisCacheDatabaseSlave.KeyExists(cacheKey))
            {
                var cahcedData = RedisCacheDatabaseSlave.StringGet(cacheKey);

                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                };

                return JsonConvert.DeserializeObject<T>(cahcedData, settings);
            }

            return null;
        }

        public async Task<T> GetAsync<T>(string cacheKey) where T : class
        {
            if (RedisCacheDatabaseSlave.KeyExists(cacheKey))
            {
                var cahcedData = await RedisCacheDatabaseSlave.StringGetAsync(cacheKey);

                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                };

                return JsonConvert.DeserializeObject<T>(cahcedData, settings);
            }

            return null;
        }

        public bool Add<T>(string cacheKey, T data) where T : class
        {
            try
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                };

                RedisCacheDatabaseMaster.StringSet(cacheKey, JsonConvert.SerializeObject(data, Formatting.None, settings));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddAsync<T>(string cacheKey, T data) where T : class
        {
            try
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                };

                await RedisCacheDatabaseMaster.StringSetAsync(cacheKey, JsonConvert.SerializeObject(data, Formatting.None, settings));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Remove(string cacheKey)
        {
            RedisCacheDatabaseMaster.KeyDelete(cacheKey);
            return true;
        }

        public bool Clear()
        {
            try
            {
                var server = ConnectionMaster.GetServer(_config["RedisMaster"]);
                server.FlushDatabase();
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}

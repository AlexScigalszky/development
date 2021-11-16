using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace CBV_SB_Shared.Redis
{
    public class RedisWrapper : IRedisWrapper
    {
        private readonly IDatabase _database;

        public RedisWrapper(IDatabase database)
        {
            _database = database;

            /// ¿se puede configurar para que use JSON o BSON?
        }

        /// <summary>
        /// Fet a value from redis for especific key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string key)
        {
            return GetFromRedis(key);
        }

        /// <summary>
        /// Fet a value from redis for especific key but it isn't in redis, the function parameter will be excuted and return thats result
        /// </summary>
        /// <param name="key"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        public async Task<string> Get(string key, Func<Task<string>> function)
        {
            var value = string.Empty;
            try
            {
                value = GetFromRedis(key);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            if (IsNull(value))
            {
                return await function();
            }
            return value;
        }

        /// <summary>
        /// Fet a value from redis for especific key but it isn't in redis, the function parameter will be excuted and return thats result
        /// </summary>
        /// <param name="key"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        public string Get(string key, Func<string> function)
        {
            var value = string.Empty;
            try
            {
                value = GetFromRedis(key);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            if (IsNull(value))
            {
                return function();
            }
            return value;
        }

        public T Get<T>(string key, Func<T> function)
        {
            string value = string.Empty;
            try
            {
                value = GetFromRedis(key);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            if (IsNull(value))
            {
                return function();
            }
            return JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// Set the a value int redis for especific key and save it using the the function parameter. 
        /// Return the saved value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        public string Put(string key, string value, Func<string, string> function)
        {
            _database.StringSet(key, value);
            function(value);
            return value;
        }

        public T Put<T>(string key, T value, Func<T, string> function)
        {
            var valueStr = JsonConvert.SerializeObject(value);
            _database.StringSet(key, valueStr);
            function(value);
            return value;
        }

        private string GetFromRedis(string key)
        {
            return _database.StringGet(key);
        }

        private static bool IsNull(string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}

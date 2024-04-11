using System;

namespace Example.Redis
{
    public interface IRedisWrapper
    {
        public string Get(string key);
        public string Get(string key, Func<string> function);
        public T Get<T>(string key, Func<T> function);
        public string Put(string key, string value, Func<string, string> function);
        public T Put<T>(string key, T value, Func<T, string> function);
    }
}

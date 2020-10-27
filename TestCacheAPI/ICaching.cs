using System;
using System.Threading.Tasks;
using static TestCacheAPI.Enumerations;

namespace TestCacheAPI
{
    public interface ICaching
    {
        #region Sync Methods
        public string GetStringValue(string key, Func<string> func);
        public string GetStringValue(string key, DateTime expire, Func<string> func);
        public T GetObjectValue<T>(string key, CacheDataType cacheType, Func<T> func);
        public T GetObjectValue<T>(string key, CacheDataType cacheType, DateTime expire, Func<T> func);
        public void RemoveValue(string key);
        public void RefreshValue(string key);
        #endregion

        #region Async Methods
        public Task<string> GetStringValueAsync(string key, Func<Task<string>> func);
        public Task<string> GetStringValueAsync(string key, DateTime expire, Func<Task<string>> func);
        public Task<T> GetObjectValueAsync<T>(string key, CacheDataType cacheType, Func<Task<T>> func);
        public Task<T> GetObjectValueAsync<T>(string key, CacheDataType cacheType, DateTime expire, Func<Task<T>> func);
        public Task RemoveValueAsync(string key);
        public Task RefreshValueAsync(string key);
        #endregion
    }
}

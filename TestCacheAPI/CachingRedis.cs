using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TestCacheAPI.Enumerations;

namespace TestCacheAPI
{
    public class CachingRedis : ICaching
    {
        private readonly IDistributedCache _distributedCache;

        public CachingRedis(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        private byte[] BytesValue { get; set; }
        private string StringValue { get; set; }

        public string GetStringValue(string key, Func<string> func)
        {
            StringValue = _distributedCache.GetString(key);

            if (!string.IsNullOrEmpty(StringValue))
            {
                StringValue = func();
                _distributedCache.SetString(key, StringValue);
            }

            return StringValue;
        }

        public string GetStringValue(string key, DateTime expire, Func<string> func)
        {
            StringValue = _distributedCache.GetString(key);

            if (!string.IsNullOrEmpty(StringValue))
            {
                var optionsCache = new DistributedCacheEntryOptions()
                                    .SetAbsoluteExpiration(expire);

                StringValue = func();
                _distributedCache.SetString(key, StringValue, optionsCache);
            }

            return StringValue;
        }

        public T GetObjectValue<T>(string key, CacheDataType cacheType, Func<T> func)
        {
            T data;

            if (cacheType == CacheDataType.BytesData)
            {
                BytesValue = _distributedCache.Get(key);

                if (BytesValue != null)
                {
                    string stringValue = Encoding.UTF8.GetString(BytesValue);
                    data = JsonConvert.DeserializeObject<T>(stringValue);
                }
                else
                {
                    data = func();
                    string jsonObject = JsonConvert.SerializeObject(data);
                    BytesValue = Encoding.UTF8.GetBytes(jsonObject);
                    _distributedCache.Set(key, BytesValue);
                }
            }
            else
            {
                StringValue = _distributedCache.GetString(key);

                if (!string.IsNullOrEmpty(StringValue))
                {
                    data = JsonConvert.DeserializeObject<T>(StringValue);
                }
                else
                {
                    data = func();
                    StringValue = JsonConvert.SerializeObject(data);
                    _distributedCache.SetString(key, StringValue);
                }
            }

            return data;
        }

        public T GetObjectValue<T>(string key, CacheDataType cacheType, DateTime expire, Func<T> func)
        {
            T data;

            if (cacheType == CacheDataType.BytesData)
            {
                BytesValue = _distributedCache.Get(key);

                if (BytesValue != null)
                {
                    string stringValue = Encoding.UTF8.GetString(BytesValue);
                    data = JsonConvert.DeserializeObject<T>(stringValue);
                }
                else
                {
                    var optionsCache = new DistributedCacheEntryOptions()
                                    .SetAbsoluteExpiration(expire);

                    data = func();
                    string jsonObject = JsonConvert.SerializeObject(data);
                    BytesValue = Encoding.UTF8.GetBytes(jsonObject);
                    _distributedCache.Set(key, BytesValue, optionsCache);
                }
            }
            else
            {
                StringValue = _distributedCache.GetString(key);

                if (!string.IsNullOrEmpty(StringValue))
                {
                    data = JsonConvert.DeserializeObject<T>(StringValue);
                }
                else
                {
                    var optionsCache = new DistributedCacheEntryOptions()
                                    .SetAbsoluteExpiration(expire);

                    data = func();
                    StringValue = JsonConvert.SerializeObject(data);
                    _distributedCache.SetString(key, StringValue, optionsCache);
                }
            }

            return data;
        }
        public void RemoveValue(string key)
        {
            _distributedCache.Remove(key);
        }

        public void RefreshValue(string key)
        {
            _distributedCache.Refresh(key);
        }

        public async Task<string> GetStringValueAsync(string key, Func<Task<string>> func)
        {
            StringValue = _distributedCache.GetString(key);

            if (!string.IsNullOrEmpty(StringValue))
            {
                StringValue = await func();
                _distributedCache.SetString(key, StringValue);
            }

            return StringValue;
        }

        public async Task<string> GetStringValueAsync(string key, DateTime expire, Func<Task<string>> func)
        {
            StringValue = _distributedCache.GetString(key);

            if (!string.IsNullOrEmpty(StringValue))
            {
                var optionsCache = new DistributedCacheEntryOptions()
                                    .SetAbsoluteExpiration(expire);

                StringValue = await func();
                _distributedCache.SetString(key, StringValue, optionsCache);
            }

            return StringValue;
        }

        public async Task<T> GetObjectValueAsync<T>(string key, CacheDataType cacheType, Func<Task<T>> func)
        {
            T data;

            if (cacheType == CacheDataType.BytesData)
            {
                BytesValue = _distributedCache.Get(key);

                if (BytesValue != null)
                {
                    string stringValue = Encoding.UTF8.GetString(BytesValue);
                    data = JsonConvert.DeserializeObject<T>(stringValue);
                }
                else
                {
                    data = await func();
                    string jsonObject = JsonConvert.SerializeObject(data);
                    BytesValue = Encoding.UTF8.GetBytes(jsonObject);
                    _distributedCache.Set(key, BytesValue);
                }
            }
            else
            {
                StringValue = _distributedCache.GetString(key);

                if (!string.IsNullOrEmpty(StringValue))
                {
                    data = JsonConvert.DeserializeObject<T>(StringValue);
                }
                else
                {
                    data = await func();
                    StringValue = JsonConvert.SerializeObject(data);
                    _distributedCache.SetString(key, StringValue);
                }
            }

            return data;
        }

        public async Task<T> GetObjectValueAsync<T>(string key, CacheDataType cacheType, DateTime expire, Func<Task<T>> func)
        {
            T data;

            if (cacheType == CacheDataType.BytesData)
            {
                BytesValue = _distributedCache.Get(key);

                if (BytesValue != null)
                {
                    string stringValue = Encoding.UTF8.GetString(BytesValue);
                    data = JsonConvert.DeserializeObject<T>(stringValue);
                }
                else
                {
                    var optionsCache = new DistributedCacheEntryOptions()
                                    .SetAbsoluteExpiration(expire);

                    data = await func();
                    string jsonObject = JsonConvert.SerializeObject(data);
                    BytesValue = Encoding.UTF8.GetBytes(jsonObject);
                    _distributedCache.Set(key, BytesValue, optionsCache);
                }
            }
            else
            {
                StringValue = _distributedCache.GetString(key);

                if (!string.IsNullOrEmpty(StringValue))
                {
                    data = JsonConvert.DeserializeObject<T>(StringValue);
                }
                else
                {
                    var optionsCache = new DistributedCacheEntryOptions()
                                    .SetAbsoluteExpiration(expire);

                    data = await func();
                    StringValue = JsonConvert.SerializeObject(data);
                    _distributedCache.SetString(key, StringValue, optionsCache);
                }
            }

            return data;
        }

        public async Task RemoveValueAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);
        }

        public async Task RefreshValueAsync(string key)
        {
            await _distributedCache.RefreshAsync(key);
        }
    }
}

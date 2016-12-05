using System;
using System.Net.Http;
using Microsoft.Extensions.Caching.Memory;

namespace KingLion.WebUtils.HttpClientPool
{
    public class HttpClientPool:IHttpClientPool
    {
        /// <summary>
        /// 记录Base Uri及其保存时长
        /// </summary>
        private readonly IMemoryCache _cache;

        /// <summary>
        /// 缓存Key前缀
        /// </summary>
        private static string _cacheKeyPrefix = string.Empty;

        /// <summary>
        /// HttpClient.Timeout
        /// </summary>
        private static TimeSpan _httpConnectionTimeout= TimeSpan.FromSeconds(60);

        public HttpClientPool(IMemoryCache cache)
        {
            _cache = cache;
        }


        private static string GetCacheKey(string key) => _cacheKeyPrefix + key;

        
        /// <summary>
        /// 获取一个HttpClient实例
        /// </summary>
        /// <param name="baseUri">服务器基地址</param>
        /// <param name="slideExpiration">若设定，在指定的时间内没有使用baseUri指定的HttpClient实例将被销毁</param>
        /// <returns></returns>
        public HttpClient GetHttpClient(string baseUri,TimeSpan? slideExpiration)
        {
            var cacheKey = GetCacheKey(baseUri);

            var client = _cache.Get<HttpClient>(cacheKey);

            if (client != null) return client;

            client = new HttpClient
            {
                BaseAddress = new Uri(baseUri),
                Timeout = _httpConnectionTimeout
            };

            if (slideExpiration != null)
            {
                _cache.Set(cacheKey, client, new MemoryCacheEntryOptions
                {
                    SlidingExpiration = slideExpiration,
                    PostEvictionCallbacks =
                    {
                        new PostEvictionCallbackRegistration()
                        {
                            EvictionCallback = (key, value, reason, substate) =>
                            {
                                //过期时，销毁实例
                                try
                                {
                                    var evictClient = value as HttpClient;
                                    evictClient?.Dispose();
                                }
                                catch
                                {
                                    //ignore
                                }
                            }
                        }
                    }
                });
            }
            return client;
        }

        /// <summary>
        /// 配置全局设定
        /// </summary>
        /// <param name="timeout">HttpClient.Timeout</param>
        /// <param name="cacheKeyPrefix">缓存键的前缀</param>
        public void Config(TimeSpan? timeout=null,string cacheKeyPrefix=null)
        {
            if (timeout != null) _httpConnectionTimeout = timeout.Value;

            if(!string.IsNullOrEmpty(cacheKeyPrefix)) _cacheKeyPrefix = cacheKeyPrefix;
        }
    }
}

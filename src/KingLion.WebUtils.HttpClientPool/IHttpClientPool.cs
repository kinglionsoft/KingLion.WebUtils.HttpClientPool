using System;
using System.Net.Http;

namespace KingLion.WebUtils.HttpClientPool
{
    public interface IHttpClientPool
    {
        /// <summary>
        /// 获取一个HttpClient实例
        /// </summary>
        /// <param name="baseUri">服务器基地址</param>
        /// <param name="slideExpiration">若设定，在指定的时间内没有使用baseUri指定的HttpClient实例将被销毁</param>
        /// <returns></returns>
        HttpClient GetHttpClient(string baseUri, TimeSpan? slideExpiration);

        /// <summary>
        /// 配置全局设定
        /// </summary>
        /// <param name="timeout">HttpClient.Timeout</param>
        /// <param name="cacheKeyPrefix">缓存键的前缀</param>
        void Config(TimeSpan? timeout = null, string cacheKeyPrefix = null);
    }
}

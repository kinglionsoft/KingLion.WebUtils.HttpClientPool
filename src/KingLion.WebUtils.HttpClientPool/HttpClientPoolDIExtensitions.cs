using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace KingLion.WebUtils.HttpClientPool
{
    public static class HttpClientPoolDIExtensitions
    {
        /// <summary>
        /// 添加HttpClient池
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddHttpClientPool(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IHttpClientPool), typeof(HttpClientPool));
            return services;
        }

        /// <summary>
        /// 启用HttpClient池
        /// </summary>
        /// <param name="app"></param>
        /// <param name="timeout">HttpClient.Timeout</param>
        /// /// <param name="cacheKeyPrefix">缓存键的前缀</param>
        public static IApplicationBuilder UseHttpClientPool(this IApplicationBuilder app, TimeSpan? timeout = null, string cacheKeyPrefix = null)
        {
            app.ApplicationServices.GetService<IHttpClientPool>().Config(timeout,cacheKeyPrefix);
            return app;
        }
    }
}
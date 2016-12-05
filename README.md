# KingLion.WebUtils.HttpClientPool

Asp .Net Core项目中，以服务基地址为标识的HttpClient池管理模块。
> 使用“池”来管理HttClient的原因，详见[https://news.cnblogs.com/n/553217/](https://news.cnblogs.com/n/553217/)、[http://www.cnblogs.com/dudu/p/csharp-httpclient-attention.html](http://www.cnblogs.com/dudu/p/csharp-httpclient-attention.html)。

### 安装

NuGet: 
```
Install-Package KingLion.WebUtils.HttpClientPool
```

### 用法

1. 启用Job Manager
    
    * Startup-> ConfigureServices：
    ```C#
    services.AddMemoryCache();
    services.AddHttpClientPool();
   ```
    * Startup-> Configure：
    ```C#
    app.UseHttpClientPool();
    ```
2. 添加作业
    * 注入IHttpClientPool；
    * 添加作业：
    ```C#
   var client=_httpClientPool.GetClient("http://xxx.com",TimeSpan.FromHours(2));
    ```
# KingLion.WebUtils.HttpClientPool

Asp .Net Core��Ŀ�У��Է������ַΪ��ʶ��HttpClient�ع���ģ�顣
> ʹ�á��ء�������HttClient��ԭ�����[https://news.cnblogs.com/n/553217/](https://news.cnblogs.com/n/553217/)��[http://www.cnblogs.com/dudu/p/csharp-httpclient-attention.html](http://www.cnblogs.com/dudu/p/csharp-httpclient-attention.html)��

### ��װ

NuGet: 
```
Install-Package KingLion.WebUtils.HttpClientPool
```

### �÷�

1. ����Job Manager
    
    * Startup-> ConfigureServices��
    ```C#
    services.AddMemoryCache();
    services.AddHttpClientPool();
   ```
    * Startup-> Configure��
    ```C#
    app.UseHttpClientPool();
    ```
2. �����ҵ
    * ע��IHttpClientPool��
    * �����ҵ��
    ```C#
   var client=_httpClientPool.GetClient("http://xxx.com",TimeSpan.FromHours(2));
    ```
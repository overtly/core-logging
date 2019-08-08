# Logging

# 项目层次说明

> Overt.Core.Logging v1.0.0


<a name="e0bf0e74"></a>
#### 1. 项目目录

```csharp
|-dllconfigs                                    配置文件保存
|
|-Exless                                        Exceptionless
|-|-ExlessLogger.cs                        	实现ILogger
|-|-ExlessLoggerProvider.cs                     实现ILoggerProvider
|
|-NLog                                    	NLog
|-|-NLogOptions.cs                       	NLog配置注入类
|
|-ServiceCollectionExtensions.cs            	netcore注入
```

<a name="d81d4a08"></a>
#### 2. 版本及支持
> - Nuget版本：V 1.0.0
> - 框架支持： netcoreapp2.2



<a name="7aaf7e9e"></a>
#### 3. 项目依赖
> - netcoreapp2.2

```csharp
NLog.Extensions.Logging 1.2.1
System.Data.SqlClient 4.4.0
    
Exceptionless.AspNetCore 4.3.2012
Microsoft.Extensions.DependencyInjection 2.2.0
Microsoft.Extensions.Logging 2.2.0
```

<a name="ecff77a8"></a>
### 使用
<a name="ee74eed7"></a>
#### 1. Nuget包引用
```csharp
Install-Package Overt.Core.Logging -Version 1.0.0
```

<a name="b3312061"></a>
#### 2. 使用
<a name="c014a30f"></a>
##### （1）NLog
> - 添加配置文件 /dllconfigs/Overt.Core.Logging.dll.config


> - 代码中使用

```csharp
// ConsoleApplication
 var host = new HostBuilder()
     .ConfigureLogging(ConfigureLogging)    //注入日志组件
     .ConfigureServices(ConfigureServices)  //提供通用注入配置
     .Build();
host.Run();



/// <summary>
/// 添加ILoggerProvider
/// </summary>
/// <param name="context"></param>
/// <param name="loggingBuilder"></param>
private static void ConfigureLogging(HostBuilderContext context, ILoggingBuilder loggingBuilder)
{
    loggingBuilder.AddConfiguration(context.Configuration.GetSection("Logging"));
    loggingBuilder.AddNLogLogging();
}

// WebApplication
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc();
    services.AddNLogLogging();
}
```

<a name="53ILn"></a>
##### （2）Exceptionless
> - 配置文件中添加Exceptionless节点

```csharp
"Exceptionless": {
  "ServerUrl": "exless服务地址",
  "ApiKey": "项目apikey"
}
```

> - 代码中使用


```csharp
// ConsoleApplication
var host = new HostBuilder()
    .Build();
host.Services.AddExlessLogging();
host.Run();



// WebApplication
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
	app.AddExlessLogging();
}

```

---



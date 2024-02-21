using Microsoft.Extensions.DependencyInjection;
using Tooark.Services;
using Tooark.Services.Factory;
using Tooark.Services.Interface;
using Tooark.Services.RabbitMQ;

namespace Tooark.Injections;

public static class TooarkDependencyInjection
{
  public static IServiceCollection AddTooarkServices(
    this IServiceCollection services,
    string rabbitMQHostname = "localhost",
    int rabbitMQPort = 5672,
    string rabbitMQUserName = "guest",
    string rabbitMQPassword = "guest")
  {
    services.AddHttpClientService();
    services.AddRabbitMQService(rabbitMQHostname, rabbitMQPort, rabbitMQUserName, rabbitMQPassword);

    return services;
  }

  public static IServiceCollection AddHttpClientService(this IServiceCollection services)
  {
    services.AddHttpClient();

    services.AddTransient<IHttpClientService>(provider =>
    {
      var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
      var httpClient = httpClientFactory.CreateClient();

      return HttpClientServiceFactory.Create(httpClient);
    });

    return services;
  }

  public static IServiceCollection AddRabbitMQService(
    this IServiceCollection services,
    string rabbitMQHostname = "localhost",
    int rabbitMQPort = 5672,
    string rabbitMQUserName = "guest",
    string rabbitMQPassword = "guest")
  {
    services.AddSingleton<RabbitMQConnectionService>(provider =>
    {
      return new RabbitMQConnectionService(rabbitMQHostname, rabbitMQPort, rabbitMQUserName, rabbitMQPassword);
    });

    services.AddSingleton<IRabbitMQService>(provider =>
    {
      var connService = provider.GetRequiredService<RabbitMQConnectionService>();
      return RabbitMQServiceFactory.Create(connService);
    });

    return services;
  }
}

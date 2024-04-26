using Microsoft.Extensions.DependencyInjection;
using Tooark.Factories;
using Tooark.Interfaces;
using Tooark.Services.Factory;
using Tooark.Services.Interface;
using Tooark.Services.RabbitMQ;

namespace Tooark.Injections;

/// <summary>
/// Classe de extensão para configurar e adicionar serviços específicos da Tooark ao contêiner de injeção de dependência.
/// </summary>
public static class TooarkDependencyInjection
{
  private const string username = "guest";
  private const string password = "guest";

  /// <summary>
  /// Adiciona e configura os serviços da Tooark, incluindo serviços HTTP e RabbitMQ.
  /// </summary>
  /// <param name="services">A coleção de serviços para adicionar os serviços da Tooark.</param>
  /// <param name="rabbitMQHostname">O hostname do servidor RabbitMQ.</param>
  /// <param name="rabbitMQPort">A porta do servidor RabbitMQ.</param>
  /// <param name="rabbitMQUserName">O nome de usuário para autenticação no RabbitMQ.</param>
  /// <param name="rabbitMQPassword">A senha para autenticação no RabbitMQ.</param>
  /// <returns>A coleção de serviços com os serviços da Tooark adicionados.</returns>
  public static IServiceCollection AddTooarkServices(
    this IServiceCollection services,
    string rabbitMQHostname = "localhost",
    int rabbitMQPort = 5672,
    string rabbitMQUserName = username,
    string rabbitMQPassword = password)
  {
    services.AddHttpClientService();
    services.AddRabbitMQService(rabbitMQHostname, rabbitMQPort, rabbitMQUserName, rabbitMQPassword);

    return services;
  }

  /// <summary>
  /// Adiciona o serviço HttpClient ao contêiner de injeção de dependência.
  /// </summary>
  /// <param name="services">A coleção de serviços para adicionar o serviço HttpClient.</param>
  /// <returns>A coleção de serviços com o serviço HttpClient adicionado.</returns>
  public static IServiceCollection AddHttpClientService(
    this IServiceCollection services)
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

  /// <summary>
  /// Adiciona e configura os serviços relacionados ao RabbitMQ ao contêiner de injeção de dependência.
  /// </summary>
  /// <param name="services">A coleção de serviços para adicionar os serviços RabbitMQ.</param>
  /// <param name="host">O hostname do servidor RabbitMQ.</param>
  /// <param name="port">A porta do servidor RabbitMQ.</param>
  /// <param name="user">O nome de usuário para autenticação no RabbitMQ.</param>
  /// <param name="pass">A senha para autenticação no RabbitMQ.</param>
  /// <returns>A coleção de serviços com os serviços RabbitMQ adicionados.</returns>
  public static IServiceCollection AddRabbitMQService(
    this IServiceCollection services,
    string host = "localhost",
    int port = 5672,
    string user = username,
    string pass = password)
  {
    services.AddSingleton<RabbitMQConnectionService>(provider =>
    {
      return new RabbitMQConnectionService(host, port, user, pass);
    });

    services.AddSingleton<IRabbitMQService>(provider =>
    {
      var connService = provider.GetRequiredService<RabbitMQConnectionService>();
      
      return RabbitMQServiceFactory.Create(connService);
    });

    services.AddHostedService<RabbitMQConsumerService>();

    return services;
  }
}

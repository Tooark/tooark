using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Tooark.Factories;
using Tooark.Helpers;
using Tooark.Interfaces;
using Tooark.Options;
using Tooark.Services.Factory;
using Tooark.Services.Interface;
using Tooark.Services.RabbitMQ;

namespace Tooark.Injections;

/// <summary>
/// Classe de extensão para configurar e adicionar serviços específicos da Tooark ao contêiner de injeção de dependência.
/// </summary>
public static class TooarkDependencyInjection
{
  /// <summary>
  /// Adiciona e configura os serviços da Tooark, incluindo serviços HTTP e RabbitMQ.
  /// </summary>
  /// <param name="services">A coleção de serviços para adicionar os serviços da Tooark.</param>
  /// <param name="options">Parâmetros para os serviços RabbitMQ.</param>
  /// <returns>A coleção de serviços com os serviços da Tooark adicionados.</returns>
  public static IServiceCollection AddTooarkServices(
    this IServiceCollection services,
    RabbitMQOptions options)
  {
    services.AddHttpClientService();
    services.AddRabbitMQPublishService(options);

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
  /// <param name="options">Parâmetros para os serviços RabbitMQ.</param>
  /// <returns>A coleção de serviços com os serviços RabbitMQ adicionados.</returns>
  public static IServiceCollection AddRabbitMQPublishService(
    this IServiceCollection services,
    RabbitMQOptions options)
  {
    services.AddSingleton<IRabbitMQPublishServiceFactory, RabbitMQPublishServiceFactory>();
    services.AddSingleton<IRabbitMQPublishService>(provider =>
    {
      var factory = provider.GetRequiredService<IRabbitMQPublishServiceFactory>();
      return factory.CreateRabbitMQPublishService(options);
    });

    services.AddHostedService<RabbitMQInitializeService>(provider =>
    {
      var connectionFactory = new ConnectionFactory
      {
        HostName = options.Hostname,
        Port = options.PortNumber,
        UserName = options.Username,
        Password = options.Password,
        AutomaticRecoveryEnabled = options.AutomaticRecovery,
        NetworkRecoveryInterval = TimeSpan.FromSeconds(options.RecoveryInterval)
      };

      var connection = connectionFactory.CreateConnection();
      var channel = connection.CreateModel();

      // Configuração padrão do serviço exchange Fanout e Direct
      RabbitMQHelper.ConfigureFanoutDirect(channel, options.QueueName, options.RoutingKey);

      // Configuração customizada do serviço exchange, caso fornecidos
      foreach (var custom in options.CustomExchange)
      {
        RabbitMQHelper.ConfigureExchangeQueue(
          channel,
          custom.NameExchange,
          custom.TypeExchange,
          custom.NameQueue,
          custom.RoutingKey,
          custom.Durable,
          custom.Exclusive,
          custom.AutoDelete);
      }

      // Registra a ação para fechar a conexão e o canal quando a aplicação for desligada
      var lifetime = provider.GetRequiredService<IHostApplicationLifetime>();
      lifetime.ApplicationStopping.Register(() =>
      {
        channel.Close();
        connection.Close();
      });

      return new RabbitMQInitializeService(channel);
    });

    return services;
  }

  /// <summary>
  /// Adiciona o serviço de consumo RabbitMQ como um serviço em background ao contêiner de injeção de dependência.
  /// </summary>
  /// <param name="services">O IServiceCollection ao qual o serviço será adicionado.</param>
  /// <param name="options">As opções de configuração para o serviço RabbitMQ.</param>
  /// <param name="processMessageFunc">A função de callback para processar as mensagens recebidas.</param>
  /// <returns>O IServiceCollection para encadeamento de chamadas.</returns>
  public static IServiceCollection AddRabbitMQConsumeBackgroundService(
    this IServiceCollection services,
    RabbitMQOptions options,
    Action<string> processMessageFunc)
  {
    services.AddSingleton<IRabbitMQConsumeServiceFactory, RabbitMQConsumeServiceFactory>();
    services.AddSingleton<IRabbitMQConsumeService>(provider =>
    {
      var factory = provider.GetRequiredService<IRabbitMQConsumeServiceFactory>();
      return factory.CreateRabbitMQConsumeService(options, processMessageFunc);
    });

    return services;
  }
}

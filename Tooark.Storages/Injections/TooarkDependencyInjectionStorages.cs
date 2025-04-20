using Amazon.S3;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tooark.Storages.Factories;
using Tooark.Storages.Interfaces;
using Tooark.Storages.Options;

namespace Tooark.Storages.Injections;

/// <summary>
/// Classe de extensão para adicionar os serviços de armazenamento ao container de injeção de dependência.
/// </summary>
public static class TooarkDependencyInjection
{
  /// <summary>
  /// Adiciona os serviços de armazenamento ao container de injeção de dependência.
  /// </summary>
  /// <param name="services">Coleção de serviços.</param>
  /// <param name="configuration">Configuração da aplicação para obter as configurações do StorageOptions.</param>
  /// <param name="awsClient">Cliente da AWS S3. Parâmetro opcional.</param>
  /// <param name="gcpClient">Cliente do Google Cloud Storage. Parâmetro opcional.</param>
  /// <param name="gcpClientSigner">Assinador de URL do Google Cloud Storage. Parâmetro opcional.</param>
  /// <returns>A coleção de serviços com os serviços de armazenamento adicionados.</returns>
  public static IServiceCollection AddTooarkStorages(
    this IServiceCollection services,
    IConfiguration configuration,
    IAmazonS3? awsClient = null,
    StorageClient? gcpClient = null,
    IUrlSigner? gcpClientSigner = null
)
  {
    // Adiciona os serviços de logging
    // services.AddLogging();

    // Configuração do StorageOptions
    var storageOptionsSection = configuration.GetSection(StorageOptions.Section);

    // Se a seção StorageOptions existir, então configura o StorageOptions e adiciona o StorageService
    if (storageOptionsSection.Exists())
    {
      // Configuração do StorageOptions
      services.Configure<StorageOptions>(options => configuration.GetSection(StorageOptions.Section));

      // Adiciona o StorageService como Singleton (uma única instância para toda a aplicação)
      services.AddSingleton<IStorageService>(provider =>
      {
        // Obtenção dos serviços necessários para a criação do StorageService
        var logger = provider.GetRequiredService<ILogger<StorageService>>();
        var options = provider.GetRequiredService<IOptions<StorageOptions>>();

        // Cria o serviço de armazenamento
        return StorageServiceFactory.Create(logger, options, awsClient, gcpClient, gcpClientSigner);
      });
    }

    // Retorna os serviços
    return services;
  }
}

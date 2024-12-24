namespace Tooark.Injections;

public static class TooarkDependencyInjection
{
  /// <summary>
  /// Adiciona o serviço de armazenamento ao contêiner de injeção de dependência.
  /// </summary>
  /// <param name="services">A coleção de serviços para adicionar o serviço de armazenamento.</param>
  /// <returns>A coleção de serviços com o serviço de armazenamento adicionado.</returns>
  public static IServiceCollection AddStorageService(this IServiceCollection services)
  {
    services.Configure<BucketOptions>(Configuration.GetSection("BucketOptions"));
    services.AddSingleton<StorageClient>();
    services.AddSingleton<AmazonS3Client>();
    services.AddSingleton<IStorageService>(provider =>
    {
      var bucketOptions = provider.GetRequiredService<IOptions<BucketOptions>>();
      var logger = provider.GetRequiredService<ILogger<StorageService>>();
      var storageClient = provider.GetService<StorageClient>();
      var s3Client = provider.GetService<AmazonS3Client>();

      return StorageServiceFactory.Create(bucketOptions, logger, storageClient, s3Client);
    });

    return services;
  }
}

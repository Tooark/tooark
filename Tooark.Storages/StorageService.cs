using Amazon.S3;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tooark.Enums;
using Tooark.Exceptions;
using Tooark.Notifications;
using Tooark.Storages.Dtos;
using Tooark.Storages.Interfaces;
using Tooark.Storages.Options;
using Tooark.Validations;

namespace Tooark.Storages;

/// <summary>
/// Classe de serviço para armazenamento de arquivos em buckets da Amazon S3 ou Google Cloud Storage.
/// </summary>
public partial class StorageService : Notification, IStorageService
{
  /// <summary>
  /// Variável privada com o logger da aplicação.
  /// </summary>
  private readonly ILogger<StorageService> _logger;

  /// <summary>
  /// Variável privada com as opções do bucket.
  /// </summary>
  private readonly StorageOptions _storageOptions;

  /// <summary>
  /// Variável privada com o cliente AWS.
  /// </summary>
  private readonly IAmazonS3? _awsClient;

  /// <summary>
  /// Variável privada com o cliente Google Cloud Storage.
  /// </summary>
  private readonly StorageClient? _gcpClient;

  /// <summary>
  /// Variável privada com o cliente de assinatura de URL do Google Cloud Storage.
  /// </summary>
  private readonly IUrlSigner? _gcpClientSigner;


  /// <summary>
  /// Construtor da classe StorageService.
  /// </summary>
  /// <param name="logger">O logger da aplicação.</param>
  /// <param name="bucketOptions">As opções do bucket.</param>
  /// <param name="awsClient">O cliente AWS. Parâmetro opcional.</param>
  /// <param name="gcpClient">O cliente Google Cloud Storage. Parâmetro opcional.</param>
  /// <param name="gcpClientSigner">O cliente de assinatura de URL do Google Cloud Storage. Parâmetro opcional.</param>
  public StorageService(
    ILogger<StorageService> logger,
    IOptions<StorageOptions> bucketOptions,
    IAmazonS3? awsClient = null,
    StorageClient? gcpClient = null,
    IUrlSigner? gcpClientSigner = null
  )
  {
    // Define o logger
    _logger = logger;

    // Define as opções do bucket
    _storageOptions = bucketOptions.Value;

    // Define o cliente AWS
    _awsClient = awsClient;

    // Define o cliente Google Cloud Storage
    _gcpClient = gcpClient;

    // Define o cliente de assinatura de URL do Google Cloud Storage
    _gcpClientSigner = gcpClientSigner;

    // Verifica se as opções do bucket são válidas
    if (_storageOptions == null || string.IsNullOrEmpty(_storageOptions.BucketName))
    {
      throw new InternalServerErrorException("Storage.StorageOptionsEmpty");
    }

    // Verifica se o provedor de nuvem é válido
    if (_storageOptions.CloudProvider == ECloudProvider.None)
    {
      throw new InternalServerErrorException("Storage.CloudProviderEmpty");
    }
    // Verifica se o provedor é da Amazon e as credenciais estão vazias
    else if (_storageOptions.CloudProvider == ECloudProvider.Amazon && _storageOptions.AWS == null)
    {
      throw new InternalServerErrorException("Storage.CredentialsEmpty;AWS");
    }
    // Verifica se o provedor é do Google e as credenciais estão vazias
    else if (_storageOptions.CloudProvider == ECloudProvider.Google && _storageOptions.GCP == null)
    {
      throw new InternalServerErrorException("Storage.CredentialsEmpty;GCP");
    }
  }


  /// <summary>
  /// Função que válida se as informações para upload do arquivo são válidas.
  /// </summary>
  /// <param name="fileName">O nome do arquivo a ser enviado.</param>
  /// <param name="bucketName">O nome do bucket onde o arquivo será enviado.</param>
  /// <param name="fileSize">O tamanho do arquivo a ser enviado.</param>
  /// <returns>Retorna verdadeiro se as informações são válidas.</returns>
  private bool ValidFileUpload(string fileName, string bucketName, long fileSize)
  {
    // Verifica se o nome do arquivo é nulo ou vazio
    NameFileNotNull(fileName);

    // Verifica se o nome do bucket é nulo ou vazio
    BucketNameNotNull(bucketName);

    // Verifica se o tamanho do arquivo é válido
    FileSizeAllow(fileSize);

    // Retorna se as informações são válidas
    return IsValid;
  }

  /// <summary>
  /// Função que válida se as informações do arquivo são válidas.
  /// </summary>
  /// <param name="fileName">O nome do arquivo.</param>
  /// <param name="bucketName">O nome do bucket onde o arquivo está.</param>
  /// <returns>Retorna verdadeiro se as informações são válidas.</returns>
  private bool ValidFileParam(string fileName, string bucketName)
  {
    // Verifica se o nome do arquivo é nulo ou vazio
    NameFileNotNull(fileName);

    // Verifica se o nome do bucket é nulo ou vazio
    BucketNameNotNull(bucketName);

    // Retorna se as informações são válidas
    return IsValid;
  }

  /// <summary>
  /// Função que valida se o nome do arquivo não é nulo ou vazio.
  /// </summary>
  /// <param name="fileName">O nome do arquivo a ser enviado.</param>
  private void NameFileNotNull(string fileName)
  {
    // Verifica se o nome do arquivo é nulo ou vazio
    if (string.IsNullOrEmpty(fileName))
    {
      // Gerar um log de erro
      _logger.LogError("Services.StorageService.NameFileNotNull: File Name is Empty");

      // Adiciona uma notificação de erro
      AddNotification("Storage.FileNameEmpty");
    }
  }

  /// <summary>
  /// Função que valida se o nome do bucket não é nulo ou vazio.
  /// </summary>
  /// <param name="bucketName">O nome do bucket onde o arquivo será enviado.</param>
  private void BucketNameNotNull(string bucketName)
  {
    // Verifica se o nome do bucket é nulo ou vazio
    if (string.IsNullOrEmpty(bucketName))
    {
      // Gerar um log de erro
      _logger.LogError("Services.StorageService.BucketNameNotNull: Bucket Name is Empty");

      // Adiciona uma notificação de erro
      AddNotification("Storage.BucketNameEmpty");
    }
  }

  /// <summary>
  /// Função que valida se o tamanho do arquivo é válido.
  /// </summary>
  /// <param name="fileSize">O tamanho do arquivo a ser enviado.</param>
  private void FileSizeAllow(long fileSize)
  {
    // Verifica se o tamanho do arquivo é válido
    AddNotifications(new Validation()
      .IsGreater(fileSize, 0, "FileSize", "Storage.FileSizeEmpty")
      .IsLowerOrEquals(fileSize, _storageOptions.FileSize, "FileSize", $"Storage.FileSizeBigger;{_storageOptions.FileSize}")
    );
  }


  /// <summary>
  /// Função que realiza o upload do arquivo em um bucket da Amazon S3 ou Google Cloud Storage.
  /// </summary>
  /// <param name="fileStream">O stream do arquivo a ser enviado.</param>
  /// <param name="fileName">O nome do arquivo a ser enviado.</param>
  /// <param name="contentType">O tipo do conteúdo do arquivo a ser enviado.</param>
  /// <param name="bucketName">O nome do bucket onde o arquivo será enviado. Parâmetro opcional.</param>
  /// <returns>Os dados do arquivo enviado <see cref="UploadResponseDto"/>. O retorno é uma Task.</returns>
  /// <exception cref="BadRequestException">Se o bucket não for encontrado.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorreu um erro ao enviar o arquivo.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorrer um erro desconhecido.</exception>
  private async Task<UploadResponseDto> UploadFileAsync(Stream fileStream, string fileName, string? contentType = null, string? bucketName = null)
  {
    try
    {
      // Verifica se o nome do bucket é nulo ou vazio e atribui o valor padrão
      bucketName ??= _storageOptions.BucketName;

      // Verifica se os parâmetros são válidos para upload
      if (ValidFileUpload(fileName, bucketName, fileStream.Length))
      {
        // Verifica se o provedor de nuvem é da Amazon
        if (_storageOptions.CloudProvider == ECloudProvider.Amazon)
        {
          // Define as opções do AWS S3
          var optionsAws = _storageOptions.AWS!;

          // Realiza o upload do arquivo no AWS S3
          return await UploadToAwsAsync(optionsAws, fileStream, bucketName, fileName, contentType);
        }
        // Verifica se o provedor de nuvem é do Google
        else if (_storageOptions.CloudProvider == ECloudProvider.Google)
        {
          // Define as opções do Google Cloud Storage
          var optionsGcp = _storageOptions.GCP!;

          // Realiza o upload do arquivo no Google Cloud Storage
          return await UploadToGcpAsync(optionsGcp, fileStream, bucketName, fileName, contentType);
        }

        // Gerar um log de erro
        _logger.LogError("Services.StorageService.UploadFileAsync: Cloud Provider Not Implemented");

        // Adiciona uma notificação de erro
        AddNotification("Storage.CloudProviderNotImplemented");
      }

      // Adiciona uma notificação de erro
      throw new BadRequestException(this);
    }
    catch (BadRequestException)
    {
      throw;
    }
    catch (InternalServerErrorException)
    {
      throw;
    }
  }

  /// <summary>
  /// Função que apaga um arquivo em um bucket da Amazon S3 ou Google Cloud Storage.
  /// </summary>
  /// <param name="fileName">O nome do arquivo a ser apagado.</param>
  /// <param name="bucketName">O nome do bucket onde o arquivo será apagado. Parâmetro opcional.</param>
  /// <returns>Uma mensagem com o resultado da operação.</returns>
  /// <exception cref="BadRequestException">Se o arquivo não for encontrado.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorreu um erro ao apagar o arquivo.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorrer um erro desconhecido.</exception>
  private async Task<string> DeleteFileAsync(string fileName, string? bucketName = null)
  {
    try
    {
      // Verifica se o nome do bucket é nulo ou vazio e atribui o valor padrão
      bucketName ??= _storageOptions.BucketName;

      // Verifica se os parâmetros são válidos para apagar
      if (ValidFileParam(fileName, bucketName))
      {
        // Verifica se o provedor de nuvem é da Amazon
        if (_storageOptions.CloudProvider == ECloudProvider.Amazon)
        {
          // Define as opções do AWS S3
          var optionsAws = _storageOptions.AWS!;

          // Apaga um arquivo no AWS S3
          return await DeleteFromAwsAsync(optionsAws, bucketName, fileName);
        }
        // Verifica se o provedor de nuvem é do Google
        else if (_storageOptions.CloudProvider == ECloudProvider.Google)
        {
          // Define as opções do Google Cloud Storage
          var optionsGcp = _storageOptions.GCP!;

          // Apaga um arquivo no Google Cloud Storage
          return await DeleteFromGcpAsync(optionsGcp, bucketName, fileName);
        }

        // Gerar um log de erro
        _logger.LogError("Services.StorageService.DeleteFileAsync: Cloud Provider Not Implemented");

        // Adiciona uma notificação de erro
        AddNotification("Storage.CloudProviderNotImplemented");
      }

      // Adiciona uma notificação de erro
      throw new BadRequestException(this);
    }
    catch (BadRequestException)
    {
      throw;
    }
    catch (InternalServerErrorException)
    {
      throw;
    }
  }

  /// <summary>
  /// Função que realiza o download do arquivo em um bucket da Amazon S3 ou Google Cloud Storage.
  /// </summary>
  /// <param name="fileName">O nome do arquivo a ser baixado.</param>
  /// <param name="bucketName">O nome do bucket onde o arquivo será baixado. Parâmetro opcional.</param>
  /// <returns>O arquivo baixo em <see cref="Stream"/>. O retorno é uma Task.</returns>
  /// <exception cref="BadRequestException">Se o arquivo não for encontrado.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorreu um erro ao baixar o arquivo.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorrer um erro desconhecido.</exception>
  private async Task<Stream> DownloadFileAsync(string fileName, string? bucketName = null)
  {
    try
    {
      // Verifica se o nome do bucket é nulo ou vazio e atribui o valor padrão
      bucketName ??= _storageOptions.BucketName;

      // Verifica se os parâmetros são válidos para download
      if (ValidFileParam(fileName, bucketName))
      {
        // Verifica se o provedor de nuvem é da Amazon
        if (_storageOptions.CloudProvider == ECloudProvider.Amazon)
        {
          // Define as opções do AWS S3
          var optionsAws = _storageOptions.AWS!;

          // Baixa um arquivo no AWS S3
          return await DownloadFromAwsAsync(optionsAws, bucketName, fileName);
        }
        // Verifica se o provedor de nuvem é do Google
        else if (_storageOptions.CloudProvider == ECloudProvider.Google)
        {
          // Define as opções do Google Cloud Storage
          var optionsGcp = _storageOptions.GCP!;

          // Baixa um arquivo no Google Cloud Storage
          return await DownloadFromGcpAsync(optionsGcp, bucketName, fileName);
        }

        // Gerar um log de erro
        _logger.LogError("Services.StorageService.DownloadFileAsync: Cloud Provider Not Implemented");

        // Adiciona uma notificação de erro
        AddNotification("Storage.CloudProviderNotImplemented");
      }

      // Adiciona uma notificação de erro
      throw new BadRequestException(this);
    }
    catch (BadRequestException)
    {
      throw;
    }
    catch (InternalServerErrorException)
    {
      throw;
    }
  }

  /// <summary>
  /// Função que assina um arquivo em um bucket da Amazon S3 ou Google Cloud Storage.
  /// </summary>
  /// <param name="fileName">O nome do arquivo a ser assinado.</param>
  /// <param name="expiresMinute">Tempo de expiração do link em minutos.</param>
  /// <param name="bucketName">O nome do bucket onde o arquivo será assinado. Parâmetro opcional.</param>
  /// <returns>Uma URL assinada temporária do arquivo. O retorno é uma Task.</returns>
  /// <exception cref="BadRequestException">Se o arquivo não for encontrado.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorreu um erro ao baixar o arquivo.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorrer um erro desconhecido.</exception>
  private async Task<string> SignerFileAsync(string fileName, int expiresMinute, string? bucketName = null)
  {
    try
    {
      // Verifica se o nome do bucket é nulo ou vazio e atribui o valor padrão
      bucketName ??= _storageOptions.BucketName;

      // Verifica se os parâmetros são válidos para assinar
      if (ValidFileParam(fileName, bucketName))
      {
        // Verifica se o provedor de nuvem é da Amazon
        if (_storageOptions.CloudProvider == ECloudProvider.Amazon)
        {
          // Define as opções do AWS S3
          var optionsAws = _storageOptions.AWS!;

          // Assina um arquivo no AWS S3
          return await SignerFromAwsAsync(optionsAws, bucketName, fileName, expiresMinute);
        }
        // Verifica se o provedor de nuvem é do Google
        else if (_storageOptions.CloudProvider == ECloudProvider.Google)
        {
          // Define as opções do Google Cloud Storage
          var optionsGcp = _storageOptions.GCP!;

          // Assina um arquivo no Google Cloud Storage
          return await SignerFromGcpAsync(optionsGcp, bucketName, fileName, expiresMinute);
        }

        // Gerar um log de erro
        _logger.LogError("Services.StorageService.SignerFileAsync: Cloud Provider Not Implemented");

        // Adiciona uma notificação de erro
        AddNotification("Storage.CloudProviderNotImplemented");
      }

      // Adiciona uma notificação de erro
      throw new BadRequestException(this);
    }
    catch (BadRequestException)
    {
      throw;
    }
    catch (InternalServerErrorException)
    {
      throw;
    }
  }


  /// <summary>
  /// Função para fazer upload de um arquivo em um bucket.
  /// </summary>
  /// <param name="fileStream">O stream do arquivo a ser enviado.</param>
  /// <param name="fileName">O nome do arquivo a ser enviado.</param>
  /// <param name="contentType">O tipo do conteúdo do arquivo a ser enviado.</param>
  /// <param name="bucketName">O nome do bucket onde o arquivo será enviado. Parâmetro opcional.</param>
  /// <returns>Os dados do arquivo enviado <see cref="UploadResponseDto"/>. O retorno é uma Task.</returns>
  /// <exception cref="BadRequestException">Se o bucket não for encontrado.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorreu um erro ao enviar o arquivo.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorrer um erro desconhecido.</exception>
  public async Task<UploadResponseDto> Upload(MemoryStream fileStream, string fileName, string? bucketName = null, string? contentType = null)
  {
    // Verifica se o arquivo é nulo
    if (fileStream == null)
    {
      // Gerar um log de erro
      _logger.LogError("Services.StorageService.Upload: File Upload is Empty");

      // Adiciona uma notificação de erro
      AddNotification("Storage.FileUploadEmpty");

      // Adiciona uma notificação de erro
      throw new BadRequestException(this);
    }

    // Realiza o upload do arquivo no bucket
    return await UploadFileAsync(fileStream, fileName, contentType, bucketName);
  }

  /// <summary>
  /// Função para apagar um arquivo do bucket.
  /// </summary>
  /// <param name="fileName">O nome do arquivo a ser apagado.</param>
  /// <param name="bucketName">O nome do bucket onde o arquivo será apagado. Parâmetro opcional.</param>
  /// <returns>Uma mensagem com o resultado da operação.</returns>
  /// <exception cref="BadRequestException">Se o arquivo não for encontrado.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorreu um erro ao apagar o arquivo.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorrer um erro desconhecido.</exception>
  public async Task<string> Delete(string fileName, string? bucketName = null)
  {
    // Apaga um arquivo no bucket
    return await DeleteFileAsync(fileName, bucketName);
  }

  /// <summary>
  /// Função para fazer download de um arquivo do bucket.
  /// </summary>
  /// <param name="fileName">O nome do arquivo a ser baixado.</param>
  /// <param name="bucketName">O nome do bucket onde o arquivo será baixado. Parâmetro opcional.</param>
  /// <returns>O arquivo baixo em <see cref="Stream"/>. O retorno é uma Task.</returns>
  /// <exception cref="BadRequestException">Se o arquivo não for encontrado.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorreu um erro ao baixar o arquivo.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorrer um erro desconhecido.</exception>
  public async Task<Stream> Download(string fileName, string? bucketName = null)
  {
    // Realiza o download do arquivo no bucket
    return await DownloadFileAsync(fileName, bucketName);
  }

  /// <summary>
  /// Função para gerar um link assinado temporário do arquivo.
  /// </summary>
  /// <param name="fileName">O nome do arquivo a ser assinado.</param>
  /// <param name="expiresMinute">Tempo de expiração do link em minutos. Parâmetro opcional.</param>
  /// <param name="bucketName">O nome do bucket onde o arquivo será assinado. Parâmetro opcional.</param>
  /// <returns>Uma URL assinada temporária do arquivo. O retorno é uma Task.</returns>
  /// <exception cref="BadRequestException">Se o arquivo não for encontrado.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorreu um erro ao baixar o arquivo.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorrer um erro desconhecido.</exception>
  public async Task<string> Signer(string fileName, int? expiresMinute = 0, string? bucketName = null)
  {
    // Verifica se o tempo é maior que zero
    expiresMinute = expiresMinute > 0 ? expiresMinute : _storageOptions.SignerMinutes;

    // Assina um arquivo no bucket
    return await SignerFileAsync(fileName, expiresMinute.Value, bucketName);
  }
}

using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tooark.Dtos;
using Tooark.Enums;
using Tooark.Exceptions;
using Tooark.Interfaces;
using Tooark.Options;

namespace Tooark.Services.Storage;

internal partial class StorageService : IStorageService
{
  private readonly ILogger<StorageService> _logger;
  private readonly IOptions<BucketOptions> _bucketOptions;
  private readonly StorageClient _storageClient;
  private readonly AmazonS3Client _s3Client;
  private readonly string _bucketName;
  private readonly long _fileSize;

  internal StorageService(
    IOptions<BucketOptions> bucketOptions,
    ILogger<StorageService> logger
  )
  {
    _logger = logger;
    _bucketOptions = bucketOptions;

    if (bucketOptions.Value == null)
    {
      throw new ArgumentNullException(nameof(bucketOptions));
    }

    _bucketName = bucketOptions.Value.BucketName;
    _fileSize = bucketOptions.Value.FileSize < 0 ? 1024 : bucketOptions.Value.FileSize;

    GoogleCredential? _googleCredential = null;

    // Se o caminho do arquivo de credenciais não for nulo carrega as credenciais a partir do arquivo
    if (_bucketOptions.Value.GCPPath != null)
    {
      _googleCredential = GoogleCredential.FromFile(_bucketOptions.Value.GCPPath);
    }
    else if (_bucketOptions.Value.GCP != null) // Se as credenciais forem passadas por JSON carrega as credenciais a partir do JSON
    {
      _googleCredential = GoogleCredential.FromJsonParameters(_bucketOptions.Value.GCP);
    }

    _storageClient = StorageClient.Create(_googleCredential);

    BasicAWSCredentials? credentials = _bucketOptions.Value.AWS;

    RegionEndpoint region = _bucketOptions.Value.AWSRegion ?? RegionEndpoint.USEast1;

    _s3Client = new AmazonS3Client(credentials, region);
  }

  /// <summary>
  /// Verifica se o nome do arquivo é nulo ou vazio.
  /// </summary>
  /// <param name="nameFile">Nome do arquivo.</param>
  /// <returns>Retorna <see langword="true"/> caso o nome do arquivo não seja nulo ou vazio.</returns>
  /// <exception cref="ArgumentException">Caso o nome do arquivo seja nulo ou vazio.</exception>
  private static bool NameFileNotNull(string nameFile)
  {
    // Verifica se o nome do arquivo é nulo ou vazio
    if (string.IsNullOrEmpty(nameFile))
    {
      throw new ArgumentException("FileNameBucketEmpty");
    }

    return true;
  }

  /// <summary>
  /// Verifica se o nome do bucket é nulo ou vazio.
  /// </summary>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <returns>Retorna <see langword="true"/> caso o nome do bucket não seja nulo ou vazio.</returns>
  /// <exception cref="ArgumentException">Caso o nome do bucket seja nulo ou vazio.</exception>
  private bool BucketNameNotNull(string bucketName)
  {
    // Verifica se o nome do bucket é nulo ou vazio
    if (string.IsNullOrEmpty(bucketName))
    {
      _logger.LogError("Services.StorageService.BucketNameNotNull:\nBucket Name is Empty");

      throw new ArgumentException("BucketNameBucketEmpty");
    }

    return true;
  }

  /// <summary>
  /// Verifica se o tamanho do arquivo é maior que o permitido.
  /// </summary>
  /// <param name="fileSize">Tamanho do arquivo.</param>
  /// <returns>Retorna <see langword="true"/> caso o tamanho do arquivo seja menor ou igual ao permitido.</returns>
  /// <exception cref="AppException">Caso o tamanho do arquivo seja maior que o permitido.</exception>
  private bool FileSizeAllow(long fileSize)
  {
    // Verifica se o tamanho do arquivo é maior que o permitido
    if (fileSize > _fileSize)
    {
      throw AppException.BadRequest($"FileSizeBigger;${_fileSize}");
    }

    return true;
  }

  /// <summary>
  /// Verifica se os parâmetros para upload são válidos.
  /// </summary>
  /// <param name="nameFile">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <param name="fileSize">Tamanho do arquivo.</param>
  /// <returns>Retorna <see langword="true"/> caso os parâmetros sejam válidos.</returns>
  /// <exception cref="AppException">Caso os parâmetros para upload sejam inválidos.</exception>
  private bool ValidFileUpload(string nameFile, string bucketName, long fileSize)
  {
    return NameFileNotNull(nameFile) && BucketNameNotNull(bucketName) && FileSizeAllow(fileSize);
  }

  /// <summary>
  /// Verifica se os parâmetros para arquivo no bucket são válidos.
  /// </summary>
  /// <param name="nameFile">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <returns>Retorna <see langword="true"/> caso os parâmetros sejam válidos.</returns>
  /// <exception cref="AppException">Caso os parâmetros para arquivo no bucket sejam inválidos.</exception>
  private bool ValidFileBucket(string nameFile, string bucketName)
  {
    return NameFileNotNull(nameFile) && BucketNameNotNull(bucketName);
  }

  /// <summary>
  /// Realiza o upload de um arquivo no bucket de modo Assíncrono.
  /// </summary>
  /// <param name="stream">Arquivo em memória para upload. Em formato <see cref="Stream"/>.</param>
  /// <param name="nameFile">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <param name="contentType">Tipo do arquivo.</param>
  /// <returns>Dados do arquivo criado no formato <see cref="StorageResponseDto"/>.</returns>
  /// <exception cref="AppException">Caso os parâmetros do arquivo para upload seja inválido.</exception>
  /// <exception cref="AppException">Caso o provedor de armazenamento em nuvem seja inválido.</exception>
  /// <exception cref="AppException">Caso ocorra um erro ao enviar o arquivo.</exception>
  private async Task<StorageResponseDto> UploadAsync(Stream stream, string nameFile, string bucketName, string? contentType = null)
  {
    // Verifica se os parâmetros para upload são válidos
    if (!ValidFileUpload(nameFile, bucketName, stream.Length))
    {
      throw AppException.BadRequest("FileUploadInvalid");
    }

    // Verifica qual o provedor de armazenamento em nuvem
    if (_bucketOptions.Value.CloudProvider == CloudProvider.AWS)
    {
      S3CannedACL cannedACL = _bucketOptions.Value.AWSAcl;

      return await AwsUploadAsync(stream, nameFile, bucketName, cannedACL);
    }
    else if (_bucketOptions.Value.CloudProvider == CloudProvider.GCP)
    {
      return await GcpUploadAsync(stream, nameFile, bucketName, contentType);
    }

    _logger.LogError("Services.StorageService.UploadAsync:\nFile Upload Deactivated");

    throw AppException.InternalServerError("FileUploadDeactivated");
  }

  /// <summary>
  /// Apaga um arquivo no bucket de modo Assíncrono.
  /// </summary>
  /// <param name="nameFile">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <returns>Dados do arquivo criado no formato <see cref="StorageResponseDto"/>.</returns>
  /// <exception cref="AppException">Caso os parâmetros do arquivo para apagar seja inválido.</exception>
  /// <exception cref="AppException">Caso o provedor de armazenamento em nuvem seja inválido.</exception>
  /// <exception cref="AppException">Caso ocorra um erro ao apagar o arquivo.</exception>
  private async Task EraseAsync(string nameFile, string bucketName)
  {
    // Verifica se os parâmetros para apagar são válidos
    if (!ValidFileBucket(nameFile, bucketName))
    {
      throw AppException.BadRequest("FileDeleteInvalid");
    }

    // Verifica qual o provedor de armazenamento em nuvem
    if (_bucketOptions.Value.CloudProvider == CloudProvider.AWS)
    {
      await AwsDeleteAsync(nameFile, bucketName);
    }
    else if (_bucketOptions.Value.CloudProvider == CloudProvider.GCP)
    {
      await GcpDeleteAsync(nameFile, bucketName);
    }

    _logger.LogError("Services.StorageService.EraseAsync:\nFile Delete Deactivated");

    throw AppException.InternalServerError("FileDeleteDeactivated");
  }

  /// <summary>
  /// Realiza o download de um arquivo para memória de modo Assíncrono.
  /// </summary>
  /// <param name="nameFile">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <param name="versionId">Versão do arquivo.</param>
  /// <returns>Arquivo em formato <see cref="Stream"/></returns>
  /// <exception cref="AppException">Caso os parâmetros para download seja inválido.</exception>
  /// <exception cref="AppException">Caso o provedor de armazenamento em nuvem seja inválido.</exception>
  /// <exception cref="AppException">Caso ocorra um erro ao baixar o arquivo.</exception>
  private async Task<Stream> PullAsync(string nameFile, string bucketName, string? versionId = null)
  {
    // Verifica se os parâmetros para download são válidos
    if (!ValidFileBucket(nameFile, bucketName))
    {
      throw AppException.BadRequest("FileDownloadInvalid");
    }

    // Verifica qual o provedor de armazenamento em nuvem
    if (_bucketOptions.Value.CloudProvider == CloudProvider.AWS)
    {
      return await AwsDownloadAsync(nameFile, bucketName, versionId);
    }
    else if (_bucketOptions.Value.CloudProvider == CloudProvider.GCP)
    {
      return await GcpDownloadAsync(nameFile, bucketName);
    }

    _logger.LogError("Services.StorageService.PullAsync:\nFile Download Deactivated");

    throw AppException.InternalServerError("FileDownloadDeactivated");
  }

  /// <summary>
  /// Cria um arquivo no bucket de modo Assíncrono.
  /// </summary>
  /// <param name="filePath">Caminho do arquivo para upload. Em formato <see cref="string"/>.</param>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <param name="contentType">Tipo do arquivo.</param>
  /// <returns>Dados do arquivo criado no formato <see cref="StorageResponseDto"/>.</returns>
  /// <exception cref="ArgumentException">Quando o caminho do arquivo é nulo ou vazio.</exception>
  /// <exception cref="AppException">Caso ocorra um erro ao enviar o arquivo.</exception>
  public async Task<StorageResponseDto> Create(string filePath, string fileName, string? bucketName = null, string? contentType = null)
  {
    bucketName ??= _bucketName;

    // Verifica se o caminho do arquivo é nulo ou vazio
    if (string.IsNullOrEmpty(filePath))
    {
      throw new ArgumentException("FileLocalUploadEmpty");
    }

    // Carrega o arquivo em memória
    using var fileStream = File.OpenRead(filePath);

    // Retorna o resultado do upload
    return await UploadAsync(fileStream, fileName, bucketName, contentType);
  }

  /// <summary>
  /// Cria um arquivo no bucket de modo Assíncrono.
  /// </summary>
  /// <param name="fileUpload">Arquivo para upload. Em formato <see cref="IFormFile"/>.</param>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <param name="contentType">Tipo do arquivo.</param>
  /// <returns>Dados do arquivo criado no formato <see cref="StorageResponseDto"/>.</returns>
  /// <exception cref="AppException">Caso ocorra um erro ao enviar o arquivo.</exception>
  public async Task<StorageResponseDto> Create(IFormFile fileUpload, string fileName, string? bucketName = null, string? contentType = null)
  {
    bucketName ??= _bucketName;

    // Cria um MemoryStream para armazenar o arquivo em memória e posiciona o cursor no início do arquivo
    using MemoryStream fileStream = new();
    await fileUpload.CopyToAsync(fileStream);
    fileStream.Position = 0;

    // Retorna o resultado do upload
    return await UploadAsync(fileStream, fileName, bucketName, contentType);
  }

  /// <summary>
  /// Cria um arquivo no bucket de modo Assíncrono.
  /// </summary>
  /// <param name="fileStream">Arquivo em memória para upload. Em formato <see cref="MemoryStream"/>.</param>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <param name="contentType">Tipo do arquivo.</param>
  /// <returns>Dados do arquivo criado no formato <see cref="StorageResponseDto"/>.</returns>
  /// <exception cref="AppException">Caso ocorra um erro ao enviar o arquivo.</exception>
  public async Task<StorageResponseDto> Create(MemoryStream fileStream, string fileName, string? bucketName = null, string? contentType = null)
  {
    bucketName ??= _bucketName;

    // Retorna o resultado do upload
    return await UploadAsync(fileStream, fileName, bucketName, contentType);
  }

  /// <summary>
  /// Atualiza um arquivo no bucket de modo Assíncrono.
  /// </summary>
  /// <param name="oldFile">Arquivo no bucket a ser substituído.</param>
  /// <param name="filePath">Caminho do arquivo para upload. Em formato <see cref="string"/>.</param>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <param name="contentType">Tipo do arquivo.</param>
  /// <returns>Dados do arquivo criado no formato <see cref="StorageResponseDto"/>.</returns>
  /// <exception cref="ArgumentException">Quando o caminho do arquivo é nulo ou vazio.</exception>
  /// <exception cref="AppException">Caso ocorra um erro ao apagar o arquivo antigo.</exception>
  /// <exception cref="AppException">Caso ocorra um erro ao enviar o arquivo.</exception>
  public async Task<StorageResponseDto> Update(string oldFile, string filePath, string fileName, string? bucketName = null, string? contentType = null)
  {
    bucketName ??= _bucketName;

    // Deleta o arquivo antigo
    await EraseAsync(oldFile, bucketName);

    // Verifica se o caminho do arquivo é nulo ou vazio
    if (string.IsNullOrEmpty(filePath))
    {
      throw new ArgumentException("FileLocalUploadEmpty");
    }

    // Carrega o arquivo em memória
    using var fileStream = File.OpenRead(filePath);

    // Retorna o resultado do upload
    return await UploadAsync(fileStream, fileName, bucketName, contentType);
  }

  /// <summary>
  /// Atualiza um arquivo no bucket de modo Assíncrono.
  /// </summary>
  /// <param name="oldFile">Arquivo no bucket a ser substituído.</param>
  /// <param name="fileUpload">Arquivo para upload. Em formato <see cref="IFormFile"/>.</param>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <param name="contentType">Tipo do arquivo.</param>
  /// <returns>Dados do arquivo criado no formato <see cref="StorageResponseDto"/>.</returns>
  /// <exception cref="AppException">Caso ocorra um erro ao apagar o arquivo antigo.</exception>
  /// <exception cref="AppException">Caso ocorra um erro ao enviar o arquivo.</exception>
  public async Task<StorageResponseDto> Update(string oldFile, IFormFile fileUpload, string fileName, string? bucketName = null, string? contentType = null)
  {
    bucketName ??= _bucketName;

    // Deleta o arquivo antigo
    await EraseAsync(oldFile, bucketName);

    // Cria um MemoryStream para armazenar o arquivo em memória e posiciona o cursor no início do arquivo
    using MemoryStream fileStream = new();
    await fileUpload.CopyToAsync(fileStream);
    fileStream.Position = 0;

    // Retorna o resultado do upload
    return await UploadAsync(fileStream, fileName, bucketName, contentType);
  }

  /// <summary>
  /// Atualiza um arquivo no bucket de modo Assíncrono.
  /// </summary>
  /// <param name="oldFile">Arquivo no bucket a ser substituído.</param>
  /// <param name="fileStream">Arquivo em memória para upload. Em formato <see cref="MemoryStream"/>.</param>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <param name="contentType">Tipo do arquivo.</param>
  /// <returns>Dados do arquivo criado no formato <see cref="StorageResponseDto"/>.</returns>
  /// <exception cref="AppException">Caso ocorra um erro ao apagar o arquivo antigo.</exception>
  /// <exception cref="AppException">Caso ocorra um erro ao enviar o arquivo.</exception>
  public async Task<StorageResponseDto> Update(string oldFile, MemoryStream fileStream, string fileName, string? bucketName = null, string? contentType = null)
  {
    bucketName ??= _bucketName;

    // Deleta o arquivo antigo
    await EraseAsync(oldFile, bucketName);

    // Retorna o resultado do upload
    return await UploadAsync(fileStream, fileName, bucketName, contentType);
  }

  /// <summary>
  /// Deleta um arquivo de modo Assíncrono.
  /// </summary>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <exception cref="AppException">Caso ocorra um erro ao apagar o arquivo.</exception>
  /// <exception cref="AppException">Caso ocorra um erro interno do servidor.</exception>
  public async Task Delete(string fileName, string? bucketName = null)
  {
    bucketName ??= _bucketName;

    await EraseAsync(fileName, bucketName);
  }

  /// <summary>
  /// Realiza o download de um arquivo para memória de modo Assíncrono.
  /// </summary>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <returns>Arquivo em formato <see cref="Stream"/></returns>
  /// <exception cref="AppException">Caso ocorra um erro ao baixar o arquivo.</exception>
  /// <exception cref="AppException">Caso ocorra um erro interno do servidor.</exception>
  public async Task<Stream> Download(string fileName, string? bucketName = null)
  {
    bucketName ??= _bucketName;

    return await PullAsync(fileName, bucketName);
  }
}

using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Google;
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

namespace Tooark.Services;

/// <summary>
/// Serviço de Bucket.
/// </summary>
public class BucketService : IBucketService
{
  /// <summary>
  /// Provedor de Log.
  /// </summary>
  private readonly ILogger<BucketService> _logger;

  /// <summary>
  /// Opções de configuração do Bucket.
  /// </summary>
  private readonly BucketOptions _bucketOptions;

  /// <summary>
  /// Cliente AWS S3.
  /// </summary>
  private readonly IAmazonS3? _awsClient;

  /// <summary>
  /// Cliente Google Cloud Storage.
  /// </summary>
  private readonly StorageClient? _gcpClient;

  /// <summary>
  /// Construtor do serviço de Bucket.
  /// </summary>
  /// <param name="logger">Provedor de Log.</param>
  /// <param name="bucketOptions">Parâmetro para configurações do Bucket.</param>
  /// <param name="awsClient">Cliente AWS S3. Parâmetro Opcional.</param>
  /// <param name="gcpClient">Cliente Google Cloud Storage. Parâmetro Opcional.</param>
  public BucketService(
    ILogger<BucketService> logger,
    IOptions<BucketOptions> bucketOptions,
    IAmazonS3? awsClient = null,
    StorageClient? gcpClient = null
  )
  {
    _logger = logger;
    _bucketOptions = bucketOptions.Value;
    _awsClient = awsClient;
    _gcpClient = gcpClient;

    // Verifica se as opções do bucket são válidas
    if (_bucketOptions == null || string.IsNullOrEmpty(_bucketOptions.BucketName))
    {
      throw AppException.ServiceUnavailable("Bucket.BucketOptionsEmpty");
    }

    // Verifica se o provedor de nuvem é válido
    if (_bucketOptions.CloudProvider == CloudProvider.None)
    {
      throw AppException.ServiceUnavailable("Bucket.CloudProviderEmpty");
    }
    else if (_bucketOptions.CloudProvider == CloudProvider.AWS)
    {
      if (_bucketOptions.AWS == null)
      {
        throw AppException.ServiceUnavailable("Bucket.CredentialsEmpty;AWS");
      }
    }
    else if (_bucketOptions.CloudProvider == CloudProvider.GCP)
    {
      if (_bucketOptions.GCP == null)
      {
        throw AppException.ServiceUnavailable("Bucket.CredentialsEmpty;GCP");
      }
    }
  }

  /// <summary>
  /// Verifica se o nome do arquivo é nulo ou vazio.
  /// </summary>
  /// <param name="nameFile">Nome do arquivo.</param>
  /// <returns>Retorna <see langword="true"/> caso o nome do arquivo não seja nulo ou vazio.</returns>
  /// <exception cref="AppException.ServiceUnavailable">Caso o nome do arquivo seja nulo ou vazio.</exception>
  private bool NameFileNotNull(string nameFile)
  {
    // Verifica se o nome do arquivo é nulo ou vazio
    if (string.IsNullOrEmpty(nameFile))
    {
      _logger.LogError("Services.BucketService.NameFileNotNull: File Name is Empty");

      throw AppException.ServiceUnavailable("Bucket.FileNameEmpty");
    }

    return true;
  }

  /// <summary>
  /// Verifica se o nome do bucket é nulo ou vazio.
  /// </summary>
  /// <param name="bucketName">Nome do bucket.</param>
  /// <returns>Retorna <see langword="true"/> caso o nome do bucket não seja nulo ou vazio.</returns>
  /// <exception cref="AppException.ServiceUnavailable">Caso o nome do bucket seja nulo ou vazio.</exception>
  private bool BucketNameNotNull(string bucketName)
  {
    // Verifica se o nome do bucket é nulo ou vazio
    if (string.IsNullOrEmpty(bucketName))
    {
      _logger.LogError("Services.BucketService.BucketNameNotNull: Bucket Name is Empty");

      throw AppException.ServiceUnavailable("Bucket.BucketNameEmpty");
    }

    return true;
  }

  /// <summary>
  /// Verifica se o tamanho do arquivo é maior que o permitido.
  /// </summary>
  /// <param name="fileSize">Tamanho do arquivo.</param>
  /// <returns>Retorna <see langword="true"/> caso o tamanho do arquivo seja menor ou igual ao permitido.</returns>
  /// <exception cref="AppException.BadRequest">Caso o tamanho do arquivo seja maior que o permitido.</exception>
  private bool FileSizeAllow(long fileSize)
  {
    // Verifica se o tamanho do arquivo é maior que o permitido
    if (fileSize > _bucketOptions.FileSize)
    {
      throw AppException.BadRequest($"Bucket.FileSizeBigger;${_bucketOptions.FileSize}");
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
  /// Envia um arquivo para o bucket.
  /// </summary>
  /// <param name="fileStream">Arquivo a ser enviado. Em formato <see cref="Stream"/>.</param>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="contentType">Tipo do arquivo. Parâmetro Opcional.</param>
  /// <returns>Retorna um <see cref="BucketResponseDto"/> com as informações do arquivo enviado.</returns>
  /// <exception cref="AppException.InternalServerError">Caso o bucket não seja encontrado.</exception>
  /// <exception cref="AppException.InternalServerError">Caso ocorra um erro ao enviar o arquivo.</exception>
  /// <exception cref="AppException.InternalServerError">Caso ocorra um erro interno do servidor.</exception>
  private async Task<BucketResponseDto> UploadFileAsync(Stream fileStream, string fileName, string? contentType = null)
  {
    try
    {
      if (ValidFileUpload(fileName, _bucketOptions.BucketName, fileStream.Length))
      {
        if (_bucketOptions.CloudProvider == CloudProvider.AWS)
        {
          // Cria o cliente AWS S3
          using var awsClient = _awsClient ?? new AmazonS3Client(_bucketOptions.AWS, _bucketOptions.AWSRegion);

          // Cria a requisição de upload
          var putRequest = new PutObjectRequest
          {
            BucketName = _bucketOptions.BucketName,
            Key = fileName,
            InputStream = fileStream,
            CannedACL = _bucketOptions.AWSAcl
          };

          // Faz o upload do arquivo
          var objectUpload = await awsClient.PutObjectAsync(putRequest);

          // Verifica se o arquivo foi enviado com sucesso
          if (objectUpload.HttpStatusCode == HttpStatusCode.OK)
          {
            // Monta o link do arquivo
            var bucketLink = $"https://{_bucketOptions.BucketName}.s3.amazonaws.com";
            var fileLink = $"{bucketLink}/{fileName}";

            // Retorna os dados do arquivo
            return new BucketResponseDto()
            {
              Id = fileLink,
              FileName = fileName,
              BucketName = _bucketOptions.BucketName,
              DownloadLink = fileLink,
              BucketLink = bucketLink
            };
          }

          _logger.LogError(
            "Services.BucketService.UploadFileAsync.AWS:\nHttp Status:{HttpStatus}\n{Exception}",
            objectUpload.HttpStatusCode,
            objectUpload.ToString());
        }
        else if (_bucketOptions.CloudProvider == CloudProvider.GCP)
        {
          // Cria as credenciais do Google Cloud Storage
          var credentials = GoogleCredential.FromJsonParameters(_bucketOptions.GCP);

          // Cria o cliente Google Cloud Storage
          using var gcpClient = _gcpClient ?? StorageClient.Create(credentials);

          // Faz o upload do arquivo
          var objectUpload = await gcpClient.UploadObjectAsync(_bucketOptions.BucketName, fileName, contentType, fileStream);

          // Retorna os dados do arquivo
          return new BucketResponseDto()
          {
            Id = objectUpload.Id,
            FileName = objectUpload.Name,
            BucketName = objectUpload.Bucket,
            DownloadLink = objectUpload.MediaLink,
            BucketLink = objectUpload.SelfLink
          };
        }
      }

      _logger.LogError("Services.BucketService.UploadFileAsync: Error Uploading File To Bucket");

      throw AppException.InternalServerError("Bucket.FileErrorUploading");
    }
    catch (AmazonS3Exception e) when (e.StatusCode == HttpStatusCode.NotFound)
    {
      _logger.LogError(
        e,
        "Services.BucketService.UploadFileAsync.AmazonS3Exception:\nBucket Not Found\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError("Bucket.BucketNotFound");
    }
    catch (GoogleApiException e) when (e.Error.Code == (int)HttpStatusCode.NotFound)
    {
      _logger.LogError(
        e,
        "Services.BucketService.UploadFileAsync.GoogleApiException:\nBucket Not Found\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError("Bucket.BucketNotFound");
    }
    catch (Exception e) when (e is AmazonS3Exception)
    {
      _logger.LogError(
        e,
        "Services.BucketService.UploadFileAsync.AmazonS3Exception:\nError Uploading File To Bucket\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError("Bucket.FileErrorUploading");
    }
    catch (Exception e) when (e is GoogleApiException)
    {
      _logger.LogError(
        e,
        "Services.BucketService.UploadFileAsync.GoogleApiException:\nError Uploading File To Bucket\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError("Bucket.FileErrorUploading");
    }
    catch (Exception e)
    {
      _logger.LogError(
        e,
        "Services.BucketService.UploadFileAsync.Exception:\nError Uploading File To Bucket\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError("Bucket.FileErrorUploading");
    }
  }

  /// <summary>
  /// Apaga um arquivo do bucket.
  /// </summary>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <exception cref="AppException.InternalServerError">Caso o arquivo não seja encontrado.</exception>
  /// <exception cref="AppException.InternalServerError">Caso ocorra um erro ao deletar o arquivo.</exception>
  /// <exception cref="AppException.InternalServerError">Caso ocorra um erro interno do servidor.</exception>
  private async Task DeleteFileAsync(string fileName)
  {
    try
    {
      // Verifica se o nome do arquivo é nulo ou vazio
      if (ValidFileBucket(fileName, _bucketOptions.BucketName))
      {
        if (_bucketOptions.CloudProvider == CloudProvider.AWS)
        {
          // Cria o cliente AWS S3
          using var awsClient = _awsClient ?? new AmazonS3Client(_bucketOptions.AWS, _bucketOptions.AWSRegion);

          // Cria a requisição de deleção
          var deleteObjectRequest = new DeleteObjectRequest
          {
            BucketName = _bucketOptions.BucketName,
            Key = fileName
          };

          // Deleta o arquivo
          await awsClient.DeleteObjectAsync(deleteObjectRequest);

          return;
        }
        else if (_bucketOptions.CloudProvider == CloudProvider.GCP)
        {
          // Cria as credenciais do Google Cloud Storage
          var credentials = GoogleCredential.FromJsonParameters(_bucketOptions.GCP);

          // Cria o cliente Google Cloud Storage
          using var gcpClient = _gcpClient ?? StorageClient.Create(credentials);

          // Deleta o arquivo
          await gcpClient.DeleteObjectAsync(_bucketOptions.BucketName, fileName);

          return;
        }
      }

      _logger.LogError("Services.BucketService.DeleteFileAsync: Error Deleting File From Bucket");

      throw AppException.InternalServerError("Bucket.FileErrorDeleting");
    }
    catch (AmazonS3Exception e) when (e.StatusCode == HttpStatusCode.NotFound)
    {
      _logger.LogError(
        e,
        "Services.BucketService.DeleteFileAsync.AmazonS3Exception:\nBucket Not Found\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError("Bucket.BucketNotFound");
    }
    catch (GoogleApiException e) when (e.Error.Code == (int)HttpStatusCode.NotFound)
    {
      _logger.LogError(
        e,
        "Services.BucketService.DeleteFileAsync.GoogleApiException:\nBucket Not Found\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError("Bucket.BucketNotFound");
    }
    catch (Exception e) when (e is AmazonS3Exception)
    {
      _logger.LogError(
        e,
        "Services.BucketService.DeleteFileAsync.AmazonS3Exception:\nError Deleting File From Bucket\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError("Bucket.FileErrorDeleting");
    }
    catch (Exception e) when (e is GoogleApiException)
    {
      _logger.LogError(
        e,
        "Services.BucketService.DeleteFileAsync.GoogleApiException:\nError Deleting File From Bucket\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError("Bucket.FileErrorDeleting");
    }
    catch (Exception e)
    {
      _logger.LogError(
        e,
        "Services.BucketService.DeleteFileAsync.Exception:\nError Deleting File From Bucket\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError("Bucket.FileErrorDeleting");
    }
  }

  /// <summary>
  /// Baixa um arquivo do bucket.
  /// </summary>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="versionId">Id da versão do arquivo.Parâmetro Opcional.</param>
  /// <returns>Arquivo em formato <see cref="Stream"/>.</returns>
  /// <exception cref="AppException.InternalServerError">Caso o arquivo não seja encontrado.</exception>
  /// <exception cref="AppException.InternalServerError">Caso ocorra um erro ao baixar o arquivo.</exception>
  /// <exception cref="AppException.InternalServerError">Caso ocorra um erro interno do servidor.</exception>
  private async Task<Stream> DownloadFileAsync(string fileName, string? versionId = null)
  {
    try
    {
      // Verifica se o nome do arquivo é nulo ou vazio
      if (ValidFileBucket(fileName, _bucketOptions.BucketName))
      {
        if (_bucketOptions.CloudProvider == CloudProvider.AWS)
        {
          // Cria o cliente AWS S3
          using var awsClient = _awsClient ?? new AmazonS3Client(_bucketOptions.AWS, _bucketOptions.AWSRegion);

          // Carrega os dados para download do arquivo
          var objectRequest = new GetObjectRequest
          {
            BucketName = _bucketOptions.BucketName,
            Key = fileName,
            VersionId = versionId
          };

          // Realiza o download do arquivo
          var objectDownload = await awsClient.GetObjectAsync(objectRequest);

          // Verifica se o arquivo foi baixado com sucesso
          if (objectDownload.HttpStatusCode == HttpStatusCode.OK)
          {
            // Carrega o Stream de resposta
            using var responseStream = objectDownload.ResponseStream;

            // Cria um novo Stream
            Stream fileStream = new MemoryStream();

            // Copia o conteúdo do ResponseStream para o novo MemoryStream
            await responseStream.CopyToAsync(fileStream);

            // Posiciona o ponteiro do Stream no início
            fileStream.Position = 0;

            // Retorna o Stream
            return fileStream;
          }

          _logger.LogError(
            "Services.BucketService.DownloadFileAsync.AWS:\nHttp Status:{HttpStatus}\n{Exception}",
            objectDownload.HttpStatusCode,
            objectDownload.ToString());
        }
        else if (_bucketOptions.CloudProvider == CloudProvider.GCP)
        {
          // Cria as credenciais do Google Cloud Storage
          var credentials = GoogleCredential.FromJsonParameters(_bucketOptions.GCP);

          // Cria o cliente Google Cloud Storage
          using var gcpClient = _gcpClient ?? StorageClient.Create(credentials);

          // Cria um novo Stream
          Stream fileStream = new MemoryStream();

          // Faz o download do arquivo
          await gcpClient.DownloadObjectAsync(_bucketOptions.BucketName, fileName, fileStream);

          // Posiciona o ponteiro do Stream no início
          fileStream.Position = 0;

          // Retorna o Stream
          return fileStream;
        }
      }

      _logger.LogError("Services.BucketService.DownloadFileAsync: Error Downloading File From Bucket");

      throw AppException.InternalServerError("Bucket.FileErrorDownloading");
    }
    catch (AmazonS3Exception e) when (e.StatusCode == HttpStatusCode.NotFound)
    {
      _logger.LogError(
        e,
        "Services.BucketService.DownloadFileAsync.AmazonS3Exception:\nBucket Not Found\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError("Bucket.BucketNotFound");
    }
    catch (GoogleApiException e) when (e.Error.Code == (int)HttpStatusCode.NotFound)
    {
      _logger.LogError(
        e,
        "Services.BucketService.DownloadFileAsync.GoogleApiException:\nBucket Not Found\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError("Bucket.BucketNotFound");
    }
    catch (Exception e) when (e is AmazonS3Exception)
    {
      _logger.LogError(
        e,
        "Services.BucketService.DownloadFileAsync.AmazonS3Exception:\nError Downloading File From Bucket\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError("Bucket.FileErrorDownloading");
    }
    catch (Exception e) when (e is GoogleApiException)
    {
      _logger.LogError(
        e,
        "Services.BucketService.DownloadFileAsync.GoogleApiException:\nError Downloading File From Bucket\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError("Bucket.FileErrorDownloading");
    }
    catch (Exception e)
    {
      _logger.LogError(
        e,
        "Services.BucketService.DownloadFileAsync.Exception:\nError Downloading File From Bucket\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError("Bucket.FileErrorDownloading");
    }
  }

  /// <summary>
  /// Cria um arquivo no bucket.
  /// </summary>
  /// <param name="filePath">Caminho do arquivo para upload. Em formato <see cref="string"/>.</param>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket. Parâmetro Opcional</param>
  /// <param name="contentType">Tipo do arquivo. Parâmetro Opcional</param>
  /// <returns>Dados do arquivo criado no formato <see cref="BucketResponseDto"/>.</returns>
  /// <exception cref="AppException.BadRequest">Quando o caminho do arquivo é nulo ou vazio.</exception>
  /// <exception cref="AppException.InternalServerError">Caso ocorra um erro ao enviar o arquivo.</exception>
  public async Task<BucketResponseDto> Create(string filePath, string fileName, string? bucketName = null, string? contentType = null)
  {
    // Verifica se o nome do bucket é nulo ou vazio
    _bucketOptions.BucketName = bucketName ?? _bucketOptions.BucketName;

    // Verifica se o caminho do arquivo é nulo ou vazio
    if (string.IsNullOrEmpty(filePath))
    {
      throw AppException.BadRequest("Bucket.FilePathEmpty");
    }

    // Carrega o arquivo em memória
    using var fileStream = File.OpenRead(filePath);

    // Retorna o resultado do upload
    return await UploadFileAsync(fileStream, fileName, contentType);
  }

  /// <summary>
  /// Cria um arquivo no bucket.
  /// </summary>
  /// <param name="fileUpload">Arquivo para upload. Em formato <see cref="IFormFile"/>.</param>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket. Parâmetro Opcional</param>
  /// <param name="contentType">Tipo do arquivo. Parâmetro Opcional</param>
  /// <returns>Dados do arquivo criado no formato <see cref="BucketResponseDto"/>.</returns>
  /// <exception cref="AppException.InternalServerError">Caso ocorra um erro ao enviar o arquivo.</exception>
  public async Task<BucketResponseDto> Create(IFormFile fileUpload, string fileName, string? bucketName = null, string? contentType = null)
  {
    // Verifica se o nome do bucket é nulo ou vazio
    _bucketOptions.BucketName = bucketName ?? _bucketOptions.BucketName;

    // Cria um MemoryStream para armazenar o arquivo em memória e posiciona o cursor no início do arquivo
    using MemoryStream fileStream = new();
    await fileUpload.CopyToAsync(fileStream);
    fileStream.Position = 0;

    // Retorna o resultado do upload
    return await UploadFileAsync(fileStream, fileName, contentType);
  }

  /// <summary>
  /// Cria um arquivo no bucket.
  /// </summary>
  /// <param name="fileStream">Arquivo em memória para upload. Em formato <see cref="MemoryStream"/>.</param>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket. Parâmetro Opcional</param>
  /// <param name="contentType">Tipo do arquivo. Parâmetro Opcional</param>
  /// <returns>Dados do arquivo criado no formato <see cref="BucketResponseDto"/>.</returns>
  /// <exception cref="AppException.InternalServerError">Caso ocorra um erro ao enviar o arquivo.</exception>
  public async Task<BucketResponseDto> Create(MemoryStream fileStream, string fileName, string? bucketName = null, string? contentType = null)
  {
    // Verifica se o nome do bucket é nulo ou vazio
    _bucketOptions.BucketName = bucketName ?? _bucketOptions.BucketName;

    // Retorna o resultado do upload
    return await UploadFileAsync(fileStream, fileName, contentType);
  }

  /// <summary>
  /// Atualiza um arquivo no bucket.
  /// </summary>
  /// <param name="oldFile">Url do arquivo no bucket a ser substituído.</param>
  /// <param name="filePath">Caminho do arquivo para upload. Em formato <see cref="string"/>.</param>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket. Parâmetro Opcional</param>
  /// <param name="contentType">Tipo do arquivo. Parâmetro Opcional</param>
  /// <returns>Dados do arquivo criado no formato <see cref="BucketResponseDto"/>.</returns>
  /// <exception cref="AppException.BadRequest">Quando o caminho do arquivo é nulo ou vazio.</exception>
  /// <exception cref="AppException.InternalServerError">Caso ocorra um erro ao enviar o arquivo.</exception>
  public async Task<BucketResponseDto> Update(string oldFile, string filePath, string fileName, string? bucketName = null, string? contentType = null)
  {
    // Verifica se o nome do bucket é nulo ou vazio
    _bucketOptions.BucketName = bucketName ?? _bucketOptions.BucketName;

    // Deleta o arquivo antigo
    await DeleteFileAsync(oldFile);

    // Verifica se o caminho do arquivo é nulo ou vazio
    if (string.IsNullOrEmpty(filePath))
    {
      throw AppException.BadRequest("Bucket.FilePathEmpty");
    }

    // Carrega o arquivo em memória
    using var fileStream = File.OpenRead(filePath);

    // Retorna o resultado do upload
    return await UploadFileAsync(fileStream, fileName, contentType);
  }

  /// <summary>
  /// Atualiza um arquivo no bucket.
  /// </summary>
  /// <param name="oldFile">Url do arquivo no bucket a ser substituído.</param>
  /// <param name="fileUpload">Arquivo para upload. Em formato <see cref="IFormFile"/>.</param>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket. Parâmetro Opcional</param>
  /// <param name="contentType">Tipo do arquivo. Parâmetro Opcional</param>
  /// <returns>Dados do arquivo criado no formato <see cref="BucketResponseDto"/>.</returns>
  /// <exception cref="AppException.InternalServerError">Caso ocorra um erro ao enviar o arquivo.</exception>
  public async Task<BucketResponseDto> Update(string oldFile, IFormFile fileUpload, string fileName, string? bucketName = null, string? contentType = null)
  {
    // Verifica se o nome do bucket é nulo ou vazio
    _bucketOptions.BucketName = bucketName ?? _bucketOptions.BucketName;

    // Deleta o arquivo antigo
    await DeleteFileAsync(oldFile);

    // Cria um MemoryStream para armazenar o arquivo em memória e posiciona o cursor no início do arquivo
    using MemoryStream fileStream = new();
    await fileUpload.CopyToAsync(fileStream);
    fileStream.Position = 0;

    // Retorna o resultado do upload
    return await UploadFileAsync(fileStream, fileName, contentType);
  }

  /// <summary>
  /// Atualiza um arquivo no bucket.
  /// </summary>
  /// <param name="oldFile">Url do arquivo no bucket a ser substituído.</param>
  /// <param name="fileStream">Arquivo em memória para upload. Em formato <see cref="MemoryStream"/>.</param>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket. Parâmetro Opcional</param>
  /// <param name="contentType">Tipo do arquivo. Parâmetro Opcional</param>
  /// <returns>Dados do arquivo criado no formato <see cref="BucketResponseDto"/>.</returns>
  /// <exception cref="AppException.InternalServerError">Caso ocorra um erro ao enviar o arquivo.</exception>
  public async Task<BucketResponseDto> Update(string oldFile, MemoryStream fileStream, string fileName, string? bucketName = null, string? contentType = null)
  {
    // Verifica se o nome do bucket é nulo ou vazio
    _bucketOptions.BucketName = bucketName ?? _bucketOptions.BucketName;

    // Deleta o arquivo antigo
    await DeleteFileAsync(oldFile);

    // Retorna o resultado do upload
    return await UploadFileAsync(fileStream, fileName, contentType);
  }

  /// <summary>
  /// Deleta um arquivo.
  /// </summary>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket. Parâmetro Opcional</param>
  /// <exception cref="AppException.InternalServerError">Caso ocorra um erro ao apagar o arquivo.</exception>
  public async Task Delete(string fileName, string? bucketName = null)
  {
    // Verifica se o nome do bucket é nulo ou vazio
    _bucketOptions.BucketName = bucketName ?? _bucketOptions.BucketName;

    // Deleta o arquivo antigo
    await DeleteFileAsync(fileName);
  }

  /// <summary>
  /// Realiza o download de um arquivo para memória.
  /// </summary>
  /// <param name="fileName">Nome do arquivo.</param>
  /// <param name="bucketName">Nome do bucket. Parâmetro Opcional</param>
  /// <returns>Arquivo em formato <see cref="Stream"/></returns>
  /// <exception cref="AppException.InternalServerError">Caso ocorra um erro ao baixar o arquivo.</exception>
  public async Task<Stream> Download(string fileName, string? bucketName = null)
  {
    // Verifica se o nome do bucket é nulo ou vazio
    _bucketOptions.BucketName = bucketName ?? _bucketOptions.BucketName;

    // Baixa o arquivo antigo
    return await DownloadFileAsync(fileName);
  }
}

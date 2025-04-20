using System.Net;
using Google;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Logging;
using Tooark.Exceptions;
using Tooark.Storages.Adapters;
using Tooark.Storages.Dtos;
using Tooark.Storages.Options;

namespace Tooark.Storages;

/// <summary>
/// Classe de serviço para o Google Cloud Storage.
/// </summary>
public partial class StorageService
{
  /// <summary>
  /// Função que faz o upload de um arquivo para o Google Cloud Storage de forma assíncrona.
  /// </summary>
  /// <param name="options">As opções do GCP contendo credenciais e outras configurações.</param>
  /// <param name="fileStream">O stream do arquivo a ser enviado.</param>
  /// <param name="bucketName">O nome do bucket GCP onde o arquivo será enviado.</param>
  /// <param name="fileName">O nome do arquivo a ser enviado.</param>
  /// <param name="contentType">O tipo MIME do arquivo. Parâmetro opcional.</param>
  /// <returns>Os dados do arquivo enviado <see cref="UploadResponseDto"/>. O retorno é uma Task.</returns>
  /// <exception cref="BadRequestException">Se o bucket não for encontrado.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorreu um erro ao enviar o arquivo.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorrer um erro desconhecido.</exception>
  private async Task<UploadResponseDto> UploadToGcpAsync(
    GcpOptions options,
    Stream fileStream,
    string bucketName,
    string fileName,
    string? contentType = null
  )
  {
    try
    {
      // Cria as credenciais do Google Cloud Storage
      var credentials = GoogleCredential.FromJsonParameters(options.GetCredentials());

      // Cria o cliente Google Cloud Storage
      using var gcpClient = _gcpClient ?? StorageClient.Create(credentials);

      // Define as opções de ACL se necessário
      UploadObjectOptions? aclOptions = options.Acl == null ? null : new() { PredefinedAcl = options.Acl };

      // Faz o upload do arquivo
      var objectUpload = await gcpClient.UploadObjectAsync(bucketName, fileName, contentType, fileStream, aclOptions);

      // Retorna os dados do arquivo
      return new UploadResponseDto(objectUpload);
    }
    catch (GoogleApiException e) when (e.Error.Code == (int)HttpStatusCode.NotFound)
    {
      _logger.LogError(
        e,
        "Services.StorageService.UploadFileAsync.UploadToGcpAsync:\nBucket Not Found\n{Exception}",
        e.Message);

      throw new BadRequestException("Storage.BucketNotFound");
    }
    catch (Exception e) when (e is GoogleApiException)
    {
      _logger.LogError(
        e,
        "Services.StorageService.UploadFileAsync.UploadToGcpAsync:\nError Uploading File To Bucket\n{Exception}",
        e.Message);

      throw new InternalServerErrorException("Storage.FileErrorUploading");
    }
    catch (Exception e)
    {
      _logger.LogError(
        e,
        "Services.StorageService.UploadFileAsync.UploadToGcpAsync:\nError Unknown\n{Exception}",
        e.Message);

      throw new InternalServerErrorException("Storage.UnknownError");
    }
  }

  /// <summary>
  /// Função que deleta um arquivo do Google Cloud Storage de forma assíncrona.
  /// </summary>
  /// <param name="options">As opções do GCP contendo credenciais e outras configurações.</param>
  /// <param name="bucketName">O nome do bucket GCP onde o arquivo será deletado.</param>
  /// <param name="fileName">O nome do arquivo a ser deletado.</param>
  /// <returns>A mensagem de sucesso ou erro. O retorno é uma Task.</returns>
  /// <exception cref="BadRequestException">Se o arquivo não for encontrado.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorreu um erro ao apagar o arquivo.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorrer um erro desconhecido.</exception>
  private async Task<string> DeleteFromGcpAsync(
    GcpOptions options,
    string bucketName,
    string fileName
  )
  {
    try
    {
      // Cria as credenciais do Google Cloud Storage
      var credentials = GoogleCredential.FromJsonParameters(options.GetCredentials());

      // Cria o cliente Google Cloud Storage
      using var gcpClient = _gcpClient ?? StorageClient.Create(credentials);

      // Faz o delete do arquivo
      await gcpClient.DeleteObjectAsync(bucketName, fileName);

      // Retorna a mensagem de sucesso
      return "Storage.DeletedObject";
    }
    catch (GoogleApiException e) when (e.Error.Code == (int)HttpStatusCode.NotFound)
    {
      _logger.LogError(
        e,
        "Services.StorageService.DeleteFileAsync.DeleteFromGcpAsync:\nFile Not Found\n{Exception}",
        e.Message);

      throw new BadRequestException("Storage.FileNotFound");
    }
    catch (Exception e) when (e is GoogleApiException)
    {
      _logger.LogError(
        e,
        "Services.StorageService.DeleteFileAsync.DeleteFromGcpAsync:\nError Deleting File From Bucket\n{Exception}",
        e.Message);

      throw new InternalServerErrorException("Storage.FileErrorDeleting");
    }
    catch (Exception e)
    {
      _logger.LogError(
        e,
        "Services.StorageService.DeleteFileAsync.DeleteFromGcpAsync:\nError Unknown\n{Exception}",
        e.Message);

      throw new InternalServerErrorException("Storage.UnknownError");
    }
  }

  /// <summary>
  /// Função que faz o download de um arquivo do Google Cloud Storage de forma assíncrona.
  /// </summary>
  /// <param name="options">As opções do GCP contendo credenciais e outras configurações.</param>
  /// <param name="bucketName">O nome do bucket GCP onde o arquivo será baixado.</param>
  /// <param name="fileName">O nome do arquivo a ser baixado.</param>
  /// <returns>O stream do arquivo baixado <see cref="Stream"/>. O retorno é uma Task.</returns>
  /// <exception cref="BadRequestException">Se o arquivo não for encontrado.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorreu um erro ao baixar o arquivo.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorrer um erro desconhecido.</exception>
  private async Task<Stream> DownloadFromGcpAsync(
    GcpOptions options,
    string bucketName,
    string fileName
  )
  {
    try
    {
      // Cria as credenciais do Google Cloud Storage
      var credentials = GoogleCredential.FromJsonParameters(options.GetCredentials());

      // Cria o cliente Google Cloud Storage
      using var gcpClient = _gcpClient ?? StorageClient.Create(credentials);

      // Arquivo em memória para download
      Stream stream = new MemoryStream();

      // Faz o download do arquivo
      var downloadObject = await gcpClient.DownloadObjectAsync(bucketName, fileName, stream);

      // Retorna o stream do arquivo
      return stream;
    }
    catch (GoogleApiException e) when (e.Error.Code == (int)HttpStatusCode.NotFound)
    {
      _logger.LogError(
        e,
        "Services.StorageService.DownloadFileAsync.DownloadFromGcpAsync:\nFile Not Found\n{Exception}",
        e.Message);

      throw new BadRequestException("Storage.FileNotFound");
    }
    catch (Exception e) when (e is GoogleApiException)
    {
      _logger.LogError(
        e,
        "Services.StorageService.DownloadFileAsync.DownloadFromGcpAsync:\nError Downloading File From Bucket\n{Exception}",
        e.Message);

      throw new InternalServerErrorException("Storage.FileErrorDownloading");
    }
    catch (Exception e)
    {
      _logger.LogError(
        e,
        "Services.StorageService.DownloadFileAsync.DownloadFromGcpAsync:\nError Unknown\n{Exception}",
        e.Message);

      throw new InternalServerErrorException("Storage.UnknownError");
    }
  }

  /// <summary>
  /// Função que assina uma URL de um arquivo no Google Cloud Storage de forma assíncrona.
  /// </summary>
  /// <param name="options">As opções do GCP contendo credenciais e outras configurações.</param>
  /// <param name="bucketName">O nome do bucket GCP onde o arquivo está.</param>
  /// <param name="fileName">O nome do arquivo a ser assinado.</param>
  /// <param name="expiresMinutes">O tempo de expiração da URL em minutos.</param>
  /// <returns>A URL assinada temporária. O retorno é uma Task.</returns>
  /// <exception cref="BadRequestException">Se o arquivo não for encontrado.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorreu um erro ao assinar o arquivo.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorrer um erro desconhecido.</exception>
  private async Task<string> SignerFromGcpAsync(
    GcpOptions options,
    string bucketName,
    string fileName,
    int expiresMinutes
  )
  {
    try
    {
      // Cria as credenciais do Google Cloud Storage
      var credentials = GoogleCredential.FromJsonParameters(options.GetCredentials());

      // Cria o cliente Google Cloud Storage
      var gcpClientSigner = _gcpClientSigner ?? new UrlSignerAdapter(UrlSigner.FromCredential(credentials));

      // Define o tempo de expiração
      var expire = TimeSpan.FromMinutes(expiresMinutes);

      // Retorna a URL assinada
      return await gcpClientSigner.SignAsync(bucketName, fileName, expire);
    }
    catch (GoogleApiException e) when (e.Error.Code == (int)HttpStatusCode.NotFound)
    {
      _logger.LogError(
        e,
        "Services.StorageService.SignerFileAsync.SignerFromGcpAsync:\nFile Not Found\n{Exception}",
        e.Message);

      throw new BadRequestException("Storage.FileNotFound");
    }
    catch (Exception e) when (e is GoogleApiException)
    {
      _logger.LogError(
        e,
        "Services.StorageService.SignerFileAsync.SignerFromGcpAsync:\nError Sing File From Bucket\n{Exception}",
        e.Message);

      throw new InternalServerErrorException("Storage.FileErrorSigner");
    }
    catch (Exception e)
    {
      _logger.LogError(
        e,
        "Services.StorageService.SignerFileAsync.SignerFromGcpAsync:\nError Unknown\n{Exception}",
        e.Message);

      throw new InternalServerErrorException("Storage.UnknownError");
    }
  }
}

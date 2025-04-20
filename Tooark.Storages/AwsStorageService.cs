using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Logging;
using Tooark.Exceptions;
using Tooark.Storages.Dtos;
using Tooark.Storages.Options;

namespace Tooark.Storages;

/// <summary>
/// Classe de serviço para o AWS S3 Storage.
/// </summary>
public partial class StorageService
{
  /// <summary>
  /// Função que faz o upload de um arquivo para o AWS S3 Storage de forma assíncrona.
  /// </summary>
  /// <param name="options">As opções do AWS contendo credenciais e outras configurações.</param>
  /// <param name="fileStream">O stream do arquivo a ser enviado.</param>
  /// <param name="bucketName">O nome do bucket AWS onde o arquivo será enviado.</param>
  /// <param name="fileName">O nome do arquivo a ser enviado.</param>
  /// <param name="contentType">O tipo MIME do arquivo. Parâmetro opcional.</param>
  /// <returns>Os dados do arquivo enviado <see cref="UploadResponseDto"/>. O retorno é uma Task.</returns>
  /// <exception cref="BadRequestException">Se o bucket não for encontrado.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorreu um erro ao enviar o arquivo.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorrer um erro desconhecido.</exception>
  private async Task<UploadResponseDto> UploadToAwsAsync(
    AwsOptions options,
    Stream fileStream,
    string bucketName,
    string fileName,
    string? contentType = null
  )
  {
    try
    {
      // Cria o cliente AWS S3 Storage
      using var awsClient = _awsClient ?? new AmazonS3Client(options, options.Region);

      // Cria a requisição de upload
      var putRequest = new PutObjectRequest
      {
        BucketName = bucketName,
        Key = fileName,
        InputStream = fileStream,
        ContentType = contentType
      };

      // Define as opções de ACL se necessário
      if (options.Acl != null)
      {
        // Define a ACL
        putRequest.CannedACL = options.Acl;
      }

      // Faz o upload do arquivo
      var objectUpload = await awsClient.PutObjectAsync(putRequest);

      // Retorna os dados do arquivo
      return new UploadResponseDto(bucketName, fileName, options.Region, objectUpload);
    }
    catch (AmazonS3Exception e) when (e.StatusCode == HttpStatusCode.NotFound)
    {
      _logger.LogError(
        e,
        "Services.StorageService.UploadFileAsync.UploadToAwsAsync:\nBucket Not Found\n{Exception}",
        e.Message);

      throw new BadRequestException("Storage.BucketNotFound");
    }
    catch (Exception e) when (e is AmazonS3Exception)
    {
      _logger.LogError(
        e,
        "Services.StorageService.UploadFileAsync.UploadToAwsAsync:\nError Uploading File To Bucket\n{Exception}",
        e.Message);

      throw new InternalServerErrorException("Storage.FileErrorUploading");
    }
    catch (Exception e)
    {
      _logger.LogError(
        e,
        "Services.StorageService.UploadFileAsync.UploadToAwsAsync:\nError Unknown\n{Exception}",
        e.Message);

      throw new InternalServerErrorException("Storage.UnknownError");
    }
  }

  /// <summary>
  /// Função que deleta um arquivo do AWS S3 Storage de forma assíncrona.
  /// </summary>
  /// <param name="options">As opções do AWS contendo credenciais e outras configurações.</param>
  /// <param name="bucketName">O nome do bucket AWS onde o arquivo será deletado.</param>
  /// <param name="fileName">O nome do arquivo a ser deletado.</param>
  /// <returns>A mensagem de sucesso ou erro. O retorno é uma Task.</returns>
  /// <exception cref="BadRequestException">Se o arquivo não for encontrado.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorreu um erro ao apagar o arquivo.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorrer um erro desconhecido.</exception>
  private async Task<string> DeleteFromAwsAsync(
    AwsOptions options,
    string bucketName,
    string fileName
  )
  {
    try
    {
      // Cria o cliente AWS S3 Storage
      using var awsClient = _awsClient ?? new AmazonS3Client(options, options.Region);

      // Cria a requisição de delete
      var deleteRequest = new DeleteObjectRequest
      {
        BucketName = bucketName,
        Key = fileName
      };

      // Faz o delete do arquivo
      await awsClient.DeleteObjectAsync(deleteRequest);

      // Retorna a mensagem de sucesso
      return "Storage.DeletedObject";
    }
    catch (AmazonS3Exception e) when (e.StatusCode == HttpStatusCode.NotFound)
    {
      _logger.LogError(
        e,
        "Services.StorageService.DeleteFileAsync.DeleteFromAwsAsync:\nFile Not Found\n{Exception}",
        e.Message);

      throw new BadRequestException("Storage.FileNotFound");
    }
    catch (Exception e) when (e is AmazonS3Exception)
    {
      _logger.LogError(
        e,
        "Services.StorageService.DeleteFileAsync.DeleteFromAwsAsync:\nError Deleting File From Bucket\n{Exception}",
        e.Message);

      throw new InternalServerErrorException("Storage.FileErrorDeleting");
    }
    catch (Exception e)
    {
      _logger.LogError(
        e,
        "Services.StorageService.DeleteFileAsync.DeleteFromAwsAsync:\nError Unknown\n{Exception}",
        e.Message);

      throw new InternalServerErrorException("Storage.UnknownError");
    }
  }

  /// <summary>
  /// Função que faz o download de um arquivo do AWS S3 Storage de forma assíncrona.
  /// </summary>
  /// <param name="options">As opções do AWS contendo credenciais e outras configurações.</param>
  /// <param name="bucketName">O nome do bucket AWS onde o arquivo será baixado.</param>
  /// <param name="fileName">O nome do arquivo a ser baixado.</param>
  /// <returns>O stream do arquivo baixado <see cref="Stream"/>. O retorno é uma Task.</returns>
  /// <exception cref="BadRequestException">Se o arquivo não for encontrado.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorreu um erro ao baixar o arquivo.</exception>
  /// <exception cref="InternalServerErrorException">Se ocorrer um erro desconhecido.</exception>
  private async Task<Stream> DownloadFromAwsAsync(
    AwsOptions options,
    string bucketName,
    string fileName
  )
  {
    try
    {
      // Cria o cliente AWS S3 Storage
      using var awsClient = _awsClient ?? new AmazonS3Client(options, options.Region);

      // Cria a requisição de download
      var getRequest = new GetObjectRequest
      {
        BucketName = bucketName,
        Key = fileName
      };

      // Faz o download do arquivo
      var objectDownload = await awsClient.GetObjectAsync(getRequest);

      // Retorna o stream do arquivo
      return objectDownload.ResponseStream;
    }
    catch (AmazonS3Exception e) when (e.StatusCode == HttpStatusCode.NotFound)
    {
      _logger.LogError(
        e,
        "Services.StorageService.DownloadFileAsync.DownloadFromAwsAsync:\nFile Not Found\n{Exception}",
        e.Message);

      throw new BadRequestException("Storage.FileNotFound");
    }
    catch (Exception e) when (e is AmazonS3Exception)
    {
      _logger.LogError(
        e,
        "Services.StorageService.DownloadFileAsync.DownloadFromAwsAsync:\nError Downloading File From Bucket\n{Exception}",
        e.Message);

      throw new InternalServerErrorException("Storage.FileErrorDownloading");
    }
    catch (Exception e)
    {
      _logger.LogError(
        e,
        "Services.StorageService.DownloadFileAsync.DownloadFromAwsAsync:\nError Unknown\n{Exception}",
        e.Message);

      throw new InternalServerErrorException("Storage.UnknownError");
    }
  }

  /// <summary>
  /// Função que gera um link assinado temporário do arquivo do AWS S3 Storage de forma assíncrona.
  /// </summary>
  /// <param name="options">As opções do AWS contendo credenciais e outras configurações.</param>
  /// <param name="bucketName">O nome do bucket AWS onde o arquivo está.</param>
  /// <param name="fileName">O nome do arquivo a ser assinado.</param>
  /// <param name="expiresMinutes">O tempo de expiração do link em minutos.</param>
  /// <returns>O link assinado temporário. O retorno é uma Task.</returns>
  private async Task<string> SignerFromAwsAsync(
    AwsOptions options,
    string bucketName,
    string fileName,
    int expiresMinutes
  )
  {
    try
    {
      // Cria o cliente AWS S3 Storage
      using var awsClient = _awsClient ?? new AmazonS3Client(options, options.Region);

      // Cria a requisição de link assinado
      var request = new GetPreSignedUrlRequest
      {
        BucketName = bucketName,
        Key = fileName,
        Expires = DateTime.UtcNow.AddMinutes(expiresMinutes)
      };

      // Gera o link assinado
      return await awsClient.GetPreSignedURLAsync(request);
    }
    catch (AmazonS3Exception e) when (e.StatusCode == HttpStatusCode.NotFound)
    {
      _logger.LogError(
        e,
        "Services.StorageService.SignerFileAsync.SignerFromAwsAsync:\nFile Not Found\n{Exception}",
        e.Message);

      throw new BadRequestException("Storage.FileNotFound");
    }
    catch (Exception e) when (e is AmazonS3Exception)
    {
      _logger.LogError(
        e,
        "Services.StorageService.SignerFileAsync.SignerFromAwsAsync:\nError Sing File From Bucket\n{Exception}",
        e.Message);

      throw new InternalServerErrorException("Storage.FileErrorSigner");
    }
    catch (Exception e)
    {
      _logger.LogError(
        e,
        "Services.StorageService.SignerFileAsync.SignerFromAwsAsync:\nError Unknown\n{Exception}",
        e.Message);

      throw new InternalServerErrorException("Storage.UnknownError");
    }
  }
}

using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Logging;
using Tooark.Dtos;
using Tooark.Exceptions;

namespace Tooark.Services.Storage;

internal partial class StorageService
{
  /// <summary>
  /// Realiza o upload de um arquivo para o AWS S3 de forma Assíncrona.
  /// </summary>
  /// <param name="fileStream">Arquivo a ser enviado. Em formato <see cref="Stream"/></param>
  /// <param name="nameFile">Nome do arquivo</param>
  /// <param name="bucketName">Nome do bucket</param>
  /// <param name="cannedACL">ACL do arquivo</param>
  /// <returns>Retorna um <see cref="StorageResponseDto"/> com as informações do arquivo enviado</returns>
  /// <exception cref="AppException">Caso o bucket não seja encontrado</exception>
  /// <exception cref="AppException">Caso ocorra um erro ao enviar o arquivo</exception>
  /// <exception cref="AppException">Caso ocorra um erro interno do servidor</exception>
  internal async Task<StorageResponseDto> AwsUploadAsync(
    Stream fileStream,
    string nameFile,
    string bucketName,
    S3CannedACL cannedACL
  )
  {
    // Verifica se o cliente S3 foi configurado
    if (_s3Client == null)
    {
      throw AppException.InternalServerError("FileUploadNotConfigured");
    }
    
    try
    {
      // Carrega os dados para upload do arquivo
      var objectRequest = new PutObjectRequest
      {
        InputStream = fileStream,
        Key = nameFile,
        BucketName = bucketName,
        CannedACL = cannedACL != S3CannedACL.NoACL ? cannedACL : S3CannedACL.Private
      };

      // Realiza o upload do arquivo
      var objectUpload = await _s3Client.PutObjectAsync(objectRequest);

      if (objectUpload.HttpStatusCode == HttpStatusCode.OK)
      {
        // Monta o link do arquivo
        var bucketLink = $"https://{bucketName}.s3.amazonaws.com";
        var fileLink = $"{bucketLink}/{nameFile}";

        // Retorna os dados do arquivo
        return new StorageResponseDto()
        {
          Id = fileLink,
          FileName = nameFile,
          BucketName = bucketName,
          DownloadLink = fileLink,
          BucketLink = bucketLink
        };
      }

      _logger.LogError(
        "Services.StorageService.AwsUploadAsync:\nHttp Status:{HttpStatus}\n{Exception}",
        objectUpload.HttpStatusCode,
        objectUpload.ToString());

      throw AppException.InternalServerError("FileErrorUploading");
    }
    catch (AmazonS3Exception e) when (e.StatusCode == HttpStatusCode.NotFound)
    {
      _logger.LogError(
        e,
        "Services.StorageService.AwsUploadAsync.AmazonS3Exception:\nBucket Not Found\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError("BucketNotFound");
    }
    catch (Exception e) when (e is AmazonS3Exception)
    {
      _logger.LogError(
        e,
        "Services.StorageService.AwsUploadAsync.AmazonS3Exception:\nFile Error Uploading\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError("FileErrorUploading");
    }
    catch (Exception e)
    {
      _logger.LogError(
        e,
        "Services.StorageService.AwsUploadAsync.Exception:\nError When Uploading File\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError();
    }
  }

  /// <summary>
  /// Deleta um arquivo do AWS S3 de forma Assíncrona.
  /// </summary>
  /// <param name="nameFile">Nome do arquivo</param>
  /// <param name="bucketName">Nome do bucket</param>
  /// <exception cref="AppException">Caso o arquivo não seja encontrado</exception>
  /// <exception cref="AppException">Caso ocorra um erro ao deletar o arquivo</exception>
  /// <exception cref="AppException">Caso ocorra um erro interno do servidor</exception>
  internal async Task AwsDeleteAsync(
    string nameFile,
    string bucketName
  )
  {
    // Verifica se o cliente S3 foi configurado
    if (_s3Client == null)
    {
      throw AppException.InternalServerError("FileUploadNotConfigured");
    }

    try
    {
      var deleteObjectRequest = new DeleteObjectRequest
      {
        BucketName = bucketName,
        Key = nameFile
      };

      await _s3Client.DeleteObjectAsync(deleteObjectRequest);
    }
    catch (AmazonS3Exception e) when (e.StatusCode == HttpStatusCode.NotFound)
    {
      _logger.LogError(
        e,
        "Services.StorageService.AwsDeleteSync.AmazonS3Exception:\nFile Not Found\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError("FileNotFound");
    }
    catch (Exception e) when (e is AmazonS3Exception)
    {
      _logger.LogError(
        e,
        "Services.StorageService.AwsDeleteSync.AmazonS3Exception:\nFile Error Deleting\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError("FileErrorDeleting");
    }
    catch (Exception e)
    {
      _logger.LogError(
        e,
        "Services.StorageService.AwsDeleteSync.Exception:\nError When Deleting File\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError();
    }
  }

  /// <summary>
  /// Realiza o download de um arquivo do AWS S3 de forma Assíncrona.
  /// </summary>
  /// <param name="nameFile">Nome do arquivo</param>
  /// <param name="bucketName">Nome do bucket</param>
  /// <param name="versionId">Versão do arquivo</param>
  /// <returns>Arquivo em formato <see cref="Stream"/></returns>
  /// <exception cref="AppException">Caso o bucket não seja encontrado</exception>
  /// <exception cref="AppException">Caso ocorra um erro ao baixar o arquivo</exception>
  /// <exception cref="AppException">Caso ocorra um erro interno do servidor</exception>
  internal async Task<Stream> AwsDownloadAsync(
    string nameFile,
    string bucketName,
    string? versionId = null
  )
  {
    // Verifica se o cliente S3 foi configurado
    if (_s3Client == null)
    {
      throw AppException.InternalServerError("FileUploadNotConfigured");
    }

    try
    {
      // Carrega os dados para download do arquivo
      var objectRequest = new GetObjectRequest
      {
        BucketName = bucketName,
        Key = nameFile,
        VersionId = versionId
      };

      // Cria um novo Stream
      Stream fileStream = new MemoryStream();

      // Realiza o download do arquivo
      GetObjectResponse objectUpload = await _s3Client.GetObjectAsync(objectRequest);

      // Atribui o Stream do arquivo
      fileStream = objectUpload.ResponseStream;
      
      // Posiciona o ponteiro do Stream no início
      fileStream.Position = 0;

      // Retorna o Stream
      return fileStream;
    }
    catch (AmazonS3Exception e) when (e.StatusCode == HttpStatusCode.NotFound)
    {
      _logger.LogError(
        e,
        "Services.StorageService.AwsDownloadAsync.AmazonS3Exception:\nBucket Not Found\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError("BucketNotFound");
    }
    catch (Exception e) when (e is AmazonS3Exception)
    {
      _logger.LogError(
        e,
        "Services.StorageService.AwsDownloadAsync.AmazonS3Exception:\nFile Error Downloading\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError("FileErrorDownloading");
    }
    catch (Exception e)
    {
      _logger.LogError(
        e,
        "Services.StorageService.AwsDownloadAsync.Exception:\nError When Downloading File\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError();
    }
  }
}

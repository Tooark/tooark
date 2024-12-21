using Google;
using Microsoft.Extensions.Logging;
using Tooark.Dtos;
using Tooark.Exceptions;

namespace Tooark.Services.Storage;

internal partial class StorageService
{
  /// <summary>
  /// Envia um arquivo para o Google Cloud Storage de forma Assíncrona.
  /// </summary>
  /// <param name="fileStream">Arquivo a ser enviado. Em formato <see cref="Stream"/></param>
  /// <param name="nameFile">Nome do arquivo</param>
  /// <param name="bucketName">Nome do bucket</param>
  /// <param name="contentType">Tipo do arquivo</param>
  /// <returns>Retorna um <see cref="StorageResponseDto"/> com as informações do arquivo enviado</returns>
  /// <exception cref="AppException">Caso o bucket não seja encontrado</exception>
  /// <exception cref="AppException">Caso ocorra um erro ao enviar o arquivo</exception>
  /// <exception cref="AppException">Caso ocorra um erro interno do servidor</exception>
  internal async Task<StorageResponseDto> GcpUploadAsync(
    Stream fileStream,
    string nameFile,
    string bucketName,
    string? contentType = null
  )
  {
    try
    {
      // Realiza o upload do arquivo
      var objectUpload = await _storageClient.UploadObjectAsync(bucketName, nameFile, contentType, fileStream);

      // Retorna os dados do arquivo
      return new StorageResponseDto()
      {
        Id = objectUpload.Id,
        FileName = objectUpload.Name,
        BucketName = objectUpload.Bucket,
        DownloadLink = objectUpload.MediaLink,
        BucketLink = objectUpload.SelfLink
      };
    }
    catch (GoogleApiException e) when (e.Error.Code == 404)
    {
      _logger.LogError(
        e,
        "Services.StorageService.GcpUploadAsync.GoogleApiException:\nBucket Not Found\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError("BucketNotFound");
    }
    catch (Exception e) when (e is GoogleApiException)
    {
      _logger.LogError(
        e,
        "Services.StorageService.GcpUploadAsync.GoogleApiException:\nFile Error Uploading\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError("FileErrorUploading");
    }
    catch (Exception e)
    {
      _logger.LogError(
        e,
        "Services.StorageService.GcpUploadAsync.Exception:\nError When Uploading File\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError();
    }
  }

  /// <summary>
  /// Deleta um arquivo do Google Cloud Storage de forma Assíncrona.
  /// </summary>
  /// <param name="nameFile">Nome do arquivo</param>
  /// <param name="bucketName">Nome do bucket</param>
  /// <exception cref="AppException">Caso o arquivo não seja encontrado</exception>
  /// <exception cref="AppException">Caso ocorra um erro ao deletar o arquivo</exception>
  /// <exception cref="AppException">Caso ocorra um erro interno do servidor</exception>
  internal async Task GcpDeleteAsync(
    string nameFile,
    string bucketName
  )
  {
    try
    {
      // Deleta o arquivo
      await _storageClient.DeleteObjectAsync(bucketName, nameFile);
    }
    catch (GoogleApiException e) when (e.Error.Code == 404)
    {
      _logger.LogError(
        e,
        "Services.StorageService.GcpDeleteAsync.GoogleApiException:\nFile Not Found\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError("FileNotFound");
    }
    catch (Exception e) when (e is GoogleApiException)
    {
      _logger.LogError(
        e,
        "Services.StorageService.GcpDeleteAsync.GoogleApiException:\nFile Error Deleting\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError("FileErrorDeleting");
    }
    catch (Exception e)
    {
      _logger.LogError(
        e,
        "Services.StorageService.GcpDeleteAsync.Exception:\nError When Deleting File\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError();
    }
  }

  /// <summary>
  /// Faz o download de um arquivo do Google Cloud Storage de forma Assíncrona.
  /// </summary>
  /// <param name="nameFile"></param>
  /// <param name="bucketName"></param>
  /// <returns>Arquivo em formato <see cref="Stream"/></returns>
  /// <exception cref="AppException">Caso o arquivo não seja encontrado</exception>
  /// <exception cref="AppException">Caso ocorra um erro ao fazer o download do arquivo</exception>
  /// <exception cref="AppException">Caso ocorra um erro interno do servidor</exception>
  internal async Task<Stream> GcpDownloadAsync(
    string nameFile,
    string bucketName
  )
  {
    try
    {
      // Cria um novo Stream
      Stream fileStream = new MemoryStream();

      // Faz o download do arquivo
      await _storageClient.DownloadObjectAsync(bucketName, nameFile, fileStream);
      
      // Posiciona o ponteiro do Stream no início
      fileStream.Position = 0;

      // Retorna o Stream
      return fileStream;
    }
    catch (GoogleApiException e) when (e.Error.Code == 404)
    {
      _logger.LogError(
        e,
        "Services.StorageService.GcpDownloadAsync.GoogleApiException:\nFile Not Found\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError("FileNotFound");
    }
    catch (Exception e) when (e is GoogleApiException)
    {
      _logger.LogError(
        e,
        "Services.StorageService.GcpDownloadAsync.GoogleApiException:\nFile Error Downloading\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError("FileErrorDownloading");
    }
    catch (Exception e)
    {
      _logger.LogError(
        e,
        "Services.StorageService.GcpDownloadAsync.Exception:\nError When Downloading File\n{Exception}",
        e.ToString());

      throw AppException.InternalServerError();
    }
  }
}

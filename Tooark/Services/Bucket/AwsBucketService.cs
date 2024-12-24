namespace Tooark.Services.Bucket;

internal partial class BucketService
{
  internal async Task<StorageResponseDto> AwsUploadAdditionalAsync(
      Stream fileStream,
      string nameFile,
      string bucketName,
      S3CannedACL cannedACL
  )
  {
    if (_s3Client == null)
    {
      throw AppException.InternalServerError("FileUploadNotConfigured");
    }

    try
    {
      var objectRequest = new PutObjectRequest
      {
        InputStream = fileStream,
        Key = nameFile,
        BucketName = bucketName,
        CannedACL = cannedACL != S3CannedACL.NoACL ? cannedACL : S3CannedACL.Private
      };

      var objectUpload = await _s3Client.PutObjectAsync(objectRequest);

      if (objectUpload.HttpStatusCode == HttpStatusCode.OK)
      {
        var bucketLink = $"https://{bucketName}.s3.amazonaws.com";
        var fileLink = $"{bucketLink}/{nameFile}";

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
          "Services.StorageService.AwsUploadAdditionalAsync:\nHttp Status:{HttpStatus}\n{Exception}",
          objectUpload.HttpStatusCode,
          objectUpload.ToString());

      throw AppException.InternalServerError("FileErrorUploading");
    }
    catch (AmazonS3Exception e) when (e.StatusCode == HttpStatusCode.NotFound)
    {
      _logger.LogError(
          e,
          "Services.StorageService.AwsUploadAdditionalAsync.AmazonS3Exception:\nBucket Not Found\n{Exception}",
          e.ToString());

      throw AppException.InternalServerError("BucketNotFound");
    }
    catch (Exception e) when (e is AmazonS3Exception)
    {
      _logger.LogError(
          e,
          "Services.StorageService.AwsUploadAdditionalAsync.AmazonS3Exception:\nFile Error Uploading\n{Exception}",
          e.ToString());

      throw AppException.InternalServerError("FileErrorUploading");
    }
    catch (Exception e)
    {
      _logger.LogError(
          e,
          "Services.StorageService.AwsUploadAdditionalAsync.Exception:\nError When Uploading File\n{Exception}",
          e.ToString());

      throw AppException.InternalServerError();
    }
  }
}

using System.Net;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Google;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Tooark.Enums;
using Tooark.Exceptions;
using Tooark.Options;
using Tooark.Services;
using Tooark.Tests.Moq.Dto;

namespace Tooark.Tests.Services;

public class BucketServiceTests
{
  private readonly Mock<ILogger<BucketService>> _loggerMock;

  private readonly Mock<IOptions<BucketOptions>> _awsOptionsMock;
  private readonly Mock<IOptions<BucketOptions>> _gcpOptionsMock;

  private readonly Mock<IAmazonS3> _awsMock;
  private readonly Mock<StorageClient> _gcpMock;

  private readonly BucketService _awsBucketService;
  private readonly BucketService _gcpBucketService;

  private readonly BucketOptions _awsBucketOptions;
  private readonly BucketOptions _gcpBucketOptions;

  public BucketServiceTests()
  {
    _loggerMock = new Mock<ILogger<BucketService>>();
    _awsOptionsMock = new Mock<IOptions<BucketOptions>>();
    _gcpOptionsMock = new Mock<IOptions<BucketOptions>>();

    _awsMock = new Mock<IAmazonS3>();
    _awsBucketOptions = new BucketOptions
    {
      BucketName = "test-bucket",
      CloudProvider = CloudProvider.AWS,
      AWS = new BasicAWSCredentials("access-key", "secret-key"),
      FileSize = 10485760 // 10 MB
    };
    _awsOptionsMock.Setup(o => o.Value).Returns(_awsBucketOptions);
    _awsBucketService = new BucketService(_loggerMock.Object, _awsOptionsMock.Object, _awsMock.Object);

    _gcpMock = new Mock<StorageClient>();
    _gcpBucketOptions = new BucketOptions
    {
      BucketName = "test-bucket",
      CloudProvider = CloudProvider.GCP,
      GCP = new GcpCredentialDto().GCP,
      FileSize = 10485760 // 10 MB
    };
    _gcpOptionsMock.Setup(o => o.Value).Returns(_gcpBucketOptions);
    _gcpBucketService = new BucketService(_loggerMock.Object, _gcpOptionsMock.Object, null, _gcpMock.Object);
  }

  // Teste para Criar um arquivo no bucket com um caminho de arquivo local na Cloud da AWS
  [Fact]
  public async Task Create_WithAwsProviderAndFilePath_ShouldReturnBucketResponseDto()
  {
    // Arrange
    var filePath = "testFile.txt";
    var fileName = "testFile.txt";
    await File.WriteAllTextAsync(filePath, "test content");

    // Mock S3 response
    _awsMock
      .Setup(s3 => s3.PutObjectAsync(It.IsAny<PutObjectRequest>(), default))
      .ReturnsAsync(new PutObjectResponse { HttpStatusCode = HttpStatusCode.OK });

    // Act
    var result = await _awsBucketService.Create(filePath, fileName);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(fileName, result.FileName);
  }

  // Teste para Criar um arquivo no bucket com um IFormFile na Cloud da AWS
  [Fact]
  public async Task Create_WithAwsProviderAndIFormFile_ShouldReturnBucketResponseDto()
  {
    // Arrange
    var fileMock = new Mock<IFormFile>();
    var fileName = "testForm.txt";
    var content = "test content";
    var fileStream = new MemoryStream();
    var writer = new StreamWriter(fileStream);
    writer.Write(content);
    writer.Flush();
    fileStream.Position = 0;
    fileMock.Setup(f => f.OpenReadStream()).Returns(fileStream);
    fileMock.Setup(f => f.FileName).Returns(fileName);
    fileMock.Setup(f => f.Length).Returns(fileStream.Length);

    // Mock S3 response
    _awsMock
      .Setup(s3 => s3.PutObjectAsync(It.IsAny<PutObjectRequest>(), default))
      .ReturnsAsync(new PutObjectResponse { HttpStatusCode = HttpStatusCode.OK });

    // Act
    var result = await _awsBucketService.Create(fileMock.Object, fileName);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(fileName, result.FileName);
  }

  // Teste para Criar um arquivo no bucket com um MemoryStream na Cloud da AWS
  [Fact]
  public async Task Create_WithAwsProviderAndMemoryStream_ShouldReturnBucketResponseDto()
  {
    // Arrange
    var fileStream = new MemoryStream();
    var writer = new StreamWriter(fileStream);
    writer.Write("test content");
    writer.Flush();
    fileStream.Position = 0;
    var fileName = "testStream.txt";

    // Mock S3 response
    _awsMock
      .Setup(s3 => s3.PutObjectAsync(It.IsAny<PutObjectRequest>(), default))
      .ReturnsAsync(new PutObjectResponse { HttpStatusCode = HttpStatusCode.OK });

    // Act
    var result = await _awsBucketService.Create(fileStream, fileName);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(fileName, result.FileName);
  }

  // Teste para Atualizar um arquivo no bucket com um caminho de arquivo local na Cloud da AWS
  [Fact]
  public async Task Update_WithAwsProviderAndFilePath_ShouldReturnBucketResponseDto()
  {
    // Arrange
    var oldFile = "oldFile.txt";
    var filePath = "testFile.txt";
    var fileName = "testFile.txt";
    await File.WriteAllTextAsync(filePath, "test content");

    // Mock S3 response
    _awsMock
      .Setup(s3 => s3.PutObjectAsync(It.IsAny<PutObjectRequest>(), default))
      .ReturnsAsync(new PutObjectResponse { HttpStatusCode = HttpStatusCode.OK });

    // Act
    var result = await _awsBucketService.Update(oldFile, filePath, fileName);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(fileName, result.FileName);
  }

  // Teste para Atualizar um arquivo no bucket com um IFormFile na Cloud da AWS
  [Fact]
  public async Task Update_WithAwsProviderAndIFormFile_ShouldReturnBucketResponseDto()
  {
    // Arrange
    var oldFile = "oldForm.txt";
    var fileMock = new Mock<IFormFile>();
    var fileName = "testForm.txt";
    var content = "test content";
    var fileStream = new MemoryStream();
    var writer = new StreamWriter(fileStream);
    writer.Write(content);
    writer.Flush();
    fileStream.Position = 0;
    fileMock.Setup(f => f.OpenReadStream()).Returns(fileStream);
    fileMock.Setup(f => f.FileName).Returns(fileName);
    fileMock.Setup(f => f.Length).Returns(fileStream.Length);

    // Mock S3 response
    _awsMock
      .Setup(s3 => s3.PutObjectAsync(It.IsAny<PutObjectRequest>(), default))
      .ReturnsAsync(new PutObjectResponse { HttpStatusCode = HttpStatusCode.OK });

    // Act
    var result = await _awsBucketService.Update(oldFile, fileMock.Object, fileName);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(fileName, result.FileName);
  }

  // Teste para Atualizar um arquivo no bucket com um MemoryStream na Cloud da AWS
  [Fact]
  public async Task Update_WithAwsProviderAndMemoryStream_ShouldReturnBucketResponseDto()
  {
    // Arrange
    var oldFile = "oldStream.txt";
    var fileStream = new MemoryStream();
    var writer = new StreamWriter(fileStream);
    writer.Write("test content");
    writer.Flush();
    fileStream.Position = 0;
    var fileName = "testStream.txt";

    // Mock S3 response
    _awsMock
      .Setup(s3 => s3.PutObjectAsync(It.IsAny<PutObjectRequest>(), default))
      .ReturnsAsync(new PutObjectResponse { HttpStatusCode = HttpStatusCode.OK });

    // Act
    var result = await _awsBucketService.Update(oldFile, fileStream, fileName);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(fileName, result.FileName);
  }

  // Teste para Deletar um arquivo no bucket na Cloud da AWS
  [Fact]
  public async Task Delete_WithAwsProvider_ShouldNotThrowException()
  {
    // Arrange
    var fileName = "testDelete.txt";

    // Mock S3 response
    _awsMock
      .Setup(s3 => s3.DeleteObjectAsync(It.IsAny<DeleteObjectRequest>(), default))
      .ReturnsAsync(new DeleteObjectResponse { HttpStatusCode = HttpStatusCode.OK });

    // Act & Assert
    await _awsBucketService.Delete(fileName);
  }

  // Teste para Baixar um arquivo do bucket na Cloud da AWS
  [Fact]
  public async Task Download_WithAwsProvider_ShouldReturnStream()
  {
    // Arrange
    var fileName = "testDownload.txt";
    var responseStream = new MemoryStream();
    var writer = new StreamWriter(responseStream);
    writer.Write("test content");
    writer.Flush();
    responseStream.Position = 0;

    // Mock S3 response
    _awsMock
      .Setup(s3 => s3.GetObjectAsync(It.IsAny<GetObjectRequest>(), default))
      .ReturnsAsync(new GetObjectResponse
      {
        HttpStatusCode = HttpStatusCode.OK,
        ResponseStream = responseStream
      });

    // Act
    var result = await _awsBucketService.Download(fileName);

    // Assert
    Assert.NotNull(result);
    Assert.IsType<MemoryStream>(result);
  }

  // Teste para Criar um arquivo no bucket com um caminho de arquivo local na Cloud da GCP
  [Fact]
  public async Task Create_WithGcpProviderAndFilePath_ShouldReturnBucketResponseDto()
  {
    // Arrange
    var filePath = "testFile.txt";
    var fileName = "testFile.txt";
    await File.WriteAllTextAsync(filePath, "test content");

    // Mock Storage response
    _gcpMock
      .Setup(storage => storage.UploadObjectAsync(
        It.IsAny<string>(), // bucketName
        It.IsAny<string>(), // objectName
        It.IsAny<string>(), // contentType
        It.IsAny<Stream>(), // source
        It.IsAny<UploadObjectOptions>(), // options
        It.IsAny<CancellationToken>(),
        It.IsAny<IProgress<Google.Apis.Upload.IUploadProgress>>()) // progress
      ).ReturnsAsync(new Google.Apis.Storage.v1.Data.Object { Name = fileName });

    // Act
    var result = await _gcpBucketService.Create(filePath, fileName);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(fileName, result.FileName);
  }

  // Teste para Criar um arquivo no bucket com um IFormFile na Cloud da GCP
  [Fact]
  public async Task Create_WithGcpProviderAndIFormFile_ShouldReturnBucketResponseDto()
  {
    // Arrange
    var fileMock = new Mock<IFormFile>();
    var fileName = "testForm.txt";
    var content = "test content";
    var fileStream = new MemoryStream();
    var writer = new StreamWriter(fileStream);
    writer.Write(content);
    writer.Flush();
    fileStream.Position = 0;
    fileMock.Setup(f => f.OpenReadStream()).Returns(fileStream);
    fileMock.Setup(f => f.FileName).Returns(fileName);
    fileMock.Setup(f => f.Length).Returns(fileStream.Length);

    // Mock Storage response
    _gcpMock
      .Setup(storage => storage.UploadObjectAsync(
        It.IsAny<string>(), // bucketName
        It.IsAny<string>(), // objectName
        It.IsAny<string>(), // contentType
        It.IsAny<Stream>(), // source
        It.IsAny<UploadObjectOptions>(), // options
        It.IsAny<CancellationToken>(),
        It.IsAny<IProgress<Google.Apis.Upload.IUploadProgress>>()) // progress
      ).ReturnsAsync(new Google.Apis.Storage.v1.Data.Object { Name = fileName });

    // Act
    var result = await _gcpBucketService.Create(fileMock.Object, fileName);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(fileName, result.FileName);
  }

  // Teste para Criar um arquivo no bucket com um MemoryStream na Cloud da GCP
  [Fact]
  public async Task Create_WithGcpProviderAndMemoryStream_ShouldReturnBucketResponseDto()
  {
    // Arrange
    var fileStream = new MemoryStream();
    var writer = new StreamWriter(fileStream);
    writer.Write("test content");
    writer.Flush();
    fileStream.Position = 0;
    var fileName = "testStream.txt";

    // Mock Storage response
    _gcpMock
      .Setup(storage => storage.UploadObjectAsync(
        It.IsAny<string>(), // bucketName
        It.IsAny<string>(), // objectName
        It.IsAny<string>(), // contentType
        It.IsAny<Stream>(), // source
        It.IsAny<UploadObjectOptions>(), // options
        It.IsAny<CancellationToken>(),
        It.IsAny<IProgress<Google.Apis.Upload.IUploadProgress>>()) // progress
      ).ReturnsAsync(new Google.Apis.Storage.v1.Data.Object { Name = fileName });

    // Act
    var result = await _gcpBucketService.Create(fileStream, fileName);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(fileName, result.FileName);
  }

  // Teste para Atualizar um arquivo no bucket com um caminho de arquivo local na Cloud da GCP
  [Fact]
  public async Task Update_WithGcpProviderAndFilePath_ShouldReturnBucketResponseDto()
  {
    // Arrange
    var oldFile = "oldFile.txt";
    var filePath = "testFile.txt";
    var fileName = "testFile.txt";
    await File.WriteAllTextAsync(filePath, "test content");

    // Mock Storage response
    _gcpMock
      .Setup(storage => storage.UploadObjectAsync(
        It.IsAny<string>(), // bucketName
        It.IsAny<string>(), // objectName
        It.IsAny<string>(), // contentType
        It.IsAny<Stream>(), // source
        It.IsAny<UploadObjectOptions>(), // options
        It.IsAny<CancellationToken>(),
        It.IsAny<IProgress<Google.Apis.Upload.IUploadProgress>>()) // progress
      ).ReturnsAsync(new Google.Apis.Storage.v1.Data.Object { Name = fileName });

    // Act
    var result = await _gcpBucketService.Update(oldFile, filePath, fileName);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(fileName, result.FileName);
  }

  // Teste para Atualizar um arquivo no bucket com um IFormFile na Cloud da GCP
  [Fact]
  public async Task Update_WithGcpProviderAndIFormFile_ShouldReturnBucketResponseDto()
  {
    // Arrange
    var oldFile = "oldForm.txt";
    var fileMock = new Mock<IFormFile>();
    var fileName = "testForm.txt";
    var content = "test content";
    var fileStream = new MemoryStream();
    var writer = new StreamWriter(fileStream);
    writer.Write(content);
    writer.Flush();
    fileStream.Position = 0;
    fileMock.Setup(f => f.OpenReadStream()).Returns(fileStream);
    fileMock.Setup(f => f.FileName).Returns(fileName);
    fileMock.Setup(f => f.Length).Returns(fileStream.Length);

    // Mock Storage response
    _gcpMock
      .Setup(storage => storage.UploadObjectAsync(
        It.IsAny<string>(), // bucketName
        It.IsAny<string>(), // objectName
        It.IsAny<string>(), // contentType
        It.IsAny<Stream>(), // source
        It.IsAny<UploadObjectOptions>(), // options
        It.IsAny<CancellationToken>(),
        It.IsAny<IProgress<Google.Apis.Upload.IUploadProgress>>()) // progress
      ).ReturnsAsync(new Google.Apis.Storage.v1.Data.Object { Name = fileName });

    // Act
    var result = await _gcpBucketService.Update(oldFile, fileMock.Object, fileName);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(fileName, result.FileName);
  }

  // Teste para Atualizar um arquivo no bucket com um MemoryStream na Cloud da GCP
  [Fact]
  public async Task Update_WithGcpProviderAndMemoryStream_ShouldReturnBucketResponseDto()
  {
    // Arrange
    var oldFile = "oldStream.txt";
    var fileStream = new MemoryStream();
    var writer = new StreamWriter(fileStream);
    writer.Write("test content");
    writer.Flush();
    fileStream.Position = 0;
    var fileName = "testStream.txt";

    // Mock Storage response
    _gcpMock
      .Setup(storage => storage.UploadObjectAsync(
        It.IsAny<string>(), // bucketName
        It.IsAny<string>(), // objectName
        It.IsAny<string>(), // contentType
        It.IsAny<Stream>(), // source
        It.IsAny<UploadObjectOptions>(), // options
        It.IsAny<CancellationToken>(),
        It.IsAny<IProgress<Google.Apis.Upload.IUploadProgress>>()) // progress
      ).ReturnsAsync(new Google.Apis.Storage.v1.Data.Object { Name = fileName });

    // Act
    var result = await _gcpBucketService.Update(oldFile, fileStream, fileName);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(fileName, result.FileName);
  }

  // Teste para Deletar um arquivo no bucket na Cloud da GCP
  [Fact]
  public async Task Delete_WithGcpProvider_ShouldNotThrowException()
  {
    // Arrange
    var fileName = "testDelete.txt";

    // Mock Storage response
    _gcpMock
      .Setup(storage => storage.DeleteObjectAsync(
        It.IsAny<string>(), // bucketName
        It.IsAny<string>(), // objectName
        It.IsAny<DeleteObjectOptions>(), // options
        It.IsAny<CancellationToken>()) // progress
      );

    // Act & Assert
    await _gcpBucketService.Delete(fileName);
  }

  // Teste para Baixar um arquivo do bucket na Cloud da GCP
  [Fact]
  public async Task Download_WithGcpProvider_ShouldReturnStream()
  {
    // Arrange
    var fileName = "testDownload.txt";
    var responseStream = new MemoryStream();
    var writer = new StreamWriter(responseStream);
    writer.Write("test content");
    writer.Flush();
    responseStream.Position = 0;

    // Mock Storage response
    _gcpMock
      .Setup(storage => storage.DownloadObjectAsync(
        It.IsAny<string>(), // bucketName
        It.IsAny<string>(), // objectName
        It.IsAny<Stream>(), // source
        It.IsAny<DownloadObjectOptions>(), // options
        It.IsAny<CancellationToken>(),
        It.IsAny<IProgress<Google.Apis.Download.IDownloadProgress>>()) // progress
      ).ReturnsAsync(new Google.Apis.Storage.v1.Data.Object { Name = fileName });

    // Act
    var result = await _gcpBucketService.Download(fileName);

    // Assert
    Assert.NotNull(result);
    Assert.IsType<MemoryStream>(result);
  }

  // Teste para Criar um arquivo no bucket com um caminho de arquivo local vazio
  [Fact]
  public async Task Create_WithEmptyFilePath_ShouldThrowBadRequestException()
  {
    // Arrange
    var filePath = "";
    var fileName = "test.txt";
    var optionsMock = new Mock<IOptions<BucketOptions>>();
    var bucketOptions = new BucketOptions()
    {
      BucketName = "test-bucket",
      CloudProvider = CloudProvider.AWS,
      AWS = new BasicAWSCredentials("access-key", "secret-key")
    };
    optionsMock.Setup(o => o.Value).Returns(bucketOptions);
    var bucketService = new BucketService(_loggerMock.Object, optionsMock.Object);

    // Act & Assert
    await Assert.ThrowsAsync<AppException>(() => bucketService.Create(filePath, fileName));
  }

  // Teste para Criar um arquivo no bucket com tamanho de arquivo excedendo o limite
  [Fact]
  public async Task Create_WithFileSizeExceedingLimit_ShouldThrowBadRequestException()
  {
    // Arrange
    var filePath = "largeFile.txt";
    var fileName = "largeFile.txt";
    var fileContent = new string('a', 10485761); // 10 MB + 1 byte
    await File.WriteAllTextAsync(filePath, fileContent);
    var optionsMock = new Mock<IOptions<BucketOptions>>();
    var bucketOptions = new BucketOptions()
    {
      BucketName = "test-bucket",
      CloudProvider = CloudProvider.AWS,
      AWS = new BasicAWSCredentials("access-key", "secret-key")
    };
    optionsMock.Setup(o => o.Value).Returns(bucketOptions);
    var bucketService = new BucketService(_loggerMock.Object, optionsMock.Object);

    // Act & Assert
    await Assert.ThrowsAsync<AppException>(() => bucketService.Create(filePath, fileName));

    // Cleanup
    File.Delete(filePath);
  }

  // Teste para Delete um arquivo no bucket com nome de arquivo inválido
  [Fact]
  public async Task Delete_WithInvalidFileName_ShouldThrowServiceUnavailableException()
  {
    // Arrange
    var fileName = "";
    var optionsMock = new Mock<IOptions<BucketOptions>>();
    var bucketOptions = new BucketOptions()
    {
      BucketName = "test-bucket",
      CloudProvider = CloudProvider.AWS,
      AWS = new BasicAWSCredentials("access-key", "secret-key")
    };
    optionsMock.Setup(o => o.Value).Returns(bucketOptions);
    var bucketService = new BucketService(_loggerMock.Object, optionsMock.Object);

    // Act & Assert
    await Assert.ThrowsAsync<AppException>(() => bucketService.Delete(fileName));
  }

  // Teste para Download de um arquivo no bucket com nome de arquivo inválido
  [Fact]
  public async Task Download_WithInvalidFileName_ShouldThrowServiceUnavailableException()
  {
    // Arrange
    var fileName = "";
    var optionsMock = new Mock<IOptions<BucketOptions>>();
    var bucketOptions = new BucketOptions()
    {
      BucketName = "test-bucket",
      CloudProvider = CloudProvider.AWS,
      AWS = new BasicAWSCredentials("access-key", "secret-key")
    };
    optionsMock.Setup(o => o.Value).Returns(bucketOptions);
    var bucketService = new BucketService(_loggerMock.Object, optionsMock.Object);

    // Act & Assert
    await Assert.ThrowsAsync<AppException>(() => bucketService.Download(fileName));
  }

  // Teste para Atualizar um arquivo no bucket da AWS com AmazonS3Exception
  [Fact]
  public async Task UploadFileAsync_WithAmazonS3Exception_ShouldThrowInternalServerError()
  {
    // Arrange
    var fileName = "test.txt";
    var fileStream = new MemoryStream();
    _awsMock
      .Setup(client => client.PutObjectAsync(It.IsAny<PutObjectRequest>(), default))
      .ThrowsAsync(new AmazonS3Exception("Error"));

    // Act & Assert
    await Assert.ThrowsAsync<AppException>(() => _awsBucketService.Create(fileStream, fileName));
  }

  // Teste para Atualizar um arquivo no bucket da GCP com GoogleApiException
  [Fact]
  public async Task UploadFileAsync_WithGoogleApiException_ShouldThrowInternalServerError()
  {
    // Arrange
    var fileName = "test.txt";
    var fileStream = new MemoryStream();
    _gcpMock
      .Setup(storage => storage.UploadObjectAsync(
        It.IsAny<string>(), // bucketName
        It.IsAny<string>(), // objectName
        It.IsAny<string>(), // contentType
        It.IsAny<Stream>(), // source
        It.IsAny<UploadObjectOptions>(), // options
        It.IsAny<CancellationToken>(),
        It.IsAny<IProgress<Google.Apis.Upload.IUploadProgress>>()) // progress
      ).ThrowsAsync(new GoogleApiException("Error"));

    // Act & Assert
    await Assert.ThrowsAsync<AppException>(() => _gcpBucketService.Create(fileStream, fileName));
  }

  // Teste para Deletar um arquivo no bucket da AWS com AmazonS3Exception
  [Fact]
  public async Task DeleteFileAsync_WithAmazonS3Exception_ShouldThrowInternalServerError()
  {
    // Arrange
    var fileName = "test.txt";
    _awsMock
      .Setup(client => client.DeleteObjectAsync(It.IsAny<DeleteObjectRequest>(), default))
      .ThrowsAsync(new AmazonS3Exception("Error"));

    // Act & Assert
    await Assert.ThrowsAsync<AppException>(() => _awsBucketService.Delete(fileName));
  }

  // Teste para Deletar um arquivo no bucket da GCP com GoogleApiException
  [Fact]
  public async Task DeleteFileAsync_WithGoogleApiException_ShouldThrowInternalServerError()
  {
    // Arrange
    var fileName = "test.txt";
    _gcpMock
      .Setup(storage => storage.DeleteObjectAsync(
        It.IsAny<string>(), // bucketName
        It.IsAny<string>(), // objectName
        It.IsAny<DeleteObjectOptions>(), // options
        It.IsAny<CancellationToken>()) // progress
      ).ThrowsAsync(new GoogleApiException("Error"));

    // Act & Assert
    await Assert.ThrowsAsync<AppException>(() => _gcpBucketService.Delete(fileName));
  }

  // Teste para Download de um arquivo no bucket da AWS com AmazonS3Exception
  [Fact]
  public async Task DownloadFileAsync_WithAmazonS3Exception_ShouldThrowInternalServerError()
  {
    // Arrange
    var fileName = "test.txt";
    _awsMock
      .Setup(client => client.GetObjectAsync(It.IsAny<GetObjectRequest>(), default))
      .ThrowsAsync(new AmazonS3Exception("Error"));

    // Act & Assert
    await Assert.ThrowsAsync<AppException>(() => _awsBucketService.Download(fileName));
  }

  // Teste para Download de um arquivo no bucket da GCP com GoogleApiException
  [Fact]
  public async Task DownloadFileAsync_WithGoogleApiException_ShouldThrowInternalServerError()
  {
    // Arrange
    var fileName = "test.txt";
    _gcpMock
      .Setup(storage => storage.DownloadObjectAsync(
        It.IsAny<string>(), // bucketName
        It.IsAny<string>(), // objectName
        It.IsAny<Stream>(), // source
        It.IsAny<DownloadObjectOptions>(), // options
        It.IsAny<CancellationToken>(),
        It.IsAny<IProgress<Google.Apis.Download.IDownloadProgress>>()) // progress
      ).ThrowsAsync(new GoogleApiException("Error"));

    // Act & Assert
    await Assert.ThrowsAsync<AppException>(() => _gcpBucketService.Download(fileName));
  }

  // Teste para Criar um arquivo no bucket com um caminho de arquivo local
  [Fact]
  public async Task Create_FilePathEmpty_ThrowsBadRequest()
  {
    // Arrange
    var optionsMock = new Mock<IOptions<BucketOptions>>();
    var bucketOptions = new BucketOptions()
    {
      BucketName = "test-bucket",
      CloudProvider = CloudProvider.AWS,
      AWS = new BasicAWSCredentials("access-key", "secret-key")
    };
    optionsMock.Setup(o => o.Value).Returns(bucketOptions);
    var bucketService = new BucketService(_loggerMock.Object, optionsMock.Object);

    // Act & Assert
    await Assert.ThrowsAsync<AppException>(() => bucketService.Create("", "test.txt"));
  }

  // Teste para Atualizar um arquivo no bucket com um caminho de arquivo local
  [Fact]
  public async Task Update_FilePathEmpty_ThrowsBadRequest()
  {
    // Arrange
    var optionsMock = new Mock<IOptions<BucketOptions>>();
    var bucketOptions = new BucketOptions()
    {
      BucketName = "test-bucket",
      CloudProvider = CloudProvider.AWS,
      AWS = new BasicAWSCredentials("access-key", "secret-key")
    };
    optionsMock.Setup(o => o.Value).Returns(bucketOptions);
    var bucketService = new BucketService(_loggerMock.Object, optionsMock.Object);

    // Act & Assert
    await Assert.ThrowsAsync<AppException>(() => bucketService.Update("oldFile.txt", "", "test.txt"));
  }

  // Teste para Atualizar um arquivo no bucket com um caminho de arquivo local com tamanho de arquivo excedendo o limite
  [Fact]
  public async Task UploadFileAsync_FileSizeBigger_ThrowsBadRequest()
  {
    // Arrange
    var largeStream = new MemoryStream(new byte[4096]);
    var optionsMock = new Mock<IOptions<BucketOptions>>();
    var bucketOptions = new BucketOptions()
    {
      BucketName = "test-bucket",
      CloudProvider = CloudProvider.AWS,
      AWS = new BasicAWSCredentials("access-key", "secret-key")
    };
    optionsMock.Setup(o => o.Value).Returns(bucketOptions);
    var bucketService = new BucketService(_loggerMock.Object, optionsMock.Object);

    // Act & Assert
    await Assert.ThrowsAsync<AppException>(() => bucketService.Create(largeStream, "test.txt"));
  }

  // Teste para Deletar um arquivo no bucket com nome de arquivo inválido
  [Fact]
  public async Task DeleteFileAsync_BucketNotFound_ThrowsInternalServerError()
  {
    // Arrange
    var optionsMock = new Mock<IOptions<BucketOptions>>();
    var bucketOptions = new BucketOptions()
    {
      BucketName = "test-bucket",
      CloudProvider = CloudProvider.AWS,
      AWS = new BasicAWSCredentials("access-key", "secret-key")
    };
    optionsMock.Setup(o => o.Value).Returns(bucketOptions);
    var bucketService = new BucketService(_loggerMock.Object, optionsMock.Object);

    // Act & Assert
    await Assert.ThrowsAsync<AppException>(() => bucketService.Delete("test.txt"));
  }

  // Teste para Download de um arquivo no bucket com nome de arquivo inválido
  [Fact]
  public async Task DownloadFileAsync_BucketNotFound_ThrowsInternalServerError()
  {
    // Arrange
    var optionsMock = new Mock<IOptions<BucketOptions>>();
    var bucketOptions = new BucketOptions()
    {
      BucketName = "test-bucket",
      CloudProvider = CloudProvider.AWS,
      AWS = new BasicAWSCredentials("access-key", "secret-key")
    };
    optionsMock.Setup(o => o.Value).Returns(bucketOptions);
    var bucketService = new BucketService(_loggerMock.Object, optionsMock.Object);

    // Act & Assert
    await Assert.ThrowsAsync<AppException>(() => bucketService.Download("test.txt"));
  }

  // Teste para para Criar uma instância do serviço com configurações nulas
  [Fact]
  public void Service_WithBucketOptionsNull_ShouldThrowBadRequestException()
  {
    // Arrange
    var optionsMock = new Mock<IOptions<BucketOptions>>();
    optionsMock.Setup(o => o.Value).Returns((BucketOptions)null!);

    // Act & Assert
    var exception = Assert.Throws<AppException>(() => new BucketService(_loggerMock.Object, optionsMock.Object));
    Assert.Equal("Bucket.BucketOptionsEmpty", exception.Message);
  }

  // Teste para para Criar uma instância do serviço com nome do bucket vazio
  [Fact]
  public void Service_WithBucketNameEmpty_ShouldThrowBadRequestException()
  {
    // Arrange
    var optionsMock = new Mock<IOptions<BucketOptions>>();
    var bucketOptions = new BucketOptions();
    optionsMock.Setup(o => o.Value).Returns(bucketOptions);

    // Act & Assert
    var exception = Assert.Throws<AppException>(() => new BucketService(_loggerMock.Object, optionsMock.Object));
    Assert.Equal("Bucket.BucketOptionsEmpty", exception.Message);
  }

  // Teste para para Criar uma instância do serviço com provedor de nuvem não suportado
  [Fact]
  public void Service_WithCloudProviderNotSupported_ShouldThrowBadRequestException()
  {
    // Arrange
    var optionsMock = new Mock<IOptions<BucketOptions>>();
    var bucketOptions = new BucketOptions()
    {
      BucketName = "test-bucket"
    };
    optionsMock.Setup(o => o.Value).Returns(bucketOptions);

    // Act & Assert
    var exception = Assert.Throws<AppException>(() => new BucketService(_loggerMock.Object, optionsMock.Object));
    Assert.Equal("Bucket.CloudProviderEmpty", exception.Message);
  }

  // Teste para para Criar uma instância do serviço com provedor de nuvem AWS sem credenciais
  [Fact]
  public void Service_WithCloudProviderAwsWithoutCredentials_ShouldThrowInternalServerErrorException()
  {
    // Arrange
    var optionsMock = new Mock<IOptions<BucketOptions>>();
    var bucketOptions = new BucketOptions()
    {
      BucketName = "test-bucket",
      CloudProvider = CloudProvider.AWS
    };
    optionsMock.Setup(o => o.Value).Returns(bucketOptions);

    // Act & Assert
    var exception = Assert.Throws<AppException>(() => new BucketService(_loggerMock.Object, optionsMock.Object));
    Assert.Equal("Bucket.CredentialsEmpty;AWS", exception.Message);
  }

  // Teste para para Criar uma instância do serviço com provedor de nuvem GCP sem credenciais
  [Fact]
  public void Service_WithCloudProviderGcpWithoutCredentials_ShouldThrowInternalServerErrorException()
  {
    // Arrange
    var optionsMock = new Mock<IOptions<BucketOptions>>();
    var bucketOptions = new BucketOptions()
    {
      BucketName = "test-bucket",
      CloudProvider = CloudProvider.GCP
    };
    optionsMock.Setup(o => o.Value).Returns(bucketOptions);

    // Act & Assert
    var exception = Assert.Throws<AppException>(() => new BucketService(_loggerMock.Object, optionsMock.Object));
    Assert.Equal("Bucket.CredentialsEmpty;GCP", exception.Message);
  }
}

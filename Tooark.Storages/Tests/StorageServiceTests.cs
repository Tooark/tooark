using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Google;
using Google.Apis.Download;
using Google.Apis.Requests;
using Google.Apis.Upload;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Tooark.Enums;
using Tooark.Exceptions;
using Tooark.Storages;
using Tooark.Storages.Interfaces;
using Tooark.Storages.Options;
using Tooark.Tests.Moq.Dto;
using Tooark.Tests.Moq.Options;

namespace Tooark.Tests.Storages;

public class StorageServiceTests
{
  private readonly Mock<ILogger<StorageService>> _loggerMock = new();
  private readonly Mock<IOptions<StorageOptions>> _optionsMock = new();
  private readonly Mock<IOptions<StorageOptions>> _optionsAwsMock = new();
  private readonly Mock<IOptions<StorageOptions>> _optionsGcpMock = new();
  private readonly StorageOptions _storageOptions;
  private readonly StorageOptions _storageAwsOptions;
  private readonly StorageOptions _storageGcpOptions;


  public StorageServiceTests()
  {
    _storageOptions = new StorageOptions
    {
      BucketName = "test-bucket",
      CloudProvider = ECloudProvider.Amazon,
      AWS = new MAwsOptions(),
      GCP = new MGcpOptions(),
      FileSize = 1024, // 1 MB
      SignerMinutes = 5
    };
    _storageAwsOptions = new StorageOptions
    {
      BucketName = "test-bucket",
      CloudProvider = ECloudProvider.Amazon,
      AWS = new MAwsOptions(),
      GCP = null,
      FileSize = 1024, // 1 MB
      SignerMinutes = 5
    };
    _storageGcpOptions = new StorageOptions
    {
      BucketName = "test-bucket",
      CloudProvider = ECloudProvider.Google,
      AWS = null,
      GCP = new MGcpOptions(),
      FileSize = 1024, // 1 MB
      SignerMinutes = 5
    };

    _optionsMock.Setup(o => o.Value).Returns(_storageOptions);
    _optionsAwsMock.Setup(o => o.Value).Returns(_storageAwsOptions);
    _optionsGcpMock.Setup(o => o.Value).Returns(_storageGcpOptions);
  }


  // Teste para verificar se o método Upload está fazendo o upload de um arquivo para a AWS.
  [Fact]
  public async Task Upload_ShouldUploadFileToAws_WhenCloudProviderIsAmazon()
  {
    // Arrange
    Mock<IAmazonS3> awsClientMock = new();
    awsClientMock
      .Setup(client => client.PutObjectAsync(
        It.IsAny<PutObjectRequest>(),
        It.IsAny<CancellationToken>()))
      .ReturnsAsync(new PutObjectResponse());
    var storageAwsService = new StorageService(_loggerMock.Object, _optionsAwsMock.Object, awsClient: awsClientMock.Object);

    var fileStream = new MemoryStream(new byte[10]);
    var fileName = "test-file";

    // Act
    var result = await storageAwsService.Upload(fileStream, fileName);

    // Assert
    Assert.NotNull(result);
  }

  // Teste para verificar se o método Upload está fazendo o upload de um arquivo para a GCP.
  [Fact]
  public async Task Upload_ShouldUploadFileToGcp_WhenCloudProviderIsGoogle()
  {
    // Arrange
    Mock<StorageClient> gcpClientMock = new();
    gcpClientMock
      .Setup(client => client.UploadObjectAsync(
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<Stream>(),
        It.IsAny<UploadObjectOptions>(),
        It.IsAny<CancellationToken>(),
        It.IsAny<IProgress<IUploadProgress>>()))
      .ReturnsAsync(new UploadResponseGcpDto());
    var storageGcpService = new StorageService(_loggerMock.Object, _optionsGcpMock.Object, gcpClient: gcpClientMock.Object);

    var fileStream = new MemoryStream(new byte[10]);
    var fileName = "test-file";

    // Act
    var result = await storageGcpService.Upload(fileStream, fileName);

    // Assert
    Assert.NotNull(result);
  }

  // Teste para verificar se o construtor está lançando uma exceção InternalServerErrorException quando as opções do bucket estão vazias.
  [Fact]
  public void Constructor_ShouldThrowException_WhenOptionsNull()
  {
    // Arrange
    StorageOptions storageOptions = null!;
    Mock<IOptions<StorageOptions>> optionsMock = new();
    optionsMock.Setup(o => o.Value).Returns(storageOptions);

    // Act
    var exception = Assert.Throws<InternalServerErrorException>(() => new StorageService(_loggerMock.Object, optionsMock.Object));

    // Assert
    Assert.Equal("Storage.StorageOptionsEmpty", exception.Message);
  }

  // Teste para verificar se o construtor está lançando uma exceção InternalServerErrorException quando o provedor de nuvem está vazio.
  [Fact]
  public void Constructor_ShouldThrowException_WhenCloudProviderNone()
  {
    // Arrange
    StorageOptions storageOptions = new()
    {
      BucketName = "test-bucket",
      CloudProvider = ECloudProvider.None
    };
    Mock<IOptions<StorageOptions>> optionsMock = new();
    optionsMock.Setup(o => o.Value).Returns(storageOptions);

    // Act
    var exception = Assert.Throws<InternalServerErrorException>(() => new StorageService(_loggerMock.Object, optionsMock.Object));

    // Assert
    Assert.Equal("Storage.CloudProviderEmpty", exception.Message);
  }

  // Teste para verificar se o construtor está lançando uma exceção InternalServerErrorException quando o provedor de nuvem é Amazon e as credenciais estão vazias.
  [Fact]
  public void Constructor_ShouldThrowException_WhenCloudProviderAmazon()
  {
    // Arrange
    StorageOptions storageOptions = new()
    {
      BucketName = "test-bucket",
      CloudProvider = ECloudProvider.Amazon
    };
    Mock<IOptions<StorageOptions>> optionsMock = new();
    optionsMock.Setup(o => o.Value).Returns(storageOptions);

    // Act
    var exception = Assert.Throws<InternalServerErrorException>(() => new StorageService(_loggerMock.Object, optionsMock.Object));

    // Assert
    Assert.Equal("Storage.CredentialsEmpty;AWS", exception.Message);
  }

  // Teste para verificar se o construtor está lançando uma exceção InternalServerErrorException quando o provedor de nuvem é Google e as credenciais estão vazias.
  [Fact]
  public void Constructor_ShouldThrowException_WhenCloudProviderGoogle()
  {
    // Arrange
    StorageOptions storageOptions = new()
    {
      BucketName = "test-bucket",
      CloudProvider = ECloudProvider.Google
    };
    Mock<IOptions<StorageOptions>> optionsMock = new();
    optionsMock.Setup(o => o.Value).Returns(storageOptions);

    // Act
    var exception = Assert.Throws<InternalServerErrorException>(() => new StorageService(_loggerMock.Object, optionsMock.Object));

    // Assert
    Assert.Equal("Storage.CredentialsEmpty;GCP", exception.Message);
  }

  // Teste para verificar se o método Upload está lançando uma exceção BadRequestException quando o provedor de nuvem não está implementado.
  [Fact]
  public async Task Upload_ShouldThrowException_WhenCloudProviderNotImplemented()
  {
    // Arrange
    var storageOptions = new StorageOptions
    {
      BucketName = "test-bucket",
      CloudProvider = ECloudProvider.Microsoft,
      AWS = new MAwsOptions(),
      GCP = new MGcpOptions(),
      FileSize = 1024 // 1 MB
    };
    Mock<IOptions<StorageOptions>> optionsMock = new();
    optionsMock.Setup(o => o.Value).Returns(storageOptions);
    var storageService = new StorageService(_loggerMock.Object, optionsMock.Object);

    var fileStream = new MemoryStream(new byte[10]);
    var fileName = "test-file";

    // Act
    var exception = await Assert.ThrowsAsync<BadRequestException>(() => storageService.Upload(fileStream, fileName));

    // Assert
    Assert.Equal("Storage.CloudProviderNotImplemented", exception.Message);
  }

  // Teste para verificar se o método Upload está lançando uma exceção BadRequestException quando o arquivo está vazio.
  [Fact]
  public async Task Upload_ShouldThrowException_WhenFileStreamEmpty()
  {
    // Arrange
    var storageService = new StorageService(_loggerMock.Object, _optionsMock.Object);
    MemoryStream fileStream = null!;
    var fileName = "test-file";

    // Act
    var exception = await Assert.ThrowsAsync<BadRequestException>(() => storageService.Upload(fileStream, fileName));

    // Assert
    Assert.Equal("Storage.FileUploadEmpty", exception.Message);
  }

  // Teste para verificar se o método Upload está lançando uma exceção BadRequestException quando o nome do arquivo está vazio.
  [Fact]
  public async Task Upload_ShouldThrowException_WhenFileNameEmpty()
  {
    // Arrange
    var storageService = new StorageService(_loggerMock.Object, _optionsMock.Object);
    var fileStream = new MemoryStream();
    string fileName = null!;

    // Act
    var exception = await Assert.ThrowsAsync<BadRequestException>(() => storageService.Upload(fileStream, fileName));

    // Assert
    Assert.Equal("Storage.FileNameEmpty", exception.Message);
  }

  // Teste para verificar se o método Upload está lançando uma exceção BadRequestException quando o nome do bucket está vazio.
  [Fact]
  public async Task Upload_ShouldThrowException_WhenBucketNameEmpty()
  {
    // Arrange
    var storageService = new StorageService(_loggerMock.Object, _optionsMock.Object);
    var fileStream = new MemoryStream(new byte[10]);
    var fileName = "test-file";
    string bucketName = string.Empty;

    // Act
    var exception = await Assert.ThrowsAsync<BadRequestException>(() => storageService.Upload(fileStream, fileName, bucketName));

    // Assert
    Assert.Equal("Storage.BucketNameEmpty", exception.Message);
  }

  // Teste para verificar se o método Upload está lançando uma exceção BadRequestException quando o tamanho do arquivo está vazio.
  [Fact]
  public async Task Upload_ShouldThrowException_WhenFileSizeEmpty()
  {
    // Arrange
    var storageOptions = new StorageOptions
    {
      BucketName = "test-bucket",
      CloudProvider = ECloudProvider.Amazon,
      AWS = new MAwsOptions(),
      FileSize = 1024 // 1 MB
    };
    Mock<IOptions<StorageOptions>> optionsMock = new();
    optionsMock.Setup(o => o.Value).Returns(storageOptions);
    var storageService = new StorageService(_loggerMock.Object, optionsMock.Object);
    var fileStream = new MemoryStream();
    var fileName = "test-file";
    string bucketName = null!;

    // Act
    var exception = await Assert.ThrowsAsync<BadRequestException>(() => storageService.Upload(fileStream, fileName, bucketName));

    // Assert
    Assert.Equal("Storage.FileSizeEmpty", exception.Message);
  }

  // Teste para verificar se o método Upload está lançando uma exceção BadRequestException quando o tamanho do arquivo é maior que o permitido.
  [Fact]
  public async Task Upload_ShouldThrowException_WhenFileSizeBigger()
  {
    // Arrange
    var storageOptions = new StorageOptions
    {
      BucketName = "test-bucket",
      CloudProvider = ECloudProvider.Amazon,
      AWS = new MAwsOptions(),
      FileSize = 1
    };
    Mock<IOptions<StorageOptions>> optionsMock = new();
    optionsMock.Setup(o => o.Value).Returns(storageOptions);
    var storageService = new StorageService(_loggerMock.Object, optionsMock.Object);
    var fileStream = new MemoryStream(new byte[10]);
    var fileName = "test-file";
    string bucketName = null!;

    // Act
    var exception = await Assert.ThrowsAsync<BadRequestException>(() => storageService.Upload(fileStream, fileName, bucketName));

    // Assert
    Assert.Equal($"Storage.FileSizeBigger;{storageOptions.FileSize}", exception.Message);
  }

  // Teste para verificar se o método Upload está lançando uma exceção BadRequestException quando não encontra o bucket na AWS.
  [Fact]
  public async Task Upload_ShouldThrowException_WhenCloudProviderIsAmazon_WithExceptionNotFound()
  {
    // Arrange
    var awsClientMock = new Mock<IAmazonS3>();
    awsClientMock
      .Setup(client => client.PutObjectAsync(
        It.IsAny<PutObjectRequest>(),
        It.IsAny<CancellationToken>()))
      .ThrowsAsync(new AmazonS3Exception("Bucket Not Found") { StatusCode = HttpStatusCode.NotFound });

    var storageAwsService = new StorageService(_loggerMock.Object, _optionsAwsMock.Object, awsClient: awsClientMock.Object);
    var fileStream = new MemoryStream(new byte[10]);
    var fileName = "test-file";

    // Act
    var exception = await Assert.ThrowsAsync<BadRequestException>(() => storageAwsService.Upload(fileStream, fileName));

    // Assert
    Assert.Equal("Storage.BucketNotFound", exception.Message);
  }

  // Teste para verificar se o método Upload está lançando uma exceção BadRequestException quando ocorre um erro interno na AWS.
  [Fact]
  public async Task Upload_ShouldThrowException_WhenCloudProviderIsAmazon_WithExceptionAmazon()
  {
    // Arrange
    var awsClientMock = new Mock<IAmazonS3>();
    awsClientMock
      .Setup(client => client.PutObjectAsync(
        It.IsAny<PutObjectRequest>(),
        It.IsAny<CancellationToken>()))
      .ThrowsAsync(new AmazonS3Exception("Internal Server Error") { StatusCode = HttpStatusCode.InternalServerError });

    var storageAwsService = new StorageService(_loggerMock.Object, _optionsAwsMock.Object, awsClient: awsClientMock.Object);
    var fileStream = new MemoryStream(new byte[10]);
    var fileName = "test-file";

    // Act
    var exception = await Assert.ThrowsAsync<InternalServerErrorException>(() => storageAwsService.Upload(fileStream, fileName));

    // Assert
    Assert.Equal("Storage.FileErrorUploading", exception.Message);
  }

  // Teste para verificar se o método Upload está lançando uma exceção BadRequestException quando ocorre um erro interno na aplicação.
  [Fact]
  public async Task Upload_ShouldThrowException_WhenCloudProviderIsAmazon_WithException()
  {
    // Arrange
    var awsClientMock = new Mock<IAmazonS3>();
    awsClientMock
      .Setup(client => client.PutObjectAsync(
        It.IsAny<PutObjectRequest>(),
        It.IsAny<CancellationToken>()))
      .ThrowsAsync(new Exception("Internal Server Error"));

    var storageAwsService = new StorageService(_loggerMock.Object, _optionsAwsMock.Object, awsClient: awsClientMock.Object);
    var fileStream = new MemoryStream(new byte[10]);
    var fileName = "test-file";

    // Act
    var exception = await Assert.ThrowsAsync<InternalServerErrorException>(() => storageAwsService.Upload(fileStream, fileName));

    // Assert
    Assert.Equal("Storage.UnknownError", exception.Message);
  }

  // Teste para verificar se o método Upload está lançando uma exceção BadRequestException quando não encontra o bucket no GCP.
  [Fact]
  public async Task Upload_ShouldThrowException_WhenCloudProviderIsGoogle_WithExceptionNotFound()
  {
    // Arrange    
    var gcpClientMock = new Mock<StorageClient>();
    gcpClientMock
      .Setup(client => client.UploadObjectAsync(
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<Stream>(),
        It.IsAny<UploadObjectOptions>(),
        It.IsAny<CancellationToken>(),
        It.IsAny<IProgress<IUploadProgress>>()))
      .ThrowsAsync(new GoogleApiException("Bucket Not Found") { Error = new RequestError() { Code = 404 } });

    var storageGcpService = new StorageService(_loggerMock.Object, _optionsGcpMock.Object, gcpClient: gcpClientMock.Object);
    var fileStream = new MemoryStream(new byte[10]);
    var fileName = "test-file";

    // Act
    var exception = await Assert.ThrowsAsync<BadRequestException>(() => storageGcpService.Upload(fileStream, fileName));

    // Assert
    Assert.Equal("Storage.BucketNotFound", exception.Message);
  }

  // Teste para verificar se o método Upload está lançando uma exceção BadRequestException quando ocorre um erro interno na GCP.
  [Fact]
  public async Task Upload_ShouldThrowException_WhenCloudProviderIsGoogle_WithExceptionGoogle()
  {
    // Arrange    
    var gcpClientMock = new Mock<StorageClient>();
    gcpClientMock
      .Setup(client => client.UploadObjectAsync(
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<Stream>(),
        It.IsAny<UploadObjectOptions>(),
        It.IsAny<CancellationToken>(),
        It.IsAny<IProgress<IUploadProgress>>()))
      .ThrowsAsync(new GoogleApiException("Internal Server Error") { Error = new RequestError() { Code = 500 } });

    var storageGcpService = new StorageService(_loggerMock.Object, _optionsGcpMock.Object, gcpClient: gcpClientMock.Object);
    var fileStream = new MemoryStream(new byte[10]);
    var fileName = "test-file";

    // Act
    var exception = await Assert.ThrowsAsync<InternalServerErrorException>(() => storageGcpService.Upload(fileStream, fileName));

    // Assert
    Assert.Equal("Storage.FileErrorUploading", exception.Message);
  }

  // Teste para verificar se o método Upload está lançando uma exceção BadRequestException quando ocorre um erro interno na aplicação.
  [Fact]
  public async Task Upload_ShouldThrowException_WhenCloudProviderIsGoogle_WithException()
  {
    // Arrange    
    var gcpClientMock = new Mock<StorageClient>();
    gcpClientMock
      .Setup(client => client.UploadObjectAsync(
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<Stream>(),
        It.IsAny<UploadObjectOptions>(),
        It.IsAny<CancellationToken>(),
        It.IsAny<IProgress<IUploadProgress>>()))
      .ThrowsAsync(new Exception("Internal Server Error"));

    var storageGcpService = new StorageService(_loggerMock.Object, _optionsGcpMock.Object, gcpClient: gcpClientMock.Object);
    var fileStream = new MemoryStream(new byte[10]);
    var fileName = "test-file";

    // Act
    var exception = await Assert.ThrowsAsync<InternalServerErrorException>(() => storageGcpService.Upload(fileStream, fileName));

    // Assert
    Assert.Equal("Storage.UnknownError", exception.Message);
  }

  // Teste para verificar se o método Delete está fazendo o delete de um arquivo da AWS.
  [Fact]
  public async Task Delete_ShouldDeleteFileToAws_WhenCloudProviderIsAmazon()
  {
    // Arrange
    Mock<IAmazonS3> awsClientMock = new();
    awsClientMock
      .Setup(client => client.DeleteObjectAsync(
        It.IsAny<DeleteObjectRequest>(),
        It.IsAny<CancellationToken>()))
      .ReturnsAsync(new DeleteObjectResponse());
    var storageAwsService = new StorageService(_loggerMock.Object, _optionsAwsMock.Object, awsClient: awsClientMock.Object);

    var fileName = "test-file";

    // Act
    var result = await storageAwsService.Delete(fileName);

    // Assert
    Assert.NotNull(result);
  }

  // Teste para verificar se o método Delete está fazendo o delete de um arquivo da GCP.
  [Fact]
  public async Task Delete_ShouldDeleteFileToGcp_WhenCloudProviderIsGoogle()
  {
    // Arrange
    Mock<StorageClient> gcpClientMock = new();
    gcpClientMock
      .Setup(client => client.DeleteObjectAsync(
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<DeleteObjectOptions>(),
        It.IsAny<CancellationToken>()))
      .Returns(Task.CompletedTask);
    var storageGcpService = new StorageService(_loggerMock.Object, _optionsGcpMock.Object, gcpClient: gcpClientMock.Object);

    var fileName = "test-file";

    // Act
    var result = await storageGcpService.Delete(fileName);

    // Assert
    Assert.NotNull(result);
  }

  // Teste para verificar se o método Delete está lançando uma exceção BadRequestException quando o provedor de nuvem não está implementado.
  [Fact]
  public async Task Delete_ShouldThrowException_WhenCloudProviderNotImplemented()
  {
    // Arrange
    var storageOptions = new StorageOptions
    {
      BucketName = "test-bucket",
      CloudProvider = ECloudProvider.Microsoft,
      AWS = new MAwsOptions(),
      GCP = new MGcpOptions(),
      FileSize = 1024 // 1 MB
    };
    Mock<IOptions<StorageOptions>> optionsMock = new();
    optionsMock.Setup(o => o.Value).Returns(storageOptions);
    var storageService = new StorageService(_loggerMock.Object, optionsMock.Object);

    var fileName = "test-file";

    // Act
    var exception = await Assert.ThrowsAsync<BadRequestException>(() => storageService.Delete(fileName));

    // Assert
    Assert.Equal("Storage.CloudProviderNotImplemented", exception.Message);
  }

  // Teste para verificar se o método Delete está lançando uma exceção BadRequestException quando o nome do arquivo está vazio.
  [Fact]
  public async Task Delete_ShouldThrowException_WhenFileNameEmpty()
  {
    // Arrange
    var storageService = new StorageService(_loggerMock.Object, _optionsMock.Object);
    string fileName = null!;

    // Act
    var exception = await Assert.ThrowsAsync<BadRequestException>(() => storageService.Delete(fileName));

    // Assert
    Assert.Equal("Storage.FileNameEmpty", exception.Message);
  }

  // Teste para verificar se o método Delete está lançando uma exceção BadRequestException quando o nome do bucket está vazio.
  [Fact]
  public async Task Delete_ShouldThrowException_WhenBucketNameEmpty()
  {
    // Arrange
    var storageService = new StorageService(_loggerMock.Object, _optionsMock.Object);
    var fileName = "test-file";
    string bucketName = string.Empty;

    // Act
    var exception = await Assert.ThrowsAsync<BadRequestException>(() => storageService.Delete(fileName, bucketName));

    // Assert
    Assert.Equal("Storage.BucketNameEmpty", exception.Message);
  }

  // Teste para verificar se o método Delete está lançando uma exceção BadRequestException quando não encontra o bucket na AWS.
  [Fact]
  public async Task Delete_ShouldThrowException_WhenCloudProviderIsAmazon_WithExceptionNotFound()
  {
    // Arrange
    var awsClientMock = new Mock<IAmazonS3>();
    awsClientMock
      .Setup(client => client.DeleteObjectAsync(
        It.IsAny<DeleteObjectRequest>(),
        It.IsAny<CancellationToken>()))
      .ThrowsAsync(new AmazonS3Exception("File Not Found") { StatusCode = HttpStatusCode.NotFound });

    var storageAwsService = new StorageService(_loggerMock.Object, _optionsAwsMock.Object, awsClient: awsClientMock.Object);
    var fileName = "test-file";

    // Act
    var exception = await Assert.ThrowsAsync<BadRequestException>(() => storageAwsService.Delete(fileName));

    // Assert
    Assert.Equal("Storage.FileNotFound", exception.Message);
  }

  // Teste para verificar se o método Delete está lançando uma exceção BadRequestException quando ocorre um erro interno na AWS.
  [Fact]
  public async Task Delete_ShouldThrowException_WhenCloudProviderIsAmazon_WithExceptionAmazon()
  {
    // Arrange
    var awsClientMock = new Mock<IAmazonS3>();
    awsClientMock
      .Setup(client => client.DeleteObjectAsync(
        It.IsAny<DeleteObjectRequest>(),
        It.IsAny<CancellationToken>()))
      .ThrowsAsync(new AmazonS3Exception("Internal Server Error") { StatusCode = HttpStatusCode.InternalServerError });

    var storageAwsService = new StorageService(_loggerMock.Object, _optionsAwsMock.Object, awsClient: awsClientMock.Object);
    var fileName = "test-file";

    // Act
    var exception = await Assert.ThrowsAsync<InternalServerErrorException>(() => storageAwsService.Delete(fileName));

    // Assert
    Assert.Equal("Storage.FileErrorDeleting", exception.Message);
  }

  // Teste para verificar se o método Delete está lançando uma exceção BadRequestException quando ocorre um erro interno na aplicação.
  [Fact]
  public async Task Delete_ShouldThrowException_WhenCloudProviderIsAmazon_WithException()
  {
    // Arrange
    var awsClientMock = new Mock<IAmazonS3>();
    awsClientMock
      .Setup(client => client.DeleteObjectAsync(
        It.IsAny<DeleteObjectRequest>(),
        It.IsAny<CancellationToken>()))
      .ThrowsAsync(new Exception("Internal Server Error"));

    var storageAwsService = new StorageService(_loggerMock.Object, _optionsAwsMock.Object, awsClient: awsClientMock.Object);
    var fileStream = new MemoryStream(new byte[10]);
    var fileName = "test-file";

    // Act
    var exception = await Assert.ThrowsAsync<InternalServerErrorException>(() => storageAwsService.Delete(fileName));

    // Assert
    Assert.Equal("Storage.UnknownError", exception.Message);
  }

  // Teste para verificar se o método Delete está lançando uma exceção BadRequestException quando não encontra o arquivo no GCP.
  [Fact]
  public async Task Delete_ShouldThrowException_WhenCloudProviderIsGoogle_WithExceptionNotFound()
  {
    // Arrange    
    var gcpClientMock = new Mock<StorageClient>();
    gcpClientMock
      .Setup(client => client.DeleteObjectAsync(
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<DeleteObjectOptions>(),
        It.IsAny<CancellationToken>()))
      .ThrowsAsync(new GoogleApiException("Bucket Not Found") { Error = new RequestError() { Code = 404 } });

    var storageGcpService = new StorageService(_loggerMock.Object, _optionsGcpMock.Object, gcpClient: gcpClientMock.Object);
    var fileName = "test-file";

    // Act
    var exception = await Assert.ThrowsAsync<BadRequestException>(() => storageGcpService.Delete(fileName));

    // Assert
    Assert.Equal("Storage.FileNotFound", exception.Message);
  }

  // Teste para verificar se o método Delete está lançando uma exceção BadRequestException quando ocorre um erro interno na GCP.
  [Fact]
  public async Task Delete_ShouldThrowException_WhenCloudProviderIsGoogle_WithExceptionGoogle()
  {
    // Arrange    
    var gcpClientMock = new Mock<StorageClient>();
    gcpClientMock
      .Setup(client => client.DeleteObjectAsync(
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<DeleteObjectOptions>(),
        It.IsAny<CancellationToken>()))
      .ThrowsAsync(new GoogleApiException("Internal Server Error") { Error = new RequestError() { Code = 500 } });

    var storageGcpService = new StorageService(_loggerMock.Object, _optionsGcpMock.Object, gcpClient: gcpClientMock.Object);
    var fileName = "test-file";

    // Act
    var exception = await Assert.ThrowsAsync<InternalServerErrorException>(() => storageGcpService.Delete(fileName));

    // Assert
    Assert.Equal("Storage.FileErrorDeleting", exception.Message);
  }

  // Teste para verificar se o método Delete está lançando uma exceção BadRequestException quando ocorre um erro interno na aplicação.
  [Fact]
  public async Task Delete_ShouldThrowException_WhenCloudProviderIsGoogle_WithException()
  {
    // Arrange    
    var gcpClientMock = new Mock<StorageClient>();
    gcpClientMock
      .Setup(client => client.DeleteObjectAsync(
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<DeleteObjectOptions>(),
        It.IsAny<CancellationToken>()))
      .ThrowsAsync(new Exception("Internal Server Error"));

    var storageGcpService = new StorageService(_loggerMock.Object, _optionsGcpMock.Object, gcpClient: gcpClientMock.Object);
    var fileName = "test-file";

    // Act
    var exception = await Assert.ThrowsAsync<InternalServerErrorException>(() => storageGcpService.Delete(fileName));

    // Assert
    Assert.Equal("Storage.UnknownError", exception.Message);
  }

  // Teste para verificar se o método Download está fazendo o download de um arquivo da AWS.
  [Fact]
  public async Task Download_ShouldDownloadFileToAws_WhenCloudProviderIsAmazon()
  {
    // Arrange
    Mock<IAmazonS3> awsClientMock = new();
    awsClientMock
      .Setup(client => client.GetObjectAsync(
        It.IsAny<GetObjectRequest>(),
        It.IsAny<CancellationToken>()))
      .ReturnsAsync(new GetObjectResponse() { ResponseStream = new MemoryStream() });
    var storageAwsService = new StorageService(_loggerMock.Object, _optionsAwsMock.Object, awsClient: awsClientMock.Object);

    var fileName = "test-file";

    // Act
    var result = await storageAwsService.Download(fileName);

    // Assert
    Assert.NotNull(result);
  }

  // Teste para verificar se o método Download está fazendo o download de um arquivo da GCP.
  [Fact]
  public async Task Download_ShouldDownloadFileToGcp_WhenCloudProviderIsGoogle()
  {
    // Arrange
    Mock<StorageClient> gcpClientMock = new();
    gcpClientMock
      .Setup(client => client.DownloadObjectAsync(
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<Stream>(),
        It.IsAny<DownloadObjectOptions>(),
        It.IsAny<CancellationToken>(),
        It.IsAny<IProgress<IDownloadProgress>>()))
      .ReturnsAsync(new Google.Apis.Storage.v1.Data.Object());
    var storageGcpService = new StorageService(_loggerMock.Object, _optionsGcpMock.Object, gcpClient: gcpClientMock.Object);

    var fileName = "test-file";

    // Act
    var result = await storageGcpService.Download(fileName);

    // Assert
    Assert.NotNull(result);
  }

  // Teste para verificar se o método Download está lançando uma exceção BadRequestException quando o provedor de nuvem não está implementado.
  [Fact]
  public async Task Download_ShouldThrowException_WhenCloudProviderNotImplemented()
  {
    // Arrange
    var storageOptions = new StorageOptions
    {
      BucketName = "test-bucket",
      CloudProvider = ECloudProvider.Microsoft,
      AWS = new MAwsOptions(),
      GCP = new MGcpOptions(),
      FileSize = 1024 // 1 MB
    };
    Mock<IOptions<StorageOptions>> optionsMock = new();
    optionsMock.Setup(o => o.Value).Returns(storageOptions);
    var storageService = new StorageService(_loggerMock.Object, optionsMock.Object);

    var fileName = "test-file";

    // Act
    var exception = await Assert.ThrowsAsync<BadRequestException>(() => storageService.Download(fileName));

    // Assert
    Assert.Equal("Storage.CloudProviderNotImplemented", exception.Message);
  }

  // Teste para verificar se o método Download está lançando uma exceção BadRequestException quando o nome do arquivo está vazio.
  [Fact]
  public async Task Download_ShouldThrowException_WhenFileNameEmpty()
  {
    // Arrange
    var storageService = new StorageService(_loggerMock.Object, _optionsMock.Object);
    string fileName = null!;

    // Act
    var exception = await Assert.ThrowsAsync<BadRequestException>(() => storageService.Download(fileName));

    // Assert
    Assert.Equal("Storage.FileNameEmpty", exception.Message);
  }

  // Teste para verificar se o método Download está lançando uma exceção BadRequestException quando o nome do bucket está vazio.
  [Fact]
  public async Task Download_ShouldThrowException_WhenBucketNameEmpty()
  {
    // Arrange
    var storageService = new StorageService(_loggerMock.Object, _optionsMock.Object);
    var fileName = "test-file";
    string bucketName = string.Empty;

    // Act
    var exception = await Assert.ThrowsAsync<BadRequestException>(() => storageService.Download(fileName, bucketName));

    // Assert
    Assert.Equal("Storage.BucketNameEmpty", exception.Message);
  }

  // Teste para verificar se o método Download está lançando uma exceção BadRequestException quando não encontra o bucket na AWS.
  [Fact]
  public async Task Download_ShouldThrowException_WhenCloudProviderIsAmazon_WithExceptionNotFound()
  {
    // Arrange
    var awsClientMock = new Mock<IAmazonS3>();
    awsClientMock
      .Setup(client => client.GetObjectAsync(
        It.IsAny<GetObjectRequest>(),
        It.IsAny<CancellationToken>()))
      .ThrowsAsync(new AmazonS3Exception("File Not Found") { StatusCode = HttpStatusCode.NotFound });

    var storageAwsService = new StorageService(_loggerMock.Object, _optionsAwsMock.Object, awsClient: awsClientMock.Object);
    var fileName = "test-file";

    // Act
    var exception = await Assert.ThrowsAsync<BadRequestException>(() => storageAwsService.Download(fileName));

    // Assert
    Assert.Equal("Storage.FileNotFound", exception.Message);
  }

  // Teste para verificar se o método Download está lançando uma exceção BadRequestException quando ocorre um erro interno na AWS.
  [Fact]
  public async Task Download_ShouldThrowException_WhenCloudProviderIsAmazon_WithExceptionAmazon()
  {
    // Arrange
    var awsClientMock = new Mock<IAmazonS3>();
    awsClientMock
      .Setup(client => client.GetObjectAsync(
        It.IsAny<GetObjectRequest>(),
        It.IsAny<CancellationToken>()))
      .ThrowsAsync(new AmazonS3Exception("Internal Server Error") { StatusCode = HttpStatusCode.InternalServerError });

    var storageAwsService = new StorageService(_loggerMock.Object, _optionsAwsMock.Object, awsClient: awsClientMock.Object);
    var fileName = "test-file";

    // Act
    var exception = await Assert.ThrowsAsync<InternalServerErrorException>(() => storageAwsService.Download(fileName));

    // Assert
    Assert.Equal("Storage.FileErrorDownloading", exception.Message);
  }

  // Teste para verificar se o método Download está lançando uma exceção BadRequestException quando ocorre um erro interno na aplicação.
  [Fact]
  public async Task Download_ShouldThrowException_WhenCloudProviderIsAmazon_WithException()
  {
    // Arrange
    var awsClientMock = new Mock<IAmazonS3>();
    awsClientMock
      .Setup(client => client.GetObjectAsync(
        It.IsAny<GetObjectRequest>(),
        It.IsAny<CancellationToken>()))
      .ThrowsAsync(new Exception("Internal Server Error"));

    var storageAwsService = new StorageService(_loggerMock.Object, _optionsAwsMock.Object, awsClient: awsClientMock.Object);
    var fileStream = new MemoryStream(new byte[10]);
    var fileName = "test-file";

    // Act
    var exception = await Assert.ThrowsAsync<InternalServerErrorException>(() => storageAwsService.Download(fileName));

    // Assert
    Assert.Equal("Storage.UnknownError", exception.Message);
  }

  // Teste para verificar se o método Download está lançando uma exceção BadRequestException quando não encontra o arquivo no GCP.
  [Fact]
  public async Task Download_ShouldThrowException_WhenCloudProviderIsGoogle_WithExceptionNotFound()
  {
    // Arrange    
    var gcpClientMock = new Mock<StorageClient>();
    gcpClientMock
      .Setup(client => client.DownloadObjectAsync(
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<Stream>(),
        It.IsAny<DownloadObjectOptions>(),
        It.IsAny<CancellationToken>(),
        It.IsAny<IProgress<IDownloadProgress>>()))
      .ThrowsAsync(new GoogleApiException("Bucket Not Found") { Error = new RequestError() { Code = 404 } });

    var storageGcpService = new StorageService(_loggerMock.Object, _optionsGcpMock.Object, gcpClient: gcpClientMock.Object);
    var fileName = "test-file";

    // Act
    var exception = await Assert.ThrowsAsync<BadRequestException>(() => storageGcpService.Download(fileName));

    // Assert
    Assert.Equal("Storage.FileNotFound", exception.Message);
  }

  // Teste para verificar se o método Download está lançando uma exceção BadRequestException quando ocorre um erro interno na GCP.
  [Fact]
  public async Task Download_ShouldThrowException_WhenCloudProviderIsGoogle_WithExceptionGoogle()
  {
    // Arrange    
    var gcpClientMock = new Mock<StorageClient>();
    gcpClientMock
      .Setup(client => client.DownloadObjectAsync(
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<Stream>(),
        It.IsAny<DownloadObjectOptions>(),
        It.IsAny<CancellationToken>(),
        It.IsAny<IProgress<IDownloadProgress>>()))
      .ThrowsAsync(new GoogleApiException("Internal Server Error") { Error = new RequestError() { Code = 500 } });

    var storageGcpService = new StorageService(_loggerMock.Object, _optionsGcpMock.Object, gcpClient: gcpClientMock.Object);
    var fileName = "test-file";

    // Act
    var exception = await Assert.ThrowsAsync<InternalServerErrorException>(() => storageGcpService.Download(fileName));

    // Assert
    Assert.Equal("Storage.FileErrorDownloading", exception.Message);
  }

  // Teste para verificar se o método Download está lançando uma exceção BadRequestException quando ocorre um erro interno na aplicação.
  [Fact]
  public async Task Download_ShouldThrowException_WhenCloudProviderIsGoogle_WithException()
  {
    // Arrange    
    var gcpClientMock = new Mock<StorageClient>();
    gcpClientMock
      .Setup(client => client.DownloadObjectAsync(
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<Stream>(),
        It.IsAny<DownloadObjectOptions>(),
        It.IsAny<CancellationToken>(),
        It.IsAny<IProgress<IDownloadProgress>>()))
      .ThrowsAsync(new Exception("Internal Server Error"));

    var storageGcpService = new StorageService(_loggerMock.Object, _optionsGcpMock.Object, gcpClient: gcpClientMock.Object);
    var fileName = "test-file";

    // Act
    var exception = await Assert.ThrowsAsync<InternalServerErrorException>(() => storageGcpService.Download(fileName));

    // Assert
    Assert.Equal("Storage.UnknownError", exception.Message);
  }

  // Teste para verificar se o método Signer está fazendo a assinatura de um arquivo da AWS.
  [Fact]
  public async Task Signer_ShouldSignerFileToAws_WhenCloudProviderIsAmazon()
  {
    // Arrange
    Mock<IAmazonS3> awsClientMock = new();
    awsClientMock
      .Setup(client => client.GetPreSignedURLAsync(
        It.IsAny<GetPreSignedUrlRequest>()))
      .ReturnsAsync("https://example.com/signed-url");
    var storageAwsService = new StorageService(_loggerMock.Object, _optionsAwsMock.Object, awsClient: awsClientMock.Object);

    var fileName = "test-file";

    // Act
    var result = await storageAwsService.Signer(fileName);

    // Assert
    Assert.NotNull(result);
  }

  // Teste para verificar se o método Signer está fazendo a assinatura de um arquivo da GCP.
  [Fact]
  public async Task Signer_ShouldSignerFileToGcp_WhenCloudProviderIsGoogle()
  {
    // Arrange
    Mock<IUrlSigner> gcpSignerClientMock = new();
    gcpSignerClientMock
      .Setup(client => client.SignAsync(
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<TimeSpan>(),
        It.IsAny<CancellationToken>()))
      .ReturnsAsync("https://example.com/signed-url");
    var storageGcpService = new StorageService(_loggerMock.Object, _optionsGcpMock.Object, gcpClientSigner: gcpSignerClientMock.Object);

    var fileName = "test-file";

    // Act
    var result = await storageGcpService.Signer(fileName);

    // Assert
    Assert.NotNull(result);
  }

  // Teste para verificar se o método Signer está fazendo a assinatura com a expiração inválida.
  [Fact]
  public async Task Signer_ShouldSignerFile_WhenExpiresMinuteInvalid()
  {
    // Arrange    
    Mock<IUrlSigner> gcpSignerClientMock = new();
    gcpSignerClientMock
      .Setup(client => client.SignAsync(
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<TimeSpan>(),
        It.IsAny<CancellationToken>()))
      .ReturnsAsync("https://example.com/signed-url");
    var storageGcpService = new StorageService(_loggerMock.Object, _optionsGcpMock.Object, gcpClientSigner: gcpSignerClientMock.Object);

    var fileName = "test-file";
    int? expiresIn = -1;

    // Act
    var result = await storageGcpService.Signer(fileName, expiresIn);

    // Assert
    Assert.NotNull(result);
  }

  // Teste para verificar se o método Signer está lançando uma exceção BadRequestException quando o provedor de nuvem não está implementado.
  [Fact]
  public async Task Signer_ShouldThrowException_WhenCloudProviderNotImplemented()
  {
    // Arrange
    var storageOptions = new StorageOptions
    {
      BucketName = "test-bucket",
      CloudProvider = ECloudProvider.Microsoft,
      AWS = new MAwsOptions(),
      GCP = new MGcpOptions(),
      FileSize = 1024 // 1 MB
    };
    Mock<IOptions<StorageOptions>> optionsMock = new();
    optionsMock.Setup(o => o.Value).Returns(storageOptions);
    var storageService = new StorageService(_loggerMock.Object, optionsMock.Object);

    var fileName = "test-file";

    // Act
    var exception = await Assert.ThrowsAsync<BadRequestException>(() => storageService.Signer(fileName));

    // Assert
    Assert.Equal("Storage.CloudProviderNotImplemented", exception.Message);
  }

  // Teste para verificar se o método Signer está lançando uma exceção BadRequestException quando o nome do arquivo está vazio.
  [Fact]
  public async Task Signer_ShouldThrowException_WhenFileNameEmpty()
  {
    // Arrange
    var storageService = new StorageService(_loggerMock.Object, _optionsMock.Object);
    string fileName = null!;

    // Act
    var exception = await Assert.ThrowsAsync<BadRequestException>(() => storageService.Signer(fileName));

    // Assert
    Assert.Equal("Storage.FileNameEmpty", exception.Message);
  }

  // Teste para verificar se o método Signer está lançando uma exceção BadRequestException quando o nome do bucket está vazio.
  [Fact]
  public async Task Signer_ShouldThrowException_WhenBucketNameEmpty()
  {
    // Arrange
    var storageService = new StorageService(_loggerMock.Object, _optionsMock.Object);
    var fileName = "test-file";
    string bucketName = string.Empty;

    // Act
    var exception = await Assert.ThrowsAsync<BadRequestException>(() => storageService.Signer(fileName, bucketName: bucketName));

    // Assert
    Assert.Equal("Storage.BucketNameEmpty", exception.Message);
  }

  // Teste para verificar se o método Signer está lançando uma exceção BadRequestException quando não encontra o bucket na AWS.
  [Fact]
  public async Task Signer_ShouldThrowException_WhenCloudProviderIsAmazon_WithExceptionNotFound()
  {
    // Arrange
    var awsClientMock = new Mock<IAmazonS3>();
    awsClientMock
      .Setup(client => client.GetPreSignedURLAsync(
        It.IsAny<GetPreSignedUrlRequest>()))
      .ThrowsAsync(new AmazonS3Exception("File Not Found") { StatusCode = HttpStatusCode.NotFound });

    var storageAwsService = new StorageService(_loggerMock.Object, _optionsAwsMock.Object, awsClient: awsClientMock.Object);
    var fileName = "test-file";

    // Act
    var exception = await Assert.ThrowsAsync<BadRequestException>(() => storageAwsService.Signer(fileName));

    // Assert
    Assert.Equal("Storage.FileNotFound", exception.Message);
  }

  // Teste para verificar se o método Signer está lançando uma exceção BadRequestException quando ocorre um erro interno na AWS.
  [Fact]
  public async Task Signer_ShouldThrowException_WhenCloudProviderIsAmazon_WithExceptionAmazon()
  {
    // Arrange
    var awsClientMock = new Mock<IAmazonS3>();
    awsClientMock
      .Setup(client => client.GetPreSignedURLAsync(
        It.IsAny<GetPreSignedUrlRequest>()))
      .ThrowsAsync(new AmazonS3Exception("Internal Server Error") { StatusCode = HttpStatusCode.InternalServerError });

    var storageAwsService = new StorageService(_loggerMock.Object, _optionsAwsMock.Object, awsClient: awsClientMock.Object);
    var fileName = "test-file";

    // Act
    var exception = await Assert.ThrowsAsync<InternalServerErrorException>(() => storageAwsService.Signer(fileName));

    // Assert
    Assert.Equal("Storage.FileErrorSigner", exception.Message);
  }

  // Teste para verificar se o método Signer está lançando uma exceção BadRequestException quando ocorre um erro interno na aplicação.
  [Fact]
  public async Task Signer_ShouldThrowException_WhenCloudProviderIsAmazon_WithException()
  {
    // Arrange
    var awsClientMock = new Mock<IAmazonS3>();
    awsClientMock
      .Setup(client => client.GetPreSignedURLAsync(
        It.IsAny<GetPreSignedUrlRequest>()))
      .ThrowsAsync(new Exception("Internal Server Error"));

    var storageAwsService = new StorageService(_loggerMock.Object, _optionsAwsMock.Object, awsClient: awsClientMock.Object);
    var fileStream = new MemoryStream(new byte[10]);
    var fileName = "test-file";

    // Act
    var exception = await Assert.ThrowsAsync<InternalServerErrorException>(() => storageAwsService.Signer(fileName));

    // Assert
    Assert.Equal("Storage.UnknownError", exception.Message);
  }

  // Teste para verificar se o método Signer está lançando uma exceção BadRequestException quando não encontra o arquivo no GCP.
  [Fact]
  public async Task Signer_ShouldThrowException_WhenCloudProviderIsGoogle_WithExceptionNotFound()
  {
    // Arrange    
    var gcpSignerClientMock = new Mock<IUrlSigner>();
    gcpSignerClientMock
      .Setup(client => client.SignAsync(
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<TimeSpan>(),
        It.IsAny<CancellationToken>()))
      .ThrowsAsync(new GoogleApiException("Bucket Not Found") { Error = new RequestError() { Code = 404 } });

    var storageGcpService = new StorageService(_loggerMock.Object, _optionsGcpMock.Object, gcpClientSigner: gcpSignerClientMock.Object);
    var fileName = "test-file";

    // Act
    var exception = await Assert.ThrowsAsync<BadRequestException>(() => storageGcpService.Signer(fileName));

    // Assert
    Assert.Equal("Storage.FileNotFound", exception.Message);
  }

  // Teste para verificar se o método Signer está lançando uma exceção BadRequestException quando ocorre um erro interno na GCP.
  [Fact]
  public async Task Signer_ShouldThrowException_WhenCloudProviderIsGoogle_WithExceptionGoogle()
  {
    // Arrange    
    var gcpSignerClientMock = new Mock<IUrlSigner>();
    gcpSignerClientMock
      .Setup(client => client.SignAsync(
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<TimeSpan>(),
        It.IsAny<CancellationToken>()))
      .ThrowsAsync(new GoogleApiException("Internal Server Error") { Error = new RequestError() { Code = 500 } });

    var storageGcpService = new StorageService(_loggerMock.Object, _optionsGcpMock.Object, gcpClientSigner: gcpSignerClientMock.Object);
    var fileName = "test-file";

    // Act
    var exception = await Assert.ThrowsAsync<InternalServerErrorException>(() => storageGcpService.Signer(fileName));

    // Assert
    Assert.Equal("Storage.FileErrorSigner", exception.Message);
  }

  // Teste para verificar se o método Signer está lançando uma exceção BadRequestException quando ocorre um erro interno na aplicação.
  [Fact]
  public async Task Signer_ShouldThrowException_WhenCloudProviderIsGoogle_WithException()
  {
    // Arrange    
    var gcpSignerClientMock = new Mock<IUrlSigner>();
    gcpSignerClientMock
      .Setup(client => client.SignAsync(
        It.IsAny<string>(),
        It.IsAny<string>(),
        It.IsAny<TimeSpan>(),
        It.IsAny<CancellationToken>()))
      .ThrowsAsync(new Exception("Internal Server Error"));

    var storageGcpService = new StorageService(_loggerMock.Object, _optionsGcpMock.Object, gcpClientSigner: gcpSignerClientMock.Object);
    var fileName = "test-file";

    // Act
    var exception = await Assert.ThrowsAsync<InternalServerErrorException>(() => storageGcpService.Signer(fileName));

    // Assert
    Assert.Equal("Storage.UnknownError", exception.Message);
  }
}

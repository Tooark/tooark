using System.Text.Json;
using Amazon.S3;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Tooark.Storages;
using Tooark.Storages.Injections;
using Tooark.Storages.Interfaces;
using Tooark.Storages.Options;

namespace Tooark.Tests.Storages.Injections;

public class TooarkDependencyInjectionStoragesTest
{
  // Teste para verificar se o método AddTooarkStorages está configurando as opções de armazenamento
  [Fact]
  public void AddTooarkStorages_ShouldConfigureStorageOptions_WhenStorageOptionsSectionExists()
  {
    // Arrange
    var services = new ServiceCollection();
    var configurationMock = new Mock<IConfigurationManager>();
    var configurationSectionMock = new Mock<IConfigurationSection>();
    var setStorageOptions = new StorageOptions { /* set properties as needed */ };

    configurationSectionMock.Setup(x => x.Value).Returns(JsonSerializer.Serialize(setStorageOptions));
    configurationMock.Setup(x => x.GetSection(StorageOptions.Section)).Returns(configurationSectionMock.Object);

    // Act
    services.AddTooarkStorages(configurationMock.Object);

    var serviceProvider = services.BuildServiceProvider();
    var options = serviceProvider.GetService<IOptions<StorageOptions>>();

    // Assert
    Assert.NotNull(options);
  }

  [Fact]
  public void AddTooarkStorages_ShouldAddStorageService_WhenStorageOptionsSectionExists()
  {
    // Arrange
    var services = new ServiceCollection();
    var configurationMock = new Mock<IConfigurationManager>();
    var configurationSectionMock = new Mock<IConfigurationSection>();
    var loggerMock = new Mock<ILogger<StorageService>>();
    var setStorageOptions = new StorageOptions
    {
      BucketName = "bucket-name",
      CloudProvider = "AWS",
      AWS = new AwsOptions()
    };

    configurationSectionMock.Setup(x => x.Value).Returns(JsonSerializer.Serialize(setStorageOptions));
    configurationMock.Setup(x => x.GetSection(StorageOptions.Section)).Returns(configurationSectionMock.Object);
    services.AddSingleton(loggerMock.Object);

    // Act
    services.AddTooarkStorages(configurationMock.Object);

    var serviceProvider = services.BuildServiceProvider();
    var storageService = serviceProvider.GetService<IStorageService>();

    // Assert
    Assert.NotNull(storageService);
  }

  [Fact]
  public void AddTooarkStorages_ShouldNotAddStorageService_WhenStorageOptionsSectionDoesNotExist()
  {
    // Arrange
    var services = new ServiceCollection();
    var configurationMock = new Mock<IConfigurationManager>();
    var configurationSectionMock = new Mock<IConfigurationSection>();
    StorageOptions setStorageOptions = null!;
    configurationSectionMock.Setup(x => x.Value).Returns(JsonSerializer.Serialize(setStorageOptions));
    configurationMock.Setup(x => x.GetSection(StorageOptions.Section)).Returns(configurationSectionMock.Object);

    // Act
    services.AddTooarkStorages(configurationMock.Object);

    // Assert
    var serviceProvider = services.BuildServiceProvider();
    var storageService = serviceProvider.GetService<IStorageService>();
    Assert.Null(storageService);
  }

  [Fact]
  public void Add1TooarkStorages_ShouldAddStorageService_WhenStorageOptionsExist()
  {
    // Arrange
    var services = new ServiceCollection();
    var configurationMock = new Mock<IConfigurationManager>();
    var configurationSectionMock = new Mock<IConfigurationSection>();
    var setStorageOptions = new StorageOptions
    {
      BucketName = "bucket-name",
      CloudProvider = "AWS",
      AWS = new AwsOptions()
    };
    configurationSectionMock.Setup(x => x.Key).Returns("StorageOptions");
    configurationSectionMock.Setup(x => x.Path).Returns("StorageOptions");
    configurationSectionMock.Setup(x => x.Value).Returns(JsonSerializer.Serialize(setStorageOptions));
    configurationMock.Setup(x => x.GetSection(StorageOptions.Section)).Returns(configurationSectionMock.Object);

    var awsClientMock = new Mock<IAmazonS3>();
    var gcpClientMock = new Mock<StorageClient>();
    var gcpClientSignerMock = new Mock<IUrlSigner>();

    // Act
    services.Configure<StorageOptions>(options => configurationMock.Object.GetSection(StorageOptions.Section));
    services.AddTooarkStorages(configurationMock.Object, awsClientMock.Object, gcpClientMock.Object, gcpClientSignerMock.Object);
    var serviceProvider = services.BuildServiceProvider();
    var storageService = serviceProvider.GetService<IStorageService>();

    // Assert
    Assert.NotNull(storageService);
  }
}

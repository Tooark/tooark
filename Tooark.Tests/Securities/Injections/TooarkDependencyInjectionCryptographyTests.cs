using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tooark.Securities.Injections;
using Tooark.Securities.Interfaces;
using Tooark.Securities.Options;

namespace Tooark.Tests.Securities.Injections;

public class TooarkDependencyInjectionCryptographyTests
{
  // Teste para verificar se o método AddTooarkCryptography adiciona o serviço corretamente.
  [Fact]
  public void AddTooarkCryptography_ShouldAddServices()
  {
    // Arrange
    var services = new ServiceCollection();
    var inMemorySettings = new Dictionary<string, string?>
    {
      { $"{CryptographyOptions.Section}:Algorithm", "GCM" },
      { $"{CryptographyOptions.Section}:Secret", "initialization-vector-test" }
    };

    IConfiguration configuration = new ConfigurationBuilder()
      .AddInMemoryCollection(inMemorySettings)
      .Build();

    // Act
    services.AddTooarkCryptography(configuration);
    var provider = services.BuildServiceProvider();
    var cryptographyService = provider.GetService<ICryptographyService>();
    var options = provider.GetService<Microsoft.Extensions.Options.IOptions<CryptographyOptions>>();

    // Assert
    Assert.NotNull(cryptographyService);
    Assert.NotNull(options);
    Assert.Equal("GCM", options.Value.Algorithm);
    Assert.Equal("initialization-vector-test", options.Value.Secret);
  }
}

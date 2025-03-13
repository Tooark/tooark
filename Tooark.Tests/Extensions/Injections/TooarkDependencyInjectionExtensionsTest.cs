using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Tooark.Extensions.Injections;

namespace Tooark.Tests.Extensions.Injections;

public class TooarkDependencyInjectionExtensionsTest
{
  // Teste para verificar se o método AddTooarkExtensions adiciona os serviços corretamente.
  [Fact]
  public void AddTooarkExtensions_ShouldAddServices()
  {
    // Arrange
    var services = new ServiceCollection();

    // Act
    services.AddTooarkExtensions();
    var serviceProvider = services.BuildServiceProvider();
    var localizer = serviceProvider.GetService<IStringLocalizer>();

    // Assert
    Assert.NotNull(localizer);
  }
}

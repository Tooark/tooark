using Tooark.Enums;

namespace Tooark.Tests.Enums;

public class ECloudProviderTest
{
  [Fact]
  public void CloudProvider_AWS_ShouldBeDefined()
  {
    // Arrange & Act
    var isDefined = Enum.IsDefined(typeof(CloudProvider), "AWS");

    // Assert
    Assert.True(isDefined, "AWS should be defined in CloudProvider enum.");
  }

  [Fact]
  public void CloudProvider_GCP_ShouldBeDefined()
  {
    // Arrange & Act
    var isDefined = Enum.IsDefined(typeof(CloudProvider), "GCP");

    // Assert
    Assert.True(isDefined, "GCP should be defined in CloudProvider enum.");
  }

  [Fact]
  public void CloudProvider_None_ShouldBeDefined()
  {
    // Arrange & Act
    var isDefined = Enum.IsDefined(typeof(CloudProvider), "None");

    // Assert
    Assert.True(isDefined, "None should be defined in CloudProvider enum.");
  }

  [Fact]
  public void CloudProvider_InvalidValue_ShouldNotBeDefined()
  {
    // Arrange & Act
    var isDefined = Enum.IsDefined(typeof(CloudProvider), "Azure");

    // Assert
    Assert.False(isDefined, "Azure should not be defined in CloudProvider enum.");
  }
}

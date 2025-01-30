using Tooark.Enums;
using Tooark.Validations.Patterns;

namespace Tooark.Tests.Enums;

public class ECloudProviderTests
{
  // Teste para converter de int para ECloudProvider e retornar o int e a string corretas.
  [Theory]
  [InlineData(0, "None")]
  [InlineData(1, "AWS")]
  [InlineData(2, "GCP")]
  [InlineData(3, "Azure")]
  public void ECloudProvider_ShouldBeValid_WhenGivenId(int id, string description)
  {
    // Arrange
    ECloudProvider cloudProvider = id;

    // Act
    int cloudProviderId = cloudProvider;
    string cloudProviderDescription = cloudProvider;

    // Assert
    Assert.Equal(id, cloudProviderId);
    Assert.Equal(description, cloudProviderDescription);
  }

  // Teste para converter de string para ECloudProvider e retornar a string e o int corretos.
  [Theory]
  [InlineData("None", 0)]
  [InlineData("AWS", 1)]
  [InlineData("GCP", 2)]
  [InlineData("Azure", 3)]
  public void ECloudProvider_ShouldBeValid_WhenGivenDescription(string description, int id)
  {
    // Arrange
    ECloudProvider cloudProvider = description;

    // Act
    string cloudProviderDescription = cloudProvider;
    int cloudProviderId = cloudProvider;

    // Assert
    Assert.Equal(description, cloudProviderDescription);
    Assert.Equal(id, cloudProviderId);
  }

  // Teste para converter de ECloudProvider com ToInt.
  [Fact]
  public void ECloudProvider_ShouldConvertWithToInt()
  {
    // Arrange & Act
    int none = 0;
    int aws = 1;
    int gcp = 2;
    int azure = 3;

    // Assert
    Assert.Equal(none, ECloudProvider.None.ToInt());
    Assert.Equal(aws, ECloudProvider.Amazon.ToInt());
    Assert.Equal(gcp, ECloudProvider.Google.ToInt());
    Assert.Equal(azure, ECloudProvider.Microsoft.ToInt());
  }

  // Teste para converter de ECloudProvider com ToString.
  [Fact]
  public void ECloudProvider_ShouldConvertWithToString()
  {
    // Arrange & Act
    string none = "None";
    string aws = "AWS";
    string gcp = "GCP";
    string azure = "Azure";

    // Assert
    Assert.Equal(none, ECloudProvider.None.ToString());
    Assert.Equal(aws, ECloudProvider.Amazon.ToString());
    Assert.Equal(gcp, ECloudProvider.Google.ToString());
    Assert.Equal(azure, ECloudProvider.Microsoft.ToString());
  }

  // Teste para converter de ECloudProvider para int.
  [Fact]
  public void ECloudProvider_ShouldImplicitConversionToInt()
  {
    // Arrange & Act
    int none = ECloudProvider.None;
    int aws = ECloudProvider.Amazon;
    int gcp = ECloudProvider.Google;
    int azure = ECloudProvider.Microsoft;

    // Assert
    Assert.Equal(0, none);
    Assert.Equal(1, aws);
    Assert.Equal(2, gcp);
    Assert.Equal(3, azure);
  }

  // Teste para converter de ECloudProvider para string.
  [Fact]
  public void ECloudProvider_ShouldImplicitConversionToString()
  {
    // Arrange & Act
    string none = ECloudProvider.None;
    string aws = ECloudProvider.Amazon;
    string gcp = ECloudProvider.Google;
    string azure = ECloudProvider.Microsoft;

    // Assert
    Assert.Equal("None", none);
    Assert.Equal("AWS", aws);
    Assert.Equal("GCP", gcp);
    Assert.Equal("Azure", azure);
  }

  // Teste para retornar o tipo de documento "None" quando o id for inválido.
  [Theory]
  [InlineData(-1)]
  [InlineData(8)]
  public void ECloudProvider_ShouldDefault_WhenIdOutRange(int id)
  {
    // Arrange & Act
    ECloudProvider cloudProvider = id;

    // Assert
    Assert.Equal(cloudProvider, ECloudProvider.None);
  }

  // Teste para retornar o tipo de documento "None" quando a descrição for inválida.
  [Theory]
  [InlineData(null)]
  [InlineData("")]
  [InlineData("invalid")]
  [InlineData("unknown")]
  public void ECloudProvider_ShouldDefault_WhenDescriptionOutRange(string? description)
  {
    // Arrange & Act
    ECloudProvider cloudProvider = description!;

    // Assert
    Assert.Equal(cloudProvider, ECloudProvider.None);
  }
}

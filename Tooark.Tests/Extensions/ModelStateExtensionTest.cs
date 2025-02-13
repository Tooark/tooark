using Microsoft.AspNetCore.Mvc.ModelBinding;
using Tooark.Extensions;

namespace Tooark.Tests.Extensions;

public class ModelStateExtensionTest
{
  // Testes do método sem erros
  [Fact]
  public void GetErrors_ReturnsEmptyList_WhenModelStateIsValid()
  {
    // Arrange
    var modelState = new ModelStateDictionary();

    // Act
    var result = modelState.GetErrors();

    // Assert
    Assert.Empty(result);
  }

  [Fact]
  public void GetErrors_ReturnsErrors_WhenModelStateHasErrors()
  {
    // Arrange
    var modelState = new ModelStateDictionary();
    modelState.AddModelError("Key1", "Error message 1");
    modelState.AddModelError("Key2", "Error message 2");

    // Act
    var result = modelState.GetErrors();

    // Assert
    Assert.Equal(2, result.Count);
    Assert.Contains("Error message 1", result);
    Assert.Contains("Error message 2", result);
  }
}

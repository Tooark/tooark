using System.Net;
using System.Text;
using System.Text.Json;
using Moq;
using Moq.Protected;
using Tooark.Exceptions;
using Tooark.Factories;
using Tooark.Interfaces;
using Tooark.Tests.Moq.Model.Person;

namespace Tooark.Tests.Services;

public class HttpClientServiceTests
{
  private readonly IHttpClientService _httpClientService;
  private readonly Mock<HttpMessageHandler> _mockHandler;

  public HttpClientServiceTests()
  {
    _mockHandler = new Mock<HttpMessageHandler>();
    var httpClient = new HttpClient(_mockHandler.Object);
    _httpClientService = HttpClientServiceFactory.Create(httpClient);
  }

  // Get Json Async retorna o objeto quando for bem-sucedido
  [Fact]
  public async Task GetFromJsonAsync_ReturnsObject_WhenSuccess()
  {
    // Arrange
    var requestUri = "https://api.example.com/data";
    var expectedObject = new { Name = "Test" };
    _mockHandler
      .Protected()
      .Setup<Task<HttpResponseMessage>>(
        "SendAsync",
        ItExpr.IsAny<HttpRequestMessage>(),
        ItExpr.IsAny<CancellationToken>()
      )
      .ReturnsAsync(new HttpResponseMessage
      {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent(JsonSerializer.Serialize(expectedObject))
      });

    // Act
    var result = await _httpClientService.GetFromJsonAsync<dynamic>(requestUri);

    // Assert
    Assert.NotNull(result);
    Assert.Equal("Test", result?.GetProperty("Name").GetString());
  }

  // Get Json Async retorna o objeto Person quando for bem-sucedido
  [Fact]
  public async Task GetFromJsonAsync_ReturnsPersonObject_WhenSuccess()
  {
    // Arrange
    var requestUri = "https://api.example.com/data";
    var expectedResponse = new Person("Teste", 20);

    _mockHandler
      .Protected()
      .Setup<Task<HttpResponseMessage>>(
        "SendAsync",
        ItExpr.IsAny<HttpRequestMessage>(),
        ItExpr.IsAny<CancellationToken>()
      )
      .ReturnsAsync(new HttpResponseMessage
      {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent(JsonSerializer.Serialize(expectedResponse))
      });

    // Act
    var result = await _httpClientService.GetFromJsonAsync<Person>(requestUri);

    // Assert
    Assert.NotNull(result);
    Assert.IsType<Person>(result);
    Assert.Equal(expectedResponse.Name, result.Name);
    Assert.Equal(expectedResponse.Age, result.Age);
  }

  // Get Json Async lança exceção de solicitação HTTP quando não encontrado
  [Fact]
  public async Task GetFromJsonAsync_ThrowsHttpRequestException_WhenNotFound()
  {
    // Arrange
    var requestUri = "https://api.example.com/data";
    _mockHandler
      .Protected()
      .Setup<Task<HttpResponseMessage>>(
        "SendAsync",
        ItExpr.IsAny<HttpRequestMessage>(),
        ItExpr.IsAny<CancellationToken>()
      )
      .ReturnsAsync(new HttpResponseMessage
      {
        StatusCode = HttpStatusCode.NotFound
      });

    // Act & Assert
    await Assert.ThrowsAsync<HttpRequestFailedException>(() => _httpClientService.GetFromJsonAsync<dynamic>(requestUri));
  }

  // Get Json Async lança exceção de desserialização de Json quando Json inválido
  [Fact]
  public async Task GetFromJsonAsync_ThrowsJsonDeserializationException_WhenInvalidJson()
  {
    // Arrange
    var requestUri = "https://api.example.com/data";
    _mockHandler
      .Protected()
      .Setup<Task<HttpResponseMessage>>(
        "SendAsync",
        ItExpr.IsAny<HttpRequestMessage>(),
        ItExpr.IsAny<CancellationToken>()
      )
      .ReturnsAsync(new HttpResponseMessage
      {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent("Invalid JSON")
      });

    // Act
    var exception = await Record.ExceptionAsync(() => _httpClientService.GetFromJsonAsync<dynamic>(requestUri));

    // Assert
    Assert.IsType<JsonDeserializationException>(exception);
  }

  // Post Json Async retorna mensagem de resposta HTTP quando bem-sucedido
  [Fact]
  public async Task PostAsJsonAsync_ReturnsHttpResponseMessage_WhenSuccess()
  {
    // Arrange
    var requestUri = "https://api.example.com/data";
    var postData = new { Name = "Test" };
    _mockHandler
      .Protected()
      .Setup<Task<HttpResponseMessage>>(
        "SendAsync",
        ItExpr.IsAny<HttpRequestMessage>(),
        ItExpr.IsAny<CancellationToken>()
      )
      .ReturnsAsync(new HttpResponseMessage
      {
        StatusCode = HttpStatusCode.Created
      });

    // Act
    var response = await _httpClientService.PostAsJsonAsync(requestUri, postData);

    // Assert
    Assert.NotNull(response);
    Assert.Equal(HttpStatusCode.Created, response.StatusCode);
  }

  // Post Json Async retorna pessoa da mensagem de resposta Http quando for bem-sucedido
  [Fact]
  public async Task PostAsJsonAsync_ReturnsHttpResponseMessagePerson_WhenSuccess()
  {
    // Arrange
    var requestUri = "https://api.example.com/data";
    var postData = new Person("Teste", 20);
    var mockResponseContent = new StringContent(JsonSerializer.Serialize(postData), Encoding.UTF8, "application/json");
    _mockHandler
      .Protected()
      .Setup<Task<HttpResponseMessage>>(
        "SendAsync",
        ItExpr.IsAny<HttpRequestMessage>(),
        ItExpr.IsAny<CancellationToken>()
      )
      .ReturnsAsync(new HttpResponseMessage
      {
        StatusCode = HttpStatusCode.Created,
        Content = mockResponseContent
      });

    // Act
    var response = await _httpClientService.PostAsJsonAsync(requestUri, postData);

    // Assert
    Assert.NotNull(response);
    Assert.Equal(HttpStatusCode.Created, response.StatusCode);
  }

  // Put Json Async retorna mensagem de resposta HTTP quando bem sucedido
  [Fact]
  public async Task PutAsJsonAsync_ReturnsHttpResponseMessage_WhenSuccess()
  {
    // Arrange
    var requestUri = "https://api.example.com/resource";
    var content = new { Data = "Sample" };
    _mockHandler
      .Protected()
      .Setup<Task<HttpResponseMessage>>(
        "SendAsync",
        ItExpr.IsAny<HttpRequestMessage>(),
        ItExpr.IsAny<CancellationToken>()
      )
      .ReturnsAsync(new HttpResponseMessage
      {
        StatusCode = HttpStatusCode.NoContent // 204 No Content is a common response for PUT
      });

    // Act
    var response = await _httpClientService.PutAsJsonAsync(requestUri, content);

    // Assert
    Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
  }

  // Put Json Async retorna pessoa da mensagem de resposta Http quando for bem-sucedido
  [Fact]
  public async Task PutAsJsonAsync_ReturnsHttpResponseMessagePerson_WhenSuccess()
  {
    // Arrange
    var requestUri = "https://api.example.com/resource";
    var content = new Person("Teste", 20);
    var mockResponseContent = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
    _mockHandler
      .Protected()
      .Setup<Task<HttpResponseMessage>>(
        "SendAsync",
        ItExpr.IsAny<HttpRequestMessage>(),
        ItExpr.IsAny<CancellationToken>()
      )
      .ReturnsAsync(new HttpResponseMessage
      {
        StatusCode = HttpStatusCode.NoContent, // 204 No Content is a common response for PUT
        Content = mockResponseContent
      });

    // Act
    var response = await _httpClientService.PutAsJsonAsync(requestUri, content);

    // Assert
    Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
  }

  // Delete Json Async retorna código de status de sucesso
  [Fact]
  public async Task DeleteAsJsonAsync_ReturnsSuccessStatusCode()
  {
    // Arrange
    var requestUri = "https://api.example.com/resource";
    _mockHandler
      .Protected()
      .Setup<Task<HttpResponseMessage>>(
        "SendAsync",
        ItExpr.IsAny<HttpRequestMessage>(),
        ItExpr.IsAny<CancellationToken>()
      )
      .ReturnsAsync(new HttpResponseMessage
      {
        StatusCode = HttpStatusCode.NoContent // 204 No Content is a common response for DELETE
      });

    // Act
    var response = await _httpClientService.DeleteAsJsonAsync(requestUri);

    // Assert
    Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
  }

  // Patch Json Async retorna mensagem de resposta HTTP quando bem-sucedido
  [Fact]
  public async Task PatchAsJsonAsync_ReturnsHttpResponseMessage_WhenSuccess()
  {
    // Arrange
    var requestUri = "https://api.example.com/resource";
    var content = new { Data = "Sample" };
    _mockHandler
      .Protected()
      .Setup<Task<HttpResponseMessage>>(
        "SendAsync",
        ItExpr.IsAny<HttpRequestMessage>(),
        ItExpr.IsAny<CancellationToken>()
      )
      .ReturnsAsync(new HttpResponseMessage
      {
        StatusCode = HttpStatusCode.OK
      });

    // Act
    var response = await _httpClientService.PatchAsJsonAsync(requestUri, content);

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
  }

  // Patch Json Async retorna pessoa da mensagem de resposta HTTP quando bem sucedido
  [Fact]
  public async Task PatchAsJsonAsync_ReturnsHttpResponseMessagePerson_WhenSuccess()
  {
    // Arrange
    var requestUri = "https://api.example.com/resource";
    var content = new Person("Teste", 20);
    var mockResponseContent = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
    _mockHandler
      .Protected()
      .Setup<Task<HttpResponseMessage>>(
        "SendAsync",
        ItExpr.IsAny<HttpRequestMessage>(),
        ItExpr.IsAny<CancellationToken>()
      )
      .ReturnsAsync(new HttpResponseMessage
      {
        StatusCode = HttpStatusCode.OK,
        Content = mockResponseContent
      });

    // Act
    var response = await _httpClientService.PatchAsJsonAsync(requestUri, content);

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
  }
}

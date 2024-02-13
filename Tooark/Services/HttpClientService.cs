using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Tooark.Exceptions;

namespace Tooark.Services;

/// <summary>
/// Fornece métodos para enviar solicitações HTTP e receber respostas HTTP de um recurso identificado por um URI.
/// </summary>
public class HttpClientService
{
  /// <summary>
  /// Inicializa uma nova instância da classe HttpClientService com um HttpClient específico.
  /// </summary>
  private readonly HttpClient _httpClient;

  /// <summary>
  /// Inicializa construtor da class.
  /// </summary>
  /// <param name="httpClient">A instância HttpClient a ser usada para enviar solicitações.</param>
  public HttpClientService(HttpClient httpClient)
  {
    _httpClient = httpClient;
  }

  /// <summary>
  /// Configura os cabeçalhos HTTP para a instância HttpClient.
  /// </summary>
  /// <param name="mediaType">O tipo de mídia (MediaType) para o cabeçalho 'Accept'. O padrão é 'aplicativo/json'.</param>
  /// <param name="bearerToken">O bearer token para o cabeçalho 'Authorization'. Opcional.</param>
  /// <param name="language">O idioma do cabeçalho 'Accept-Language'. Opcional.</param>
  /// <param name="cookie">O cookie para o cabeçalho 'Cookie'. Opcional.</param>
  /// <remarks>
  /// Este método configura os cabeçalhos comuns que são enviados com cada solicitação HTTP.
  /// Se um valor de cabeçalho não for fornecido, esse cabeçalho não será adicionado à solicitação.
  /// </remarks>
  private void ConfigureHeaders(string mediaType = "application/json", string? bearerToken = null, string? language = null, string? cookie = null)
  {
    _httpClient.DefaultRequestHeaders.Clear();

    if (!string.IsNullOrEmpty(mediaType))
    {
      _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
    }

    if (!string.IsNullOrEmpty(bearerToken))
    {
      _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
    }

    if (!string.IsNullOrEmpty(language))
    {
      _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(language));
    }

    if (!string.IsNullOrEmpty(cookie))
    {
      _httpClient.DefaultRequestHeaders.Add("Cookie", cookie);
    }
  }

  /// <summary>
  /// Envia uma solicitação HTTP de forma assíncrona e retorna o corpo da resposta desserializado como o tipo T especificado.
  /// </summary>
  /// <typeparam name="T">O tipo do objeto para o qual desserializar o corpo da resposta.</typeparam>
  /// <param name="request">A mensagem de solicitação HTTP a ser enviada.</param>
  /// <returns>O corpo de resposta desserializado como o tipo T especificado.</returns>
  /// <exception cref="JsonDeserializationException">Lançado quando ocorre um erro durante a desserialização da resposta JSON.</exception>
  /// <exception cref="HttpRequestFailedException">Lançado quando a solicitação HTTP falha.</exception>
  private async Task<T> SendAsync<T>(HttpRequestMessage request)
  {
    try
    {
      var response = await _httpClient.SendAsync(request);
      string errorContent;

      if (response.IsSuccessStatusCode)
      {
        if (typeof(T) == typeof(HttpResponseMessage))
        {
          return (T)(object)response;
        }
      
        var result = await response.Content.ReadFromJsonAsync<T>();

        if (result != null)
        {
          return result;
        }
        else
        {
          throw new HttpRequestException($"Response empty with status code {response.StatusCode}.");
        }
      }

      errorContent = await response.Content.ReadAsStringAsync();
      throw new HttpRequestException($"Request failed with status code {response.StatusCode} and content: {errorContent}");
    }
    catch (JsonException jsonEx)
    {
      throw new JsonDeserializationException("An error occurred while deserializing the JSON response.", jsonEx);
    }
    catch (HttpRequestException httpEx)
    {
      throw new HttpRequestFailedException(httpEx.StatusCode, "An error occurred while sending the HTTP request.");
    }
  }

  /// <summary>
  /// Envia uma solicitação GET para o URI especificado e retorna o corpo da resposta desserializado como o tipo T especificado.
  /// </summary>
  /// <typeparam name="T">O tipo do objeto para o qual desserializar o corpo da resposta.</typeparam>
  /// <param name="requestUri">O URI para o qual a solicitação é enviada.</param>
  /// <param name="bearerToken">O bearer token para Authorization.</param>
  /// <param name="language">O idioma do cabeçalho Accept-Language.</param>
  /// <param name="cookie">O cookie para o cabeçalho Cookie.</param>
  /// <param name="parameters">Parâmetros adicionais para a string de consulta.</param>
  /// <returns>O corpo de resposta desserializado como o tipo T especificado.</returns>
  public async Task<T?> GetFromJsonAsync<T>(string requestUri, string? bearerToken = null, string? language = null, string? cookie = null, params KeyValuePair<string, string>[] parameters)
  {
    ConfigureHeaders(bearerToken: bearerToken, language: language, cookie: cookie);

    if (parameters.Length > 0)
    {
      using var content = new FormUrlEncodedContent(parameters);
      string queryParams = await content.ReadAsStringAsync();

      requestUri = $"{requestUri}?{queryParams}";
    }

    var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

    return await SendAsync<T>(request);
  }

  /// <summary>
  /// Envia uma solicitação POST para o URI especificado com o conteúdo fornecido e retorna a mensagem de resposta HTTP.
  /// </summary>
  /// <typeparam name="T">O tipo de conteúdo a ser serializado no corpo da solicitação.</typeparam>
  /// <param name="requestUri">O URI para o qual a solicitação é enviada.</param>
  /// <param name="content">O conteúdo a ser serializado no corpo da solicitação.</param>
  /// <param name="bearerToken">O bearer token para Authorization.</param>
  /// <param name="language">O idioma do cabeçalho Accept-Language.</param>
  /// <param name="cookie">O cookie para o cabeçalho Cookie.</param>
  /// <returns>A mensagem de resposta HTTP.</returns>
  public async Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T content, string? bearerToken = null, string? language = null, string? cookie = null)
  {
    ConfigureHeaders(bearerToken: bearerToken, language: language, cookie: cookie);

    var request = new HttpRequestMessage(HttpMethod.Post, requestUri)
    {
      Content = JsonContent.Create(content)
    };

    return await SendAsync<HttpResponseMessage>(request);
  }

  /// <summary>
  /// Envia uma solicitação PUT para o URI especificado com o conteúdo fornecido e retorna a mensagem de resposta HTTP.
  /// </summary>
  /// <typeparam name="T">O tipo de conteúdo a ser serializado no corpo da solicitação.</typeparam>
  /// <param name="requestUri">O URI para o qual a solicitação é enviada.</param>
  /// <param name="content">O conteúdo a ser serializado no corpo da solicitação.</param>
  /// <param name="bearerToken">O bearer token para Authorization.</param>
  /// <param name="language">O idioma do cabeçalho Accept-Language.</param>
  /// <param name="cookie">O cookie para o cabeçalho Cookie.</param>
  /// <returns>A mensagem de resposta HTTP.</returns>
  public async Task<HttpResponseMessage> PutAsJsonAsync<T>(string requestUri, T content, string? bearerToken = null, string? language = null, string? cookie = null)
  {
    ConfigureHeaders(bearerToken: bearerToken, language: language, cookie: cookie);

    var request = new HttpRequestMessage(HttpMethod.Put, requestUri)
    {
      Content = JsonContent.Create(content)
    };

    return await SendAsync<HttpResponseMessage>(request);
  }

  /// <summary>
  /// Envia uma solicitação DELETE para o URI especificado e retorna a mensagem de resposta HTTP.
  /// </summary>
  /// <param name="requestUri">O URI para o qual a solicitação é enviada.</param>
  /// <param name="bearerToken">O bearer token para Authorization.</param>
  /// <param name="language">O idioma do cabeçalho Accept-Language.</param>
  /// <param name="cookie">O cookie para o cabeçalho Cookie.</param>
  /// <returns>A mensagem de resposta HTTP.</returns>
  public async Task<HttpResponseMessage> DeleteAsJsonAsync(string requestUri, string? bearerToken = null, string? language = null, string? cookie = null)
  {
    ConfigureHeaders(bearerToken: bearerToken, language: language, cookie: cookie);

    var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);

    return await SendAsync<HttpResponseMessage>(request);
  }

  /// <summary>
  /// Envia uma solicitação PATCH para o URI especificado com o conteúdo fornecido e retorna a mensagem de resposta HTTP.
  /// </summary>
  /// <typeparam name="T">O tipo de conteúdo a ser serializado no corpo da solicitação.</typeparam>
  /// <param name="requestUri">O URI para o qual a solicitação é enviada.</param>
  /// <param name="content">O conteúdo a ser serializado no corpo da solicitação.</param>
  /// <param name="bearerToken">O bearer token para Authorization.</param>
  /// <param name="language">O idioma do cabeçalho Accept-Language.</param>
  /// <param name="cookie">O cookie para o cabeçalho Cookie.</param>
  /// <returns>A mensagem de resposta HTTP.</returns>
  public async Task<HttpResponseMessage> PatchAsJsonAsync<T>(string requestUri, T content, string? bearerToken = null, string? language = null, string? cookie = null)
  {
    ConfigureHeaders(bearerToken: bearerToken, language: language, cookie: cookie);

    var request = new HttpRequestMessage(HttpMethod.Patch, requestUri)
    {
      Content = JsonContent.Create(content)
    };

    return await SendAsync<HttpResponseMessage>(request);
  }
}

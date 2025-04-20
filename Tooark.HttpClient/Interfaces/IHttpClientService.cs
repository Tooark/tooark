namespace Tooark.Interfaces;

/// <summary>
/// Define uma interface para serviços de cliente HTTP para enviar solicitações HTTP e receber respostas HTTP de um recurso identificado por um URI.
/// </summary>
public interface IHttpClientService
{
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
  Task<T?> GetFromJsonAsync<T>(string requestUri, string? bearerToken = null, string? language = null, string? cookie = null, params KeyValuePair<string, string>[] parameters);

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
  Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T content, string? bearerToken = null, string? language = null, string? cookie = null);

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
  Task<HttpResponseMessage> PutAsJsonAsync<T>(string requestUri, T content, string? bearerToken = null, string? language = null, string? cookie = null);

  /// <summary>
  /// Envia uma solicitação DELETE para o URI especificado e retorna a mensagem de resposta HTTP.
  /// </summary>
  /// <param name="requestUri">O URI para o qual a solicitação é enviada.</param>
  /// <param name="bearerToken">O bearer token para Authorization.</param>
  /// <param name="language">O idioma do cabeçalho Accept-Language.</param>
  /// <param name="cookie">O cookie para o cabeçalho Cookie.</param>
  /// <returns>A mensagem de resposta HTTP.</returns>
  Task<HttpResponseMessage> DeleteAsJsonAsync(string requestUri, string? bearerToken = null, string? language = null, string? cookie = null);

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
  Task<HttpResponseMessage> PatchAsJsonAsync<T>(string requestUri, T content, string? bearerToken = null, string? language = null, string? cookie = null);
}

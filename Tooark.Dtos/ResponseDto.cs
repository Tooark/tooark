using Microsoft.AspNetCore.Http;
using Tooark.Notifications;

namespace Tooark.Dtos;

/// <summary>
/// Classe de resposta padrão.
/// </summary>
/// <typeparam name="T">Tipo de dado de resposta.</typeparam>
/// <remarks>
/// A classe <see cref="ResponseDto{T}"/> é uma classe de resposta padrão para operações de API.
/// Fornece um objeto de resposta com dados, erros, metadados e informações de paginação.
/// </remarks>
public class ResponseDto<T> : Dto
{
  /// <summary>
  /// Dados de resposta.
  /// </summary>
  public T? Data { get; private set; }

  /// <summary>
  /// Lista de erros.
  /// </summary>
  public IList<string> Errors { get; private set; } = [];

  /// <summary>
  /// Dados de paginação.
  /// </summary>
  public PaginationDto? Pagination { get; private set; }

  /// <summary>
  /// Metadados.
  /// </summary>
  public IList<MetadataDto> Metadata { get; private set; } = [];

  /// <summary>
  /// Construtor com dados de resposta.
  /// </summary>
  /// <typeparam name="T">Tipo de dado de resposta.</typeparam>
  /// <param name="data">Dados de resposta.</param>
  public ResponseDto(T? data)
  {
    // Verifica se os dados são uma notificação
    if (data is Notification notification && !notification.IsValid)
    {
      // Atribui as mensagens de erro com as strings localizadas correspondentes caso existam
      Errors = [.. notification.Messages.Select(x => _localizer[x])];
    }
    else
    {
      // Atribui a resposta
      Data = data;
    }
  }

  /// <summary>
  /// Construtor padrão.
  /// </summary>
  /// <typeparam name="T">Tipo de dado de resposta.</typeparam>
  /// <param name="data">Dados de resposta.</param>
  /// <param name="errors">Lista de erros.</param>
  public ResponseDto(T data, IList<string> errors)
  {
    // Atribui os valores base
    Data = data;
    Errors = errors;
  }

  /// <summary>
  /// Construtor com dados de resposta.
  /// </summary>
  /// <typeparam name="T">Tipo de dado de resposta.</typeparam>
  /// <param name="data">Dados de resposta.</param>
  /// <param name="total">Total de registros.</param>
  /// <param name="request">Requisição.</param>
  public ResponseDto(T? data, int total, HttpRequest request)
  {
    // Atribui a resposta
    Data = data;

    // Atribui os dados de paginação
    Pagination = new PaginationDto(total, request);
  }

  /// <summary>
  /// Construtor com erro.
  /// </summary>
  /// <param name="error">Mensagem de erro.</param>
  public ResponseDto(string error)
  {
    // Atribui o erro com a string localizada correspondente
    Errors.Add(_localizer[error]);
  }

  /// <summary>
  /// Construtor com lista de erros.
  /// </summary>
  /// <param name="erros">Lista de erros.</param>
  public ResponseDto(IList<string> errors)
  {
    // Atribui os erros com as strings localizadas correspondentes
    Errors = [.. errors.Select(x => _localizer[x])];
  }

  /// <summary>
  /// Construtor com exceção.
  /// </summary>
  /// <param name="exception">Exceção.</param>
  public ResponseDto(Exception exception)
  {
    // Atribui o erro da exceção com a string localizada correspondente
    Errors.Add(_localizer[exception.Message]);
  }

  /// <summary>
  /// Construtor com mensagem e validador de sucesso.
  /// </summary>
  /// <param name="message">Mensagem de resposta.</param>
  /// <param name="isSuccess">Validador de sucesso.</param>
  public ResponseDto(string message, bool isSuccess)
  {
    // Verifica se a operação foi bem sucedida
    if (isSuccess)
    {
      // Atribui a mensagem a resposta
      Data = (T)Convert.ChangeType(message, typeof(T));
    }
    else
    {
      // Atribui o erro com a string localizada correspondente
      Errors.Add(_localizer[message]);
    }
  }

  /// <summary>
  /// Construtor com lista de itens de notificação.
  /// </summary>
  /// <param name="notifications">Lista de itens de notificação.</param>
  public ResponseDto(IReadOnlyCollection<NotificationItem> notifications)
  {
    // Atribui as mensagens de erro com as strings localizadas correspondentes
    Errors = [.. notifications.Select(x => _localizer[x])];
  }


  /// <summary>
  /// Adiciona dados de paginação.
  /// </summary>
  /// <param name="pagination">Objeto de paginação.</param>
  public void SetPagination(PaginationDto pagination)
  {
    // Atribui os dados de paginação
    Pagination = pagination;
  }

  /// <summary>
  /// Adiciona metadados.
  /// </summary>
  /// <param name="metadata">Lista de metadados.</param>
  public void SetMetadata(IList<MetadataDto> metadata)
  {
    // Atribui os metadados
    Metadata = metadata;
  }

  /// <summary>
  /// Adiciona um metadado.
  /// </summary>
  /// <param name="metadata">Metadado.</param>
  public void AddMetadata(MetadataDto metadata)
  {
    // Adiciona um metadado
    Metadata.Add(metadata);
  }
}

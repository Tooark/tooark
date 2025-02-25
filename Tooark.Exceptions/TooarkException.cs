using System.Net;

namespace Tooark.Exceptions;

/// <summary>
/// Classe base para exceções do Tooark.
/// </summary>
public abstract class TooarkException : Exception
{
  /// <summary>
  /// Construtor padrão.
  /// </summary>
  /// <param name="message">Mensagem de erro.</param>
  public TooarkException(string message) : base(message) { }

  /// <summary>
  /// Construtor que aceita uma exceção interna.
  /// </summary>
  /// <param name="message">Mensagem de erro.</param>
  /// <param name="innerException">Exceção interna.</param>
  public TooarkException(string message, Exception innerException) : base(message, innerException) { }


  /// <summary>
  /// Função abstrata para obter as mensagens de erro.
  /// </summary>
  /// <returns>Lista de mensagens de erro.</returns>
  public abstract List<string> GetErrorMessages();

  /// <summary>
  /// Função abstrata para obter o status code da exceção.
  /// </summary>
  /// <returns>Status code da exceção.</returns>
  public abstract HttpStatusCode GetStatusCode();
}

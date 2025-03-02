using System.Net;

namespace Tooark.Exceptions;

/// <summary>
/// Classe base para exceções do Tooark.
/// </summary>
public abstract class TooarkException : Exception
{
  /// <summary>
  /// Lista privada de mensagens de erro.
  /// </summary>
  private readonly IList<string> _errors;


  /// <summary>
  /// Construtor da classe.
  /// </summary>
  /// <param name="message">Mensagem de erro.</param>
  public TooarkException(string message) : base(message)
  {
    // Inicializa a lista de mensagens de erro.
    _errors = [message];
  }

  /// <summary>
  /// Construtor da classe.
  /// </summary>
  /// <param name="errors">Lista de mensagens de erro.</param>
  public TooarkException(IList<string> errors) : base(errors.FirstOrDefault())
  {
    // Inicializa a lista de mensagens de erro.
    _errors = errors;
  }


  /// <summary>
  /// Função abstrata para obter as mensagens de erro.
  /// </summary>
  /// <returns>Lista de mensagens de erro.</returns>
  public virtual IList<string> GetErrorMessages() => _errors;

  /// <summary>
  /// Função abstrata para obter o status code da exceção.
  /// </summary>
  /// <returns>Status code da exceção.</returns>
  public abstract HttpStatusCode GetStatusCode();
}

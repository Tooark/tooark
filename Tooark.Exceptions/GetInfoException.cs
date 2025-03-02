using System.Net;

namespace Tooark.Exceptions;

/// <summary>
/// Classe para exceções para métodos buscar campo name, title e description.
/// </summary>
/// <remarks>
/// Construtor padrão.
/// </remarks>
/// <param name="message">Mensagem de erro.</param>
public class GetInfoException(string message) : TooarkException(message)
{
  /// <summary>
  /// Função virtual para obter o status code da exceção.
  /// </summary>
  /// <returns>Status code da exceção. Padronizado para 400 (BadRequest).</returns>
  public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}

using Tooark.Exceptions;
using Tooark.Extensions;

namespace Tooark.ValueObjects;

/// <summary>
/// Representa um documento.
/// </summary>
public class Document(string number, string? type = null)
{
  /// <summary>
  /// Valor privado do número do documento.
  /// </summary>
  private readonly string _number = number ?? throw AppException.BadRequest("Field.Required;Number");

  /// <summary>
  /// Valor privado do tipo do documento.
  /// </summary>
  private readonly string? _type = !string.IsNullOrEmpty(type) ? type.ToNormalize() : type;

  /// <summary>
  /// Valor do número do documento.
  /// </summary>
  public string Number { get => _number; }

  /// <summary>
  /// Valor do tipo do documento.
  /// </summary>
  public string? Type { get => _type; }


  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar o número do documento.
  /// </summary>
  /// <returns>Uma string que representa o número do documento.</returns>
  public override string ToString() => _number;

  /// <summary>
  /// Conversão implícita de <see cref="Document"/> para <see cref="string"/>.
  /// </summary>
  /// <param name="document">Instância de <see cref="Document"/>.</param>
  /// <returns>O número do documento.</returns>
  public static implicit operator string(Document document) => document._number;

  /// <summary>
  /// Conversão implícita de <see cref="string"/> para <see cref="Document"/>.
  /// </summary>
  /// <param name="value">Número do documento.</param>
  /// <returns>Uma instância de <see cref="Document"/>.</returns>
  public static implicit operator Document(string value) => new(value);
}

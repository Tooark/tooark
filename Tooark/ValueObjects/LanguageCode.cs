using Tooark.Exceptions;

namespace Tooark.ValueObjects;

/// <summary>
/// Classe quq representa o código de um idioma.
/// </summary>
public class LanguageCode : ValueObject
{
  /// <summary>
  /// Código do idioma interno para leitura e escrita.
  /// </summary>
  private readonly string _code = null!;

  /// <summary>
  /// Código do idioma.
  /// </summary>
  public string Code { get => _code; }


  /// <summary>
  /// Construtor para a classe LanguageCode.
  /// </summary>
  /// <param name="code">Código do idioma.</param>
  /// <exception cref="AppException.BadRequest">Lançado quando o código do idioma não tem 5 caracteres.</exception>
  /// <exception cref="AppException.BadRequest">Lançado quando o código do idioma tem '-' no meio.</exception>
  public LanguageCode(string code)
  {
    // Verifica se o código do idioma tem 5 caracteres
    if (code.Length != 5)
    {
      throw AppException.BadRequest("LanguageCode.ErrorLength;5");
    }

    // Verifica se o código do idioma tem '-' no meio
    if (code[2] != '-')
    {
      throw AppException.BadRequest("LanguageCode.ErrorFormat");
    }

    // Padroniza o código do idioma
    _code = code[..2].ToLowerInvariant() + "-" + code[3..].ToUpperInvariant();
  }


  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar o valor do languageCode.
  /// </summary>
  /// <returns>O valor do languageCode.</returns>
  public override string ToString() => _code;

  /// <summary>
  /// Converte um languageCode para string.
  /// </summary>
  /// <param name="languageCode">Instância de <see cref="LanguageCode"/>.</param>
  /// <returns>Retorna o valor do languageCode.</returns>
  public static implicit operator string(LanguageCode languageCode) => languageCode._code;

  /// <summary>
  /// Converte uma string para languageCode.
  /// </summary>
  /// <param name="languageCode">Valor do languageCode.</param>
  /// <returns>Retorna uma instância de <see cref="LanguageCode"/>.</returns>
  public static implicit operator LanguageCode(string languageCode) => new(languageCode);
}

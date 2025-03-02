using System.Text.RegularExpressions;
using Tooark.Validations;
using Tooark.Validations.Patterns;

namespace Tooark.ValueObjects;

/// <summary>
/// Representa um link de vídeo. Plataformas: YouTube, Vimeo e Dailymotion.
/// </summary>
public sealed class LinkVideo : ValueObject
{
  /// <summary>
  /// O link privado do vídeo.
  /// </summary>
  private readonly string _link = null!;


  /// <summary>
  /// Inicializa uma nova instância da classe LinkVideo com os parâmetros especificados.
  /// </summary>
  /// <param name="link">O valor do link a ser validado.</param>
  /// <param name="youtube">Validar link do YouTube. Padrão: true.</param>
  /// <param name="vimeo">Validar link do Vimeo. Padrão: true.</param>
  /// <param name="dailymotion">Validar link do Dailymotion. Padrão: true.</param>
  public LinkVideo(string link, bool youtube = true, bool vimeo = true, bool dailymotion = true)
  {
    // Método de validação de expressão regular.
    static bool RegexValidation(string link, string pattern) => Regex.IsMatch(link, pattern, RegexOptions.None, TimeSpan.FromMilliseconds(300));

    // Verifica se o link é válido.
    bool linkIsValid =
      (youtube && RegexValidation(link, RegexPattern.YouTube)) ||
      (vimeo && RegexValidation(link, RegexPattern.Vimeo)) ||
      (dailymotion && RegexValidation(link, RegexPattern.Dailymotion));

    // Verifica se o link é válido.
    if (linkIsValid)
    {
      // Define o valor do link
      _link = link;
    }
    else
    {
      // Adiciona as notificação de validação do link
      AddNotification("Field.Invalid;LinkVideo", "LinkVideo", "T.VOJ.LVI1");
    }
  }


  /// <summary>
  /// Obtém o link do vídeo.
  /// </summary>
  public string Link { get => _link; }


  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar o valor do link.
  /// </summary>
  /// <returns>O valor do link.</returns>
  public override string ToString() => _link;

  /// <summary>
  /// Define uma conversão implícita de um objeto LinkVideo para uma string.
  /// </summary>
  /// <param name="linkVideo">O objeto LinkVideo a ser convertido.</param>
  /// <returns>Uma string que representa o valor do link.</returns>
  public static implicit operator string(LinkVideo linkVideo) => linkVideo._link;

  /// <summary>
  /// Define uma conversão implícita de uma string para um objeto LinkVideo.
  /// </summary>
  /// <param name="link">A string a ser convertida em um objeto LinkVideo.</param>
  /// <returns>O objeto LinkVideo criado a partir da string fornecida.</returns>
  public static implicit operator LinkVideo(string link) => new(link);
}

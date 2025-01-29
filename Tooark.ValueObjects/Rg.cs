using Tooark.Enums;

namespace Tooark.ValueObjects;

/// <summary>
/// Representa um RG.
/// </summary>
public sealed class Rg : ValueObject
{
  /// <summary>
  /// Valor privado do número do RG.
  /// </summary>
  private readonly string _number = null!;


  /// <summary>
  /// Inicializa uma nova instância da classe Rg com o número.
  /// </summary>
  /// <param name="number">O número do RG a ser validado.</param>
  public Rg(string number)
  {
    // Valida documento do tipo RG
    string value = new Document(number, EDocumentType.RG);

    // Verifica se é válido, então não existe notificação
    if (value != null)
    {
      // Define o valor do número da RG
      _number = value;
    }
    else
    {
      // Adiciona uma notificação de validação do RG
      AddNotification("Rg", "Field.Invalid;Rg");
    }
  }


  /// <summary>
  /// Valor do número do RG.
  /// </summary>
  public string Number { get => _number; }


  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar o valor do RG.
  /// </summary>
  /// <returns>O valor do RG.</returns>
  public override string ToString() => _number;

  /// <summary>
  /// Define uma conversão implícita de um objeto Rg para uma string.
  /// </summary>
  /// <param name="document">O objeto Rg a ser convertido.</param>
  /// <returns>Uma string que representa o valor do RG.</returns>
  public static implicit operator string(Rg document) => document._number;

  /// <summary>
  /// Define uma conversão implícita de uma string para um objeto Rg.
  /// </summary>
  /// <param name="value">A string a ser convertida em um objeto Rg.</param>
  /// <returns>Um objeto Rg criado a partir da string fornecida.</returns>
  public static implicit operator Rg(string value) => new(value);
}

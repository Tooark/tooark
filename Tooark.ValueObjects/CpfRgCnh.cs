using Tooark.Enums;

namespace Tooark.ValueObjects;

/// <summary>
/// Representa um CPF, RG ou CNH.
/// </summary>
public sealed class CpfRgCnh : ValueObject
{
  /// <summary>
  /// Valor privado do número do CPF, RG ou CNH.
  /// </summary>
  private readonly string _number = null!;


  /// <summary>
  /// Inicializa uma nova instância da classe CpfRgCnh com o número.
  /// </summary>
  /// <param name="number">O número do CPF, RG ou CNH a ser validado.</param>
  public CpfRgCnh(string number)
  {
    // Valida documento do tipo CPF, RG ou CNH
    string value = new Document(number, EDocumentType.CPF_RG_CNH);

    // Verifica se é válido, então não existe notificação
    if (value != null)
    {
      // Define o valor do número da CPF, RG ou CNH
      _number = value;
    }
    else
    {
      // Adiciona uma notificação de validação do CPF, RG ou CNH
      AddNotification("CpfRgCnh", "Field.Invalid;CpfRgCnh");
    }
  }


  /// <summary>
  /// Valor do número do CPF, RG ou CNH.
  /// </summary>
  public string Number { get => _number; }


  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar o valor do CPF, RG ou CNH.
  /// </summary>
  /// <returns>O valor do CPF, RG ou CNH.</returns>
  public override string ToString() => _number;

  /// <summary>
  /// Define uma conversão implícita de um objeto CpfRgCnh para uma string.
  /// </summary>
  /// <param name="document">O objeto CpfRgCnh a ser convertido.</param>
  /// <returns>Uma string que representa o valor do CPF, RG ou CNH.</returns>
  public static implicit operator string(CpfRgCnh document) => document._number;

  /// <summary>
  /// Define uma conversão implícita de uma string para um objeto CpfRgCnh.
  /// </summary>
  /// <param name="value">A string a ser convertida em um objeto CpfRgCnh.</param>
  /// <returns>Um objeto CpfRgCnh criado a partir da string fornecida.</returns>
  public static implicit operator CpfRgCnh(string value) => new(value);
}

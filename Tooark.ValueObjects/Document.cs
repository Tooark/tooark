using System.Text.RegularExpressions;
using Tooark.Validations;

namespace Tooark.ValueObjects;

/// <summary>
/// Representa um documento.
/// </summary>
public sealed class Document : ValueObject
{
  /// <summary>
  /// Valor privado do número do documento.
  /// </summary>
  private readonly string _number = null!;

  /// <summary>
  /// Valor privado do tipo do documento.
  /// </summary>
  private readonly DocumentType _type = null!;


  /// <summary>
  /// Inicializa uma nova instância da classe Document com o número e o tipo especificados.
  /// </summary>
  /// <param name="number">O número do documento a ser validado.</param>
  /// <param name="type">O tipo do documento a ser validado. Parâmetro opcional.</param>
  public Document(string number, DocumentType? type = null)
  {
    // Define o tipo do documento como "None" se não for informado
    type ??= DocumentType.None;

    // Adiciona as notificações de validação do documento
    AddNotifications(new Contract()
      .Match(number, MountRegex(type), "Document", "Field.Invalid;Document")
    );

    // Verifica é valido então não existe notificação
    if (IsValid)
    {
      // Verifica se o número do Documento é válido
      if (Validate(number, type))
      {
        // Define o valor do número do documento e o tipo do documento
        _number = number;
        _type = type!;
      }
      else
      {
        // Adiciona uma notificação de validação do Documento
        AddNotification("Document.Number", "Field.Invalid;Document.Number");
      }
    }
  }


  /// <summary>
  /// Valor do número do documento.
  /// </summary>
  public string Number { get => _number; }

  /// <summary>
  /// Valor do tipo do documento.
  /// </summary>
  public DocumentType Type { get => _type; }


  /// <summary>
  /// Valida um número de CNH, CNPJ, CPF ou RG.
  /// </summary>
  /// <param name="value">O número do CNH, CNPJ, CPF ou RG a ser validado.</param>
  /// <param name="type">O tipo do documento a ser validado.</param>
  /// <returns>True se o número do CNH, CNPJ, CPF ou RG for válido</returns>
  internal static bool Validate(string value, DocumentType type)
  {
    // Remove os caracteres especiais do CNH, CNPJ, CPF ou RG
    value = value.Trim().Replace(".", "").Replace("/", "").Replace("-", "");

    // Verifica se o valor é maior que 0
    if (long.Parse(value[..8]) > 0)
    {
      // Verifica se o valor é um CNH, CNPJ, CPF ou RG
      return type.ToString() switch
      {
        "None" => Regex.IsMatch(value, type.ToRegex(), RegexOptions.None, TimeSpan.FromMilliseconds(300)),
        "CPF" => Cpf.Validate(value),
        "RG" => Rg.Validate(value),
        "CNH" => Cnh.Validate(value),
        "CNPJ" => Cnpj.Validate(value),
        "CPF_CNPJ" => CpfCnpj.Validate(value),
        "CPF_RG" => CpfRg.Validate(value),
        "CPF_RG_CNH" => CpfRgCnh.Validate(value),
        _ => true,
      };
    }
    else
    {
      // Retorna falso se não for um CNH, CNPJ, CPF ou RG
      return false;
    }
  }

  /// <summary>
  /// Monta a regex do tipo de documento.
  /// </summary>
  /// <param name="value">O tipo de documento.</param>
  /// <returns>A regex do tipo de documento.</returns>
  private static string MountRegex(DocumentType value) => value.ToRegex();

  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar o valor do documento.
  /// </summary>
  /// <returns>O valor do documento.</returns>
  public override string ToString() => _number;

  /// <summary>
  /// Define uma conversão implícita de um objeto Document para uma string.
  /// </summary>
  /// <param name="document">O objeto Document a ser convertido.</param>
  /// <returns>Uma string que representa o valor do documento.</returns>
  public static implicit operator string(Document document) => document._number;

  /// <summary>
  /// Define uma conversão implícita de uma string para um objeto Document.
  /// </summary>
  /// <param name="value">A string a ser convertida em um objeto Document.</param>
  /// <returns>Um objeto Document criado a partir da string fornecida.</returns>
  public static implicit operator Document(string value) => new(value);
}

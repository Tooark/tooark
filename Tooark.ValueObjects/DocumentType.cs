using Tooark.Validations.Patterns;

namespace Tooark.ValueObjects;

/// <summary>
/// Representa um tipo de documento.
/// </summary>
public sealed class DocumentType
{
  /// <summary>
  /// Documento do tipo "None".
  /// </summary>
  public static readonly DocumentType None = new(0, "None", @"^[a-zA-Z0-9.-]*$");

  /// <summary>
  /// Documento do tipo "CPF".
  /// </summary>
  public static readonly DocumentType CPF = new(1, "CPF", RegexPattern.Cpf);

  /// <summary>
  /// Documento do tipo "RG".
  /// </summary>
  public static readonly DocumentType RG = new(2, "RG", RegexPattern.Rg);

  /// <summary>
  /// Documento do tipo "CNH".
  /// </summary>
  public static readonly DocumentType CNH = new(3, "CNH", RegexPattern.Cnh);

  /// <summary>
  /// Documento do tipo "CNPJ".
  /// </summary>
  public static readonly DocumentType CNPJ = new(4, "CNPJ", RegexPattern.Cnpj);

  /// <summary>
  /// Documento do tipo "CPF" ou "CNPJ".
  /// </summary>
  public static readonly DocumentType CPF_CNPJ = new(5, "CPF_CNPJ", RegexPattern.CpfCnpj);

  /// <summary>
  /// Documento do tipo "CPF" ou "RG".
  /// </summary>
  public static readonly DocumentType CPF_RG = new(6, "CPF_RG", RegexPattern.CpfRg);

  /// <summary>
  /// Documento do tipo "CPF", "RG" ou "CNH".
  /// </summary>
  public static readonly DocumentType CPF_RG_CNH = new(7, "CPF_RG_CNH", RegexPattern.CpfRgCnh);


  /// <summary>
  /// Construtor privado da classe.
  /// </summary>
  /// <param name="id">Id do tipo de documento.</param>
  /// <param name="description">Descrição do tipo de documento.</param>
  /// <param name="patternRegex">Padrão de regex do tipo de documento.</param>
  /// <returns>Uma nova instância de <see cref="DocumentType"/>.</returns>
  private DocumentType(int id, string description, string patternRegex)
  {
    Id = id;
    Description = description;
    PatternRegex = patternRegex;
  }
  

  /// <summary>
  /// Id do tipo de documento.
  /// </summary>
  private int Id { get; }

  /// <summary>
  /// Descrição do tipo de documento.
  /// </summary>
  private string Description { get; }

  /// <summary>
  /// Padrão de regex do tipo de documento.
  /// </summary>
  private string PatternRegex { get; }


  /// <summary>
  /// Função que retorna um tipo de documento a partir de sua descrição.
  /// </summary>
  /// <param name="description">Descrição do tipo de documento.</param>
  /// <returns>Uma instância de <see cref="DocumentType"/>.</returns>
  private static DocumentType FromDescription(string description) => description?.ToUpperInvariant() switch
  {
    "CPF" => CPF,
    "RG" => RG,
    "CNH" => CNH,
    "CNPJ" => CNPJ,
    "CPF_CNPJ" => CPF_CNPJ,
    "CPF_RG" => CPF_RG,
    "CPF_RG_CNH" => CPF_RG_CNH,
    _ => None
  };

  /// <summary>
  /// Função que retorna um tipo de documento a partir de seu id.
  /// </summary>
  /// <param name="id">Id do tipo de documento.</param>
  /// <returns>Uma instância de <see cref="DocumentType"/>.</returns>
  private static DocumentType FromId(int id) => id switch
  {
    1 => CPF,
    2 => RG,
    3 => CNH,
    4 => CNPJ,
    5 => CPF_CNPJ,
    6 => CPF_RG,
    7 => CPF_RG_CNH,
    _ => None
  };


  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar a descrição do tipo de documento.
  /// </summary>
  /// <returns>A descrição do tipo de documento.</returns>
  public override string ToString() => Description;

  /// <summary>
  /// Método que retorna o id do tipo de documento.
  /// </summary>
  /// <returns>O id do tipo de documento.</returns>
  public int ToInt() => Id;

  /// <summary>
  /// Método que retorna o padrão de regex do tipo de documento.
  /// </summary>
  /// <returns>O padrão de regex do tipo de documento.</returns>
  public string ToRegex() => PatternRegex;

  /// <summary>
  /// Conversão implícita de <see cref="DocumentType"/> para <see cref="int"/>.
  /// </summary>
  /// <param name="document">Instância de <see cref="DocumentType"/>.</param>
  /// <returns>Id do tipo de documento.</returns>
  public static implicit operator int(DocumentType document) => document.Id;

  /// <summary>
  /// Conversão implícita de <see cref="DocumentType"/> para <see cref="string"/>.
  /// </summary>
  /// <param name="document">Instância de <see cref="DocumentType"/>.</param>
  /// <returns>Descrição do tipo de documento.</returns>
  public static implicit operator string(DocumentType document) => document.Description;

  /// <summary>
  /// Conversão implícita de <see cref="int"/> para <see cref="DocumentType"/>.
  /// </summary>
  /// <param name="id">Id do tipo de documento.</param>
  /// <returns>Uma instância de <see cref="DocumentType"/>.</returns>
  public static implicit operator DocumentType(int id) => FromId(id);

  /// <summary>
  /// Conversão implícita de <see cref="string"/> para <see cref="DocumentType"/>.
  /// </summary>
  /// <param name="description">Descrição do tipo de documento.</param>
  /// <returns>Uma instância de <see cref="DocumentType"/>.</returns>
  public static implicit operator DocumentType(string description) => FromDescription(description);
}

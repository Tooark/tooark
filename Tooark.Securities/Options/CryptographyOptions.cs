namespace Tooark.Securities.Options;

/// <summary>
/// Classe que representa as opções de configuração para criptografia.
/// </summary>
public class CryptographyOptions
{
  #region Section

  /// <summary>
  /// Seção de configuração das opções de criptografia.
  /// </summary>
  public const string Section = "Cryptography";

  #endregion

  #region Private Properties

  /// <summary>
  /// Algoritmo de criptografia a ser utilizado.
  /// </summary>
  private string _algorithm = null!;

  #endregion

  #region Properties

  /// <summary>
  /// Algoritmo de criptografia a ser utilizado.
  /// </summary>
  /// <remarks>
  /// Valores válidos (case insensitive): 
  ///   - "CBC"  que representa o modo AES-256-CBC
  ///   - "GCM"  que representa o modo AES-256-GCM
  ///   - "CBCUnsafe" que representa o modo AES-256-CBC com IV zerado (legado)
  /// </remarks>
  public string Algorithm
  {
    get => _algorithm;
    set => _algorithm = value?.ToUpperInvariant() switch
    {
      "CBC" => "CBC",
      "GCM" => "GCM",
      "CBCUNSAFE" => "CBCZeroIv",
      _ => "GCM"
    } ?? "GCM";
  }

  /// <summary>
  /// Chave secreta para algoritmos simétricos.
  /// </summary>
  public string? Secret { get; set; } = null;

  /// <summary>
  /// Chave secreta em Base64 para algoritmos simétricos.
  /// </summary>
  /// <remarks>
  /// Se informada, será usada diretamente como chave simétrica.
  /// </remarks>
  public string? SecretBase64 { get; set; } = null;

  #endregion
}

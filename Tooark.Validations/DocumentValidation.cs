using Tooark.Validations.Messages;
using Tooark.Validations.Patterns;

namespace Tooark.Validations;

/// <summary>
/// Classe de validação de Documento.
/// </summary>
public partial class Validation
{
  #region IsCpf
  /// <summary>
  /// Verifica se o valor corresponde ao formato de CPF. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Validação.</returns>
  public Validation IsCpf(string value, string property) =>
    IsCpf(value, property, ValidationErrorMessages.IsDocument(property, ValidationErrorMessages.CpfFormatter));

  /// <summary>
  /// Verifica se o valor corresponde ao formato de CPF.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Validação.</returns>
  public Validation IsCpf(string value, string property, string message) =>
    Match(value, RegexPattern.Cpf, property, message);
  #endregion

  #region IsRg
  /// <summary>
  /// Verifica se o valor corresponde ao formato de RG. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Validação.</returns>
  public Validation IsRg(string value, string property) =>
    IsRg(value, property, ValidationErrorMessages.IsDocument(property, ValidationErrorMessages.RgFormatter));

  /// <summary>
  /// Verifica se o valor corresponde ao formato de RG.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Validação.</returns>
  public Validation IsRg(string value, string property, string message) =>
    Match(value, RegexPattern.Rg, property, message);
  #endregion

  #region IsCnh
  /// <summary>
  /// Verifica se o valor corresponde ao formato de CNH. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Validação.</returns>
  public Validation IsCnh(string value, string property) =>
    IsCnh(value, property, ValidationErrorMessages.IsDocument(property, ValidationErrorMessages.CnhFormatter));

  /// <summary>
  /// Verifica se o valor corresponde ao formato de CNH.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Validação.</returns>
  public Validation IsCnh(string value, string property, string message) =>
    Match(value, RegexPattern.Cnh, property, message);
  #endregion

  #region IsCpfRg
  /// <summary>
  /// Verifica se o valor corresponde ao formato de CPF ou RG. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Validação.</returns>
  public Validation IsCpfRg(string value, string property) =>
    IsCpfRg(value, property, ValidationErrorMessages.IsDocument(property, ValidationErrorMessages.CpfRgFormatter));

  /// <summary>
  /// Verifica se o valor corresponde ao formato de CPF ou RG.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Validação.</returns>
  public Validation IsCpfRg(string value, string property, string message) =>
    Match(value, RegexPattern.CpfRg, property, message);
  #endregion

  #region IsCpfRgCnh
  /// <summary>
  /// Verifica se o valor corresponde ao formato de CPF, RG ou CNH. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Validação.</returns>
  public Validation IsCpfRgCnh(string value, string property) =>
    IsCpfRgCnh(value, property, ValidationErrorMessages.IsDocument(property, ValidationErrorMessages.CpfRgCnhFormatter));

  /// <summary>
  /// Verifica se o valor corresponde ao formato de CPF, RG ou CNH.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Validação.</returns>
  public Validation IsCpfRgCnh(string value, string property, string message) =>
    Match(value, RegexPattern.CpfRgCnh, property, message);
  #endregion

  #region IsCnpj
  /// <summary>
  /// Verifica se o valor corresponde ao formato de CNPJ. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Validação.</returns>
  public Validation IsCnpj(string value, string property) =>
    IsCnpj(value, property, ValidationErrorMessages.IsDocument(property, ValidationErrorMessages.CnpjFormatter));

  /// <summary>
  /// Verifica se o valor corresponde ao formato de CNPJ.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Validação.</returns>
  public Validation IsCnpj(string value, string property, string message) =>
    Match(value, RegexPattern.Cnpj, property, message);
  #endregion

  #region IsCpfCnpj
  /// <summary>
  /// Verifica se o valor corresponde ao formato de CPF ou CNPJ. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Validação.</returns>
  public Validation IsCpfCnpj(string value, string property) =>
    IsCpfCnpj(value, property, ValidationErrorMessages.IsDocument(property, ValidationErrorMessages.CpfCnpjFormatter));

  /// <summary>
  /// Verifica se o valor corresponde ao formato de CPF ou CNPJ.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Validação.</returns>
  public Validation IsCpfCnpj(string value, string property, string message) =>
    Match(value, RegexPattern.CpfCnpj, property, message);
  #endregion
}

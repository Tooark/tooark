using Tooark.Validations.Messages;
using Tooark.Validations.Patterns;

namespace Tooark.Validations;

/// <summary>
/// Classe de validação de Type.
/// </summary>
public partial class Contract
{
  #region IsGuid
  /// <summary>
  /// Verifica se o valor corresponde ao formato de GUID. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsGuid(string value, string property) =>
    IsGuid(value, property, ValidationErrorMessages.IsValid(property, "Guid"));

  /// <summary>
  /// Verifica se o valor corresponde ao formato de GUID.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsGuid(string value, string property, string message) =>
    Match(value, RegexPattern.Guid, property, message);
  #endregion

  #region IsLetter
  /// <summary>
  /// Verifica se o valor corresponde ao formato de Letras. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsLetter(string value, string property) =>
    IsLetter(value, property, ValidationErrorMessages.IsValid(property, "Letter"));

  /// <summary>
  /// Verifica se o valor corresponde ao formato de Letras.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsLetter(string value, string property, string message) =>
    Match(value, RegexPattern.Letter, property, message);
  #endregion

  #region IsLetterLower
  /// <summary>
  /// Verifica se o valor corresponde ao formato de Letra Minúscula. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsLetterLower(string value, string property) =>
    IsLetterLower(value, property, ValidationErrorMessages.IsValid(property, "LetterLower"));

  /// <summary>
  /// Verifica se o valor corresponde ao formato de Letra Minúscula.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsLetterLower(string value, string property, string message) =>
    Match(value, RegexPattern.LetterLower, property, message);
  #endregion

  #region IsLetterUpper
  /// <summary>
  /// Verifica se o valor corresponde ao formato de Letra Maiúscula. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsLetterUpper(string value, string property) =>
    IsLetterUpper(value, property, ValidationErrorMessages.IsValid(property, "LetterUpper"));

  /// <summary>
  /// Verifica se o valor corresponde ao formato de Letra Maiúscula.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsLetterUpper(string value, string property, string message) =>
    Match(value, RegexPattern.LetterUpper, property, message);
  #endregion

  #region IsNumeric
  /// <summary>
  /// Verifica se o valor corresponde ao formato de Números. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsNumeric(string value, string property) =>
    IsNumeric(value, property, ValidationErrorMessages.IsValid(property, "Numeric"));

  /// <summary>
  /// Verifica se o valor corresponde ao formato de Números.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsNumeric(string value, string property, string message) =>
    Match(value, RegexPattern.Numeric, property, message);
  #endregion

  #region IsLetterNumeric
  /// <summary>
  /// Verifica se o valor corresponde ao formato de Letras e Números. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsLetterNumeric(string value, string property) =>
    IsLetterNumeric(value, property, ValidationErrorMessages.IsValid(property, "LetterNumeric"));

  /// <summary>
  /// Verifica se o valor corresponde ao formato de Letras e Números.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsLetterNumeric(string value, string property, string message) =>
    Match(value, RegexPattern.LetterNumeric, property, message);
  #endregion

  #region IsHexadecimal
  /// <summary>
  /// Verifica se o valor corresponde ao formato de Hexadecimal. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsHexadecimal(string value, string property) =>
    IsHexadecimal(value, property, ValidationErrorMessages.IsValid(property, "Hexadecimal"));

  /// <summary>
  /// Verifica se o valor corresponde ao formato de Hexadecimal.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsHexadecimal(string value, string property, string message) =>
    Match(value, RegexPattern.Hexadecimal, property, message);
  #endregion

  #region IsZipCode
  /// <summary>
  /// Verifica se o valor corresponde ao formato de Código Postal. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsZipCode(string value, string property) =>
    IsZipCode(value, property, ValidationErrorMessages.IsValid(property, "ZipCode"));

  /// <summary>
  /// Verifica se o valor corresponde ao formato de Código Postal.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsZipCode(string value, string property, string message) =>
    Match(value, RegexPattern.ZipCode, property, message);
  #endregion

  #region IsBase64
  /// <summary>
  /// Verifica se o valor corresponde ao formato de Base64. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsBase64(string value, string property) =>
    IsBase64(value, property, ValidationErrorMessages.IsValid(property, "Base64"));

  /// <summary>
  /// Verifica se o valor corresponde ao formato de Base64.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsBase64(string value, string property, string message) =>
    Match(value, RegexPattern.Base64, property, message);
  #endregion

  #region IsPassword
  /// <summary>
  /// Verifica se o valor corresponde ao formato de Senha Complexa. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsPassword(string value, string property) =>
    IsPassword(value, property, ValidationErrorMessages.IsValid(property, "Password"));

  /// <summary>
  /// Verifica se o valor corresponde ao formato de Senha Complexa.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsPassword(string value, string property, string message) =>
    Match(value, RegexPattern.Password, property, message);
  #endregion
}

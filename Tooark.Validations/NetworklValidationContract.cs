using Tooark.Validations.Messages;
using Tooark.Validations.Patterns;

namespace Tooark.Validations;

/// <summary>
/// Classe de validação de Network.
/// </summary>
public partial class Contract
{
  #region IsIp
  /// <summary>
  /// Verifica se o valor corresponde ao formato de IP. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsIp(string value, string property) =>
    IsIp(value, property, ValidationErrorMessages.IsValid(property, "Ip"));

  /// <summary>
  /// Verifica se o valor corresponde ao formato de IP.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsIp(string value, string property, string message) =>
    Match(value, RegexPattern.Ip, property, message);
  #endregion

  #region IsIpv4
  /// <summary>
  /// Verifica se o valor corresponde ao formato de Ipv4. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsIpv4(string value, string property) =>
    IsIpv4(value, property, ValidationErrorMessages.IsValid(property, "Ipv4"));

  /// <summary>
  /// Verifica se o valor corresponde ao formato de Ipv4.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsIpv4(string value, string property, string message) =>
    Match(value, RegexPattern.Ipv4, property, message);
  #endregion

  #region IsIpv6
  /// <summary>
  /// Verifica se o valor corresponde ao formato de Ipv6. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsIpv6(string value, string property) =>
    IsIpv6(value, property, ValidationErrorMessages.IsValid(property, "Ipv6"));

  /// <summary>
  /// Verifica se o valor corresponde ao formato de Ipv6.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsIpv6(string value, string property, string message) =>
    Match(value, RegexPattern.Ipv6, property, message);
  #endregion

  #region IsMacAddress
  /// <summary>
  /// Verifica se o valor corresponde ao formato de Mac Address. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsMacAddress(string value, string property) =>
    IsMacAddress(value, property, ValidationErrorMessages.IsValid(property, "MacAddress"));

  /// <summary>
  /// Verifica se o valor corresponde ao formato de Mac Address.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsMacAddress(string value, string property, string message) =>
    Match(value, RegexPattern.MacAddress, property, message);
  #endregion
}

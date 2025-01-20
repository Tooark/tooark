﻿using System.Text.RegularExpressions;
using Tooark.Validations.Messages;

namespace Tooark.Validations;

/// <summary>
/// Classe de validação de Regex.
/// </summary>
public partial class Contract
{
  #region Validates
  /// <summary>
  /// Função para validar condição de correspondência.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="pattern">Padrão a ser comparado.</param>
  /// <param name="options">Opções de regex para Case Sensitive.</param>
  /// <param name="timeout">Tempo limite para validação. Em milissegundos.</param>
  /// <returns>Função de validação.</returns>
  private readonly Func<string, string, RegexOptions, int, bool> MatchFunc = (value, pattern, options, timeout) =>
    Regex.IsMatch(value ?? "", pattern, options, TimeSpan.FromMilliseconds(timeout));

  /// <summary>
  /// Função para validar condição.
  /// </summary>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <param name="condition">Condição a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  private Contract Validate(string property, string message, Func<bool> condition)
  {
    // Se a condição for verdadeira, adicione a notificação.
    if (condition())
    {
      // Adiciona a notificação.
      AddNotification(message, property);
    }

    // Retorna o contrato de validação.
    return this;
  }
  #endregion


  #region Match
  /// <summary>
  /// Verifica se o valor corresponde ao padrão. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="pattern">Padrão a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract Match(string value, string pattern, string property) =>
    Match(value, pattern, property, ValidationErrorMessages.Match(property, value));

  /// <summary>
  /// Verifica se o valor corresponde ao padrão. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="pattern">Padrão a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="options">Opções de regex para Case Sensitive.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract Match(string value, string pattern, string property, RegexOptions options) =>
    Match(value, pattern, property, ValidationErrorMessages.Match(property, value), options);

  /// <summary>
  /// Verifica se o valor corresponde ao padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="pattern">Padrão a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <param name="options">Opções de regex para Case Sensitive. Parâmetro opcional. Padrão RegexOptions.None.</param>
  /// <param name="timeout">Tempo limite para validação. Em milissegundos. Parâmetro opcional. Padrão 300.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract Match(
    string value,
    string pattern,
    string property,
    string message,
    RegexOptions options = RegexOptions.None,
    int timeout = 300
  ) => Validate(property, message, () => !MatchFunc(value, pattern, options, timeout));
  #endregion

  #region NotMatch
  /// <summary>
  /// Verifica se o valor não corresponde ao padrão. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="pattern">Padrão a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract NotMatch(string value, string pattern, string property) =>
    NotMatch(value, pattern, property, ValidationErrorMessages.NotMatch(property, value));

  /// <summary>
  /// Verifica se o valor não corresponde ao padrão. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="pattern">Padrão a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="options">Opções de regex para Case Sensitive.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract NotMatch(string value, string pattern, string property, RegexOptions options) =>
    NotMatch(value, pattern, property, ValidationErrorMessages.NotMatch(property, value), options);

  /// <summary>
  /// Verifica se o valor não corresponde ao padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="pattern">Padrão a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <param name="options">Opções de regex para Case Sensitive. Parâmetro opcional. Padrão RegexOptions.None.</param>
  /// <param name="timeout">Tempo limite para validação. Em milissegundos. Parâmetro opcional. Padrão 300.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract NotMatch(
    string value,
    string pattern,
    string property,
    string message,
    RegexOptions options = RegexOptions.None,
    int timeout = 300
  ) => Validate(property, message, () => MatchFunc(value, pattern, options, timeout));
  #endregion
}

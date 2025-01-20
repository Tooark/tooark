﻿using Tooark.Validations.Messages;

namespace Tooark.Validations;

/// <summary>
/// Classe de validação de object.
/// </summary>
public partial class Contract
{
  #region Formatters
  /// <summary>
  /// Função para formatar para tipo object.
  /// </summary>
  /// <param name="value">Valor a ser formatado.</param>
  /// <returns>Valor formatado.</returns>
  private static string ObjectFormat(object value) => $"{value}";
  #endregion

  #region Validates
  /// <summary>
  /// Função para validar condição.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Valor a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <param name="condition">Condição a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  private Contract Validate(object value, object comparer, string property, string message, Func<object, object, bool> condition)
  {
    // Se a condição for verdadeira, adicione a notificação.
    if (condition(value, comparer))
    {
      // Adiciona a notificação.
      AddNotification(message, property);
    }

    // Retorna o contrato de validação.
    return this;
  }
  #endregion


  #region AreEquals
  /// <summary>
  /// Verifica se o valor é igual ao valor comparado. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Valor a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract AreEquals(object value, object comparer, string property) =>
    AreEquals(value, comparer, property, ValidationErrorMessages.AreEquals(property, ObjectFormat(comparer)));

  /// <summary>
  /// Verifica se o valor é igual ao valor comparado.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Valor a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract AreEquals(object value, object comparer, string property, string message) =>
    Validate(value, comparer, property, message, (v, c) => !v.Equals(c));
  #endregion

  #region AreNotEquals
  /// <summary>
  /// Verifica se o valor não é igual ao valor comparado. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Valor a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract AreNotEquals(object value, object comparer, string property) =>
    AreNotEquals(value, comparer, property, ValidationErrorMessages.AreNotEquals(property, ObjectFormat(comparer)));

  /// <summary>
  /// Verifica se o valor não é igual ao valor comparado.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Valor a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract AreNotEquals(object value, object comparer, string property, string message) =>
    Validate(value, comparer, property, message, (v, c) => v.Equals(c));
  #endregion

  #region IsNull
  /// <summary>
  /// Valida se o valor é nulo. Com a mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Nome da propriedade.</param>
  /// <returns>Contrato.</returns>
  public Contract IsNull(object? value, string property) =>
    IsNull(value, property, ValidationErrorMessages.IsNull(property));

  /// <summary>
  /// Valida se o valor é nulo.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Nome da propriedade.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato.</returns>
  public Contract IsNull(object? value, string property, string message) =>
    ValidateNull(value, false, property, message);
  #endregion

  #region IsNotNull
  /// <summary>
  /// Valida se o valor não é nulo. Com a mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Nome da propriedade.</param>
  /// <returns>Contrato.</returns>
  public Contract IsNotNull(object? value, string property) =>
    IsNotNull(value, property, ValidationErrorMessages.IsNotNull(property));

  /// <summary>
  /// Valida se o valor é nulo.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Nome da propriedade.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato.</returns>
  public Contract IsNotNull(object? value, string property, string message) =>
    ValidateNull(value, true, property, message);
  #endregion
}

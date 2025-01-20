﻿using Tooark.Validations.Messages;

namespace Tooark.Validations;

/// <summary>
/// Classe de validação de string.
/// </summary>
public partial class Contract
{
  #region Converters
  /// <summary>
  /// Função para converter para string.
  /// </summary>
  /// <typeparam name="T">Tipo a ser convertido.</typeparam>
  /// <param name="value">Valor a ser convertido.</param>
  /// <returns>Valor convertido.</returns>
  private static string ConvertToString(int value) => Convert.ToString(value) ?? "";
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
  private Contract Validate(string value, int comparer, string property, string message, Func<string, int, bool> condition)
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

  /// <summary>
  /// Função para validar condição.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Valor a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <param name="condition">Condição a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  private Contract Validate(string value, string comparer, string property, string message, Func<string, string, bool> condition)
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

  /// <summary>
  /// Função para validar lista de condição.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="list">Lista a ser comparada.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <param name="condition">Condição a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  private Contract ValidateList(string value, string[] list, string property, string message, Func<string, string[], bool> condition)
  {
    // Se a condição for verdadeira, adicione a notificação.
    if (condition(value, list))
    {
      // Adiciona a notificação.
      AddNotification(message, property);
    }

    // Retorna o contrato de validação.
    return this;
  }
  #endregion


  #region IsGreater
  /// <summary>
  /// Verifica se o tamanho do valor é maior que o valor comparado. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Tamanho a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsGreater(string value, int comparer, string property) =>
    IsGreater(value, comparer, property, ValidationErrorMessages.IsGreater(property, ConvertToString(comparer)));

  /// <summary>
  /// Verifica se o tamanho do valor é maior que o valor comparado.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Tamanho a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsGreater(string value, int comparer, string property, string message) =>
    Validate(value, comparer, property, message, (v, c) => v.Length <= c);
  #endregion

  #region IsGreaterOrEquals
  /// <summary>
  /// Verifica se o tamanho do valor é maior ou igual ao valor comparado. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Tamanho a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsGreaterOrEquals(string value, int comparer, string property) =>
    IsGreaterOrEquals(value, comparer, property, ValidationErrorMessages.IsGreaterOrEquals(property, ConvertToString(comparer)));

  /// <summary>
  /// Verifica se o tamanho do valor é maior ou igual ao valor comparado.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Tamanho a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsGreaterOrEquals(string value, int comparer, string property, string message) =>
    Validate(value, comparer, property, message, (v, c) => v.Length < c);
  #endregion

  #region IsLower
  /// <summary>
  /// Verifica se o tamanho do valor é menor que o valor comparado. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Tamanho a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsLower(string value, int comparer, string property) =>
    IsLower(value, comparer, property, ValidationErrorMessages.IsLower(property, ConvertToString(comparer)));

  /// <summary>
  /// Verifica se o tamanho do valor é menor que o valor comparado.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Tamanho a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsLower(string value, int comparer, string property, string message) =>
    Validate(value, comparer, property, message, (v, c) => v.Length >= c);
  #endregion

  #region IsLowerOrEquals
  /// <summary>
  /// Verifica se o tamanho do valor é menor ou igual ao valor comparado. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Tamanho a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsLowerOrEquals(string value, int comparer, string property) =>
    IsLowerOrEquals(value, comparer, property, ValidationErrorMessages.IsLowerOrEquals(property, ConvertToString(comparer)));

  /// <summary>
  /// Verifica se o tamanho do valor é menor ou igual ao valor comparado.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Tamanho a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsLowerOrEquals(string value, int comparer, string property, string message) =>
    Validate(value, comparer, property, message, (v, c) => v.Length > c);
  #endregion

  #region IsBetween
  /// <summary>
  /// Verifica se o tamanho do valor é entre os valores comparados. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="start">Valor inicial.</param>
  /// <param name="end">Valor final.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsBetween(string value, int start, int end, string property) =>
    IsBetween(value, start, end, property, ValidationErrorMessages.IsBetween(property, ConvertToString(start), ConvertToString(end)));

  /// <summary>
  /// Verifica se o tamanho do valor é entre os valores comparados
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="start">Valor inicial.</param>
  /// <param name="end">Valor final.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsBetween(string value, int start, int end, string property, string message) =>
    Validate(value, start, property, message, (v, s) => v.Length < s || v.Length > end);
  #endregion

  #region IsNotBetween
  /// <summary>
  /// Verifica se o tamanho do valor não é entre os valores comparados. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="start">Valor inicial.</param>
  /// <param name="end">Valor final.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsNotBetween(string value, int start, int end, string property) =>
    IsNotBetween(value, start, end, property, ValidationErrorMessages.IsNotBetween(property, ConvertToString(start), ConvertToString(end)));

  /// <summary>
  /// Verifica se o tamanho do valor não é entre os valores comparados
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="start">Valor inicial.</param>
  /// <param name="end">Valor final.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract IsNotBetween(string value, int start, int end, string property, string message) =>
    Validate(value, start, property, message, (v, s) => v.Length >= s && v.Length <= end);
  #endregion

  #region AreEquals
  /// <summary>
  /// Verifica se o valor é igual ao valor comparado. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Valor a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract AreEquals(string value, string comparer, string property) =>
    AreEquals(value, comparer, property, ValidationErrorMessages.AreEquals(property, comparer));

  /// <summary>
  /// Verifica se o valor é igual ao valor comparado.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Valor a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract AreEquals(string value, string comparer, string property, string message) =>
    Validate(value, comparer, property, message, (v, c) => v != c);

  /// <summary>
  /// Verifica se o tamanho valor é igual ao valor comparado. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Tamanho a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract AreEquals(string value, int comparer, string property) =>
    AreEquals(value, comparer, property, ValidationErrorMessages.AreEquals(property, ConvertToString(comparer)));

  /// <summary>
  /// Verifica se o tamanho valor é igual ao valor comparado.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Tamanho a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract AreEquals(string value, int comparer, string property, string message) =>
    Validate(value, comparer, property, message, (v, c) => v.Length != c);
  #endregion

  #region AreNotEquals
  /// <summary>
  /// Verifica se o valor não é igual ao valor comparado. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Valor a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract AreNotEquals(string value, string comparer, string property) =>
    AreNotEquals(value, comparer, property, ValidationErrorMessages.AreNotEquals(property, comparer));

  /// <summary>
  /// Verifica se o valor não é igual ao valor comparado.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Valor a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract AreNotEquals(string value, string comparer, string property, string message) =>
    Validate(value, comparer, property, message, (v, c) => v == c);

  /// <summary>
  /// Verifica se o tamanho valor não é igual ao valor comparado. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Tamanho a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract AreNotEquals(string value, int comparer, string property) =>
    AreNotEquals(value, comparer, property, ValidationErrorMessages.AreNotEquals(property, ConvertToString(comparer)));

  /// <summary>
  /// Verifica se o tamanho valor não é igual ao valor comparado.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Tamanho a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract AreNotEquals(string value, int comparer, string property, string message) =>
    Validate(value, comparer, property, message, (v, c) => v.Length == c);
  #endregion

  #region Contains
  /// <summary>
  /// Verifica se o valor está contido dentro do valor comparado. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Valor a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract Contains(string value, string comparer, string property) =>
    Contains(value, comparer, property, ValidationErrorMessages.Contains(property, comparer));

  /// <summary>
  /// Verifica se o valor está contido dentro do valor comparado.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Valor a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract Contains(string value, string comparer, string property, string message) =>
    Validate(value, comparer, property, message, (v, c) => !v.Contains(c));

  /// <summary>
  /// Verifica se contém o valor na lista. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="list">Lista de valores a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract Contains(string value, string[] list, string property) =>
    Contains(value, list, property, ValidationErrorMessages.Contains(property, value));

  /// <summary>
  /// Verifica se contém o valor na lista.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="list">Lista de valores a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract Contains(string value, string[] list, string property, string message) =>
    ValidateList(value, list, property, message, (v, l) => !l.Contains(v));
  #endregion

  #region NotContains
  /// <summary>
  /// Verifica se o valor não está contido dentro do valor comparado. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Valor a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract NotContains(string value, string comparer, string property) =>
    NotContains(value, comparer, property, ValidationErrorMessages.NotContains(property, comparer));

  /// <summary>
  /// Verifica se o valor não está contido dentro do valor comparado.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Valor a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract NotContains(string value, string comparer, string property, string message) =>
    Validate(value, comparer, property, message, (v, c) => v.Contains(c));

  /// <summary>
  /// Verifica se não contém o valor na lista. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="list">Lista de valores a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract NotContains(string value, string[] list, string property) =>
    NotContains(value, list, property, ValidationErrorMessages.NotContains(property, value));

  /// <summary>
  /// Verifica se não contém o valor na lista.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="list">Lista de valores a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato de validação.</returns>
  public Contract NotContains(string value, string[] list, string property, string message) =>
    ValidateList(value, list, property, message, (v, l) => l.Contains(v));
  #endregion

  #region IsNull
  /// <summary>
  /// Valida se o valor é nulo. Com a mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Nome da propriedade.</param>
  /// <returns>Contrato.</returns>
  public Contract IsNull(string? value, string property) =>
    IsNull(value, property, ValidationErrorMessages.IsNull(property));

  /// <summary>
  /// Valida se o valor é nulo.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Nome da propriedade.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato.</returns>
  public Contract IsNull(string? value, string property, string message) =>
    ValidateNull(value, false, property, message);
  #endregion

  #region IsNotNull
  /// <summary>
  /// Valida se o valor não é nulo. Com a mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Nome da propriedade.</param>
  /// <returns>Contrato.</returns>
  public Contract IsNotNull(string? value, string property) =>
    IsNotNull(value, property, ValidationErrorMessages.IsNotNull(property));

  /// <summary>
  /// Valida se o valor é nulo.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Nome da propriedade.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato.</returns>
  public Contract IsNotNull(string? value, string property, string message) =>
    ValidateNull(value, true, property, message);
  #endregion

  #region IsNullOrEmpty
  /// <summary>
  /// Valida se o valor é nulo ou vazio. Com a mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Nome da propriedade.</param>
  /// <returns>Contrato.</returns>
  public Contract IsNullOrEmpty(string? value, string property) =>
    IsNullOrEmpty(value, property, ValidationErrorMessages.IsNullOrEmpty(property));

  /// <summary>
  /// Valida se o valor é nulo ou vazio.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Nome da propriedade.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato.</returns>
  public Contract IsNullOrEmpty(string? value, string property, string message) 
  {
    // Se a condição for verdadeira, adicione a notificação.
    if (!string.IsNullOrEmpty(value))
    {
      // Adiciona a notificação.
      AddNotification(message, property);
    }

    // Retorna o contrato de validação.
    return this;
  }
  #endregion

  #region IsNotNullOrEmpty
  /// <summary>
  /// Valida se o valor não é nulo nem vazio. Com a mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Nome da propriedade.</param>
  /// <returns>Contrato.</returns>
  public Contract IsNotNullOrEmpty(string? value, string property) =>
    IsNotNullOrEmpty(value, property, ValidationErrorMessages.IsNotNullOrEmpty(property));

  /// <summary>
  /// Valida se o valor não é nulo nem vazio.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Nome da propriedade.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato.</returns>
  public Contract IsNotNullOrEmpty(string? value, string property, string message) 
  {
    // Se a condição for verdadeira, adicione a notificação.
    if (string.IsNullOrEmpty(value))
    {
      // Adiciona a notificação.
      AddNotification(message, property);
    }

    // Retorna o contrato de validação.
    return this;
  }
  #endregion

  #region IsNullOrWhiteSpace
  /// <summary>
  /// Valida se o valor é nulo ou espaço em branco. Com a mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Nome da propriedade.</param>
  /// <returns>Contrato.</returns>
  public Contract IsNullOrWhiteSpace(string? value, string property) =>
    IsNullOrWhiteSpace(value, property, ValidationErrorMessages.IsNullOrWhiteSpace(property));

  /// <summary>
  /// Valida se o valor é nulo ou espaço em branco.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Nome da propriedade.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato.</returns>
  public Contract IsNullOrWhiteSpace(string? value, string property, string message) 
  {
    // Se a condição for verdadeira, adicione a notificação.
    if (!string.IsNullOrWhiteSpace(value))
    {
      // Adiciona a notificação.
      AddNotification(message, property);
    }

    // Retorna o contrato de validação.
    return this;
  }
  #endregion

  #region IsNotNullOrWhiteSpace
  /// <summary>
  /// Valida se o valor não é nulo nem espaço em branco. Com a mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Nome da propriedade.</param>
  /// <returns>Contrato.</returns>
  public Contract IsNotNullOrWhiteSpace(string? value, string property) =>
    IsNotNullOrWhiteSpace(value, property, ValidationErrorMessages.IsNotNullOrWhiteSpace(property));

  /// <summary>
  /// Valida se o valor não é nulo nem espaço em branco.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Nome da propriedade.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Contrato.</returns>
  public Contract IsNotNullOrWhiteSpace(string? value, string property, string message) 
  {
    // Se a condição for verdadeira, adicione a notificação.
    if (string.IsNullOrWhiteSpace(value))
    {
      // Adiciona a notificação.
      AddNotification(message, property);
    }

    // Retorna o contrato de validação.
    return this;
  }
  #endregion
}

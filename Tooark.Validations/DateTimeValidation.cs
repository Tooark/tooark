﻿using Tooark.Validations.Messages;

namespace Tooark.Validations;

/// <summary>
/// Classe de validação de datas e horas.
/// </summary>
public partial class Validation
{
  #region Formatters
  /// <summary>
  /// Função para formatar para tipo DateTime.
  /// </summary>
  /// <param name="value">Valor a ser formatado.</param>
  /// <returns>Valor formatado.</returns>
  private static string DateTimeFormat(DateTime value) => $"{value:yyyy-MM-ddTHH:mm:ss}";
  #endregion

  #region Converters
  /// <summary>
  /// Função para converter para DateTime.
  /// </summary>
  /// <typeparam name="T">Tipo a ser convertido.</typeparam>
  /// <param name="value">Valor a ser convertido.</param>
  /// <returns>Valor convertido.</returns>
  private static DateTime ConvertToDateTime<T>(T value) where T : IConvertible => Convert.ToDateTime(value);
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
  /// <returns>Validação.</returns>
  private Validation Validate(DateTime value, DateTime comparer, string property, string message, Func<DateTime, DateTime, bool> condition)
  {
    // Se a condição for verdadeira, adicione a notificação.
    if (condition(value, comparer))
    {
      // Adiciona a notificação.
      AddNotification(message, property, "T.VLD.DTT1");
    }

    // Retorna uma validação.
    return this;
  }

  /// <summary>
  /// Função para validar condição.
  /// </summary>
  /// <typeparam name="T">Tipo a ser convertido.</typeparam>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Valor a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <param name="condition">Condição a ser validada.</param>
  /// <returns>Validação.</returns>
  private Validation Validate<T>(DateTime value, T comparer, string property, string message, Func<DateTime, DateTime, bool> condition) where T : IConvertible =>
    Validate(value, ConvertToDateTime(comparer), property, message, condition);

  /// <summary>
  /// Função para validar lista de condição.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="list">Lista a ser comparada.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <param name="condition">Condição a ser validada.</param>
  /// <returns>Validação.</returns>
  private Validation ValidateList(DateTime value, DateTime[] list, string property, string message, Func<DateTime, DateTime[], bool> condition)
  {
    // Se a condição for verdadeira, adicione a notificação.
    if (condition(value, list))
    {
      // Adiciona a notificação.
      AddNotification(message, property, "T.VLD.DTT2");
    }

    // Retorna uma validação.
    return this;
  }

  /// <summary>
  /// Função para validar lista de condição.
  /// </summary>
  /// <typeparam name="T">Tipo a ser convertido.</typeparam>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="list">Lista a ser comparada.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <param name="condition">Condição a ser validada.</param>
  /// <returns>Validação.</returns>
  private Validation ValidateList<T>(DateTime value, T[] list, string property, string message, Func<DateTime, DateTime[], bool> condition) where T : IConvertible =>
    ValidateList(value, [.. list.Select(item => ConvertToDateTime(item))], property, message, condition);
  #endregion


  #region IsGreater
  /// <summary>
  /// Verifica se o valor é maior que o valor comparado. Com mensagem padrão.
  /// </summary>
  /// <typeparam name="T">Tipo a ser convertido.</typeparam>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Valor a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Validação.</returns>
  public Validation IsGreater<T>(DateTime value, T comparer, string property) where T : IConvertible =>
    IsGreater(value, comparer, property, ValidationErrorMessages.IsGreater(property, DateTimeFormat(ConvertToDateTime(comparer))));

  /// <summary>
  /// Verifica se o valor é maior que o valor comparado.
  /// </summary>
  /// <typeparam name="T">Tipo a ser convertido.</typeparam>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Valor a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Validação.</returns>
  public Validation IsGreater<T>(DateTime value, T comparer, string property, string message) where T : IConvertible =>
    Validate(value, comparer, property, message, (v, c) => v <= c);
  #endregion

  #region IsGreaterOrEquals
  /// <summary>
  /// Verifica se o valor é maior ou igual ao valor comparado. Com mensagem padrão.
  /// </summary>
  /// <typeparam name="T">Tipo a ser convertido.</typeparam>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Valor a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Validação.</returns>
  public Validation IsGreaterOrEquals<T>(DateTime value, T comparer, string property) where T : IConvertible =>
    IsGreaterOrEquals(value, comparer, property, ValidationErrorMessages.IsGreaterOrEquals(property, DateTimeFormat(ConvertToDateTime(comparer))));

  /// <summary>
  /// Verifica se o valor é maior ou igual ao valor comparado.
  /// </summary>
  /// <typeparam name="T">Tipo a ser convertido.</typeparam>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Valor a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Validação.</returns>
  public Validation IsGreaterOrEquals<T>(DateTime value, T comparer, string property, string message) where T : IConvertible =>
    Validate(value, comparer, property, message, (v, c) => v < c);
  #endregion

  #region IsLower
  /// <summary>
  /// Verifica se o valor é menor que o valor comparado. Com mensagem padrão.
  /// </summary>
  /// <typeparam name="T">Tipo a ser convertido.</typeparam>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Valor a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Validação.</returns>
  public Validation IsLower<T>(DateTime value, T comparer, string property) where T : IConvertible =>
    IsLower(value, comparer, property, ValidationErrorMessages.IsLower(property, DateTimeFormat(ConvertToDateTime(comparer))));

  /// <summary>
  /// Verifica se o valor é menor que o valor comparado.
  /// </summary>
  /// <typeparam name="T">Tipo a ser convertido.</typeparam>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Valor a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Validação.</returns>
  public Validation IsLower<T>(DateTime value, T comparer, string property, string message) where T : IConvertible =>
    Validate(value, comparer, property, message, (v, c) => v >= c);
  #endregion

  #region IsLowerOrEquals
  /// <summary>
  /// Verifica se o valor é menor ou igual ao valor comparado. Com mensagem padrão.
  /// </summary>
  /// <typeparam name="T">Tipo a ser convertido.</typeparam>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Valor a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Validação.</returns>
  public Validation IsLowerOrEquals<T>(DateTime value, T comparer, string property) where T : IConvertible =>
    IsLowerOrEquals(value, comparer, property, ValidationErrorMessages.IsLowerOrEquals(property, DateTimeFormat(ConvertToDateTime(comparer))));

  /// <summary>
  /// Verifica se o valor é menor ou igual ao valor comparado.
  /// </summary>
  /// <typeparam name="T">Tipo a ser convertido.</typeparam>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Valor a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Validação.</returns>
  public Validation IsLowerOrEquals<T>(DateTime value, T comparer, string property, string message) where T : IConvertible =>
    Validate(value, comparer, property, message, (v, c) => v > c);
  #endregion

  #region IsBetween
  /// <summary>
  /// Verifica se o valor é entre os valores comparados. Com mensagem padrão.
  /// </summary>
  /// <typeparam name="T">Tipo a ser convertido.</typeparam>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="start">Valor inicial.</param>
  /// <param name="end">Valor final.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Validação.</returns>
  public Validation IsBetween<T>(DateTime value, T start, T end, string property) where T : IConvertible =>
    IsBetween(value, start, end, property, ValidationErrorMessages.IsBetween(property, DateTimeFormat(ConvertToDateTime(start)), DateTimeFormat(ConvertToDateTime(end))));

  /// <summary>
  /// Verifica se o valor é entre os valores comparados
  /// </summary>
  /// <typeparam name="T">Tipo a ser convertido.</typeparam>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="start">Valor inicial.</param>
  /// <param name="end">Valor final.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Validação.</returns>
  public Validation IsBetween<T>(DateTime value, T start, T end, string property, string message) where T : IConvertible =>
    Validate(value, start, property, message, (v, s) => v < s || v > ConvertToDateTime(end));
  #endregion

  #region IsNotBetween
  /// <summary>
  /// Verifica se o valor não é entre os valores comparados. Com mensagem padrão.
  /// </summary>
  /// <typeparam name="T">Tipo a ser convertido.</typeparam>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="start">Valor inicial.</param>
  /// <param name="end">Valor final.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Validação.</returns>
  public Validation IsNotBetween<T>(DateTime value, T start, T end, string property) where T : IConvertible =>
    IsNotBetween(value, start, end, property, ValidationErrorMessages.IsNotBetween(property, DateTimeFormat(ConvertToDateTime(start)), DateTimeFormat(ConvertToDateTime(end))));

  /// <summary>
  /// Verifica se o valor não é entre os valores comparados
  /// </summary>
  /// <typeparam name="T">Tipo a ser convertido.</typeparam>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="start">Valor inicial.</param>
  /// <param name="end">Valor final.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Validação.</returns>
  public Validation IsNotBetween<T>(DateTime value, T start, T end, string property, string message) where T : IConvertible =>
    Validate(value, start, property, message, (v, s) => v >= s && v <= ConvertToDateTime(end));
  #endregion

  #region IsMin
  /// <summary>
  /// Verifica se o valor é igual ao valor mínimo do tipo. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Validação.</returns>
  public Validation IsMin(DateTime value, string property) =>
    IsMin(value, property, ValidationErrorMessages.IsMin(property, DateTimeFormat(DateTime.MinValue)));

  /// <summary>
  /// Verifica se o valor é igual ao valor mínimo do tipo.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Validação.</returns>
  public Validation IsMin(DateTime value, string property, string message) =>
    Validate(value, DateTime.MinValue, property, message, (v, c) => v != c);
  #endregion

  #region IsNotMin
  /// <summary>
  /// Verifica se o valor é não igual ao valor mínimo do tipo. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Validação.</returns>
  public Validation IsNotMin(DateTime value, string property) =>
    IsNotMin(value, property, ValidationErrorMessages.IsNotMin(property, DateTimeFormat(DateTime.MinValue)));

  /// <summary>
  /// Verifica se o valor é não igual ao valor mínimo do tipo.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Validação.</returns>
  public Validation IsNotMin(DateTime value, string property, string message) =>
    Validate(value, DateTime.MinValue, property, message, (v, c) => v == c);
  #endregion

  #region IsMax
  /// <summary>
  /// Verifica se o valor é igual ao valor máximo do tipo. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Validação.</returns>
  public Validation IsMax(DateTime value, string property) =>
    IsMax(value, property, ValidationErrorMessages.IsMax(property, DateTimeFormat(DateTime.MaxValue)));

  /// <summary>
  /// Verifica se o valor é igual ao valor máximo do tipo.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Validação.</returns>
  public Validation IsMax(DateTime value, string property, string message) =>
    Validate(value, DateTime.MaxValue, property, message, (v, c) => v != c);
  #endregion

  #region IsNotMax
  /// <summary>
  /// Verifica se o valor é não igual ao valor máximo do tipo. Com mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Validação.</returns>
  public Validation IsNotMax(DateTime value, string property) =>
    IsNotMax(value, property, ValidationErrorMessages.IsNotMax(property, DateTimeFormat(DateTime.MaxValue)));

  /// <summary>
  /// Verifica se o valor é não igual ao valor máximo do tipo.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Validação.</returns>
  public Validation IsNotMax(DateTime value, string property, string message) =>
    Validate(value, DateTime.MaxValue, property, message, (v, c) => v == c);
  #endregion

  #region AreEquals
  /// <summary>
  /// Verifica se o valor é igual ao valor comparado. Com mensagem padrão.
  /// </summary>
  /// <typeparam name="T">Tipo a ser convertido.</typeparam>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Valor a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Validação.</returns>
  public Validation AreEquals<T>(DateTime value, T comparer, string property) where T : IConvertible =>
    AreEquals(value, comparer, property, ValidationErrorMessages.AreEquals(property, DateTimeFormat(ConvertToDateTime(comparer))));

  /// <summary>
  /// Verifica se o valor é igual ao valor comparado.
  /// </summary>
  /// <typeparam name="T">Tipo a ser convertido.</typeparam>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Valor a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Validação.</returns>
  public Validation AreEquals<T>(DateTime value, T comparer, string property, string message) where T : IConvertible =>
    Validate(value, comparer, property, message, (v, c) => v != c);
  #endregion

  #region AreNotEquals
  /// <summary>
  /// Verifica se o valor não é igual ao valor comparado. Com mensagem padrão.
  /// </summary>
  /// <typeparam name="T">Tipo a ser convertido.</typeparam>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Valor a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Validação.</returns>
  public Validation AreNotEquals<T>(DateTime value, T comparer, string property) where T : IConvertible =>
    AreNotEquals(value, comparer, property, ValidationErrorMessages.AreNotEquals(property, DateTimeFormat(ConvertToDateTime(comparer))));

  /// <summary>
  /// Verifica se o valor não é igual ao valor comparado.
  /// </summary>
  /// <typeparam name="T">Tipo a ser convertido.</typeparam>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="comparer">Valor a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Validação.</returns>
  public Validation AreNotEquals<T>(DateTime value, T comparer, string property, string message) where T : IConvertible =>
    Validate(value, comparer, property, message, (v, c) => v == c);
  #endregion

  #region Contains
  /// <summary>
  /// Verifica se contém o valor na lista. Com mensagem padrão.
  /// </summary>
  /// <typeparam name="T">Tipo a ser convertido.</typeparam>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="list">Lista de valores a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Validação.</returns>
  public Validation Contains<T>(DateTime value, T[] list, string property) where T : IConvertible =>
    Contains(value, list, property, ValidationErrorMessages.Contains(property, DateTimeFormat(value)));

  /// <summary>
  /// Verifica se contém o valor na lista.
  /// </summary>
  /// <typeparam name="T">Tipo a ser convertido.</typeparam>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="list">Lista de valores a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Validação.</returns>
  public Validation Contains<T>(DateTime value, T[] list, string property, string message) where T : IConvertible =>
    ValidateList(value, list, property, message, (v, l) => !l.Contains(v));
  #endregion

  #region NotContains
  /// <summary>
  /// Verifica se não contém o valor na lista. Com mensagem padrão.
  /// </summary>
  /// <typeparam name="T">Tipo a ser convertido.</typeparam>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="list">Lista de valores a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Validação.</returns>
  public Validation NotContains<T>(DateTime value, T[] list, string property) where T : IConvertible =>
    NotContains(value, list, property, ValidationErrorMessages.NotContains(property, DateTimeFormat(value)));

  /// <summary>
  /// Verifica se não contém o valor na lista.
  /// </summary>
  /// <typeparam name="T">Tipo a ser convertido.</typeparam>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="list">Lista de valores a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Validação.</returns>
  public Validation NotContains<T>(DateTime value, T[] list, string property, string message) where T : IConvertible =>
    ValidateList(value, list, property, message, (v, l) => l.Contains(v));
  #endregion

  #region All
  /// <summary>
  /// Verifica se todos os valores são iguais ao valor comparado. Com mensagem padrão.
  /// </summary>
  /// <typeparam name="T">Tipo a ser convertido.</typeparam>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="list">Lista de valores a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Validação.</returns>
  public Validation All<T>(DateTime value, T[] list, string property) where T : IConvertible =>
    All(value, list, property, ValidationErrorMessages.All(property, DateTimeFormat(value)));

  /// <summary>
  /// Verifica se todos os valores são iguais ao valor comparado.
  /// </summary>
  /// <typeparam name="T">Tipo a ser convertido.</typeparam>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="list">Lista de valores a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Validação.</returns>
  public Validation All<T>(DateTime value, T[] list, string property, string message) where T : IConvertible =>
    ValidateList(value, list, property, message, (v, l) => !l.All(x => x == v));
  #endregion

  #region NotAll
  /// <summary>
  /// Verifica se todos os valores são diferentes ao valor comparado. Com mensagem padrão.
  /// </summary>
  /// <typeparam name="T">Tipo a ser convertido.</typeparam>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="list">Lista de valores a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <returns>Validação.</returns>
  public Validation NotAll<T>(DateTime value, T[] list, string property) where T : IConvertible =>
    NotAll(value, list, property, ValidationErrorMessages.NotAll(property, DateTimeFormat(value)));

  /// <summary>
  /// Verifica se todos os valores são diferentes ao valor comparado.
  /// </summary>
  /// <typeparam name="T">Tipo a ser convertido.</typeparam>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="list">Lista de valores a ser comparado.</param>
  /// <param name="property">Propriedade a ser validada.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Validação.</returns>
  public Validation NotAll<T>(DateTime value, T[] list, string property, string message) where T : IConvertible =>
    ValidateList(value, list, property, message, (v, l) => l.All(x => x == v));
  #endregion

  #region IsNull
  /// <summary>
  /// Valida se o valor é nulo. Com a mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Nome da propriedade.</param>
  /// <returns>Validação.</returns>
  public Validation IsNull(DateTime? value, string property) =>
    IsNull(value, property, ValidationErrorMessages.IsNull(property));

  /// <summary>
  /// Valida se o valor é nulo.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Nome da propriedade.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Validação.</returns>
  public Validation IsNull(DateTime? value, string property, string message) =>
    ValidateNull(value, false, property, message);
  #endregion

  #region IsNotNull
  /// <summary>
  /// Valida se o valor não é nulo. Com a mensagem padrão.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Nome da propriedade.</param>
  /// <returns>Validação.</returns>
  public Validation IsNotNull(DateTime? value, string property) =>
    IsNotNull(value, property, ValidationErrorMessages.IsNotNull(property));

  /// <summary>
  /// Valida se o valor é nulo.
  /// </summary>
  /// <param name="value">Valor a ser validado.</param>
  /// <param name="property">Nome da propriedade.</param>
  /// <param name="message">Mensagem de erro.</param>
  /// <returns>Validação.</returns>
  public Validation IsNotNull(DateTime? value, string property, string message) =>
    ValidateNull(value, true, property, message);
  #endregion
}

using Tooark.Validations;

namespace Tooark.ValueObjects;

/// <summary>
/// Representa uma string delimitada por ponto e vírgula.
/// </summary>
/// <remarks>
/// Conversão entre listas e strings delimitadas por ponto e vírgula.
/// Esta classe é utilizada para validar e manipular strings que contêm múltiplos valores separados por ponto e vírgula.
/// </remarks>
public class DelimitedString : ValueObject
{
  /// <summary>
  /// Delimitador padrão utilizado para separar os valores.
  /// </summary>
  private const char DefaultDelimiter = ';';

  /// <summary>
  /// Valor privado da string delimitada.
  /// </summary>
  private readonly string _value = string.Empty;

  /// <summary>
  /// Lista de valores privados da string delimitada.
  /// </summary>
  private readonly string[] _values = [];


  /// <summary>
  /// Inicializa uma nova instância da classe DelimitedString com o valor especificado.
  /// </summary>
  /// <param name="value">O valor da string delimitada por ponto e vírgula a ser validada.</param>
  public DelimitedString(string value)
  {
    // Adiciona as notificações de validação da string delimitada
    AddNotifications(new Validation()
      .IsNotNullOrWhiteSpace(value, nameof(DelimitedString), "Field.Invalid;DelimitedString")
    );

    // Verifica se é válido então não existe notificação
    if (IsValid)
    {
      // Define o valor e a lista de valores da string delimitada
      _value = value.Trim();
      _values = [.. _value.Split(DefaultDelimiter, StringSplitOptions.RemoveEmptyEntries).Select(v => v.Trim())];
    }
  }

  /// <summary>
  /// Inicializa uma nova instância da classe DelimitedString com os valores especificados.
  /// </summary>
  /// <param name="values">Os valores a serem concatenados na string delimitada.</param>
  public DelimitedString(params string[] values)
  {
    // Adiciona as notificações de validação da lista de strings
    AddNotifications(new Validation()
      .IsGreater(values, 0, nameof(DelimitedString), "Field.Invalid;DelimitedString")
    );

    // Verifica se é válido então não existe notificação
    if (IsValid)
    {
      // Define o valor e a lista de valores da string delimitada
      _value = string.Join(DefaultDelimiter, values);
      _values = values;
    }
  }


  /// <summary>
  /// Valor da string delimitada.
  /// </summary>
  public string Value { get => _value; }

  /// <summary>
  /// Lista de valores da string delimitada.
  /// </summary>
  public string[] Values { get => _values; }


  /// <summary>
  /// Sobrescreve o método <see cref="object.ToString"/> para retornar a string delimitada.
  /// </summary>
  /// <returns>String delimitada.</returns>
  public override string ToString() => _value;

  /// <summary>
  /// Converte a string delimitada em uma lista de strings.
  /// </summary>
  /// <returns>Lista de strings.</returns>
  public string[] ToList() => _values;

  /// <summary>
  /// Conversão implícita de DelimitedString para string.
  /// </summary>
  /// <param name="delimitedString">Instância de DelimitedString.</param>
  /// <returns>String delimitada.</returns>
  public static implicit operator string(DelimitedString delimitedString) => delimitedString._value;

  /// <summary>
  /// Conversão implícita de DelimitedString para lista de strings.
  /// </summary>
  /// <param name="delimitedString">Instância de DelimitedString.</param>
  /// <returns>Lista de strings.</returns>
  public static implicit operator string[](DelimitedString delimitedString) => delimitedString._values;

  /// <summary>
  /// Conversão implícita de DelimitedString para lista de strings.
  /// </summary>
  /// <param name="delimitedString">Instância de DelimitedString.</param>
  /// <returns>Lista de strings.</returns>
  public static implicit operator List<string>(DelimitedString delimitedString) => new([.. delimitedString._values]);

  /// <summary>
  /// Conversão implícita de string para DelimitedString.
  /// </summary>
  /// <param name="value">String delimitada.</param>
  /// <returns>Instância de DelimitedString.</returns>
  public static implicit operator DelimitedString(string value) => new(value);

  /// <summary>
  /// Conversão implícita de lista de strings para DelimitedString.
  /// </summary>
  /// <param name="values">Lista de strings.</param>
  /// <returns>Instância de DelimitedString.</returns>
  public static implicit operator DelimitedString(string[] values) => new(values);

  /// <summary>
  /// Conversão implícita de lista de strings para DelimitedString.
  /// </summary>
  /// <param name="values">Lista de strings.</param>
  /// <returns>Instância de DelimitedString.</returns>
  public static implicit operator DelimitedString(List<string> values) => new([.. values]);
}

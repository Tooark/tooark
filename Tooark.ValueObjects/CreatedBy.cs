using Tooark.Validations;

namespace Tooark.ValueObjects;

/// <summary>
/// Representa um Guid do Criado Por.
/// </summary>
public sealed class CreatedBy : ValueObject
{
  /// <summary>
  /// Valor privado do Guid do Criado Por.
  /// </summary>
  private readonly Guid _value = Guid.Empty;

  /// <summary>
  /// Inicializa uma nova instância da classe CreatedBy com o valor especificado.
  /// </summary>
  /// <param name="value">O valor do Guid do Criado Por a ser validado.</param>
  public CreatedBy(Guid value)
  {
    // Adiciona as notificações de validação do Guid do Criado Por.
    AddNotifications(new Validation()
      .IsNotEmpty(value, "CreatedBy", "Field.Invalid;CreatedBy")
    );

    // Verifica é valido então não existe notificação
    if (IsValid)
    {
      // Define o valor do Guid do Criado Por
      _value = value;
    }
  }


  /// <summary>
  /// Obtém o valor do Guid do Criado Por.
  /// </summary>
  public Guid Value { get => _value; }


  /// <summary>
  /// Define uma conversão implícita de um objeto CreatedBy para uma Guid.
  /// </summary>
  /// <param name="email">O objeto CreatedBy a ser convertido.</param>
  /// <returns>Uma Guid que representa o valor do CreatedBy.</returns>
  public static implicit operator Guid(CreatedBy email) => email._value;

  /// <summary>
  /// Define uma conversão implícita de uma Guid para um objeto CreatedBy.
  /// </summary>
  /// <param name="value">A Guid a ser convertida em um objeto CreatedBy.</param>
  /// <returns>Um objeto CreatedBy criado a partir do Guid fornecida.</returns>
  public static implicit operator CreatedBy(Guid value) => new(value);
}

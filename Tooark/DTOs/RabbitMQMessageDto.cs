using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tooark.DTOs;

/// <summary>
/// Representa uma mensagem DTO para ser usada nas mensagens do RabbitMQ com o título e dados especificados.
/// </summary>
/// <typeparam name="T">O tipo de dados que a mensagem carrega.</typeparam>
/// <param name="title">O título da mensagem.</param>
/// <param name="data">Os dados da mensagem.</param>
public class RabbitMQMessageDto<T>(string title, T data)
{
  /// <summary>
  /// Obtém o título da mensagem.
  /// </summary>
  public string Title { get; private set; } = title;

  /// <summary>
  /// Obtém os dados da mensagem.
  /// </summary>
  public T Data { get; private set; } = data;

  /// <summary>
  /// Converte implicitamente um objeto RabbitMQMessageDto para uma string JSON.
  /// </summary>
  /// <param name="messageObject">O objeto RabbitMQMessageDto a ser serializado.</param>
  /// <returns>Uma string JSON representando o objeto RabbitMQMessageDto.</returns>
  public static implicit operator string(RabbitMQMessageDto<T> messageObject)
  {
    JsonSerializerOptions jsonSerializerOptions = new()
    {
      ReferenceHandler = ReferenceHandler.Preserve,
      WriteIndented = false
    };
    JsonSerializerOptions _options = jsonSerializerOptions;

    return JsonSerializer.Serialize(messageObject, _options);
  }

  /// <summary>
  /// Converte implicitamente uma string JSON para um objeto RabbitMQMessageDto.
  /// </summary>
  /// <param name="message">A string JSON a ser desserializada.</param>
  /// <returns>O objeto RabbitMQMessageDto desserializado.</returns>
  public static implicit operator RabbitMQMessageDto<T>(string message)
  {
    JsonSerializerOptions jsonSerializerOptions = new()
    {
      ReferenceHandler = ReferenceHandler.Preserve,
      WriteIndented = false
    };
    JsonSerializerOptions _options = jsonSerializerOptions;

    var messageObject =
      JsonSerializer.Deserialize<RabbitMQMessageDto<T>>(message, _options) ??
      throw new InvalidOperationException("A mensagem não pode ser desserializada para o tipo RabbitMQMessageDto<T>.");

    return messageObject;
  }
}

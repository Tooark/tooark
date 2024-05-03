using System.Text.Encodings.Web;
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
  private static readonly JsonSerializerOptions Options = new()
  {
    ReferenceHandler = ReferenceHandler.Preserve,
    WriteIndented = false,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
  };

  /// <summary>
  /// Obtém o título da mensagem.
  /// </summary>
  public string Title { get; private set; } = title;

  /// <summary>
  /// Obtém a mensagem serializada como uma string JSON.
  /// </summary>
  public string Message { get; private set; } = JsonSerializer.Serialize(data, Options);

  /// <summary>
  /// Converte implicitamente um objeto RabbitMQMessageDto para uma string JSON.
  /// </summary>
  /// <param name="messageObject">O objeto RabbitMQMessageDto a ser serializado.</param>
  /// <returns>Uma string JSON representando o objeto RabbitMQMessageDto.</returns>
  public static implicit operator string(RabbitMQMessageDto<T> messageObject) => JsonSerializer.Serialize(messageObject, Options);

  /// <summary>
  /// Converte implicitamente uma string JSON para um objeto RabbitMQMessageDto.
  /// </summary>
  /// <param name="messageString">A string JSON a ser desserializada.</param>
  /// <returns>O objeto RabbitMQMessageDto desserializado.</returns>
  public static implicit operator RabbitMQMessageDto<T>(string messageString)
  {
    var messageData =
      JsonSerializer.Deserialize<dynamic>(messageString, Options) ??
      throw new InvalidOperationException("A mensagem não pode ser desserializada para o tipo RabbitMQMessageDto<T>.");

    var title = messageData.GetProperty("Title").GetString();
    var message = JsonSerializer.Deserialize<T>(messageData.GetProperty("Message").GetString(), Options);

    return new RabbitMQMessageDto<T>(title, message);
  }

  /// <summary>
  /// Obtém os dados da mensagem como o tipo especificado.
  /// </summary>
  /// <returns>Os dados da mensagem como o tipo T.</returns>
  public T GetData()
  {
    return
      JsonSerializer.Deserialize<T>(Message, Options) ??
      throw new InvalidOperationException("Os dados não podem ser desserializados.");
  }
}

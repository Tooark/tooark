using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Text;
using Tooark.Exceptions;
using Tooark.Interfaces;
using Tooark.Options;

namespace Tooark.Services.RabbitMQ;

/// <summary>
/// Serviço para interagir com o RabbitMQ, permitindo publicar mensagens.
/// </summary>
internal class RabbitMQPublishService : IRabbitMQPublishService, IDisposable
{
  private readonly IConnection _connection;
  private readonly IModel _channel;
  private bool _disposed;

  /// <summary>
  /// Construtor do RabbitMQPublishService.
  /// </summary>
  /// <param name="options">Parâmetros para os serviços RabbitMQ.</param>
  internal RabbitMQPublishService(RabbitMQOptions options)
  {
    try
    {
      var factory = new ConnectionFactory()
      {
        HostName = options.Hostname,
        Port = options.PortNumber,
        UserName = options.Username,
        Password = options.Password,
        AutomaticRecoveryEnabled = options.AutomaticRecovery,
        NetworkRecoveryInterval = TimeSpan.FromSeconds(options.RecoveryInterval),
      };

      _connection = factory.CreateConnection();
      _channel = _connection.CreateModel();
    }
    catch (Exception ex)
    {
      throw new RabbitMQServiceException(
        "Não foi possível estabelecer uma conexão com o servidor RabbitMQ.",
        ex);
    }
  }

  /// <summary>
  /// Publica uma mensagem em uma exchange especificada com uma chave de roteamento opcional.
  /// Este método é utilizado internamente pelos métodos públicos de publicação para enviar mensagens ao RabbitMQ.
  /// </summary>
  /// <param name="message">A mensagem a ser publicada no RabbitMQ.</param>
  /// <param name="exchangeName">O nome da exchange onde a mensagem será publicada.</param>
  /// <param name="routingKey">A chave de roteamento para a mensagem. Opcional para exchanges do tipo fanout.</param>
  /// <exception cref="RabbitMQServiceException">Lança uma exceção se não for possível alcançar o broker do RabbitMQ ou ocorrer um erro ao publicar a mensagem.</exception>
  private async Task PublishMessageInternal(string message, string exchangeName, string routingKey = "")
  {
    try
    {
      var body = Encoding.UTF8.GetBytes(message);

      await Task.Run(() =>_channel.BasicPublish(
        exchange: exchangeName,
        routingKey: routingKey,
        basicProperties: null,
        body: body));
    }
    catch (BrokerUnreachableException ex)
    {
      throw new RabbitMQServiceException(
        "Não foi possível alcançar o broker do RabbitMQ.",
        exchangeName,
        routingKey,
        ex);
    }
    catch (Exception ex)
    {
      throw new RabbitMQServiceException(
        "Erro ao publicar mensagem.",
        exchangeName,
        routingKey,
        ex);
    }
  }

  /// <summary>
  /// Publica uma mensagem no exchange_fanout.
  /// </summary>
  /// <param name="message">Mensagem a ser publicada.</param>
  public async Task PublishMessage(string message)
  {
    await PublishMessageInternal(message, "exchange_fanout");
  }

  /// <summary>
  /// Publica uma mensagem no exchange_direct com uma chave de roteamento.
  /// </summary>
  /// <param name="message">Mensagem a ser publicada.</param>
  /// <param name="routingKey">Chave de roteamento para o exchange_direct.</param>
  public async Task PublishMessage(string message, string routingKey)
  {
    await PublishMessageInternal(message, "exchange_direct", routingKey);
  }

  /// <summary>
  /// Publica uma mensagem em uma exchange customizada, fornecida como parâmetro.
  /// </summary>
  /// <param name="message">Mensagem a ser publicada.</param>
  /// <param name="routingKey">Chave de roteamento para o exchange_direct.</param>
  /// <param name="exchangeName">Mensagem a ser publicada.</param>
  public async Task PublishMessage(string message, string routingKey, string exchangeName)
  {
    await PublishMessageInternal(message, exchangeName, routingKey);
  }

  /// <summary>
  /// Libera os recursos usados pelo serviço RabbitMQ.
  /// </summary>
  public void Dispose()
  {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  /// <summary>
  /// Libera os recursos não gerenciados usados pelo serviço RabbitMQ e, opcionalmente, libera os recursos gerenciados.
  /// </summary>
  /// <param name="disposing">true para liberar recursos gerenciados e não gerenciados; false para liberar apenas recursos não gerenciados.</param>
  protected virtual void Dispose(bool disposing)
  {
    if (!_disposed)
    {
      if (disposing)
      {
        // Libera recursos gerenciados
        if (_channel.IsOpen)
        {
          _channel.Close();
        }

        _connection.Close();
      }

      // Libera recursos não gerenciados se houver
      _disposed = true;
    }
  }

  /// <summary>
  /// Destruidor da classe RabbitMQPublishService.
  /// </summary>
  ~RabbitMQPublishService()
  {
    Dispose(false);
  }
}

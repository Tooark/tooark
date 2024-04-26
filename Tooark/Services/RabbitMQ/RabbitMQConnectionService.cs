using RabbitMQ.Client;
using Tooark.Exceptions;

namespace Tooark.Services.RabbitMQ;

/// <summary>
/// Serviço responsável por gerenciar a conexão com o servidor RabbitMQ.
/// </summary>
public class RabbitMQConnectionService : IDisposable
{
  private readonly IConnection _connection;
  private bool _disposed = false;

  private readonly bool automaticRecovery = true;
  private readonly int recoveryInterval = 5;

  /// <summary>
  /// Inicializa uma nova instância do serviço de conexão RabbitMQ.
  /// </summary>
  /// <param name="hostname">O nome do host do servidor RabbitMQ.</param>
  /// <param name="port">A porta do servidor RabbitMQ.</param>
  /// <param name="userName">O nome de usuário para autenticação.</param>
  /// <param name="password">A senha para autenticação.</param>
  public RabbitMQConnectionService(string hostname, int port, string userName, string password)
  {
    var factory = new ConnectionFactory()
    {
      HostName = hostname,
      Port = port,
      UserName = userName,
      Password = password,
      AutomaticRecoveryEnabled = automaticRecovery,
      NetworkRecoveryInterval = TimeSpan.FromSeconds(recoveryInterval),
    };

    try
    {
      _connection = factory.CreateConnection();
    }
    catch (Exception ex)
    {
      throw new RabbitMQServiceException(
        "Não foi possível estabelecer uma conexão com o servidor RabbitMQ.",
        ex);
    }
  }

  /// <summary>
  /// Cria um canal de comunicação com o servidor RabbitMQ.
  /// </summary>
  /// <returns>Um canal para interagir com o RabbitMQ.</returns>
  public IModel CreateChannel()
  {
    return _disposed ? throw new ObjectDisposedException(nameof(RabbitMQConnectionService)) : _connection.CreateModel();
  }

  /// <summary>
  /// Fecha a conexão com o servidor RabbitMQ.
  /// </summary>
  public void Close()
  {
    if (_disposed)
    {
      return;
    }

    _connection?.Close();
    Dispose();
  }

  /// <summary>
  /// Libera os recursos usados pela conexão RabbitMQ.
  /// </summary>
  public void Dispose()
  {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  /// <summary>
  /// Método protegido para liberar recursos não gerenciados e, opcionalmente, gerenciados.
  /// </summary>
  /// <param name="disposing">Indica se o método foi chamado pelo método Dispose (true) ou pelo finalizador (false).</param>
  protected virtual void Dispose(bool disposing)
  {
    if (!_disposed)
    {
      if (disposing)
      {
        _connection?.Dispose();
      }

      _disposed = true;
    }
  }

  /// <summary>
  /// Destruidor da classe RabbitMQConnectionService.
  /// </summary>
  ~RabbitMQConnectionService()
  {
    Dispose(false);
  }
}

using RabbitMQ.Client;

namespace Tooark.Services.RabbitMQ;

public class RabbitMQConnectionService
{
  private readonly IConnection _connection;

  public RabbitMQConnectionService(string hostname, int port, string userName, string password)
  {
    var factory = new ConnectionFactory()
    {
      HostName = hostname,
      Port = port,
      UserName = userName,
      Password = password
    };

    _connection = factory.CreateConnection();
  }

  public IModel CreateChannel()
  {
    return _connection.CreateModel();
  }

  public void Close()
  {
    _connection?.Close();
  }
}

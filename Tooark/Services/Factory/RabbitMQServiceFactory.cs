using Tooark.Services.Interface;
using Tooark.Services.RabbitMQ;

namespace Tooark.Services.Factory;

public static class RabbitMQServiceFactory
{
  public static IRabbitMQService Create(RabbitMQConnectionService connectionService)
  {
    return new RabbitMQService(connectionService);
  }
}

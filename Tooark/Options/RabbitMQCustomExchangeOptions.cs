using RabbitMQ.Client;

namespace Tooark.Options;

public class RabbitMQCustomExchangeOptions
{
  public string NameExchange = null!;
  public string TypeExchange = ExchangeType.Fanout;
  public string NameQueue = null!;
  public string RoutingKey = null!;
  public bool Durable = true;
  public bool Exclusive = false;
  public bool AutoDelete = false;
}

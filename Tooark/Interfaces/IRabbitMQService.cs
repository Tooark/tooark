using RabbitMQ.Client;

namespace Tooark.Interfaces;

public interface IRabbitMQService
{        
  void PublishMessage(string message);
          
  void PublishMessage(string message, string routingKey);
            
  void PublishMessage(string message, string routingKey, string exchangeName);
        
  IModel GetChannel();
}

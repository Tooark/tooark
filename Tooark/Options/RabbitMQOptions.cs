namespace Tooark.Options;

public class RabbitMQOptions
{
  public const string Section = "RabbitMQ";

  public string Hostname { get; set; } = "localhost";
  public int PortNumber { get; set; } = 5672;
  public string Username { get; set; } = "guest";
  public string Password { get; set; } = "guest";
  public bool AutomaticRecovery { get; set; } = true;
  public int RecoveryInterval { get; set; } = 5;
  public string QueueName { get; set; } = "";
}

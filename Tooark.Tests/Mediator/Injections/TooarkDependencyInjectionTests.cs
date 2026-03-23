using Microsoft.Extensions.DependencyInjection;
using Tooark.Mediator.Abstractions;
using Tooark.Mediator.Handlers;
using Tooark.Mediator.Injections;

namespace Tooark.Tests.Mediator.Injections;

public class TooarkDependencyInjectionTests
{
  [Fact]
  public void AddTooarkMediator_ShouldThrowArgumentNullException_WhenServicesIsNull()
  {
    // Arrange
    ServiceCollection? services = null;

    // Act & Assert
    Assert.Throws<ArgumentNullException>(() => services!.AddTooarkMediator(typeof(TooarkDependencyInjectionTests).Assembly));
  }

  [Fact]
  public async Task AddTooarkMediator_ShouldRegisterMediatorAndHandlers_WhenAssemblyIsProvided()
  {
    // Arrange
    var services = new ServiceCollection();

    // Act
    services.AddTooarkMediator(typeof(TooarkDependencyInjectionTests).Assembly);

    var provider = services.BuildServiceProvider();
    var mediator = provider.GetRequiredService<IMediator>();

    // Assert
    Assert.NotNull(mediator);

    var queryResult = await mediator.Send(new TestQuery("query-result"));
    Assert.Equal("query-result", queryResult);

    var commandResult = await mediator.Send(new TestCommand("command-result"));
    Assert.Equal("command-result", commandResult);

    await mediator.Send(new VoidCommand());

    NotificationCounter.Reset();
    await mediator.Publish(new TestNotification());
    Assert.Equal(1, NotificationCounter.Value);
  }

  [Fact]
  public async Task AddTooarkMediator_ShouldUseCallingAssembly_WhenNoAssemblyIsProvided()
  {
    // Arrange
    var services = new ServiceCollection();

    // Act
    services.AddTooarkMediator();

    var provider = services.BuildServiceProvider();
    var mediator = provider.GetRequiredService<IMediator>();

    // Assert
    var result = await mediator.Send(new TestQuery("from-calling-assembly"));
    Assert.Equal("from-calling-assembly", result);
  }

  [Fact]
  public void AddTooarkMediator_ShouldNotDuplicateRegistrations_WhenAssembliesAreRepeated()
  {
    // Arrange
    var services = new ServiceCollection();
    var assembly = typeof(TooarkDependencyInjectionTests).Assembly;

    // Act
    services.AddTooarkMediator(assembly, assembly);

    // Assert
    var requestHandlerRegistrations = services.Count(service =>
      service.ServiceType == typeof(IRequestHandler<TestQuery, string>)
      && service.ImplementationType == typeof(TestQueryHandler));

    var notificationHandlerRegistrations = services.Count(service =>
      service.ServiceType == typeof(INotificationHandler<TestNotification>)
      && service.ImplementationType == typeof(TestNotificationHandler));

    Assert.Equal(1, requestHandlerRegistrations);
    Assert.Equal(1, notificationHandlerRegistrations);
  }

  [Fact]
  public void AddTooarkMediator_ShouldRegisterHandlersByConcreteHandlerInterfaces()
  {
    // Arrange
    var services = new ServiceCollection();

    // Act
    services.AddTooarkMediator(typeof(TooarkDependencyInjectionTests).Assembly);

    // Assert
    Assert.Contains(services, service =>
      service.ServiceType == typeof(IQueryHandler<TestQuery, string>)
      && service.ImplementationType == typeof(TestQueryHandler));

    Assert.Contains(services, service =>
      service.ServiceType == typeof(ICommandHandler<TestCommand, string>)
      && service.ImplementationType == typeof(TestCommandHandler));

    Assert.Contains(services, service =>
      service.ServiceType == typeof(ICommandHandler<VoidCommand>)
      && service.ImplementationType == typeof(VoidCommandHandler));
  }

  private sealed record TestQuery(string Value) : IQuery<string>;

  private sealed class TestQueryHandler : IQueryHandler<TestQuery, string>
  {
    public Task<string> Handle(TestQuery request, CancellationToken cancellationToken)
    {
      return Task.FromResult(request.Value);
    }
  }

  private sealed record TestCommand(string Value) : ICommand<string>;

  private sealed class TestCommandHandler : ICommandHandler<TestCommand, string>
  {
    public Task<string> Handle(TestCommand request, CancellationToken cancellationToken)
    {
      return Task.FromResult(request.Value);
    }
  }

  private sealed record VoidCommand : ICommand;

  private sealed class VoidCommandHandler : ICommandHandler<VoidCommand>
  {
    public Task<Unit> Handle(VoidCommand request, CancellationToken cancellationToken)
    {
      return Unit.Task;
    }
  }

  private sealed record TestNotification : INotification;

  private static class NotificationCounter
  {
    private static int _value;

    public static int Value => _value;

    public static void Reset()
    {
      Interlocked.Exchange(ref _value, 0);
    }

    public static void Increment()
    {
      Interlocked.Increment(ref _value);
    }
  }

  private sealed class TestNotificationHandler : INotificationHandler<TestNotification>
  {
    public Task Handle(TestNotification notification, CancellationToken cancellationToken)
    {
      NotificationCounter.Increment();
      return Task.CompletedTask;
    }
  }
}

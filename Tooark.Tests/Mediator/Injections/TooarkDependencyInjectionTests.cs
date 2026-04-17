using Microsoft.Extensions.DependencyInjection;
using Tooark.Exceptions;
using Tooark.Mediator;
using Tooark.Mediator.Abstractions;
using Tooark.Mediator.Enums;
using Tooark.Mediator.Handlers;
using Tooark.Mediator.Injections;
using Tooark.Mediator.Options;

namespace Tooark.Tests.Mediator.Injections;

public class TooarkDependencyInjectionTests
{
  [Fact]
  public void AddTooarkMediator_ShouldThrowInternalServerErrorException_WhenServicesIsNull()
  {
    // Arrange
    ServiceCollection? services = null;

    // Act & Assert
    Assert.Throws<InternalServerErrorException>(() => services!.AddTooarkMediator(typeof(TooarkDependencyInjectionTests).Assembly));
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

    var queryResult = await mediator.SendAsync(new TestQuery("query-result"));
    Assert.Equal("query-result", queryResult);

    var commandResult = await mediator.SendAsync(new TestCommand("command-result"));
    Assert.Equal("command-result", commandResult);

    await mediator.SendAsync(new VoidCommand());

    NotificationCounter.Reset();
    await mediator.PublishAsync(new TestNotification());
    Assert.Equal(1, NotificationCounter.Value);
  }

  [Fact]
  public void AddTooarkMediator_ShouldUseDefaultAssembly_WhenNoAssemblyIsProvided()
  {
    // Arrange
    var services = new ServiceCollection();

    // Act
    services.AddTooarkMediator();

    var provider = services.BuildServiceProvider();
    var mediator = provider.GetRequiredService<IMediator>();

    // Assert
    Assert.NotNull(mediator);
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
      service.ServiceType == typeof(INotifyHandler<TestNotification>)
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

  [Fact]
  public void AddTooarkMediator_ShouldRegisterISenderAndIPublisher()
  {
    // Arrange
    var services = new ServiceCollection();

    // Act
    services.AddTooarkMediator(typeof(TooarkDependencyInjectionTests).Assembly);

    var provider = services.BuildServiceProvider();

    // Assert
    var sender = provider.GetRequiredService<ISender>();
    var publisher = provider.GetRequiredService<IPublisher>();
    var mediator = provider.GetRequiredService<IMediator>();

    Assert.NotNull(sender);
    Assert.NotNull(publisher);
    Assert.NotNull(mediator);
  }

  [Fact]
  public void AddTooarkMediator_ShouldApplyConfiguredOptions()
  {
    // Arrange
    var services = new ServiceCollection();

    // Act
    services.AddTooarkMediator(
      options => options.NotifyPublishStrategy = ENotifyStrategy.Sequential,
      typeof(TooarkDependencyInjectionTests).Assembly);

    var provider = services.BuildServiceProvider();

    // Assert
    var options = provider.GetRequiredService<MediatorOptions>();
    Assert.Equal(ENotifyStrategy.Sequential, options.NotifyPublishStrategy);
  }

  private sealed record TestQuery(string Value) : IQuery<string>;

  private sealed class TestQueryHandler : IQueryHandler<TestQuery, string>
  {
    public Task<string> HandleAsync(TestQuery request, CancellationToken cancellationToken)
    {
      return Task.FromResult(request.Value);
    }
  }

  private sealed record TestCommand(string Value) : ICommand<string>;

  private sealed class TestCommandHandler : ICommandHandler<TestCommand, string>
  {
    public Task<string> HandleAsync(TestCommand request, CancellationToken cancellationToken)
    {
      return Task.FromResult(request.Value);
    }
  }

  private sealed record VoidCommand : ICommand;

  private sealed class VoidCommandHandler : ICommandHandler<VoidCommand>
  {
    public Task<Unit> HandleAsync(VoidCommand request, CancellationToken cancellationToken)
    {
      return Unit.Task;
    }
  }

  private sealed record TestNotification : INotify;

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

  private sealed class TestNotificationHandler : INotifyHandler<TestNotification>
  {
    public Task HandleAsync(TestNotification notification, CancellationToken cancellationToken)
    {
      NotificationCounter.Increment();
      return Task.CompletedTask;
    }
  }
}

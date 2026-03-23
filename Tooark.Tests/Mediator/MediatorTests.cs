using Microsoft.Extensions.DependencyInjection;
using Tooark.Mediator.Abstractions;
using Tooark.Mediator.Handlers;

namespace Tooark.Tests.Mediator;

public class MediatorTests
{
  [Fact]
  public async Task Send_ShouldDispatchRequestToHandler()
  {
    // Arrange
    var services = new ServiceCollection();
    services.AddTransient<IMediator, global::Tooark.Mediator.Mediator>();
    services.AddTransient<IRequestHandler<PingRequest, string>, PingRequestHandler>();

    var provider = services.BuildServiceProvider();
    var mediator = provider.GetRequiredService<IMediator>();

    // Act
    var result = await mediator.Send(new PingRequest("pong"));

    // Assert
    Assert.Equal("pong", result);
  }

  [Fact]
  public async Task Send_ShouldThrowArgumentNullException_WhenRequestIsNull()
  {
    // Arrange
    var services = new ServiceCollection();
    services.AddTransient<IMediator, global::Tooark.Mediator.Mediator>();

    var provider = services.BuildServiceProvider();
    var mediator = provider.GetRequiredService<IMediator>();

    // Act & Assert
    await Assert.ThrowsAsync<ArgumentNullException>(() => mediator.Send<string>(null!));
  }

  [Fact]
  public async Task Send_ShouldThrowInvalidOperationException_WhenHandlerDoesNotExist()
  {
    // Arrange
    var services = new ServiceCollection();
    services.AddTransient<IMediator, global::Tooark.Mediator.Mediator>();

    var provider = services.BuildServiceProvider();
    var mediator = provider.GetRequiredService<IMediator>();

    // Act
    var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => mediator.Send(new PingRequest("pong")));

    // Assert
    Assert.Contains("Nenhum handler encontrado", exception.Message);
  }

  [Fact]
  public async Task Send_ShouldThrowInvalidOperationException_WhenHandlerReturnsNullTask()
  {
    // Arrange
    var services = new ServiceCollection();
    services.AddTransient<IMediator, global::Tooark.Mediator.Mediator>();
    services.AddTransient<IRequestHandler<NullTaskRequest, string>, NullTaskRequestHandler>();

    var provider = services.BuildServiceProvider();
    var mediator = provider.GetRequiredService<IMediator>();

    // Act
    var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => mediator.Send(new NullTaskRequest()));

    // Assert
    Assert.Contains("Falha ao executar handler", exception.Message);
  }

  [Fact]
  public async Task Publish_ShouldInvokeAllNotificationHandlers()
  {
    // Arrange
    TestNotificationCounter.Reset();

    var services = new ServiceCollection();
    services.AddTransient<IMediator, global::Tooark.Mediator.Mediator>();
    services.AddTransient<INotificationHandler<TestNotification>, TestNotificationHandlerOne>();
    services.AddTransient<INotificationHandler<TestNotification>, TestNotificationHandlerTwo>();

    var provider = services.BuildServiceProvider();
    var mediator = provider.GetRequiredService<IMediator>();

    // Act
    await mediator.Publish(new TestNotification());

    // Assert
    Assert.Equal(2, TestNotificationCounter.Value);
  }

  [Fact]
  public async Task Publish_ShouldNotThrow_WhenNoNotificationHandlerExists()
  {
    // Arrange
    var services = new ServiceCollection();
    services.AddTransient<IMediator, global::Tooark.Mediator.Mediator>();

    var provider = services.BuildServiceProvider();
    var mediator = provider.GetRequiredService<IMediator>();

    // Act & Assert
    await mediator.Publish(new TestNotification());
  }

  [Fact]
  public async Task Publish_ShouldThrowArgumentNullException_WhenNotificationIsNull()
  {
    // Arrange
    var services = new ServiceCollection();
    services.AddTransient<IMediator, global::Tooark.Mediator.Mediator>();

    var provider = services.BuildServiceProvider();
    var mediator = provider.GetRequiredService<IMediator>();

    // Act & Assert
    await Assert.ThrowsAsync<ArgumentNullException>(() => mediator.Publish(null!));
  }

  [Fact]
  public async Task Publish_ShouldThrowInvalidOperationException_WhenHandlerReturnsNullTask()
  {
    // Arrange
    var services = new ServiceCollection();
    services.AddTransient<IMediator, global::Tooark.Mediator.Mediator>();
    services.AddTransient<INotificationHandler<NullTaskNotification>, NullTaskNotificationHandler>();

    var provider = services.BuildServiceProvider();
    var mediator = provider.GetRequiredService<IMediator>();

    // Act
    var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => mediator.Publish(new NullTaskNotification()));

    // Assert
    Assert.Contains("Falha ao executar notification handler", exception.Message);
  }

  private sealed record PingRequest(string Message) : IRequest<string>;

  private sealed class PingRequestHandler : IRequestHandler<PingRequest, string>
  {
    public Task<string> Handle(PingRequest request, CancellationToken cancellationToken)
    {
      return Task.FromResult(request.Message);
    }
  }

  private sealed record NullTaskRequest : IRequest<string>;

  private sealed class NullTaskRequestHandler : IRequestHandler<NullTaskRequest, string>
  {
    public Task<string> Handle(NullTaskRequest request, CancellationToken cancellationToken)
    {
      return null!;
    }
  }

  private sealed record TestNotification : INotification;

  private static class TestNotificationCounter
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

  private sealed class TestNotificationHandlerOne : INotificationHandler<TestNotification>
  {
    public Task Handle(TestNotification notification, CancellationToken cancellationToken)
    {
      TestNotificationCounter.Increment();
      return Task.CompletedTask;
    }
  }

  private sealed class TestNotificationHandlerTwo : INotificationHandler<TestNotification>
  {
    public Task Handle(TestNotification notification, CancellationToken cancellationToken)
    {
      TestNotificationCounter.Increment();
      return Task.CompletedTask;
    }
  }

  private sealed record NullTaskNotification : INotification;

  private sealed class NullTaskNotificationHandler : INotificationHandler<NullTaskNotification>
  {
    public Task Handle(NullTaskNotification notification, CancellationToken cancellationToken)
    {
      return null!;
    }
  }
}

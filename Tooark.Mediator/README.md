# Tooark.Mediator

Biblioteca com implementação de Mediator para projetos .NET, focada em estruturas CQRS/CQS com baixo acoplamento entre camadas de aplicação.

## Instalação

```bash
dotnet add package Tooark.Mediator
```

## Conceitos

- `ICommand` e `ICommand<TResponse>`: comandos (escrita)
- `IQuery<TResponse>`: consultas (leitura)
- `IRequest<TResponse>`: contrato base de requisição
- `IRequestHandler<TRequest, TResponse>`: handler para requests
- `INotification` e `INotificationHandler<TNotification>`: notificações/eventos
- `IMediator`: dispatcher de requests e notificações

## Configuração

```csharp
using Tooark.Mediator.Injections;

builder.Services.AddTooarkMediator(typeof(Program).Assembly);
```

## Exemplo CQRS

```csharp
using Tooark.Mediator.Abstractions;
using Tooark.Mediator.Handlers;

public sealed record CreateUserCommand(string Name) : ICommand<Guid>;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
{
  public Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
  {
    return Task.FromResult(Guid.NewGuid());
  }
}

public sealed record GetUserByIdQuery(Guid Id) : IQuery<string>;

public sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, string>
{
  public Task<string> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
  {
    return Task.FromResult($"Usuário {request.Id}");
  }
}
```

## Exemplo de uso

```csharp
using Tooark.Mediator.Abstractions;

public sealed class UsersService(IMediator mediator)
{
  public Task<Guid> CreateAsync(string name, CancellationToken cancellationToken)
  {
    return mediator.Send(new CreateUserCommand(name), cancellationToken);
  }

  public Task<string> GetAsync(Guid id, CancellationToken cancellationToken)
  {
    return mediator.Send(new GetUserByIdQuery(id), cancellationToken);
  }
}
```

# Tooark.Mediator

Biblioteca com implementação de Mediator para projetos .NET, focada em CQRS/CQS com baixo acoplamento entre camadas.

## Conteúdo

- [Visão Geral](#visão-geral)
- [Instalação](#instalação)
- [Configuração](#configuração)
- [Componentes](#componentes)
- [Exemplos de Uso](#exemplos-de-uso)
- [Dependências](#dependências)
- [Contribuição](#contribuição)
- [Licença](#licença)

## Visão Geral

O pacote `Tooark.Mediator` fornece:

- implementação concreta de `IMediator`;
- registro automático de handlers por assembly;
- estratégia configurável de publicação de notificações;
- integração com DI do `Microsoft.Extensions.DependencyInjection`.

---

## 🔧 Instalação

```bash
dotnet add package Tooark.Mediator
```

---

## ⚙️ Configuração

```csharp
using Tooark.Mediator.Injections;

builder.Services.AddTooarkMediator(typeof(Program).Assembly);
```

Também é possível configurar as opções do mediador:

```csharp
using Tooark.Mediator.Enums;
using Tooark.Mediator.Injections;

builder.Services.AddTooarkMediator(options =>
{
  options.NotifyPublishStrategy = ENotifyStrategy.Sequential;
}, typeof(Program).Assembly);
```

---

## 📦 Componentes

### Classe principal

- `Mediator`: implementação de `IMediator`.

### Injeção de dependência

- `TooarkDependencyInjection.AddTooarkMediator(IServiceCollection, params Assembly[])`
- `TooarkDependencyInjection.AddTooarkMediator(IServiceCollection, Action<MediatorOptions>, params Assembly[])`

### Opções

- `MediatorOptions`
  - `NotifyPublishStrategy` (padrão: `ENotifyStrategy.ParallelWhenAll`)

### Estratégias de publicação

- `ENotifyStrategy.ParallelWhenAll`
- `ENotifyStrategy.Sequential`

### Handlers suportados

- `IRequestHandler<TRequest, TResponse>`
- `ICommandHandler<TCommand, TResponse>`
- `ICommandHandler<TCommand>`
- `IQueryHandler<TQuery, TResponse>`
- `INotifyHandler<TNotify>`

---

## 📝 Exemplos de Uso

### Exemplo CQRS

```csharp
using Tooark.Mediator.Abstractions;
using Tooark.Mediator.Handlers;

public sealed record CreateUserCommand(string Name) : ICommand<Guid>;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
{
  public Task<Guid> HandleAsync(CreateUserCommand request, CancellationToken cancellationToken = default)
  {
    return Task.FromResult(Guid.NewGuid());
  }
}

public sealed record GetUserByIdQuery(Guid Id) : IQuery<string>;

public sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, string>
{
  public Task<string> HandleAsync(GetUserByIdQuery request, CancellationToken cancellationToken = default)
  {
    return Task.FromResult($"Usuário {request.Id}");
  }
}
```

### Exemplo de uso com IMediator

```csharp
using Tooark.Mediator.Abstractions;

public sealed class UsersService(IMediator mediator)
{
  public Task<Guid> CreateAsync(string name, CancellationToken cancellationToken)
  {
    return mediator.SendAsync(new CreateUserCommand(name), cancellationToken);
  }

  public Task<string> GetAsync(Guid id, CancellationToken cancellationToken)
  {
    return mediator.SendAsync(new GetUserByIdQuery(id), cancellationToken);
  }
}
```

### Exemplo de publicação de notificação

```csharp
using Tooark.Mediator.Abstractions;
using Tooark.Mediator.Handlers;

public sealed record UserCreatedNotify(Guid UserId) : INotify;

public sealed class UserCreatedNotifyHandler : INotifyHandler<UserCreatedNotify>
{
  public Task HandleAsync(UserCreatedNotify notification, CancellationToken cancellationToken = default)
  {
    Console.WriteLine($"Usuário criado: {notification.UserId}");
    return Task.CompletedTask;
  }
}

await mediator.PublishAsync(new UserCreatedNotify(Guid.NewGuid()), cancellationToken);
```

---

## 📋 Dependências

| Pacote                                                  | Versão | Descrição                             |
| ------------------------------------------------------- | ------ | ------------------------------------- |
| `Tooark.Exceptions`                                     | —      | Exceções (ex.: `BadRequestException`) |
| `Tooark.Mediator.Abstractions`                          | —      | Contratos base do padrão Mediator     |
| `Microsoft.Extensions.DependencyInjection.Abstractions` | 8.x    | Abstrações de injeção de dependência  |

---

## 🪪 Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests no repositório [Tooark.Mediator](https://github.com/Tooark/tooark/issues).

## 📄 Licença

Este projeto está licenciado sob a licença BSD 3-Clause. Veja o arquivo [LICENSE](../LICENSE) para mais detalhes.

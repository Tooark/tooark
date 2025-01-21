# Tooark.Notifications

## Descrição

O namespace `Tooark.Notifications` fornece uma estrutura para criar e gerenciar notificações.

## Classes

### NotificationItem

Representa a estrutura de uma notificação.

#### Propriedades

- `Key`: A chave da notificação.
- `Message`: A mensagem da notificação.

#### Construtores

- `NotificationItem(string message)`: Cria uma nova instância de `NotificationItem` com uma mensagem.
- `NotificationItem(string key, string message)`: Cria uma nova instância de `NotificationItem` com uma chave e uma mensagem.

```csharp
// Exemplo de uso
var notification1 = new NotificationItem("Mensagem de exemplo");
var notification2 = new NotificationItem("ChaveExemplo", "Mensagem de exemplo com chave");
```

### Notification

Representa uma classe abstrata que gerencia uma lista de notificações.

#### Propriedades

- `Notifications`: Retorna a lista de notificações.
- `IsValid`: Retorna True se a lista de notificações estiver vazia.
- `Count`: Retorna o tamanho da lista de notificações.
- `Keys`: Retorna a lista de chaves das notificações.
- `Messages`: Retorna a lista de mensagens das notificações.

#### Métodos

- `AddNotification(NotificationItem notification)`: Adiciona uma nova notificação à lista de notificações.
- `AddNotification(string message)`: Adiciona uma nova notificação à lista de notificações.
- `AddNotification(string message, string key)`: Adiciona uma nova notificação à lista de notificações.
- `AddNotification(Type property, string message)`: Adiciona uma nova notificação à lista de notificações.
- `AddNotifications(ICollection<NotificationItem> notifications)`: Adiciona uma coleção de notificações à lista de notificações.
- `AddNotifications(Notification notification)`: Adiciona a lista de notificações de uma notificação à lista de notificações.
- `AddNotifications(params Notification[] notifications)`: Adiciona uma coleção de notificações à lista de notificações.
- `Clear()`: Limpa a lista de notificações.

```csharp
// Exemplo de uso
public class MyNotification : Notification
{
  public void NotifyError(string message)
  {
    AddNotification(new NotificationItem("Error", message));
  }
}

var myNotification = new MyNotification();

myNotification.NotifyError("Ocorreu um erro.");

var isValid = myNotification.IsValid; // False
var count = myNotification.Count; // 1
var keys = myNotification.Keys; // ["Error"]
var messages = myNotification.Messages; // ["Ocorreu um erro."]
var notifications = myNotification.Notifications; // [NotificationItem("Error", "Ocorreu um erro.")]
```

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests no repositório [Tooark.Notifications](https://github.com/Tooark/tooark).

## Licença

Este projeto está licenciado sob a licença MIT. Veja o arquivo [LICENSE](../LICENSE) para mais detalhes.

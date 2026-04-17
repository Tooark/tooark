using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tooark.Exceptions;
using Tooark.Mediator.Abstractions;
using Tooark.Mediator.Handlers;
using Tooark.Mediator.Options;

namespace Tooark.Mediator.Injections;

/// <summary>
/// Classe para adicionar os serviços do Mediator ao container de injeção de dependência.
/// </summary>
public static partial class TooarkDependencyInjection
{
  /// <summary>
  /// Adiciona os serviços do Mediator ao container de injeção de dependência, escaneando os assemblies fornecidos para registrar os handlers.
  /// </summary>
  /// <remarks>
  /// Se nenhum assembly for fornecido, o assembly chamador será escaneado por padrão.
  /// </remarks>
  /// <param name="services">Coleção de serviços.</param>
  /// <param name="assemblies">Assemblies a serem escaneados para registrar os handlers do Mediator. Se nenhum for fornecido, o assembly chamador será usado.</param>
  /// <returns>A coleção de serviços com os serviços do Mediator adicionados.</returns>
  public static IServiceCollection AddTooarkMediator(
    this IServiceCollection services,
    params Assembly[] assemblies
  )
  {
    // Adiciona os serviços do Mediator ao container de injeção de dependência, usando uma configuração padrão (sem opções personalizadas)
    return services.AddTooarkMediator(_ => { }, assemblies);
  }

  /// <summary>
  /// Adiciona os serviços do Mediator ao container de injeção de dependência, permitindo
  /// configuração de options e escaneando os assemblies fornecidos para registrar os handlers.
  /// </summary>
  /// <param name="services">Coleção de serviços.</param>
  /// <param name="configure">Ação para configurar as opções do Mediator.</param>
  /// <param name="assemblies">Assemblies a serem escaneados para registrar os handlers do Mediator. Se nenhum for fornecido, o assembly chamador será usado.</param>
  /// <returns>A coleção de serviços com os serviços do Mediator adicionados.</returns>
  public static IServiceCollection AddTooarkMediator(
    this IServiceCollection services,
    Action<MediatorOptions> configure,
    params Assembly[] assemblies
  )
  {
    // Verifica se a coleção de serviços é nula
    if (services == null)
    {
      throw new InternalServerErrorException("Mediator.Null.Service");
    }

    // Verifica se a ação de configuração é nula
    if (configure == null)
    {
      throw new InternalServerErrorException("Mediator.Null.Configure");
    }

    // Cria uma instância de MediatorOptions e aplica a configuração fornecida
    var options = new MediatorOptions();
    configure(options);

    // Registra as opções do Mediator
    services.TryAddSingleton(options);

    // Registra o Mediator e suas interfaces (ISender e IPublisher) no container de injeção de dependência
    services.TryAddTransient<IMediator, Mediator>();
    services.TryAddTransient<ISender>(serviceProvider => serviceProvider.GetRequiredService<IMediator>());
    services.TryAddTransient<IPublisher>(serviceProvider => serviceProvider.GetRequiredService<IMediator>());

    // Determina os assemblies a serem escaneados para registrar os handlers do Mediator
    var assembliesToScan = assemblies.Length > 0 ? assemblies : [Assembly.GetCallingAssembly()];

    // Itera sobre os assemblies a serem escaneados e registra os handlers do Mediator encontrados em cada assembly
    foreach (var assembly in assembliesToScan.Distinct())
    {
      RegisterHandlersFromAssembly(services, assembly);
    }

    return services;
  }

  /// <summary>
  /// Registra os handlers do Mediator encontrados no assembly fornecido, associando-os às suas
  /// interfaces correspondentes (IRequestHandler, INotifyHandler, ICommandHandler, IQueryHandler).
  /// </summary>
  /// <param name="services">Coleção de serviços.</param>
  /// <param name="assembly">Assembly a ser escaneado para registrar os handlers do Mediator.</param>
  private static void RegisterHandlersFromAssembly(IServiceCollection services, Assembly assembly)
  {
    //
    var handlerInterfaces = new[]
    {
      typeof(IRequestHandler<,>),
      typeof(INotifyHandler<>),
      typeof(ICommandHandler<,>),
      typeof(ICommandHandler<>),
      typeof(IQueryHandler<,>)
    };

    // Obtém todas as classes concretas do assembly
    var implementations = assembly
      .GetTypes()
      .Where(type => type is { IsClass: true, IsAbstract: false });

    // Itera sobre as classes concretas
    foreach (var implementation in implementations)
    {
      // Obtém todas as interfaces genéricas implementadas pela classe concreta que correspondem aos handlers do Mediator
      var interfaces = implementation.GetInterfaces()
        .Where(@interface => @interface.IsGenericType)
        .Where(@interface => handlerInterfaces.Contains(@interface.GetGenericTypeDefinition()))
        .ToList();

      // Itera sobre as interfaces encontradas
      foreach (var @interface in interfaces)
      {
        // Registra a classe concreta como implementação da interface correspondente no container de injeção de dependência
        services.TryAddEnumerable(ServiceDescriptor.Transient(@interface, implementation));
      }
    }
  }
}

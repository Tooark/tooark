using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Tooark.Mediator.Abstractions;
using Tooark.Mediator.Handlers;

namespace Tooark.Mediator.Injections;

public static class TooarkDependencyInjection
{
  public static IServiceCollection AddTooarkMediator(this IServiceCollection services, params Assembly[] assemblies)
  {
    ArgumentNullException.ThrowIfNull(services);

    services.AddTransient<IMediator, Mediator>();

    var assembliesToScan = assemblies.Length > 0 ? assemblies : [Assembly.GetCallingAssembly()];

    foreach (var assembly in assembliesToScan.Distinct())
    {
      RegisterHandlersFromAssembly(services, assembly);
    }

    return services;
  }

  private static void RegisterHandlersFromAssembly(IServiceCollection services, Assembly assembly)
  {
    var handlerInterfaces = new[]
    {
      typeof(IRequestHandler<,>),
      typeof(INotificationHandler<>),
      typeof(ICommandHandler<,>),
      typeof(ICommandHandler<>),
      typeof(IQueryHandler<,>)
    };

    var implementations = assembly
      .GetTypes()
      .Where(type => type is { IsClass: true, IsAbstract: false });

    foreach (var implementation in implementations)
    {
      var interfaces = implementation.GetInterfaces()
        .Where(@interface => @interface.IsGenericType)
        .Where(@interface => handlerInterfaces.Contains(@interface.GetGenericTypeDefinition()))
        .ToList();

      foreach (var @interface in interfaces)
      {
        services.AddTransient(@interface, implementation);
      }
    }
  }
}

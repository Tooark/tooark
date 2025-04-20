# Filters

## No ASP.NET Core, você pode criar vários tipos de filtros para personalizar o comportamento das suas aplicações. Os principais tipos de filtros são

1. Filtros de Autorização (IAuthorizationFilter):

   Usados para verificar se o usuário está autorizado a acessar um recurso.

2. Filtros de Recurso (IResourceFilter):

   Executados antes e depois da execução do middleware. Podem ser usados para manipular o contexto da requisição.

3. Filtros de Ação (IActionFilter):

   Executados antes e depois da execução de uma ação do controlador. Podem ser usados para manipular os parâmetros da ação ou o resultado da ação.

4. Filtros de Exceção (IExceptionFilter):

   Usados para capturar e manipular exceções lançadas durante a execução de uma ação do controlador.

5. Filtros de Resultado (IResultFilter):

   Executados antes e depois da execução do resultado de uma ação. Podem ser usados para manipular o resultado da ação antes de ser enviado ao cliente.

## Diferenças em relação a Middleware

Sim, filtros e middlewares são conceitos diferentes no ASP.NET Core, embora ambos sejam usados para manipular requisições e respostas.

### Middleware

- **Middleware** é um componente que faz parte do pipeline de requisição/resposta do ASP.NET Core. Cada middleware pode processar a requisição, passar a requisição para o próximo middleware no pipeline ou interromper a cadeia de execução.
- Middlewares são configurados no método `Configure` da classe `Startup`.
- Exemplos de middlewares incluem autenticação, autorização, logging, roteamento, etc.

Exemplo de um middleware simples:

```csharp
public class CustomMiddleware
{
  private readonly RequestDelegate _next;

  public CustomMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  public async Task InvokeAsync(HttpContext context)
  {
    // Lógica antes de passar para o próximo middleware
    await context.Response.WriteAsync("Antes do próximo middleware.\n");

    await _next(context);

    // Lógica após o próximo middleware
    await context.Response.WriteAsync("Depois do próximo middleware.\n");
  }
}

// Registro do middleware no pipeline
public void Configure(IApplicationBuilder app)
{
  app.UseMiddleware<CustomMiddleware>();
}
```

### Filtros

- **Filtros** são usados para adicionar lógica antes ou depois da execução de ações do controlador. Eles são específicos para o MVC e são aplicados a controladores ou ações.
- Filtros são configurados no método `ConfigureServices` da classe `Startup` ou diretamente nos controladores/ações.
- Existem diferentes tipos de filtros, como filtros de autorização, filtros de recurso, filtros de ação, filtros de exceção e filtros de resultado.

Exemplo de um filtro de ação simples:

```csharp
public class CustomActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Lógica antes da execução da ação
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Lógica após a execução da ação
    }
}

// Registro do filtro globalmente
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers(options =>
    {
        options.Filters.Add<CustomActionFilter>();
    });
}
```

### Diferenças Principais

- **Escopo:** Middlewares são aplicados globalmente a todas as requisições, enquanto filtros são aplicados apenas a controladores e ações específicas.
- **Pipeline:** Middlewares fazem parte do pipeline de requisição/resposta, enquanto filtros são executados no contexto do MVC, antes ou depois das ações do controlador.
- **Configuração:** Middlewares são configurados no método `Configure`, enquanto filtros são configurados no método `ConfigureServices` ou diretamente nos controladores/ações.

Ambos são poderosos e úteis para diferentes cenários de manipulação de requisições e respostas em uma aplicação ASP.NET Core.

## AuthozireAttribute e AuthorizationFilter

Dado que você já tem um atributo `AuthorizeAttribute` que implementa `IAuthorizationFilter` e contém lógica de autorização personalizada, não há necessidade de adicionar um `AuthorizationFilter` separado, pois o `AuthorizeAttribute` já está cumprindo esse papel.

No entanto, se você deseja manter um `AuthorizationFilter` separado para casos de uso diferentes ou para reutilização em outros contextos, você pode adicionar lógica adicional ou diferente ao `AuthorizationFilter`. Aqui estão algumas ideias do que você pode adicionar ao `AuthorizationFilter`:

1. **Verificação de Regras de Negócio Específicas:** Adicione lógica para verificar regras de negócio específicas que não estão cobertas pelo `AuthorizeAttribute`.

2. **Logging:** Adicione lógica para registrar tentativas de acesso não autorizadas.

3. **Customização de Mensagens de Erro:** Adicione lógica para personalizar as mensagens de erro retornadas ao cliente.

Aqui está um exemplo atualizado do `AuthorizationFilter` com algumas dessas adições:

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Tooark.Filters
{
    public class CustomAuthorizationFilter : IAuthorizationFilter
    {
        private readonly ILogger<CustomAuthorizationFilter> _logger;

        public CustomAuthorizationFilter(ILogger<CustomAuthorizationFilter> logger)
        {
            _logger = logger;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Lógica de autorização personalizada
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                _logger.LogWarning("Acesso não autorizado: {Path}", context.HttpContext.Request.Path);
                context.Result = new JsonResult(new { message = "Usuário não autenticado" })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                return;
            }

            // Verificação de regras de negócio específicas
            if (!HasRequiredPermissions(context))
            {
                _logger.LogWarning("Acesso negado devido a permissões insuficientes: {Path}", context.HttpContext.Request.Path);
                context.Result = new JsonResult(new { message = "Permissões insuficientes" })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
                return;
            }
        }

        private bool HasRequiredPermissions(AuthorizationFilterContext context)
        {
            // Adicione a lógica para verificar as permissões necessárias
            // Exemplo: Verificar se o usuário tem uma permissão específica
            var userPermissions = context.HttpContext.User.Claims
                .Where(c => c.Type == "Permission")
                .Select(c => c.Value)
                .ToList();

            // Suponha que a rota requer uma permissão específica chamada "RequiredPermission"
            var requiredPermission = "RequiredPermission";
            return userPermissions.Contains(requiredPermission);
        }
    }
}
```

Este CustomAuthorizationFilter agora inclui:

- **Logging:** Registra tentativas de acesso não autorizadas.
- **Customização de Mensagens de Erro:** Retorna mensagens de erro personalizadas para o cliente.
- **Verificação de Regras de Negócio Específicas:** Verifica se o usuário tem as permissões necessárias.

Você pode registrar este filtro globalmente ou aplicá-lo a controladores ou ações específicas conforme necessário.

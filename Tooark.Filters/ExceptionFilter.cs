using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Tooark.Filters;

public class ExceptionFilter : IExceptionFilter
{
  public void OnException(ExceptionContext context)
  {
    // Verifica se a exceção é do tipo TooarkException
    if (context.Exception is TooarkException tooarkException)
    {
      // Define o código de status HTTP com base na exceção
      context.HttpContext.Response.StatusCode = (int)tooarkException.GetStatusCode();
      // Retorna uma resposta JSON com as mensagens de erro da exceção
      context.Result = new ObjectResult(new ResponseErrorMessagesJson
      {
        Errors = tooarkException.GetErrorMessages()
      });
    }
    else
    {
      // Define o código de status HTTP como 500 (Erro Interno do Servidor)
      context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
      // Retorna uma resposta JSON com uma mensagem de erro genérica
      context.Result = new ObjectResult(new ResponseErrorMessagesJson
      {
        Errors = ["Erro desconhecido!"]
      });
    }
  }
}

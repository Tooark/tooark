using System.Net;

namespace Tooark.Exceptions;

public class HttpClientException(HttpStatusCode statusCode, string content) : TooarkException(content)
{
  public HttpStatusCode StatusCode { get; private set; } = statusCode;
  public string Content { get; private set; } = content;

  public override List<string> GetErrorMessages()
  {
    return [Message, Content];
  }

  public override HttpStatusCode GetStatusCode()
  {
    return StatusCode;
  }
}

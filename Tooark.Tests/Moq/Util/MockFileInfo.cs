using Microsoft.Extensions.FileProviders;

namespace Tooark.Tests.Moq.Util;

public class MockFileInfo(string content) : IFileInfo
{
  private readonly string _content = content;

  public bool Exists => true;
  public long Length => _content.Length;
  public string PhysicalPath => null;
  public string Name => "MockFile";
  public DateTimeOffset LastModified => DateTimeOffset.Now;
  public bool IsDirectory => false;

  public Stream CreateReadStream()
  {
    return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(_content));
  }
}

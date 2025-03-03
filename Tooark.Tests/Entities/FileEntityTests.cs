using Tooark.Entities;
using Tooark.Enums;

namespace Tooark.Tests.Entities;

public class FileEntityTests
{
  // Uma classe concreta para testar FileEntity
  private class TestFileEntity : FileEntity
  {
    // Construtor padrão
    public TestFileEntity() { }

    // Construtor com parâmetros base
    public TestFileEntity(string fileName, string title, Guid createdBy) :
    base(fileName, title, createdBy)
    { }

    // Construtor com parâmetros e URL pública
    public TestFileEntity(string fileName, string title, string link, Guid createdBy) :
    base(fileName, title, link, createdBy)
    { }

    // Construtor com parâmetros, URL pública, formato e tipo
    public TestFileEntity(string fileName, string title, string link, string fileFormat, EFileType type, long size, Guid createdBy) :
    base(fileName, title, link, fileFormat, type, size, createdBy)
    { }
  }

  // Teste se o construtor atribui valores padrão às propriedades
  [Fact]
  public void Constructor_ShouldAssignDefaultValues()
  {
    // Arrange & Act
    var fileEntity = new TestFileEntity();

    // Assert
    Assert.True(fileEntity.IsValid);
    Assert.Null(fileEntity.FileName);
    Assert.Null(fileEntity.Title);
    Assert.Null(fileEntity.Link);
    Assert.Equal(Guid.Empty, fileEntity.CreatedBy);
  }

  // Teste se o construtor atribui valores padrão às propriedades com parâmetros base
  [Fact]
  public void Constructor_ShouldAssignValues_WhenValidParameters()
  {
    // Arrange
    var fileName = "https://example.com/file";
    var title = "Test File";
    var createdBy = Guid.NewGuid();

    // Act
    var fileEntity = new TestFileEntity(fileName, title, createdBy);

    // Assert
    Assert.True(fileEntity.IsValid);
    Assert.Equal(fileName, fileEntity.FileName);
    Assert.Equal(title, fileEntity.Title);
    Assert.Equal(fileName, fileEntity.Link);
    Assert.Equal(createdBy, fileEntity.CreatedBy);
  }

  // Teste se o construtor atribui valores padrão às propriedades com parâmetros e URL pública
  [Fact]
  public void Constructor_ShouldAssignValues_WhenValidParametersWithPublicUrl()
  {
    // Arrange
    var fileName = "https://example.com/file";
    var title = "Test File";
    var link = "https://example.com/publicfile";
    var createdBy = Guid.NewGuid();

    // Act
    var fileEntity = new TestFileEntity(fileName, title, link, createdBy);

    // Assert
    Assert.True(fileEntity.IsValid);
    Assert.Equal(fileName, fileEntity.FileName);
    Assert.Equal(title, fileEntity.Title);
    Assert.Equal(link, fileEntity.Link);
    Assert.Equal(createdBy, fileEntity.CreatedBy);
  }

  // Teste se o construtor atribui valores padrão às propriedades com parâmetros, URL pública, formato e tipo
  [Fact]
  public void Constructor_ShouldAssignValues_WhenValidParametersWithExtensionAndType()
  {
    // Arrange
    var fileName = "https://example.com/file";
    var title = "Test File";
    var link = "https://example.com/publicfile";
    var fileFormat = ".txt";
    var type = EFileType.Unknown;
    var size = 1024;
    var createdBy = Guid.NewGuid();

    // Act
    var fileEntity = new TestFileEntity(fileName, title, link, fileFormat, type, size, createdBy);

    // Assert
    Assert.Equal(fileName, fileEntity.FileName);
    Assert.Equal(title, fileEntity.Title);
    Assert.Equal(link, fileEntity.Link);
    Assert.Equal(fileFormat, fileEntity.FileFormat);
    Assert.Equal(type, fileEntity.Type);
    Assert.Equal(size, fileEntity.Size);
    Assert.Equal(createdBy, fileEntity.CreatedBy);
  }

  // Teste se o construtor atribui valores padrão às propriedades com parâmetros, URL pública, formato e tipo
  [Fact]
  public void Constructor_ShouldGenerateNotification_WhenInvalidFileUrl()
  {
    // Arrange
    var fileName = "invalid-url";
    var title = "Test File";
    var createdBy = Guid.NewGuid();

    // Act
    var fileEntity = new TestFileEntity(fileName, title, createdBy);

    // Assert
    Assert.False(fileEntity.IsValid);
    Assert.Equal("Field.Invalid;FileName", fileEntity.Notifications.First());
    Assert.Null(fileEntity.FileName);
    Assert.Null(fileEntity.Title);
    Assert.Null(fileEntity.Link);
    Assert.Null(fileEntity.FileFormat);
    Assert.Equal(EFileType.Unknown, fileEntity.Type);
    Assert.Equal(0, fileEntity.Size);
    Assert.Equal(createdBy, fileEntity.CreatedBy);
  }

  // Teste se o construtor atribui valores padrão quando o nome é inválido
  [Fact]
  public void Constructor_ShouldGenerateNotification_WhenNameIsEmpty()
  {
    // Arrange
    var fileName = "https://example.com/file";
    var title = "";
    var createdBy = Guid.NewGuid();

    // Act
    var fileEntity = new TestFileEntity(fileName, title, createdBy);

    // Assert
    Assert.False(fileEntity.IsValid);
    Assert.Contains("Field.Required;Title", fileEntity.Notifications.First());
    Assert.Null(fileEntity.FileName);
    Assert.Null(fileEntity.Title);
    Assert.Null(fileEntity.Link);
    Assert.Null(fileEntity.FileFormat);
    Assert.Equal(EFileType.Unknown, fileEntity.Type);
    Assert.Equal(0, fileEntity.Size);
    Assert.Equal(createdBy, fileEntity.CreatedBy);

  }

  // Teste se o construtor atribui valores padrão quando a URL pública é inválida
  [Fact]
  public void Constructor_ShouldGenerateNotification_WhenPublicUrlIsInvalid()
  {
    // Arrange
    var fileName = "https://example.com/file";
    var title = "Test File";
    var link = "invalid-url";
    var createdBy = Guid.NewGuid();

    // Act
    var fileEntity = new TestFileEntity(fileName, title, link, createdBy);

    // Assert
    Assert.False(fileEntity.IsValid);
    Assert.Equal("Field.Invalid;Link", fileEntity.Notifications.First());
    Assert.Null(fileEntity.FileName);
    Assert.Null(fileEntity.Title);
    Assert.Null(fileEntity.Link);
    Assert.Null(fileEntity.FileFormat);
    Assert.Equal(EFileType.Unknown, fileEntity.Type);
    Assert.Equal(0, fileEntity.Size);
    Assert.Equal(createdBy, fileEntity.CreatedBy);
  }

  // Teste se o construtor atribui valores padrão quando o formato do arquivo é inválido
  [Fact]
  public void Constructor_ShouldGenerateNotification_WhenFileFormatIsInvalid()
  {
    // Arrange
    var fileName = "https://example.com/file";
    var title = "Test File";
    var link = "https://example.com/file";
    var fileFormat = "";
    var type = EFileType.Unknown;
    var size = 1024;
    var createdBy = Guid.NewGuid();

    // Act
    var fileEntity = new TestFileEntity(fileName, title, link, fileFormat, type, size, createdBy);

    // Assert
    Assert.False(fileEntity.IsValid);
    Assert.Equal("Field.Required;FileFormat", fileEntity.Notifications.First());
    Assert.Null(fileEntity.FileName);
    Assert.Null(fileEntity.Title);
    Assert.Null(fileEntity.Link);
    Assert.Null(fileEntity.FileFormat);
    Assert.Equal(EFileType.Unknown, fileEntity.Type);
    Assert.Equal(size, fileEntity.Size);
    Assert.Equal(createdBy, fileEntity.CreatedBy);
  }
}

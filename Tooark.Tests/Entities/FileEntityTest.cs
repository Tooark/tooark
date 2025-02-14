using Tooark.Entities;
using Tooark.Enums;

namespace Tooark.Tests.Entities;

public class FileEntityTest
{
  // Uma classe concreta para testar FileEntity
  private class TestFileEntity : FileEntity
  {
    // Construtor padrão
    public TestFileEntity() { }

    // Construtor com parâmetros base
    public TestFileEntity(string fileUrl, string name, Guid createdBy) :
    base(fileUrl, name, createdBy)
    { }

    // Construtor com parâmetros e URL pública
    public TestFileEntity(string fileUrl, string name, string publicUrl, Guid createdBy) :
    base(fileUrl, name, publicUrl, createdBy)
    { }

    // Construtor com parâmetros, URL pública, formato e tipo
    public TestFileEntity(string fileUrl, string name, string publicUrl, string fileFormat, EFileType type, Guid createdBy) :
    base(fileUrl, name, publicUrl, fileFormat, type, createdBy)
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
    Assert.Null(fileEntity.FileUrl);
    Assert.Null(fileEntity.Name);
    Assert.Null(fileEntity.PublicUrl);
    Assert.Equal(Guid.Empty, fileEntity.CreatedBy);
  }

  // Teste se o construtor atribui valores padrão às propriedades com parâmetros base
  [Fact]
  public void Constructor_ShouldAssignValues_WhenValidParameters()
  {
    // Arrange
    var fileUrl = "https://example.com/file";
    var name = "Test File";
    var createdBy = Guid.NewGuid();

    // Act
    var fileEntity = new TestFileEntity(fileUrl, name, createdBy);

    // Assert
    Assert.True(fileEntity.IsValid);
    Assert.Equal(fileUrl, fileEntity.FileUrl);
    Assert.Equal(name, fileEntity.Name);
    Assert.Equal(fileUrl, fileEntity.PublicUrl);
    Assert.Equal(createdBy, fileEntity.CreatedBy);
  }

  // Teste se o construtor atribui valores padrão às propriedades com parâmetros e URL pública
  [Fact]
  public void Constructor_ShouldAssignValues_WhenValidParametersWithPublicUrl()
  {
    // Arrange
    var fileUrl = "https://example.com/file";
    var name = "Test File";
    var publicUrl = "https://example.com/publicfile";
    var createdBy = Guid.NewGuid();

    // Act
    var fileEntity = new TestFileEntity(fileUrl, name, publicUrl, createdBy);

    // Assert
    Assert.True(fileEntity.IsValid);
    Assert.Equal(fileUrl, fileEntity.FileUrl);
    Assert.Equal(name, fileEntity.Name);
    Assert.Equal(publicUrl, fileEntity.PublicUrl);
    Assert.Equal(createdBy, fileEntity.CreatedBy);
  }

  // Teste se o construtor atribui valores padrão às propriedades com parâmetros, URL pública, formato e tipo
  [Fact]
  public void Constructor_ShouldAssignValues_WhenValidParametersWithExtensionAndType()
  {
    // Arrange
    var fileUrl = "https://example.com/file";
    var name = "Test File";
    var publicUrl = "https://example.com/publicfile";
    var fileFormat = ".txt";
    var type = EFileType.Unknown;
    var createdBy = Guid.NewGuid();

    // Act
    var fileEntity = new TestFileEntity(fileUrl, name, publicUrl, fileFormat, type, createdBy);

    // Assert
    Assert.Equal(fileUrl, fileEntity.FileUrl);
    Assert.Equal(name, fileEntity.Name);
    Assert.Equal(publicUrl, fileEntity.PublicUrl);
    Assert.Equal(fileFormat, fileEntity.FileFormat);
    Assert.Equal(type, fileEntity.Type);
    Assert.Equal(createdBy, fileEntity.CreatedBy);
  }

  // Teste se o construtor atribui valores padrão às propriedades com parâmetros, URL pública, formato e tipo
  [Fact]
  public void Constructor_ShouldGenerateNotification_WhenInvalidFileUrl()
  {
    // Arrange
    var fileUrl = "invalid-url";
    var name = "Test File";
    var createdBy = Guid.NewGuid();

    // Act
    var fileEntity = new TestFileEntity(fileUrl, name, createdBy);

    // Assert
    Assert.False(fileEntity.IsValid);
    Assert.Equal("Field.Invalid;FileUrl", fileEntity.Notifications.First());
    Assert.Null(fileEntity.FileUrl);
    Assert.Null(fileEntity.Name);
    Assert.Null(fileEntity.PublicUrl);
    Assert.Null(fileEntity.FileFormat);
    Assert.Equal(EFileType.Unknown, fileEntity.Type);
    Assert.Equal(createdBy, fileEntity.CreatedBy);
  }

  // Teste se o construtor atribui valores padrão quando o nome é inválido
  [Fact]
  public void Constructor_ShouldGenerateNotification_WhenNameIsEmpty()
  {
    // Arrange
    var fileUrl = "https://example.com/file";
    var name = "";
    var createdBy = Guid.NewGuid();

    // Act
    var fileEntity = new TestFileEntity(fileUrl, name, createdBy);

    // Assert
    Assert.False(fileEntity.IsValid);
    Assert.Contains("Field.Required;Name", fileEntity.Notifications.First());
    Assert.Null(fileEntity.FileUrl);
    Assert.Null(fileEntity.Name);
    Assert.Null(fileEntity.PublicUrl);
    Assert.Null(fileEntity.FileFormat);
    Assert.Equal(EFileType.Unknown, fileEntity.Type);
    Assert.Equal(createdBy, fileEntity.CreatedBy);

  }

  // Teste se o construtor atribui valores padrão quando a URL pública é inválida
  [Fact]
  public void Constructor_ShouldGenerateNotification_WhenPublicUrlIsInvalid()
  {
    // Arrange
    var fileUrl = "https://example.com/file";
    var name = "Test File";
    var publicUrl = "invalid-url";
    var createdBy = Guid.NewGuid();

    // Act
    var fileEntity = new TestFileEntity(fileUrl, name, publicUrl, createdBy);

    // Assert
    Assert.False(fileEntity.IsValid);
    Assert.Equal("Field.Invalid;PublicUrl", fileEntity.Notifications.First());
    Assert.Null(fileEntity.FileUrl);
    Assert.Null(fileEntity.Name);
    Assert.Null(fileEntity.PublicUrl);
    Assert.Null(fileEntity.FileFormat);
    Assert.Equal(EFileType.Unknown, fileEntity.Type);
    Assert.Equal(createdBy, fileEntity.CreatedBy);
  }

  // Teste se o construtor atribui valores padrão quando o formato do arquivo é inválido
  [Fact]
  public void Constructor_ShouldGenerateNotification_WhenFileFormatIsInvalid()
  {
    // Arrange
    var fileUrl = "https://example.com/file";
    var name = "Test File";
    var publicUrl = "https://example.com/file";
    var fileFormat = "";
    var type = EFileType.Unknown;
    var createdBy = Guid.NewGuid();

    // Act
    var fileEntity = new TestFileEntity(fileUrl, name, publicUrl, fileFormat, type, createdBy);

    // Assert
    Assert.False(fileEntity.IsValid);
    Assert.Equal("Field.Required;FileFormat", fileEntity.Notifications.First());
    Assert.Null(fileEntity.FileUrl);
    Assert.Null(fileEntity.Name);
    Assert.Null(fileEntity.PublicUrl);
    Assert.Null(fileEntity.FileFormat);
    Assert.Equal(EFileType.Unknown, fileEntity.Type);
    Assert.Equal(createdBy, fileEntity.CreatedBy);
  }
}

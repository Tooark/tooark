using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Tooark.DTOs;
using Tooark.Tests.Moq.Model.Person;

namespace Tooark.Tests.DTOs;

public class RabbitMQMessageDtoTests
{
  private static readonly JsonSerializerOptions Options = new()
  {
    ReferenceHandler = ReferenceHandler.Preserve,
    WriteIndented = false,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
  };

  // RabbitMQMessageDto converte em string json, retorna json válido com tipo int
  [Theory]
  [InlineData("Test1", 12345)]
  [InlineData("Test2", 67890)]
  [InlineData("Test3", -1032)]
  [InlineData("Test4", -9867)]
  public void RabbitMQMessageDto_ConvertToJSONString_ReturnsValidJSON_WithIntType(string title, int value)
  {
    // Arrange
    var messageDtoInt = new RabbitMQMessageDto<int>(title, value);

    // Act
    string jsonStringInt = messageDtoInt;

    // Assert
    Assert.Contains($"\"Title\":\"{title}\"", jsonStringInt);
    Assert.Contains($"\"Message\":\"{value}\"", jsonStringInt);
  }

  // RabbitMQMessageDto converte em string json, retorna json válido com tipo string
  [Theory]
  [InlineData("Test1", "abc")]
  [InlineData("Test2", "def")]
  [InlineData("Test3", "ghi")]
  [InlineData("Test4", "jkl")]
  public void RabbitMQMessageDto_ConvertToJSONString_ReturnsValidJSON_WithStringType(string title, string value)
  {
    // Arrange
    var messageDtoString = new RabbitMQMessageDto<string>(title, value);

    // Act
    string jsonStringString = messageDtoString;

    // Assert
    Assert.Contains($"\"Title\":\"{title}\"", jsonStringString);
    Assert.Contains($"\"Message\":\"\\\"{value}\\\"\"", jsonStringString);
  }

  // RabbitMQMessageDto converte em string json, retorna json válido com tipo boolean
  [Theory]
  [InlineData("Test1", true)]
  [InlineData("Test2", false)]
  public void RabbitMQMessageDto_ConvertToJSONString_ReturnsValidJSON_WithBooleanType(string title, bool value)
  {
    // Arrange
    var messageDtoBool = new RabbitMQMessageDto<bool>(title, value);

    // Act
    string jsonStringBool = messageDtoBool;

    // Assert
    Assert.Contains($"\"Title\":\"{title}\"", jsonStringBool);
    Assert.Contains($"\"Message\":\"{value.ToString().ToLower()}\"", jsonStringBool);
  }

  // RabbitMQMessageDto converte em string json, retorna json válido com tipo objeto Person
  [Theory]
  [InlineData("abc", 123)]
  [InlineData("def", 456)]
  [InlineData("ghi", 789)]
  [InlineData("jkl", -10)]
  public void RabbitMQMessageDto_ConvertToJSONString_ReturnsValidJSON_WithBooleanPerson(string name, int age)
  {
    // Arrange
    var person = new Person(name, age);
    string personString = JsonSerializer.Serialize(person, Options);

    // Act
    string messageDto = new RabbitMQMessageDto<Person>("TestTitle", person);
    string messageValid = JsonSerializer.Serialize(personString, Options);

    // Assert
    Assert.Contains($"\"Title\":\"TestTitle\"", messageDto);
    Assert.Contains($"\"Message\":{messageValid}", messageDto);
  }

  // RabbitMQMessageDto converte de string json, retorna objeto int válido
  [Theory]
  [InlineData("{\"Title\":\"TestTitle\",\"Message\":\"12345\"}", 12345)]
  [InlineData("{\"Title\":\"TestTitle\",\"Message\":\"67890\"}", 67890)]
  [InlineData("{\"Title\":\"TestTitle\",\"Message\":\"-1032\"}", -1032)]
  [InlineData("{\"Title\":\"TestTitle\",\"Message\":\"-9867\"}", -9867)]
  public void RabbitMQMessageDto_ConvertFromJSONString_ReturnsValidObjectInt(string value, int valid)
  {
    // Arrange
    string jsonInt = value;
    string validString = JsonSerializer.Serialize(valid, Options);

    // Act
    RabbitMQMessageDto<int> messageDto = jsonInt;

    // Assert
    Assert.Equal("TestTitle", messageDto.Title);
    Assert.Equal(validString, messageDto.Message);
  }

  // RabbitMQMessageDto converte de string json, retorna objeto string válido
  [Theory]
  [InlineData("{\"Title\":\"TestTitle\",\"Message\":\"\\\"abc\\\"\"}", "abc")]
  [InlineData("{\"Title\":\"TestTitle\",\"Message\":\"\\\"def\\\"\"}", "def")]
  [InlineData("{\"Title\":\"TestTitle\",\"Message\":\"\\\"ghi\\\"\"}", "ghi")]
  [InlineData("{\"Title\":\"TestTitle\",\"Message\":\"\\\"jkl\\\"\"}", "jkl")]
  public void RabbitMQMessageDto_ConvertFromJSONString_ReturnsValidObjectString(string value, string valid)
  {
    // Arrange
    string jsonString = value;
    string validString = JsonSerializer.Serialize(valid, Options);

    // Act
    RabbitMQMessageDto<string> messageDto = jsonString;

    // Assert
    Assert.Equal("TestTitle", messageDto.Title);
    Assert.Equal(validString, messageDto.Message);
  }


  // RabbitMQMessageDto converte de string json, retorna objeto boolean válido
  [Theory]
  [InlineData("{\"Title\":\"TestTitle\",\"Message\":\"true\"}", true)]
  [InlineData("{\"Title\":\"TestTitle\",\"Message\":\"false\"}", false)]
  public void RabbitMQMessageDto_ConvertFromJSONString_ReturnsValidObjectBoolean(string value, bool valid)
  {
    // Arrange
    string jsonString = value;
    string validString = JsonSerializer.Serialize(valid, Options);

    // Act
    RabbitMQMessageDto<bool> messageDto = jsonString;

    // Assert
    Assert.Equal("TestTitle", messageDto.Title);
    Assert.Equal(validString, messageDto.Message);
  }

  // RabbitMQMessageDto converte de string json, retorna objeto Person válido
  [Theory]
  [InlineData("{\"Title\":\"TestTitle\",\"Message\":\"{\\\"Name\\\":\\\"abc\\\",\\\"Age\\\":123}\"}", "abc", 123)]
  [InlineData("{\"Title\":\"TestTitle\",\"Message\":\"{\\\"Name\\\":\\\"def\\\",\\\"Age\\\":456}\"}", "def", 456)]
  [InlineData("{\"Title\":\"TestTitle\",\"Message\":\"{\\\"Name\\\":\\\"ghi\\\",\\\"Age\\\":789}\"}", "ghi", 789)]
  [InlineData("{\"Title\":\"TestTitle\",\"Message\":\"{\\\"Name\\\":\\\"jkl\\\",\\\"Age\\\":-10}\"}", "jkl", -10)]
  public void RabbitMQMessageDto_ConvertFromJSONString_ReturnsValidObjectPerson(string value, string validName, int validAge)
  {
    // Arrange
    string jsonString = value;
    var validPerson = JsonSerializer.Serialize(new Person(validName, validAge), Options);

    // Act
    RabbitMQMessageDto<Person> messageDto = jsonString;

    // Assert
    Assert.Equal("TestTitle", messageDto.Title);
    Assert.Equal(validPerson, messageDto.Message);
  }

  // RabbitMQMessageDto as opções de serialização são consistentes
  [Fact]
  public void RabbitMQMessageDto_SerializationOptions_AreConsistent()
  {
    // Arrange
    var messageDto = new RabbitMQMessageDto<string>("TestTitle", "TestData");
    string jsonString = messageDto;

    // Act
    RabbitMQMessageDto<string> deserializedMessageDto = jsonString;

    // Assert
    Assert.Equal(messageDto.Title, deserializedMessageDto.Title);
    Assert.Equal(messageDto.Message, deserializedMessageDto.Message);
  }

  // RabbitMQMessageDto desserializar json inválido lança exceção json
  [Fact]
  public void RabbitMQMessageDto_DeserializeInvalidJSON_ThrowsJsonException()
  {
    // Arrange
    string invalidJsonString = "{\"Title\":\"TestTitle\",\"Message\":}";

    // Act & Assert
    Assert.Throws<JsonException>(() => (RabbitMQMessageDto<string>)invalidJsonString);
  }

  // RabbitMQMessageDto converter de string json inválida lança exceção de operação inválida
  [Fact]
  public void RabbitMQMessageDto_ConvertFromInvalidJSONString_ThrowsInvalidOperationException()
  {
    // Arrange
    string invalidJsonString = "invalid json";

    // Act & Assert
    Assert.Throws<JsonException>(() => (RabbitMQMessageDto<string>)invalidJsonString);
  }

  // GetData retorna dados desserializados quando a mensagem é válida Json
  [Fact]
  public void GetData_ReturnsDeserializedData_WhenMessageIsValidJson()
  {
    // Arrange
    string title = "TestTitle";
    string data = "TestData";
    var messageDto = new RabbitMQMessageDto<string>(title, data);

    // Act
    string result = messageDto.GetData();

    // Assert
    Assert.Equal("TestData", result);
  }

  // GetData lança exceção de operação inválida quando a mensagem é json inválida
  [Fact]
  public void GetData_ThrowsInvalidOperationException_WhenMessageIsInvalidJson()
  {
    // Arrange
    string title = "TestTitle";
    string invalidData = null!;
    var messageDto = new RabbitMQMessageDto<string>(title, invalidData);

    // Act & Assert
    Assert.Throws<InvalidOperationException>(() => messageDto.GetData());
  }
}

using Tooark.Extensions;

namespace Tooark.Tests.Extensions;

public class StringExtensionsTests
{
  // Um método de teste para verificar se a função ToNormalize retorna uma string vazia se a entrada for nula ou vazia
  [Fact]
  public void ToNormalize_ShouldReturnEmptyString_WhenInputIsNullEmpty()
  {
    // Arrange
    string input = null!;
    string expected = "";

    // Act
    string result = input.ToNormalize();

    // Assert
    Assert.Equal(expected, result);
  }

  // Um método de teste para verificar se a função ToNormalize remove os espaços da entrada
  [Fact]
  public void ToNormalize_ShouldRemoveSpaces_WhenInputHasSpaces()
  {
    // Arrange
    string input = "Hello World";
    string expected = "HELLOWORLD";

    // Act
    string result = input.ToNormalize();

    // Assert
    Assert.Equal(expected, result);
  }

  // Um método de teste para verificar se a função ToNormalize converte a entrada para minúsculas
  [Fact]
  public void ToNormalize_ShouldConvertToLowercase_WhenInputHasUppercase()
  {
    // Arrange
    string input = "HELLO WORLD";
    string expected = "HELLOWORLD";

    // Act
    string result = input.ToNormalize();

    // Assert
    Assert.Equal(expected, result);
  }

  // Um método de teste para verificar se a função ToNormalize substitui os caracteres especiais por caracteres sem acentuação
  [Fact]
  public void ToNormalize_ShouldReplaceSpecialCharacters_WhenInputHasSpecialCharacters()
  {
    // Arrange
    string input = "Ça va? Je m'appelle Tooark.";
    string expected = "CAVAJEMAPPELLETOOARK";

    // Act
    string result = input.ToNormalize();

    // Assert
    Assert.Equal(expected, result);
  }

  // Um método de teste para verificar se a função ToNormalize ignora os caracteres que não são números ou letras minúsculas
  [Fact]
  public void ToNormalize_ShouldIgnoreOtherCharacters_WhenInputHasOtherCharacters()
  {
    // Arrange
    string input = "Hello, I'm Tooark! 123.";
    string expected = "HELLOIMTOOARK123";

    // Act
    string result = input.ToNormalize();

    // Assert
    Assert.Equal(expected, result);
  }

  // Uma lista de 48 testes com valores de entrada e resultados esperados gerados aleatoriamente
  [Theory]
  [InlineData(null, "")]
  [InlineData("", "")]
  [InlineData(" ", "")]
  [InlineData("  ", "")]
  [InlineData("Hello World", "HELLOWORLD")]
  [InlineData("Ç Ñ Ÿ Ý Â Ä À Å Á Ã", "CNYYAAAAAA")]
  [InlineData("É Ê Ë È Ï Î Ì Í Ô Ö Ò Ó Õ Ü Û Ù Ú", "EEEEIIIIOOOOOUUUU")]
  [InlineData("123 ABC", "123ABC")]
  [InlineData("!@#$%^&*()_+", "")]
  [InlineData("çñÿâäàåáãéêëèïîìíôöòóðõüûùú", "CNYAAAAAAEEEEIIIIOOOOOUUUU")]
  [InlineData("123 abc ÇÑÝÂÄÀÅÁÃÉÊËÈÏÎÌÍÔÖÒÓÐÕÜÛÙÚ", "123ABCCNYAAAAAAEEEEIIIIOOOOOUUUU")]
  [InlineData("Olá, eu sou o Tooark!", "OLAEUSOUOTOOARK")]
  [InlineData("¿Qué tal? Me llamo Tooark.", "QUETALMELLAMOTOOARK")]
  [InlineData("Wie geht's? Ich heiße Tooark.", "WIEGEHTSICHHEIETOOARK")]
  [InlineData("こんにちは、私はTooarkです。", "TOOARK")]
  [InlineData("你好，我叫Tooark。", "TOOARK")]
  [InlineData("안녕하세요, 저는 Tooark입니다.", "TOOARK")]
  [InlineData("Привет, я Tooark.", "TOOARK")]
  [InlineData("Cześć, nazywam się Tooark.", "CZENAZYWAMSITOOARK")]
  [InlineData("Hello, I'm Tooark.", "HELLOIMTOOARK")]
  [InlineData("Γεια σας, είμαι ο Tooark.", "TOOARK")]
  [InlineData("Bună, eu sunt Tooark.", "BUNEUSUNTTOOARK")]
  [InlineData("Sveiki, aš esu Tooark.", "SVEIKIAESUTOOARK")]
  [InlineData("Здравейте, аз съм Tooark.", "TOOARK")]
  [InlineData("Sawubona, ngiyi-Tooark.", "SAWUBONANGIYITOOARK")]
  [InlineData("Shwmae, fi yw Tooark.", "SHWMAEFIYWTOOARK")]
  [InlineData("สวัสดีครับ ผมชื่อ Tooark", "TOOARK")]
  [InlineData("नमस्ते, मेरा नाम Tooark है।", "TOOARK")]
  [InlineData("नमस्कार, माझे नाव Tooark आहे.", "TOOARK")]
  [InlineData("નમસ્તે, મારું નામ Tooark છે.", "TOOARK")]
  [InlineData("ਸਤਿ ਸ੍ਰੀ ਅਕਾਲ, ਮੇਰਾ ਨਾਮ Tooark ਹੈ.", "TOOARK")]
  [InlineData("வணக்கம், என் பெயர் Tooark.", "TOOARK")]
  [InlineData("నమస్కారం, నా పేరు Tooark.", "TOOARK")]
  [InlineData("ನಮಸ್ಕಾರ, ನನ್ನ ಹೆಸರು Tooark.", "TOOARK")]
  [InlineData("നമസ്കാരം, എന്റെ പേര് Tooark.", "TOOARK")]
  [InlineData("হ্যালো, আমার নাম Tooark.", "TOOARK")]
  [InlineData("sdùVWMäwúý", "SDUVWMAWUY")]
  [InlineData("ûëlbsSô1óv", "UELBSSO1OV")]
  [InlineData("33dòïêgDQR", "33DOIEGDQR")]
  [InlineData("0óaïJnYëMí", "0OAIJNYEMI")]
  [InlineData(" Nð 139 Qli ", "N139QLI")]
  [InlineData("ÿm0oeTEmbã", "YM0OETEMBA")]
  [InlineData("äCjqåqjVZq", "ACJQAQJVZQ")]
  [InlineData("Tf4îJåzàge", "TF4IJAZAGE")]
  [InlineData("ù1nCÿ0ú7éW", "U1NCY0U7EW")]
  [InlineData("òî7gåP0siH", "OI7GAP0SIH")]
  [InlineData("SB8làbhûH1", "SB8LABHUH1")]
  [InlineData(" âdçd51KU8 ", "ADCD51KU8")]
  public void ToNormalize_ReturnsExpectedResult(string? input, string expected)
  {
    // Arrange & Act
    string result = input!.ToNormalize();

    // Assert
    Assert.Equal(expected, result);
  }

  // Um método de teste para verificar se a função ToNormalizeRegex retorna uma string vazia se a entrada for nula ou vazia
  [Fact]
  public void ToNormalizeRegex_ShouldReturnEmptyString_WhenInputIsNullEmpty()
  {
    // Arrange
    string input = null!;
    string expected = "";

    // Act
    string result = input.ToNormalizeRegex();

    // Assert
    Assert.Equal(expected, result);
  }

  // Um método de teste para verificar se a função ToNormalizeRegex remove os espaços da entrada
  [Fact]
  public void ToNormalizeRegex_ShouldRemoveSpaces_WhenInputHasSpaces()
  {
    // Arrange
    string input = "Hello World";
    string expected = "HELLOWORLD";

    // Act
    string result = input.ToNormalizeRegex();

    // Assert
    Assert.Equal(expected, result);
  }

  // Um método de teste para verificar se a função ToNormalizeRegex converte a entrada para minúsculas
  [Fact]
  public void ToNormalizeRegex_ShouldConvertToLowercase_WhenInputHasUppercase()
  {
    // Arrange
    string input = "HELLO WORLD";
    string expected = "HELLOWORLD";

    // Act
    string result = input.ToNormalizeRegex();

    // Assert
    Assert.Equal(expected, result);
  }

  // Um método de teste para verificar se a função ToNormalizeRegex substitui os caracteres especiais por caracteres sem acentuação
  [Fact]
  public void ToNormalizeRegex_ShouldReplaceSpecialCharacters_WhenInputHasSpecialCharacters()
  {
    // Arrange
    string input = "Ça va? Je m'appelle Tooark.";
    string expected = "CAVAJEMAPPELLETOOARK";

    // Act
    string result = input.ToNormalizeRegex();

    // Assert
    Assert.Equal(expected, result);
  }

  // Um método de teste para verificar se a função ToNormalizeRegex ignora os caracteres que não são números ou letras minúsculas
  [Fact]
  public void ToNormalizeRegex_ShouldIgnoreOtherCharacters_WhenInputHasOtherCharacters()
  {
    // Arrange
    string input = "Hello, I'm Tooark! 123.";
    string expected = "HELLOIMTOOARK123";

    // Act
    string result = input.ToNormalizeRegex();

    // Assert
    Assert.Equal(expected, result);
  }

  // Uma lista de 48 testes com valores de entrada e resultados esperados gerados aleatoriamente
  [Theory]
  [InlineData(null, "")]
  [InlineData("", "")]
  [InlineData(" ", "")]
  [InlineData("  ", "")]
  [InlineData("Hello World", "HELLOWORLD")]
  [InlineData("Ç Ñ Ÿ Ý Â Ä À Å Á Ã", "CNYYAAAAAA")]
  [InlineData("É Ê Ë È Ï Î Ì Í Ô Ö Ò Ó Õ Ü Û Ù Ú", "EEEEIIIIOOOOOUUUU")]
  [InlineData("123 ABC", "123ABC")]
  [InlineData("!@#$%^&*()_+", "")]
  [InlineData("çñÿâäàåáãéêëèïîìíôöòóðõüûùú", "CNYAAAAAAEEEEIIIIOOOOOUUUU")]
  [InlineData("123 abc ÇÑÝÂÄÀÅÁÃÉÊËÈÏÎÌÍÔÖÒÓÐÕÜÛÙÚ", "123ABCCNYAAAAAAEEEEIIIIOOOOOUUUU")]
  [InlineData("Olá, eu sou o Tooark!", "OLAEUSOUOTOOARK")]
  [InlineData("¿Qué tal? Me llamo Tooark.", "QUETALMELLAMOTOOARK")]
  [InlineData("Wie geht's? Ich heiße Tooark.", "WIEGEHTSICHHEIETOOARK")]
  [InlineData("こんにちは、私はTooarkです。", "TOOARK")]
  [InlineData("你好，我叫Tooark。", "TOOARK")]
  [InlineData("안녕하세요, 저는 Tooark입니다.", "TOOARK")]
  [InlineData("Привет, я Tooark.", "TOOARK")]
  [InlineData("Cześć, nazywam się Tooark.", "CZENAZYWAMSITOOARK")]
  [InlineData("Hello, I'm Tooark.", "HELLOIMTOOARK")]
  [InlineData("Γεια σας, είμαι ο Tooark.", "TOOARK")]
  [InlineData("Bună, eu sunt Tooark.", "BUNEUSUNTTOOARK")]
  [InlineData("Sveiki, aš esu Tooark.", "SVEIKIAESUTOOARK")]
  [InlineData("Здравейте, аз съм Tooark.", "TOOARK")]
  [InlineData("Sawubona, ngiyi-Tooark.", "SAWUBONANGIYITOOARK")]
  [InlineData("Shwmae, fi yw Tooark.", "SHWMAEFIYWTOOARK")]
  [InlineData("สวัสดีครับ ผมชื่อ Tooark", "TOOARK")]
  [InlineData("नमस्ते, मेरा नाम Tooark है।", "TOOARK")]
  [InlineData("नमस्कार, माझे नाव Tooark आहे.", "TOOARK")]
  [InlineData("નમસ્તે, મારું નામ Tooark છે.", "TOOARK")]
  [InlineData("ਸਤਿ ਸ੍ਰੀ ਅਕਾਲ, ਮੇਰਾ ਨਾਮ Tooark ਹੈ.", "TOOARK")]
  [InlineData("வணக்கம், என் பெயர் Tooark.", "TOOARK")]
  [InlineData("నమస్కారం, నా పేరు Tooark.", "TOOARK")]
  [InlineData("ನಮಸ್ಕಾರ, ನನ್ನ ಹೆಸರು Tooark.", "TOOARK")]
  [InlineData("നമസ്കാരം, എന്റെ പേര് Tooark.", "TOOARK")]
  [InlineData("হ্যালো, আমার নাম Tooark.", "TOOARK")]
  [InlineData("sdùVWMäwúý", "SDUVWMAWUY")]
  [InlineData("ûëlbsSô1óv", "UELBSSO1OV")]
  [InlineData("33dòïêgDQR", "33DOIEGDQR")]
  [InlineData("0óaïJnYëMí", "0OAIJNYEMI")]
  [InlineData(" Nð 139 Qli ", "N139QLI")]
  [InlineData("ÿm0oeTEmbã", "YM0OETEMBA")]
  [InlineData("äCjqåqjVZq", "ACJQAQJVZQ")]
  [InlineData("Tf4îJåzàge", "TF4IJAZAGE")]
  [InlineData("ù1nCÿ0ú7éW", "U1NCY0U7EW")]
  [InlineData("òî7gåP0siH", "OI7GAP0SIH")]
  [InlineData("SB8làbhûH1", "SB8LABHUH1")]
  [InlineData(" âdçd51KU8 ", "ADCD51KU8")]
  public void ToNormalizeRegex_ReturnsExpectedResult(string? input, string expected)
  {
    // Arrange & Act
    string result = input!.ToNormalizeRegex();

    // Assert
    Assert.Equal(expected, result);
  }

  // Um método de teste para verificar se a função ToPascalCase converte SnakeCase para PascalCase
  [Theory]
  [InlineData("_", "")]
  [InlineData("t", "T")]
  [InlineData("_t", "T")]
  [InlineData("t_", "T")]
  [InlineData("_hello_tooark", "HelloTooark")]
  [InlineData("hello_tooark_", "HelloTooark")]
  [InlineData("hello__tooark", "HelloTooark")]
  [InlineData("_hello_package_tooark", "HelloPackageTooark")]
  [InlineData("hello_package_tooark_", "HelloPackageTooark")]
  [InlineData("hello__package_tooark", "HelloPackageTooark")]
  public void ToPascalCase_ShouldConvertSnakeCaseToPascalCase(string input, string expected)
  {
    // Arrange & Act
    string result = input.ToPascalCase();

    // Assert
    Assert.Equal(expected, result);
  }

  // Um método de teste para verificar se a função ToPascalCase retorna string vazia quando a entrada é uma string vazia
  [Fact]
  public void ToPascalCase_ShouldHandleEmptyString()
  {
    // Arrange
    string input = "";
    string expected = "";

    // Act
    string result = input.ToPascalCase();

    // Assert
    Assert.Equal(expected, result);
  }

  // Um método de teste para verificar se a função ToPascalCase retorna string nula quando a entrada é nula
  [Fact]
  public void ToPascalCase_ShouldHandleNullString()
  {
    // Arrange
    string input = null!;

    // Act
    string result = input.ToPascalCase();

    // Assert
    Assert.Null(result);
  }
}

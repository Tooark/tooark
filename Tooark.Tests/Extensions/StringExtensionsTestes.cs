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
    string expected = "helloworld";

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
    string expected = "helloworld";

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
    string expected = "cavajemappelletooark";

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
    string expected = "helloimtooark123";

    // Act
    string result = input.ToNormalize();

    // Assert
    Assert.Equal(expected, result);
  }

  // Uma lista de 100 testes com valores de entrada e resultados esperados gerados aleatoriamente
  [Theory]
  [InlineData(" ", "")]
  [InlineData("çñÿâäàåáãéêëèïîìíôöòóðõüûùú", "cnyaaaaaaeeeeiiiioooooouuuu")]
  [InlineData("123 abc ÇÑÝÂÄÀÅÁÃÉÊËÈÏÎÌÍÔÖÒÓÐÕÜÛÙÚ", "123abccnyaaaaaaeeeeiiiioooooouuuu")]
  [InlineData("Olá, eu sou o Tooark!", "olaeusouotooark")]
  [InlineData("¿Qué tal? Me llamo Tooark.", "quetalmellamotooark")]
  [InlineData("Wie geht's? Ich heiße Tooark.", "wiegehtsichheietooark")]
  [InlineData("こんにちは、私はTooarkです。", "tooark")]
  [InlineData("你好，我叫Tooark。", "tooark")]
  [InlineData("안녕하세요, 저는 Tooark입니다.", "tooark")]
  [InlineData("Привет, я Tooark.", "tooark")]
  [InlineData("Cześć, nazywam się Tooark.", "czenazywamsitooark")]
  [InlineData("Hello, I'm Tooark.", "helloimtooark")]
  [InlineData("Γεια σας, είμαι ο Tooark.", "tooark")]
  [InlineData("Bună, eu sunt Tooark.", "buneusunttooark")]
  [InlineData("Sveiki, aš esu Tooark.", "sveikiaesutooark")]
  [InlineData("Здравейте, аз съм Tooark.", "tooark")]
  [InlineData("Sawubona, ngiyi-Tooark.", "sawubonangiyitooark")]
  [InlineData("Shwmae, fi yw Tooark.", "shwmaefiywtooark")]
  [InlineData("สวัสดีครับ ผมชื่อ Tooark", "tooark")]
  [InlineData("नमस्ते, मेरा नाम Tooark है।", "tooark")]
  [InlineData("नमस्कार, माझे नाव Tooark आहे.", "tooark")]
  [InlineData("નમસ્તે, મારું નામ Tooark છે.", "tooark")]
  [InlineData("ਸਤਿ ਸ੍ਰੀ ਅਕਾਲ, ਮੇਰਾ ਨਾਮ Tooark ਹੈ.", "tooark")]
  [InlineData("வணக்கம், என் பெயர் Tooark.", "tooark")]
  [InlineData("నమస్కారం, నా పేరు Tooark.", "tooark")]
  [InlineData("ನಮಸ್ಕಾರ, ನನ್ನ ಹೆಸರು Tooark.", "tooark")]
  [InlineData("നമസ്കാരം, എന്റെ പേര് Tooark.", "tooark")]
  [InlineData("হ্যালো, আমার নাম Tooark.", "tooark")]
  [InlineData("sdùVWMäwúý", "sduvwmawuy")]
  [InlineData("ûëlbsSô1óv", "uelbsso1ov")]
  [InlineData("33dòïêgDQR", "33doiegdqr")]
  [InlineData("0óaïJnYëMí", "0oaijnyemi")]
  [InlineData(" Nð 139 Qli ", "no139qli")]
  [InlineData("ÿm0oeTEmbã", "ym0oetemba")]
  [InlineData("äCjqåqjVZq", "acjqaqjvzq")]
  [InlineData("Tf4îJåzàge", "tf4ijazage")]
  [InlineData("ù1nCÿ0ú7éW", "u1ncy0u7ew")]
  [InlineData("òî7gåP0siH", "oi7gap0sih")]
  [InlineData("SB8làbhûH1", "sb8labhuh1")]
  [InlineData(" âdçd51KU8 ", "adcd51ku8")]
  public void ToNormalize_ReturnsExpectedResult(string input, string expected)
  {
    string result = input.ToNormalize();

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
    string expected = "helloworld";

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
    string expected = "helloworld";

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
    string expected = "cavajemappelletooark";

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
    string expected = "helloimtooark123";

    // Act
    string result = input.ToNormalizeRegex();

    // Assert
    Assert.Equal(expected, result);
  }

  // Uma lista de 100 testes com valores de entrada e resultados esperados gerados aleatoriamente
  [Theory]
  [InlineData(" ", "")]
  [InlineData("çñÿâäàåáãéêëèïîìíôöòóðõüûùú", "cnyaaaaaaeeeeiiiioooooouuuu")]
  [InlineData("123 abc ÇÑÝÂÄÀÅÁÃÉÊËÈÏÎÌÍÔÖÒÓÐÕÜÛÙÚ", "123abccnyaaaaaaeeeeiiiioooooouuuu")]
  [InlineData("Olá, eu sou o Tooark!", "olaeusouotooark")]
  [InlineData("¿Qué tal? Me llamo Tooark.", "quetalmellamotooark")]
  [InlineData("Wie geht's? Ich heiße Tooark.", "wiegehtsichheietooark")]
  [InlineData("こんにちは、私はTooarkです。", "tooark")]
  [InlineData("你好，我叫Tooark。", "tooark")]
  [InlineData("안녕하세요, 저는 Tooark입니다.", "tooark")]
  [InlineData("Привет, я Tooark.", "tooark")]
  [InlineData("Cześć, nazywam się Tooark.", "czenazywamsitooark")]
  [InlineData("Hello, I'm Tooark.", "helloimtooark")]
  [InlineData("Γεια σας, είμαι ο Tooark.", "tooark")]
  [InlineData("Bună, eu sunt Tooark.", "buneusunttooark")]
  [InlineData("Sveiki, aš esu Tooark.", "sveikiaesutooark")]
  [InlineData("Здравейте, аз съм Tooark.", "tooark")]
  [InlineData("Sawubona, ngiyi-Tooark.", "sawubonangiyitooark")]
  [InlineData("Shwmae, fi yw Tooark.", "shwmaefiywtooark")]
  [InlineData("สวัสดีครับ ผมชื่อ Tooark", "tooark")]
  [InlineData("नमस्ते, मेरा नाम Tooark है।", "tooark")]
  [InlineData("नमस्कार, माझे नाव Tooark आहे.", "tooark")]
  [InlineData("નમસ્તે, મારું નામ Tooark છે.", "tooark")]
  [InlineData("ਸਤਿ ਸ੍ਰੀ ਅਕਾਲ, ਮੇਰਾ ਨਾਮ Tooark ਹੈ.", "tooark")]
  [InlineData("வணக்கம், என் பெயர் Tooark.", "tooark")]
  [InlineData("నమస్కారం, నా పేరు Tooark.", "tooark")]
  [InlineData("ನಮಸ್ಕಾರ, ನನ್ನ ಹೆಸರು Tooark.", "tooark")]
  [InlineData("നമസ്കാരം, എന്റെ പേര് Tooark.", "tooark")]
  [InlineData("হ্যালো, আমার নাম Tooark.", "tooark")]
  [InlineData("sdùVWMäwúý", "sduvwmawuy")]
  [InlineData("ûëlbsSô1óv", "uelbsso1ov")]
  [InlineData("33dòïêgDQR", "33doiegdqr")]
  [InlineData("0óaïJnYëMí", "0oaijnyemi")]
  [InlineData(" Nð 139 Qli ", "no139qli")]
  [InlineData("ÿm0oeTEmbã", "ym0oetemba")]
  [InlineData("äCjqåqjVZq", "acjqaqjvzq")]
  [InlineData("Tf4îJåzàge", "tf4ijazage")]
  [InlineData("ù1nCÿ0ú7éW", "u1ncy0u7ew")]
  [InlineData("òî7gåP0siH", "oi7gap0sih")]
  [InlineData("SB8làbhûH1", "sb8labhuh1")]
  [InlineData(" âdçd51KU8 ", "adcd51ku8")]
  public void ToNormalizeRegex_ReturnsExpectedResult(string input, string expected)
  {
    string result = input.ToNormalizeRegex();

    Assert.Equal(expected, result);
  }
}

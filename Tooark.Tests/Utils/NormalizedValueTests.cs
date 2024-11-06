using Tooark.Utils;

namespace Tooark.Tests.Utils;

public class NormalizeValueTests
{
  // Um método de teste para verificar se a função NormalizeValue retorna uma string vazia se a entrada for nula ou vazia
  [Fact]
  public void NormalizeValue_ShouldReturnEmptyString_WhenInputIsNullEmpty()
  {
    // Arrange
    string input = null!;
    string expected = "";

    // Act
    string actual = Util.NormalizeValue(input);

    // Assert
    Assert.Equal(expected, actual);
  }

  // Um método de teste para verificar se a função NormalizeValue remove os espaços da entrada
  [Fact]
  public void NormalizeValue_ShouldRemoveSpaces_WhenInputHasSpaces()
  {
    // Arrange
    string input = "Hello World";
    string expected = "HELLOWORLD";

    // Act
    string actual = Util.NormalizeValue(input);

    // Assert
    Assert.Equal(expected, actual);
  }

  // Um método de teste para verificar se a função NormalizeValue converte a entrada para minúsculas
  [Fact]
  public void NormalizeValue_ShouldConvertToLowercase_WhenInputHasUppercase()
  {
    // Arrange
    string input = "HELLO WORLD";
    string expected = "HELLOWORLD";

    // Act
    string actual = Util.NormalizeValue(input);

    // Assert
    Assert.Equal(expected, actual);
  }

  // Um método de teste para verificar se a função NormalizeValue substitui os caracteres especiais por caracteres sem acentuação
  [Fact]
  public void NormalizeValue_ShouldReplaceSpecialCharacters_WhenInputHasSpecialCharacters()
  {
    // Arrange
    string input = "Ça va? Je m'appelle Tooark.";
    string expected = "CAVAJEMAPPELLETOOARK";

    // Act
    string actual = Util.NormalizeValue(input);

    // Assert
    Assert.Equal(expected, actual);
  }

  // Um método de teste para verificar se a função NormalizeValue ignora os caracteres que não são números ou letras minúsculas
  [Fact]
  public void NormalizeValue_ShouldIgnoreOtherCharacters_WhenInputHasOtherCharacters()
  {
    // Arrange
    string input = "Hello, I'm Tooark! 123.";
    string expected = "HELLOIMTOOARK123";

    // Act
    string actual = Util.NormalizeValue(input);

    // Assert
    Assert.Equal(expected, actual);
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
  public void NormalizeValue_ShouldIgnoreOtherCharacters_WhenInputHasOtherCharacters_RandomString(string? input, string expected)
  {
    // Act
    string actual = Util.NormalizeValue(input!);

    // Assert
    Assert.Equal(expected, actual, ignoreCase: false);
  }

  // Um método de teste para verificar se a função NormalizeValueRegex retorna uma string vazia se a entrada for nula ou vazia
  [Fact]
  public void NormalizeValueRegex_ShouldReturnEmptyString_WhenInputIsNullEmpty()
  {
    // Arrange
    string input = null!;
    string expected = "";

    // Act
    string actual = Util.NormalizeValueRegex(input);

    // Assert
    Assert.Equal(expected, actual);
  }

  // Um método de teste para verificar se a função NormalizeValueRegex remove os espaços da entrada
  [Fact]
  public void NormalizeValueRegex_ShouldRemoveSpaces_WhenInputHasSpaces()
  {
    // Arrange
    string input = "Hello World";
    string expected = "HELLOWORLD";

    // Act
    string actual = Util.NormalizeValueRegex(input);

    // Assert
    Assert.Equal(expected, actual);
  }

  // Um método de teste para verificar se a função NormalizeValueRegex converte a entrada para minúsculas
  [Fact]
  public void NormalizeValueRegex_ShouldConvertToLowercase_WhenInputHasUppercase()
  {
    // Arrange
    string input = "HELLO WORLD";
    string expected = "HELLOWORLD";

    // Act
    string actual = Util.NormalizeValueRegex(input);

    // Assert
    Assert.Equal(expected, actual);
  }

  // Um método de teste para verificar se a função NormalizeValueRegex substitui os caracteres especiais por caracteres sem acentuação
  [Fact]
  public void NormalizeValueRegex_ShouldReplaceSpecialCharacters_WhenInputHasSpecialCharacters()
  {
    // Arrange
    string input = "Ça va? Je m'appelle Tooark.";
    string expected = "CAVAJEMAPPELLETOOARK";

    // Act
    string actual = Util.NormalizeValueRegex(input);

    // Assert
    Assert.Equal(expected, actual);
  }

  // Um método de teste para verificar se a função NormalizeValueRegex ignora os caracteres que não são números ou letras minúsculas
  [Fact]
  public void NormalizeValueRegex_ShouldIgnoreOtherCharacters_WhenInputHasOtherCharacters()
  {
    // Arrange
    string input = "Hello, I'm Tooark! 123.";
    string expected = "HELLOIMTOOARK123";

    // Act
    string actual = Util.NormalizeValueRegex(input);

    // Assert
    Assert.Equal(expected, actual);
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
  public void NormalizeValueRegex_ShouldIgnoreOtherCharacters_WhenInputHasOtherCharacters_RandomString(string? input, string expected)
  {
    // Act
    string actual = Util.NormalizeValueRegex(input!);

    // Assert
    Assert.Equal(expected, actual, ignoreCase: false);
  }
}

using System.Security.Cryptography;
using System.Text;

namespace Tooark.Utils;

/// <summary>
/// Classe estática que fornece métodos para gerar strings.
/// </summary>
public static class GenerateString
{
  /// <summary>
  /// Converte um número inteiro em uma representação equivalente alfabético do número.
  /// </summary>
  /// <param name="number">O número inteiro maior que zero a ser convertido.</param>
  /// <returns>Uma string representando o equivalente alfabético do número.</returns>
  /// <example>
  /// <code>
  /// string result = Sequential(1); // result: "a"
  /// string result = Sequential(27); // result: "aa"
  /// </code>
  /// </example>
  public static string Sequential(int number)
  {
    return InternalGenerateString.Sequential(number);
  }

  /// <summary>
  /// Gera uma string com critérios específicos.
  /// </summary>
  /// <param name="len">Comprimento da string a ser gerada. Valor padrão é 12. Deve ser maior ou igual a 8.</param>
  /// <param name="upper">Indica se deve incluir caracteres maiúsculos. Valor padrão é true.</param>
  /// <param name="lower">Indica se deve incluir caracteres minúsculos. Valor padrão é true.</param>
  /// <param name="number">Indica se deve incluir números. Valor padrão é true.</param>
  /// <param name="special">Indica se deve incluir caracteres especiais. Valor padrão é true.</param>
  /// <param name="similarity">Indica se deve utilizar caracteres semelhantes. Valor padrão é false.</param>
  /// <returns>Uma string gerada de acordo com os critérios especificados.</returns>
  /// <exception cref="ArgumentException">Se todos os tipos de caracteres estiverem desativados.</exception>
  public static string Password(int len = 12, bool upper = true, bool lower = true, bool number = true, bool special = true, bool similarity = false)
  {
    return InternalGenerateString.Password(len, upper, lower, number, special, similarity);
  }

  /// <summary>
  ///  Gera uma string hexadecimal aleatória.
  /// </summary>
  /// <param name="sizeToken">O tamanho da string hexadecimal a ser gerada.</param>
  /// <returns>Uma string hexadecimal aleatória.</returns>
  public static string Hexadecimal(int sizeToken = 128)
  {
    return InternalGenerateString.Hexadecimal(sizeToken);
  }

  /// <summary>
  ///  Gera uma string Guid sem hífens.
  /// </summary>
  /// <returns>Uma string Guid sem hífens.</returns>
  public static string GuidCode()
  {
    return InternalGenerateString.GuidCode();
  }

  /// <summary>
  /// Gera uma string de token.
  /// </summary>
  /// <param name="length">O comprimento da string de token a ser gerada. Valor padrão é 256. Deve ser maior ou igual a 256.</param>
  /// <returns>Uma string de token de no mínimo 256 caracteres.</returns>
  public static string Token(int length = 256)
  { 
    return InternalGenerateString.Token(length);
  }
}

/// <summary>
/// Classe estática interna que fornece métodos para gerar strings.
/// </summary>
internal static class InternalGenerateString
{
  /// <summary>
  /// Converte um número inteiro em uma representação equivalente alfabético do número.
  /// </summary>
  /// <param name="number">O número inteiro maior que zero a ser convertido.</param>
  /// <returns>Uma string representando o equivalente alfabético do número.</returns>
  /// <example>
  /// <code>
  /// string result = Sequential(1); // result: "a"
  /// string result = Sequential(27); // result: "aa"
  /// </code>
  /// </example>
  internal static string Sequential(int number)
  {
    // Cria um objeto StringBuilder para armazenar a string resultante.
    var result = new StringBuilder();

    // Converte o número inteiro para sua representação alfabética.
    while (number > 0)
    {
      // Decrementa o número em 1 para que a divisão funcione corretamente.
      number--;

      // Calcula o caractere correspondente ao número.
      char letter = (char)('A' + (number % 26));

      // Adiciona o caractere ao início da string.
      result.Insert(0, letter);

      // Divide o número por 26.
      number /= 26;
    }

    // Retorna a string resultante.
    return result.ToString();
  }

  /// <summary>
  /// Gera uma string com critérios específicos.
  /// </summary>
  /// <param name="length">Comprimento da string a ser gerada. Valor padrão é 12. Deve ser maior ou igual a 8.</param>
  /// <param name="upperChar">Indica se deve incluir caracteres maiúsculos. Valor padrão é true.</param>
  /// <param name="lowerChar">Indica se deve incluir caracteres minúsculos. Valor padrão é true.</param>
  /// <param name="numberChar">Indica se deve incluir números. Valor padrão é true.</param>
  /// <param name="specialChar">Indica se deve incluir caracteres especiais. Valor padrão é true.</param>
  /// <param name="similarChar">Indica se deve utilizar caracteres semelhantes. Valor padrão é false.</param>
  /// <returns>Uma string gerada de acordo com os critérios especificados.</returns>
  internal static string Password(
    int length = 12,
    bool upperChar = true,
    bool lowerChar = true,
    bool numberChar = true,
    bool specialChar = true,
    bool similarChar = false)
  {
    // Verifica se pelo menos um tipo de caractere está ativado.
    if (!upperChar && !lowerChar && !numberChar && !specialChar)
    {
      // Aplica critérios padrão.
      upperChar = true;
      lowerChar = true;
      numberChar = true;
      specialChar = true;
    }

    // Define o comprimento da string aleatória.
    int lenRandom = length >= 8 ? length : 8;

    // Define o tipo de caractere para cada posição da string.
    int[] typeChar = new int[lenRandom];

    // Define o array de caracteres gerados.
    var arrayChar = new char[lenRandom];

    // Define a lista de caracteres a serem gerados de forma embaralhada.
    int[] listChar;

    // Define a lista de conjuntos de caracteres que estarão disponíveis para uso.
    List<string> chars = [];

    // Define utiliza caracteres similares ou apenas distintos.
    int distinctOrComplete = similarChar ? 1 : 0;

    // Define os conjuntos de caracteres para maiúsculos.
    var charUpper = new[] {
      "ABCDEFGHJKLMNPQRSTUVWXYZ",
      "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
    };

    // Define os conjuntos de caracteres para minúsculos.
    var charLower = new[] {
      "abcdefghijkmnopqrstuvwxyz",
      "abcdefghijklmnopqrstuvwxyz",
    };

    // Define os conjuntos de caracteres para números.
    var charNumber = new[] {
      "123456789",
      "1234567890",
    };

    // Define os conjuntos de caracteres especiais.
    var charSpecial = new[] {
      "@#$%&*_",
      "@#$%&*!()[]{},.;<>:_-|",
    };

    // Adiciona os conjuntos de caracteres maiúsculos com similares ou distintos.
    if (upperChar)
    {
      chars.Add(charUpper[distinctOrComplete]);
    }

    // Adiciona os conjuntos de caracteres minúsculos com similares ou distintos.
    if (lowerChar)
    {
      chars.Add(charLower[distinctOrComplete]);
    }

    // Adiciona os conjuntos de caracteres números com similares ou distintos.
    if (numberChar)
    {
      chars.Add(charNumber[distinctOrComplete]);
    }

    // Adiciona os conjuntos de caracteres especiais com similares ou distintos.
    if (specialChar)
    {
      chars.Add(charSpecial[distinctOrComplete]);
    }

    // Define o tipo de caractere para cada posição da string.
    for (int i = 0; i < lenRandom; i++)
    {
      // Define o tipo de caractere para cada posição da string.
      typeChar[i] = i % chars.Count;
    }

    // Embaralha a ordem dos caracteres.
    listChar = [.. typeChar.OrderBy(item => RandomNumberGenerator.GetInt32(lenRandom))];

    // Gera a string aleatória.
    for (int i = 0; i < arrayChar.Length; i++)
    {
      // Seleciona um caractere aleatório do conjunto de caracteres correspondente.
      arrayChar[i] = chars[listChar[i]][RandomNumberGenerator.GetInt32(chars[listChar[i]].Length)];
    }

    // Retorna a string gerada.
    var resultString = new string(arrayChar);

    // Retorna a string gerada.
    return resultString;
  }

  /// <summary>
  ///  Gera uma string hexadecimal aleatória.
  /// </summary>
  /// <param name="sizeToken">O tamanho da string hexadecimal a ser gerada.</param>
  /// <returns>Uma string hexadecimal aleatória.</returns>
  internal static string Hexadecimal(int sizeToken = 128) => Convert.ToHexString(RandomNumberGenerator.GetBytes((sizeToken > 2 ? sizeToken : 2) / 2));

  /// <summary>
  ///  Gera uma string Guid sem hífens.
  /// </summary>
  /// <returns>Uma string Guid sem hífens.</returns>
  internal static string GuidCode() => Guid.NewGuid().ToString("N").ToUpperInvariant();

  /// <summary>
  /// Gera uma string de token.
  /// </summary>
  /// <param name="length">O comprimento da string de token a ser gerada. Valor padrão é 256. Deve ser maior ou igual a 256.</param>
  /// <returns>Uma string de token de no mínimo 256 caracteres.</returns>
  internal static string Token(int length = 256) => $"{GuidCode()}{Hexadecimal(length > 256 ? length - 32 : 224)}";
}

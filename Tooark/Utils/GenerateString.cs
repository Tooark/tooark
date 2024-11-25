using System.Security.Cryptography;
using System.Text;

namespace Tooark.Utils;

/// <summary>
/// Classe estática parcial que fornece métodos para geração de strings.
/// </summary>
public static partial class Util
{
  /// <summary>
  /// Converte um número dado para sua representação em letras.
  /// </summary>
  /// <param name="number">O número a ser convertido.</param>
  /// <returns>Uma string representando o equivalente em letras do número dado.</returns>
  public static string SequentialString(int number)
  {
    return InternalUtil.SequentialString(number);
  }

  /// <summary>
  /// Gera uma string com critérios específicos.
  /// </summary>
  /// <param name="len">Comprimento da string a ser gerada. Valor padrão é 12.</param>
  /// <param name="upper">Indica se deve incluir caracteres maiúsculos. Valor padrão é true.</param>
  /// <param name="lower">Indica se deve incluir caracteres minúsculos. Valor padrão é true.</param>
  /// <param name="number">Indica se deve incluir números. Valor padrão é true.</param>
  /// <param name="special">Indica se deve incluir caracteres especiais. Valor padrão é true.</param>
  /// <param name="similarity">Indica se deve evitar caracteres semelhantes. Valor padrão é false.</param>
  /// <returns>Uma string gerada de acordo com os critérios especificados.</returns>
  /// <exception cref="ArgumentException">Se todos os tipos de caracteres estiverem desativados.</exception>
  public static string CriteriaString(int len = 12, bool upper = true, bool lower = true, bool number = true, bool special = true, bool similarity = false)
  {
    return InternalUtil.CriteriaString(len, upper, lower, number, special, similarity);
  }

  /// <summary>
  /// Gera uma string hexadecimal aleatória.
  /// </summary>
  /// <param name="sizeToken">O tamanho da string hexadecimal a ser gerada. Valor padrão é 128.</param>
  /// <returns>Uma string hexadecimal aleatória.</returns>
  public static string HexString(int sizeToken = 128)
  {
    return InternalUtil.HexString(sizeToken);
  }
}

/// <summary>
/// Classe interna estática parcial que fornece métodos para geração de strings.
/// </summary>
internal static partial class InternalUtil
{
  /// <summary>
  /// Converte um número inteiro em uma string representando o equivalente alfabético.
  /// </summary>
  /// <param name="number">O número inteiro a ser convertido. Deve ser um número positivo.</param>
  /// <returns>Uma string representando o equivalente alfabético do número dado.</returns>
  /// <example>
  /// <code>
  /// string result = SequentialString(1); // result: "a"
  /// string result = SequentialString(27); // result: "aa"
  /// </code>
  /// </example>
  internal static string SequentialString(int number)
  {
    var result = new StringBuilder();

    // Converte o número inteiro para sua representação alfabética.
    while (number > 0)
    {
      // Decrementa o número em 1 para que a divisão funcione corretamente.
      number--;

      // Calcula o caractere correspondente ao número.
      char letter = (char)('a' + (number % 26));

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
  /// <param name="lenString">Comprimento da string a ser gerada. Valor padrão é 12. Mínimo 8 caracteres.</param>
  /// <param name="upperChar">Indica se deve incluir caracteres maiúsculos. Valor padrão é true.</param>
  /// <param name="lowerChar">Indica se deve incluir caracteres minúsculos. Valor padrão é true.</param>
  /// <param name="numberChar">Indica se deve incluir números. Valor padrão é true.</param>
  /// <param name="specialChar">Indica se deve incluir caracteres especiais. Valor padrão é true.</param>
  /// <param name="useSimilarChars">Indica se deve evitar caracteres semelhantes. Valor padrão é false.</param>
  /// <returns>Uma string gerada de acordo com os critérios especificados.</returns>
  /// <exception cref="ArgumentException">Se todos os tipos de caracteres estiverem desativados.</exception>
  internal static string CriteriaString(
    int lenString = 12,
    bool upperChar = true,
    bool lowerChar = true,
    bool numberChar = true,
    bool specialChar = true,
    bool useSimilarChars = false)
  {
    // Verifica se pelo menos um tipo de caractere está ativado.
    if (!upperChar && !lowerChar && !numberChar && !specialChar)
    {
      throw new ArgumentException("MissingParameter");
    }

    // Define o comprimento da string aleatória.
    int lenRandom = lenString >= 8 ? lenString : 12;

    // Inicializa os arrays e variáveis necessárias.
    int[] typeChar = new int[lenRandom];
    var arrayChar = new char[lenRandom];
    int[] listChar;
    List<string> chars = [];

    // Define utiliza caracteres similares ou apenas distintos.
    int distOrFull = useSimilarChars ? 1 : 0;

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
    if(upperChar)
    {
      chars.Add(charUpper[distOrFull]);
    }

    // Adiciona os conjuntos de caracteres minúsculos com similares ou distintos.
    if(lowerChar)
    {
      chars.Add(charLower[distOrFull]);
    }

    // Adiciona os conjuntos de caracteres números com similares ou distintos.
    if(numberChar)
    {
      chars.Add(charNumber[distOrFull]);
    }

    // Adiciona os conjuntos de caracteres especiais com similares ou distintos.
    if(specialChar)
    {
      chars.Add(charSpecial[distOrFull]);
    }

    // Define o tipo de caractere para cada posição da string.
    for (int i = 0; i < lenRandom; i++)
    {
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
  internal static string HexString(int sizeToken = 128)
  {
    // Retorna uma string hexadecimal aleatória.
    return Convert.ToHexString(RandomNumberGenerator.GetBytes(sizeToken));
  }
}

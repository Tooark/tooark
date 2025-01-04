using Microsoft.AspNetCore.Http;

namespace Tooark.Utils;

/// <summary>
/// Classe estática parcial que fornece métodos para conversão de arquivos e extração de extensões.
/// </summary>
public static partial class Util
{
  /// <summary>
  /// Converte uma string base64 para <see cref="MemoryStream"/>.
  /// </summary>
  /// <param name="stringFile">String de arquivo em base64.</param>
  /// <returns>Retorna um <see cref="MemoryStream"/>.</returns>
  public static MemoryStream? ConvertBase64ToMemoryStream(string stringFile)
  {
    return InternalUtil.ConvertBase64ToMemoryStream(stringFile);
  }

  /// <summary>
  /// Converte uma string base64 para <see cref="MemoryStream"/>.
  /// </summary>
  /// <param name="fromFile">Arquivo em formato de <see cref="IFormFile"/>.</param>
  /// <returns>Retorna um <see cref="MemoryStream"/>.</returns>
  public static MemoryStream? ConvertBase64ToMemoryStream(IFormFile fromFile)
  {
    using var reader = new StreamReader(fromFile.OpenReadStream());
    string stringFile = reader.ReadToEnd();

    return InternalUtil.ConvertBase64ToMemoryStream(stringFile);
  }

  /// <summary>
  /// Extrai a extensão do arquivo.
  /// </summary>
  /// <param name="stringFile">String de arquivo em base64.</param>
  /// <returns>Retorna a extensão do arquivo.</returns>
  public static string? ExtractExtension(string stringFile)
  {
    return InternalUtil.ExtractExtension(stringFile);
  }

  /// <summary>
  /// Extrai a extensão do arquivo.
  /// </summary>
  /// <param name="fromFile">Arquivo em formato de <see cref="IFormFile"/>.</param>
  /// <returns>Retorna um <see cref="MemoryStream"/>.</returns>
  public static string? ExtractExtension(IFormFile fromFile)
  {
    using var reader = new StreamReader(fromFile.OpenReadStream());
    string stringFile = reader.ReadToEnd();

    return InternalUtil.ExtractExtension(stringFile);
  }  
}

/// <summary>
/// Classe interna estática parcial que fornece métodos para conversão de arquivos e extração de extensões.
/// </summary>
internal static partial class InternalUtil
{
  /// <summary>
  /// Converte uma string base64 para <see cref="MemoryStream"/>.
  /// </summary>
  /// <param name="file">String de arquivo em base64.</param>
  /// <returns>Retorna um <see cref="MemoryStream"/>.</returns>
  internal static MemoryStream? ConvertBase64ToMemoryStream(string? file)
  {
    // Verifica se o arquivo é nulo ou vazio
    if (!string.IsNullOrEmpty(file))
    {
      // Verifica se o arquivo contém a palavra base64
      if (file.ToLower().Contains("base64", StringComparison.OrdinalIgnoreCase))
      {
        try
        {
          // Pega a string do arquivo
          var fileString = file[(file.LastIndexOf(',') + 1)..];

          // Converte a string para byte
          byte[] byteArray = Convert.FromBase64String(fileString);

          // Retorna o arquivo em MemoryStream
          return new MemoryStream(byteArray);
        }
        catch (Exception)
        {
          // Retorna nulo caso ocorra um erro
          return null;
        }
      }
    }

    return null;
  }

  /// <summary>
  /// Extrai a extensão do arquivo.
  /// </summary>
  /// <param name="file">String de arquivo em base64.</param>
  /// <returns>Retorna a extensão do arquivo.</returns>
  internal static string? ExtractExtension(string? file)
  {
    // Verifica se o arquivo é nulo ou vazio
    if (!string.IsNullOrEmpty(file))
    {
      // Verifica se o arquivo contém a palavra base64
      if (file.ToLower().Contains("base64", StringComparison.OrdinalIgnoreCase))
      {
        // Pega a posição da barra e o tamanho da extensão
        var position = file.IndexOf('/') + 1;
        var length = file.IndexOf(';') - position;

        // Verifica se a posição é maior ou igual a 0 e o tamanho é maior que 0
        if (position >= 0 && length > 0)
        {
          // Retorna a extensão do arquivo
          return file.Substring(position, length);
        }
      }
    }

    return null;
  }
}

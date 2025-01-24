using Microsoft.AspNetCore.Http;

namespace Tooark.Utils;

/// <summary>
/// Classe estática que fornece métodos para conversão de arquivos e extração de extensões.
/// </summary>
public static class FileConvert
{
  /// <summary>
  /// Converte uma string base64 para <see cref="MemoryStream"/>.
  /// </summary>
  /// <param name="stringFile">String de arquivo em base64.</param>
  /// <returns>Retorna um <see cref="MemoryStream"/>.</returns>
  public static MemoryStream? ToMemoryStream(string stringFile)
  {
    return InternalFileConvert.ToMemoryStream(stringFile);
  }

  /// <summary>
  /// Converte uma IFormFile para <see cref="MemoryStream"/>.
  /// </summary>
  /// <param name="fromFile">Arquivo em formato de <see cref="IFormFile"/>.</param>
  /// <returns>Retorna um <see cref="MemoryStream"/>.</returns>
  public static MemoryStream? ToMemoryStream(IFormFile fromFile)
  {
    return InternalFileConvert.ToMemoryStream(fromFile);
  }

  /// <summary>
  /// Extrai a extensão do arquivo.
  /// </summary>
  /// <param name="stringFile">String de arquivo em base64.</param>
  /// <returns>Retorna a extensão do arquivo.</returns>
  public static string? Extension(string stringFile)
  {
    return InternalFileConvert.Extension(stringFile);
  }

  /// <summary>
  /// Extrai a extensão do arquivo.
  /// </summary>
  /// <param name="fromFile">Arquivo em formato de <see cref="IFormFile"/>.</param>
  /// <returns>Retorna a extensão do arquivo.</returns>
  public static string? Extension(IFormFile fromFile)
  {
    return InternalFileConvert.Extension(fromFile);
  }
}

/// <summary>
/// Classe estática interna que fornece métodos para conversão de arquivos e extração de extensões.
/// </summary>
internal static class InternalFileConvert
{
  /// <summary>
  /// Converte uma string base64 para <see cref="MemoryStream"/>.
  /// </summary>
  /// <param name="file">String de arquivo em base64.</param>
  /// <returns>Retorna um <see cref="MemoryStream"/>.</returns>
  internal static MemoryStream? ToMemoryStream(string? file)
  {
    // Verifica se o arquivo é nulo ou vazio, e se o arquivo contém a palavra base64
    if (!string.IsNullOrEmpty(file) && file.Contains("base64,", StringComparison.OrdinalIgnoreCase))
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
      catch
      {
        // Retorna nulo caso ocorra um erro
        return null;
      }
    }

    // Retorna nulo caso o arquivo seja inválido
    return null;
  }

  /// <summary>
  /// Converte uma IFormFile para <see cref="MemoryStream"/>.
  /// </summary>
  /// <param name="fromFile">Arquivo em formato de <see cref="IFormFile"/>.</param>
  /// <returns>Retorna um <see cref="MemoryStream"/>.</returns>
  internal static MemoryStream? ToMemoryStream(IFormFile fromFile)
  {
    // Verifica se o arquivo é nulo
    if (fromFile != null)
    {
      // Lê o arquivo
      using var reader = new StreamReader(fromFile.OpenReadStream());

      // Pega a string do arquivo
      string stringFile = reader.ReadToEnd();

      // Retorna o arquivo em MemoryStream
      return ToMemoryStream(stringFile);
    }

    // Retorna nulo caso o arquivo seja inválido
    return null;
  }

  /// <summary>
  /// Extrai a extensão do arquivo.
  /// </summary>
  /// <param name="file">String de arquivo em base64.</param>
  /// <returns>Retorna a extensão do arquivo.</returns>
  internal static string? Extension(string? file)
  {
    // Verifica se o arquivo é nulo ou vazio, e se o arquivo contém a palavra base64
    if (!string.IsNullOrEmpty(file) && file.Contains("base64,", StringComparison.OrdinalIgnoreCase))
    {
      // Pega a posição da barra e o tamanho da extensão
      var position = file.IndexOf('/') + 1;
      var length = file.IndexOf(';') - position;

      // Verifica se a posição é maior ou igual a 0 e o tamanho é maior que 0
      if (position >= 0 && length > 0)
      {
        // Retorna a extensão do arquivo
        return file.Substring(position, length).ToUpperInvariant();
      }
    }

    // Retorna nulo caso o arquivo seja inválido
    return null;
  }

  /// <summary>
  /// Extrai a extensão do arquivo.
  /// </summary>
  /// <param name="fromFile">Arquivo em formato de <see cref="IFormFile"/>.</param>
  /// <returns>Retorna a extensão do arquivo.</returns>
  internal static string? Extension(IFormFile fromFile)
  {
    // Verifica se o arquivo é nulo
    if (fromFile != null)
    {
      // Pega a extensão do arquivo
      var extension = Path.GetExtension(fromFile.FileName);

      // Verifica se a extensão não é nula ou vazia
      if (!string.IsNullOrEmpty(extension))
      {
        // Remove o ponto da extensão
        return extension[1..].ToUpperInvariant();
      }

      // Retorna nulo caso a extensão seja nula ou vazia
      return null;
    }

    // Retorna nulo caso o arquivo seja inválido
    return null;
  }
}

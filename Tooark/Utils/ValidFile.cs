using Microsoft.AspNetCore.Http;

namespace Tooark.Utils;

public static partial class Util
{
  /// <summary>
  /// Verifica se o arquivo é uma imagem válida
  /// </summary>
  /// <param name="file">Arquivo a ser verificado</param>
  /// <param name="fileSize">Tamanho máximo do arquivo</param>
  /// <returns>Retorna verdadeiro se o arquivo for uma imagem válida</returns>
  public static bool IsValidImageFile(IFormFile file, long fileSize)
  {
    return InternalUtil.IsValidImage(file, fileSize);
  }

  /// <summary>
  ///  Verifica se o arquivo é um documento válido
  /// </summary>
  /// <param name="file">Arquivo a ser verificado</param>
  /// <param name="fileSize">Tamanho máximo do arquivo</param>
  /// <returns>Retorna verdadeiro se o arquivo for um documento válido</returns>
  public static bool IsValidDocumentFile(IFormFile file, long fileSize)
  {
    return InternalUtil.IsValidDocument(file, fileSize);
  }

  /// <summary>
  ///  Verifica se o arquivo é um video válido
  /// </summary>
  /// <param name="file">Arquivo a ser verificado</param>
  /// <param name="fileSize">Tamanho máximo do arquivo</param>
  /// <returns>Retorna verdadeiro se o arquivo for um video válido</returns>
  public static bool IsValidVideoFile(IFormFile file, long fileSize)
  {
    return InternalUtil.IsValidVideo(file, fileSize);
  }

  /// <summary>
  ///  Verifica se o arquivo é valido
  /// </summary>
  /// <param name="file">Arquivo a ser verificado</param>
  /// <param name="fileSize">Tamanho máximo do arquivo</param>
  /// <param name="permittedExtensions">Extensões permitidas</param>
  /// <returns>Retorna verdadeiro se o arquivo for válido</returns>
  public static bool IsValidCustomExtensions(IFormFile file, long fileSize, string[]? permittedExtensions = null)
  {
    return InternalUtil.IsValidCustom(file, fileSize, permittedExtensions);
  }
}

internal static partial class InternalUtil
{
  private static readonly string[] DefaultImageExtensions = [".JPG", ".JPEG", ".PNG", ".GIF", ".BMP", ".SVG"];
  private static readonly string[] DefaultDocumentExtensions = [".TXT", ".CSV", ".LOG", ".PDF", ".DOC", ".DOCX", ".XLS", ".XLSX", ".PPT", ".PPTX"];
  private static readonly string[] DefaultVideoExtensions = [".AVI", ".MP4", ".MPG", ".MPEG", ".WMV"];

  /// <summary>
  ///  Verifica se o tamanho do arquivo é válido
  /// </summary>
  /// <param name="file">Arquivo a ser verificado</param>
  /// <param name="fileSize">Tamanho máximo do arquivo</param>
  /// <returns>Retorna verdadeiro se o tamanho do arquivo for válido</returns>
  internal static bool FileSizeIsValid(IFormFile file, long fileSize)
  {
    // Verifica o tamanho do arquivo
    return file.Length >= 0 && file.Length <= fileSize;
  }

  /// <summary>
  /// Verifica se o arquivo é uma imagem válida
  /// </summary>
  /// <param name="file">Arquivo a ser verificado</param>
  /// <param name="fileSize">Tamanho máximo do arquivo</param>
  /// <returns>Retorna verdadeiro se o arquivo for uma imagem válida</returns>
  internal static bool IsValidImage(IFormFile file, long fileSize)
  {
    return IsValidFile(file, fileSize, DefaultImageExtensions);
  }

  /// <summary>
  ///  Verifica se o arquivo é um documento válido
  /// </summary>
  /// <param name="file">Arquivo a ser verificado</param>
  /// <param name="fileSize">Tamanho máximo do arquivo</param>
  /// <returns>Retorna verdadeiro se o arquivo for um documento válido</returns>
  internal static bool IsValidDocument(IFormFile file, long fileSize)
  {
    return IsValidFile(file, fileSize, DefaultDocumentExtensions);
  }

  /// <summary>
  ///  Verifica se o arquivo é um video válido
  /// </summary>
  /// <param name="file">Arquivo a ser verificado</param>
  /// <param name="fileSize">Tamanho máximo do arquivo</param>
  /// <returns>Retorna verdadeiro se o arquivo for um video válido</returns>
  internal static bool IsValidVideo(IFormFile file, long fileSize)
  {
    return IsValidFile(file, fileSize, DefaultVideoExtensions);
  }

  /// <summary>
  ///  Verifica se o arquivo é valido
  /// </summary>
  /// <param name="file">Arquivo a ser verificado</param>
  /// <param name="fileSize">Tamanho máximo do arquivo</param>
  /// <param name="permittedExtensions">Extensões permitidas</param>
  /// <returns>Retorna verdadeiro se o arquivo for válido</returns>
  internal static bool IsValidCustom(IFormFile file, long fileSize, string[]? permittedExtensions = null)
  {
    permittedExtensions ??= [.. DefaultImageExtensions, .. DefaultDocumentExtensions, .. DefaultVideoExtensions];

    permittedExtensions = permittedExtensions.Select(x => x.ToUpperInvariant()).ToArray();

    return IsValidFile(file, fileSize, permittedExtensions);
  }

  /// <summary>
  /// Verifica se o arquivo é válido
  /// </summary>
  /// <param name="file">Arquivo a ser verificado</param>
  /// <param name="fileSize">Tamanho máximo do arquivo</param>
  /// <param name="permittedExtensions">Extensões permitidas</param>
  /// <returns>Retorna verdadeiro se o arquivo for válido</returns>
  private static bool IsValidFile(IFormFile file, long fileSize, string[] permittedExtensions)
  {
    // Verifica o tamanho do arquivo
    if (FileSizeIsValid(file, fileSize))
    {
      var ext = Path.GetExtension(file.FileName).ToUpperInvariant();

      // Verifica a extensão do arquivo para evitar ameaças de segurança associadas a tipos de arquivos desconhecidos
      if (!string.IsNullOrEmpty(ext) && permittedExtensions.Contains<string>(ext))
      {
        return true;
      }
    }

    return false;
  }
}

using Microsoft.AspNetCore.Http;

namespace Tooark.Utils;

/// <summary>
/// Classe estática que fornece métodos para verificar a validade de arquivos.
/// </summary>
public static class FileValid
{
  /// <summary>
  /// Verifica se o arquivo é uma imagem válida.
  /// </summary>
  /// <remarks>
  /// As extensões de imagem permitidas são: .JPG, .JPEG, .PNG, .GIF, .BMP, .SVG e .WEBP.
  /// </remarks>
  /// <param name="file">Arquivo a ser verificado.</param>
  /// <param name="fileSize">Tamanho máximo do arquivo em bytes. Parâmetro opcional. Valor padrão é 5242880 bytes (5MB).</param>
  /// <returns>Retorna verdadeiro se o arquivo for uma imagem válida.</returns>
  public static bool IsImage(IFormFile file, long fileSize = 0)
  {
    return InternalFileValid.IsImage(file, fileSize);
  }

  /// <summary>
  /// Verifica se o arquivo é um documento válido.
  /// </summary>
  /// <remarks>
  /// As extensões de documento permitidas são: .TXT, .CSV, .LOG, .PDF, .DOC, .DOCX, .XLS, .XLSX, .PPT e .PPTX.
  /// </remarks>
  /// <param name="file">Arquivo a ser verificado.</param>
  /// <param name="fileSize">Tamanho máximo do arquivo em bytes. Parâmetro opcional. Valor padrão é 5242880 bytes (5MB).</param>
  /// <returns>Retorna verdadeiro se o arquivo for um documento válido.</returns>
  public static bool IsDocument(IFormFile file, long fileSize = 0)
  {
    return InternalFileValid.IsDocument(file, fileSize);
  }

  /// <summary>
  /// Verifica se o arquivo é um video válido.
  /// </summary>
  /// <remarks>
  /// As extensões de video permitidas são: .AVI, .MP4, .MPG, .MPEG e .WMV.
  /// </remarks>
  /// <param name="file">Arquivo a ser verificado.</param>
  /// <param name="fileSize">Tamanho máximo do arquivo em bytes. Parâmetro opcional. Valor padrão é 5242880 bytes (5MB).</param>
  /// <returns>Retorna verdadeiro se o arquivo for um video válido.</returns>
  public static bool IsVideo(IFormFile file, long fileSize = 0)
  {
    return InternalFileValid.IsVideo(file, fileSize);
  }

  /// <summary>
  /// Verifica se o arquivo é valido para extensões personalizadas.
  /// </summary>
  /// <param name="file">Arquivo a ser verificado.</param>
  /// <param name="fileSize">Tamanho máximo do arquivo em bytes. Parâmetro opcional. Valor padrão é 5242880 bytes (5MB).</param>
  /// <param name="permittedExtensions">Extensões permitidas. Parâmetro opcional. Valor padrão é todas as extensões de imagem, documento e vídeo.</param>
  /// <returns>Retorna verdadeiro se o arquivo for válido.</returns>
  public static bool IsCustom(IFormFile file, long fileSize = 0, string[]? permittedExtensions = null)
  {
    return InternalFileValid.IsCustom(file, fileSize, permittedExtensions);
  }
}

/// <summary>
/// Classe estática interna que fornece métodos para verificar a validade de arquivos.
/// </summary>
internal static class InternalFileValid
{
  private static readonly long DefaultFileSize = 5242880; // 5MB
  private static readonly string[] DefaultImageExtensions = [".JPG", ".JPEG", ".PNG", ".GIF", ".BMP", ".SVG", ".WEBP"];
  private static readonly string[] DefaultDocumentExtensions = [".TXT", ".CSV", ".LOG", ".PDF", ".DOC", ".DOCX", ".XLS", ".XLSX", ".PPT", ".PPTX"];
  private static readonly string[] DefaultVideoExtensions = [".AVI", ".MP4", ".MPG", ".MPEG", ".WMV"];

  /// <summary>
  /// Verifica se o tamanho do arquivo é válido.
  /// </summary>
  /// <param name="file">Arquivo a ser verificado.</param>
  /// <param name="fileSize">Tamanho máximo do arquivo em bytes. Valor padrão é 5242880 bytes (5MB).</param>
  /// <returns>Retorna verdadeiro se o tamanho do arquivo for válido.</returns>
  private static bool FileSize(IFormFile file, long fileSize = 0)
  {
    // Se o tamanho do arquivo não for especificado, o tamanho padrão será 5MB
    fileSize = fileSize > 0 ? fileSize : DefaultFileSize;

    // Verifica o tamanho do arquivo
    return file.Length > 0 && file.Length <= fileSize;
  }

  /// <summary>
  /// Verifica se o arquivo é válido.
  /// </summary>
  /// <param name="file">Arquivo a ser verificado. Formato de <see cref="IFormFile"/></param>
  /// <param name="fileSize">Tamanho máximo do arquivo em bytes.</param>
  /// <param name="permittedExtensions">Extensões permitidas.</param>
  /// <returns>Retorna verdadeiro se o arquivo for válido.</returns>
  private static bool IsValid(IFormFile file, long fileSize, string[] permittedExtensions)
  {
    // Verifica se o arquivo não é nulo e se o tamanho do arquivo é válido
    if (file != null && FileSize(file, fileSize))
    {
      // Obtém a extensão do arquivo
      var ext = Path.GetExtension(file.FileName).ToUpperInvariant();

      // Verifica a extensão do arquivo para evitar ameaças de segurança associadas a tipos de arquivos desconhecidos
      if (!string.IsNullOrEmpty(ext) && permittedExtensions.Contains(ext))
      {
        // Verifica se a extensão do arquivo é válida
        return true;
      }
    }

    // Retorna falso se o arquivo não for válido
    return false;
  }

  /// <summary>
  /// Verifica se o arquivo é uma imagem válida.
  /// </summary>
  /// <param name="file">Arquivo a ser verificado. Formato de <see cref="IFormFile"/></param>
  /// <param name="fileSize">Tamanho máximo do arquivo em bytes. Parâmetro opcional. Valor padrão é 5242880 bytes (5MB).</param>
  /// <returns>Retorna verdadeiro se o arquivo for uma imagem válida.</returns>
  internal static bool IsImage(IFormFile file, long fileSize = 0) => IsValid(file, fileSize, DefaultImageExtensions);

  /// <summary>
  /// Verifica se o arquivo é um documento válido.
  /// </summary>
  /// <param name="file">Arquivo a ser verificado. Formato de <see cref="IFormFile"/></param>
  /// <param name="fileSize">Tamanho máximo do arquivo em bytes. Parâmetro opcional. Valor padrão é 5242880 bytes (5MB).</param>
  /// <returns>Retorna verdadeiro se o arquivo for um documento válido.</returns>
  internal static bool IsDocument(IFormFile file, long fileSize = 0) => IsValid(file, fileSize, DefaultDocumentExtensions);

  /// <summary>
  /// Verifica se o arquivo é um video válido.
  /// </summary>
  /// <param name="file">Arquivo a ser verificado. Formato de <see cref="IFormFile"/></param>
  /// <param name="fileSize">Tamanho máximo do arquivo em bytes. Parâmetro opcional. Valor padrão é 5242880 bytes (5MB).</param>
  /// <returns>Retorna verdadeiro se o arquivo for um video válido.</returns>
  internal static bool IsVideo(IFormFile file, long fileSize = 0) => IsValid(file, fileSize, DefaultVideoExtensions);

  /// <summary>
  /// Verifica se o arquivo é valido para extensões personalizadas.
  /// </summary>
  /// <param name="file">Arquivo a ser verificado. Formato de <see cref="IFormFile"/></param>
  /// <param name="fileSize">Tamanho máximo do arquivo em bytes. Parâmetro opcional. Valor padrão é 5242880 bytes (5MB).</param>
  /// <param name="permittedExtensions">Extensões permitidas. Parâmetro opcional. Valor padrão é todas as extensões de imagem, documento e vídeo.</param>
  /// <returns>Retorna verdadeiro se o arquivo for válido.</returns>
  internal static bool IsCustom(IFormFile file, long fileSize = 0, string[]? permittedExtensions = null)
  {
    // Se as extensões permitidas não forem especificadas, as extensões padrão serão usadas
    if (permittedExtensions == null || permittedExtensions.Length == 0)
    {
      // Se as extensões permitidas não forem especificadas, as extensões padrão serão usadas
      permittedExtensions = [.. DefaultImageExtensions, .. DefaultDocumentExtensions, .. DefaultVideoExtensions];
    }
    else
    {
      // Converte todas as extensões para maiúsculas
      permittedExtensions = [.. permittedExtensions.Select(x => x.ToUpperInvariant())];
    }

    // Verifica se o arquivo é válido
    return IsValid(file, fileSize, permittedExtensions);
  }
}

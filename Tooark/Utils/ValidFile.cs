// using System.Security.Cryptography;

// namespace Tooark.Utils;

// public static partial class Util
// {
//   public static string IsValidImageFile(string value)
//   {
//     return InternalUtil.NormalizeValue(value);
//   }

//   public static string IsValidTextFile(string value)
//   {
//     return InternalUtil.NormalizeValueRegex(value);
//   }

//   public static string IsValidVideoFile(string value)
//   {
//     return InternalUtil.NormalizeValue(value);
//   }

//   public static string IsValidFile(string value)
//   {
//     return InternalUtil.NormalizeValueRegex(value);
//   }
// }

// internal static partial class InternalUtil
// {
//   internal static bool IsValidImageFile(IFormFile file, long fileSize)
//   {
//     // Verifica o tamanho do arquivo
//     if (file.Length < 0 || file.Length > fileSize)
//     {
//       return false;
//     }

//     // Verifica a extensão do arquivo para evitar ameaças de segurança associadas a tipos de arquivos desconhecidos
//     string[] permittedExtensions = new string[] { ".jpg", ".jpeg", ".png", ".bmp", ".svg", ".tiff" };
//     var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
//     if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains<string>(ext))
//     {
//       return false;
//     }

//     return true;
//   }

//   internal static bool IsValidTextFile(IFormFile file, long fileSize)
//   {
//     // Verifica o tamanho do arquivo
//     if (file.Length < 0 || file.Length > fileSize)
//     {
//       return false;
//     }

//     // Verifica a extensão do arquivo para evitar ameaças de segurança associadas a tipos de arquivos desconhecidos            
//     string[] permittedExtensions = new string[] { ".txt", ".csv", ".log", ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx" };
//     var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
//     if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains<string>(ext))
//     {
//       return false;
//     }

//     return true;
//   }

//   internal static bool IsValidVideoFile(IFormFile file, long fileSize)
//   {
//     // Verifica o tamanho do arquivo
//     if (file.Length < 0 || file.Length > fileSize)
//     {
//       return false;
//     }

//     // Verifica a extensão do arquivo para evitar ameaças de segurança associadas a tipos de arquivos desconhecidos
//     string[] permittedExtensions = new string[] { ".avi", ".mp4", ".mpg", ".mpeg", ".wmv" };
//     var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
//     if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains<string>(ext))
//     {
//       return false;
//     }

//     return true;
//   }

//   internal static bool IsValidFile(IFormFile file, long fileSize)
//   {
//     // Verifica o tamanho do arquivo
//     if (file.Length < 0 || file.Length > fileSize)
//     {
//       return false;
//     }

//     return IsValidImageFile(file, fileSize) ||
//            IsValidTextFile(file, fileSize) ||
//            IsValidVideoFile(file, fileSize);
//   }
// }

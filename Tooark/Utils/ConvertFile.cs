// using System.Security.Cryptography;

// namespace Tooark.Utils;

// public static partial class Util
// {
//   public static string ConvertBase64ToMemoryStream(string value)
//   {
//     return InternalUtil.NormalizeValueRegex(value);
//   }
// }

// internal static partial class InternalUtil
// {
//   internal static MemoryStream? ConvertBase64ToMemoryStream(string? file)
//   {
//     if (!string.IsNullOrEmpty(file))
//     {
//       if (file.ToLower().Contains("base64"))
//       {
//         var fileString = file[(file.LastIndexOf(',') + 1)..];
//         byte[] byteArray = Convert.FromBase64String(fileString);

//         return new MemoryStream(byteArray);
//       }
//     }

//     return null;
//   }
// }

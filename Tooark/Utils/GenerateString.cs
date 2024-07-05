// using System.Security.Cryptography;

// namespace Tooark.Utils;

// public static partial class Util
// {
//   public static string GenerateCriteriaString(string value)
//   {
//     return InternalUtil.NormalizeValue(value);
//   }

//   public static string GenerateHexString(string value)
//   {
//     return InternalUtil.NormalizeValueRegex(value);
//   }
// }

// internal static partial class InternalUtil
// {
//   internal static string GenerateCriteriaString(
//     int lenString = 12,
//     bool useSimilarChars = false,
//     bool upperChar = true,
//     bool lowerChar = true,
//     bool numberChar = true,
//     bool specialChar = true)
//   {
//     int lenRandom = lenString >= 8 ? lenString : 12;
//     int[] typeChar = new int[lenRandom];
//     var arrayChar = new char[lenRandom];
//     int[] listChar;

//     var distinctChars = new[] {
//       "ABCDEFGHJKLMNPQRSTUVWXYZ",
//       "abcdefghijkmnopqrstuvwxyz",
//       "123456789",
//       "@#$%&*"
//     };

//     var fullChars = new[] {
//       "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
//       "abcdefghijklmnopqrstuvwxyz",
//       "1234567890",
//       "@#$%&*"
//     };

//     var chars = useSimilarChars ? fullChars : distinctChars;

//     for (int i = 0; i < lenRandom; i++)
//     {
//       typeChar[i] = i % 4;
//     }

//     listChar = [.. typeChar.OrderBy(item => RandomNumberGenerator.GetInt32(lenRandom))];

//     for (int i = 0; i < arrayChar.Length; i++)
//     {
//       arrayChar[i] = chars[listChar[i]][RandomNumberGenerator.GetInt32(chars[listChar[i]].Length)];
//     }

//     var resultString = new string(arrayChar);

//     return resultString;
//   }

//   internal static string GenerateHexString(int sizeToken = 128)
//   {
//     return Convert.ToHexString(RandomNumberGenerator.GetBytes(sizeToken));
//   }
// }

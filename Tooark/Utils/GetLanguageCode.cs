using static Tooark.Utils.Util;

namespace Tooark.Utils;

/// <summary>
/// Classe estática parcial que fornece métodos buscar campo name, title e description.
/// </summary>
public static partial class Util
{
  /// <summary>
  /// Obtém o nome localizado de uma lista de objetos.
  /// </summary>
  /// <param name="listValue">A lista de objetos.</param>
  /// <param name="languageCode">O código de idioma opcional. Se nulo, usa o idioma atual.</param>
  /// <returns>O nome localizado.</returns>
  public static string GetName<T>(IList<T> listValue, string? languageCode = null)
  {
    return InternalUtil.GetName(listValue, languageCode);
  }

  /// <summary>
  /// Obtém o título localizado de uma lista de objetos.
  /// </summary>
  /// <param name="listValue">A lista de objetos.</param>
  /// <param name="languageCode">O código de idioma opcional. Se nulo, usa o idioma atual.</param>
  /// <returns>O título localizado.</returns>
  public static string GetTitle<T>(IList<T> listValue, string? languageCode = null)
  {
    return InternalUtil.GetTitle(listValue, languageCode);
  }

  /// <summary>
  /// Obtém a descrição localizada de uma lista de objetos.
  /// </summary>
  /// <param name="listValue">A lista de objetos.</param>
  /// <param name="languageCode">O código de idioma opcional. Se nulo, usa o idioma atual.</param>
  public static string GetDescription<T>(IList<T> listValue, string? languageCode = null)
  {
    return InternalUtil.GetDescription(listValue, languageCode);
  }

  /// <summary>
  /// Obtém um valor de propriedade localizado de uma lista de objetos com base no código de idioma fornecido.
  /// </summary>
  /// <typeparam name="T">O tipo de objeto na lista.</typeparam>
  /// <param name="listValue">A lista de objetos do tipo T.</param>
  /// <param name="property">O nome da propriedade a ser obtida do objeto.</param>
  /// <param name="currentLanguageCode">O código de idioma atual. Se nulo, usa o idioma padrão da aplicação.</param>
  /// <returns>
  /// O valor da propriedade localizada como uma string. Se o item correspondente ao idioma atual não for encontrado,
  /// tenta retornar o valor no idioma padrão. Se nenhum item for encontrado, retorna uma string vazia.
  /// </returns>
  /// <exception cref="InvalidOperationException">
  /// Lança uma exceção se a propriedade 'LanguageCode' ou a propriedade especificada não existir no tipo T.
  /// </exception>
  public static string GetLanguageCode<T>(IList<T> listValue, string property, string currentLanguageCode = null!)
  {
    return InternalUtil.GetLanguageCode(listValue, property, currentLanguageCode);
  }
}

/// <summary>
/// Classe interna estática parcial que fornece métodos buscar campo name, title e description.
/// </summary>
internal static partial class InternalUtil
{
  /// <summary>
  /// Obtém o nome localizado de uma lista de objetos.
  /// </summary>
  /// <param name="listValue">A lista de objetos.</param>
  /// <param name="languageCode">O código de idioma opcional. Se nulo, usa o idioma atual.</param>
  /// <returns>O nome localizado.</returns>
  internal static string GetName<T>(IList<T> listValue, string? languageCode = null!)
  {
    languageCode ??= Languages.Current;
    return GetLanguageCode(listValue, "Name", languageCode);
  }

  /// <summary>
  /// Obtém o título localizado de uma lista de objetos.
  /// </summary>
  /// <param name="listValue">A lista de objetos.</param>
  /// <param name="languageCode">O código de idioma opcional. Se nulo, usa o idioma atual.</param>
  /// <returns>O título localizado.</returns>
  internal static string GetTitle<T>(IList<T> listValue, string? languageCode = null!)
  {
    languageCode ??= Languages.Current;

    return GetLanguageCode(listValue, "Title", languageCode);
  }

  /// <summary>
  /// Obtém a descrição localizada de uma lista de objetos.
  /// </summary>
  /// <param name="listValue">A lista de objetos.</param>
  /// <param name="languageCode">O código de idioma opcional. Se nulo, usa o idioma atual.</param>
  /// <returns>A descrição localizada.</returns>
  internal static string GetDescription<T>(IList<T> listValue, string? languageCode = null!)
  {
    languageCode ??= Languages.Current;
    return GetLanguageCode(listValue, "Description", languageCode);
  }

  /// <summary>
  /// Obtém um valor de propriedade localizado de uma lista de objetos com base no código de idioma fornecido.
  /// </summary>
  /// <typeparam name="T">O tipo de objeto na lista.</typeparam>
  /// <param name="listValue">A lista de objetos do tipo T.</param>
  /// <param name="property">O nome da propriedade a ser obtida do objeto.</param>
  /// <param name="currentLanguageCode">O código de idioma atual. Se nulo, usa o idioma padrão da aplicação.</param>
  /// <returns>
  /// O valor da propriedade localizada como uma string. Se o item correspondente ao idioma atual não for encontrado,
  /// tenta retornar o valor no idioma padrão. Se nenhum item for encontrado, retorna uma string vazia.
  /// </returns>
  /// <exception cref="InvalidOperationException">
  /// Lança uma exceção se a propriedade 'LanguageCode' ou a propriedade especificada não existir no tipo T.
  /// </exception>
  internal static string GetLanguageCode<T>(IList<T> listValue, string property, string currentLanguageCode = null!)
  {
    currentLanguageCode ??= Languages.Current;
    var defaultLanguageCode = Languages.Default;

    var languageCodeProperty = typeof(T).GetProperty("LanguageCode");
    var nameProperty = typeof(T).GetProperty(property);

    if (languageCodeProperty == null)
    {
      throw new InvalidOperationException($"PropertyNotExist;LanguageCode");
    }

    if (nameProperty == null)
    {
      throw new InvalidOperationException($"PropertyNotExist;{property}");
    }

    var currentLanguageItem = listValue.FirstOrDefault(x => (string)languageCodeProperty.GetValue(x)! == currentLanguageCode) ??
                              listValue.FirstOrDefault(x => (string)languageCodeProperty.GetValue(x)! == defaultLanguageCode) ??
                              listValue.FirstOrDefault();

    return currentLanguageItem != null ?
      (string)nameProperty.GetValue(currentLanguageItem)! :
      string.Empty;
  }
}

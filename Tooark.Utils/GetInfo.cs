namespace Tooark.Utils;

/// <summary>
/// Classe estática que fornece métodos buscar campo name, title e description.
/// </summary>
public static class GetInfo
{
  /// <summary>
  /// Obtém o nome localizado de uma lista de objetos.
  /// </summary>
  /// <remarks>
  /// O método tenta obter o nome no idioma solicitado. Se não encontrar, tenta obter no idioma padrão da aplicação.
  /// Se não encontrar, retorna o primeiro item da lista.
  /// </remarks>
  /// <param name="list">A lista de objetos.</param>
  /// <param name="languageCode">O código de idioma. Parâmetro opcional. Padrão é o idioma atual.</param>
  /// <returns>O nome localizado.</returns>
  public static string Name<T>(IList<T> list, string? languageCode = null)
  {
    return InternalGetInfo.Name(list, languageCode);
  }

  /// <summary>
  /// Obtém o título localizado de uma lista de objetos.
  /// </summary>
  /// <remarks>
  /// O método tenta obter o nome no idioma solicitado. Se não encontrar, tenta obter no idioma padrão da aplicação.
  /// Se não encontrar, retorna o primeiro item da lista.
  /// </remarks>
  /// <param name="list">A lista de objetos.</param>
  /// <param name="languageCode">O código de idioma. Parâmetro opcional. Padrão é o idioma atual.</param>
  /// <returns>O título localizado.</returns>
  public static string Title<T>(IList<T> list, string? languageCode = null)
  {
    return InternalGetInfo.Title(list, languageCode);
  }

  /// <summary>
  /// Obtém a descrição localizado de uma lista de objetos.
  /// </summary>
  /// <remarks>
  /// O método tenta obter o nome no idioma solicitado. Se não encontrar, tenta obter no idioma padrão da aplicação.
  /// Se não encontrar, retorna o primeiro item da lista.
  /// </remarks>
  /// <param name="list">A lista de objetos.</param>
  /// <param name="languageCode">O código de idioma. Parâmetro opcional. Padrão é o idioma atual.</param>
  /// <returns>A descrição localizada.</returns>
  public static string Description<T>(IList<T> list, string? languageCode = null)
  {
    return InternalGetInfo.Description(list, languageCode);
  }

  /// <summary>
  /// Obtém as palavras-chave localizadas de uma lista de objetos.
  /// </summary>
  /// <remarks>
  /// O método tenta obter as palavras-chave no idioma solicitado. Se não encontrar, tenta obter no idioma padrão da aplicação.
  /// Se não encontrar, retorna o primeiro item da lista.
  /// </remarks>
  /// <param name="list">A lista de objetos.</param>
  /// <param name="languageCode">O código de idioma. Parâmetro opcional. Padrão é o idioma atual.</param>
  /// <returns>As palavras-chave localizadas.</returns>
  public static string Keywords<T>(IList<T> list, string? languageCode = null)
  {
    return InternalGetInfo.Keywords(list, languageCode);
  }

  /// <summary>
  /// Obtém um valor localizado de uma propriedade em uma lista de objetos.
  /// </summary>
  /// <typeparam name="T">O tipo de objeto na lista.</typeparam>
  /// <param name="list">A lista de objetos do tipo T.</param>
  /// <param name="property">O nome da propriedade a ser obtida do objeto.</param>
  /// <param name="languageCode">O código de idioma atual. Se nulo, usa o idioma padrão da aplicação.</param>
  /// <returns>
  /// O valor da propriedade localizada como uma string. Se o item correspondente ao idioma atual não for encontrado,
  /// tenta retornar o valor no idioma padrão. Se nenhum item for encontrado, retorna uma string vazia.
  /// </returns>
  /// <exception cref="InvalidOperationException">
  /// Lança uma exceção se a propriedade 'LanguageCode' ou a propriedade especificada não existir no tipo T.
  /// </exception>
  public static string Custom<T>(IList<T> list, string property, string? languageCode = null)
  {
    return InternalGetInfo.GetLanguageCode(list, property, languageCode);
  }
}

/// <summary>
/// Classe estática interna que fornece métodos buscar campo name, title e description.
/// </summary>
internal static class InternalGetInfo
{
  /// <summary>
  /// Obtém o nome localizado de uma lista de objetos.
  /// </summary>
  /// <remarks>
  /// O método tenta obter o nome no idioma solicitado. Se não encontrar, tenta obter no idioma padrão da aplicação.
  /// Se não encontrar, retorna o primeiro item da lista.
  /// </remarks>
  /// <param name="list">A lista de objetos.</param>
  /// <param name="languageCode">O código de idioma. Parâmetro opcional. Padrão é o idioma atual.</param>
  /// <returns>O nome localizado.</returns>
  internal static string Name<T>(IList<T> list, string? languageCode = null)
  {
    // Se o código de idioma for nulo, usa o idioma atual.
    languageCode ??= Language.Current;

    // Retorna o nome localizado no idioma solicitado.
    return GetLanguageCode(list, "Name", languageCode);
  }

  /// <summary>
  /// Obtém o título localizado de uma lista de objetos.
  /// </summary>
  /// <remarks>
  /// O método tenta obter o título no idioma solicitado. Se não encontrar, tenta obter no idioma padrão da aplicação.
  /// Se não encontrar, retorna o primeiro item da lista.
  /// </remarks>
  /// <param name="list">A lista de objetos.</param>
  /// <param name="languageCode">O código de idioma. Parâmetro opcional. Padrão é o idioma atual.</param>
  /// <returns>O título localizado.</returns>
  internal static string Title<T>(IList<T> list, string? languageCode = null!)
  {
    // Se o código de idioma for nulo, usa o idioma atual.
    languageCode ??= Language.Current;

    // Retorna o título localizado no idioma solicitado.
    return GetLanguageCode(list, "Title", languageCode);
  }

  /// <summary>
  /// Obtém a descrição localizado de uma lista de objetos.
  /// </summary>
  /// <remarks>
  /// O método tenta obter a descrição no idioma solicitado. Se não encontrar, tenta obter no idioma padrão da aplicação.
  /// Se não encontrar, retorna o primeiro item da lista.
  /// </remarks>
  /// <param name="list">A lista de objetos.</param>
  /// <param name="languageCode">O código de idioma. Parâmetro opcional. Padrão é o idioma atual.</param>
  /// <returns>A descrição localizada.</returns>
  internal static string Description<T>(IList<T> list, string? languageCode = null!)
  {
    // Se o código de idioma for nulo, usa o idioma atual.
    languageCode ??= Language.Current;

    // Retorna a descrição localizada no idioma solicitado.
    return GetLanguageCode(list, "Description", languageCode);
  }

  /// <summary>
  /// Obtém as palavras-chave localizadas de uma lista de objetos.
  /// </summary>
  /// <remarks>
  /// O método tenta obter as palavras-chave no idioma solicitado. Se não encontrar, tenta obter no idioma padrão da aplicação.
  /// Se não encontrar, retorna o primeiro item da lista.
  /// </remarks>
  /// <param name="list">A lista de objetos.</param>
  /// <param name="languageCode">O código de idioma. Parâmetro opcional. Padrão é o idioma atual.</param>
  /// <returns>As palavras-chave localizadas.</returns>
  internal static string Keywords<T>(IList<T> list, string? languageCode = null!)
  {
    // Se o código de idioma for nulo, usa o idioma atual.
    languageCode ??= Language.Current;

    // Retorna as palavras-chave localizadas no idioma solicitado.
    return GetLanguageCode(list, "Keywords", languageCode);
  }
    
  /// <summary>
  /// Obtém um valor de uma propriedade localizado de uma lista de objetos.
  /// </summary>
  /// <remarks>
  /// O método tenta obter no idioma solicitado.
  /// Se não encontrar, tenta obter no idioma padrão da aplicação.
  /// Se não encontrar, tenta obter o primeiro item da lista.
  /// Se não encontrar, retorna uma string vazia.
  /// </remarks>
  /// <typeparam name="T">O tipo de objeto na lista.</typeparam>
  /// <param name="list">A lista de objetos do tipo T.</param>
  /// <param name="nameProperty">O nome da propriedade a ser obtida do objeto.</param>
  /// <param name="languageCode">O código de idioma. Parâmetro opcional. Padrão é o idioma atual.</param>
  /// <returns> O valor da propriedade localizada como uma string.</returns>
  /// <exception cref="InvalidOperationException">
  /// Lança uma exceção se a propriedade 'LanguageCode' ou a propriedade especificada não existir no tipo T.
  /// </exception>
  internal static string GetLanguageCode<T>(IList<T> list, string nameProperty, string? languageCode = null)
  {
    // Se o código de idioma for nulo, usa o idioma atual.
    languageCode ??= Language.Current;

    // Código de idioma padrão.
    var defaultLanguageCode = Language.Default;

    // Obtém as propriedades 'LanguageCode'.
    var languageCodeProperty = typeof(T).GetProperty("LanguageCode");

    // Obtém a propriedade especificada.
    var property = typeof(T).GetProperty(nameProperty);

    // Se a propriedade 'LanguageCode' não existir, lança uma exceção.
    if (languageCodeProperty == null)
    {
      // Lança uma exceção se a propriedade 'LanguageCode' não existir.
      throw new InvalidOperationException($"NotFound.Property;LanguageCode");
    }

    // Se a propriedade especificada não existir, lança uma exceção.
    if (property == null)
    {
      // Lança uma exceção se a propriedade especificada não existir.
      throw new InvalidOperationException($"NotFound.Property;{nameProperty}");
    }

    // Obtém o item atual com base no código de idioma.
    var item = list.FirstOrDefault(x => (string)languageCodeProperty.GetValue(x)! == languageCode) ??
               list.FirstOrDefault(x => (string)languageCodeProperty.GetValue(x)! == defaultLanguageCode) ??
               list.FirstOrDefault();

    // Retorna o valor da propriedade especificada. Se não encontrar, retorna uma string vazia.
    return EqualityComparer<T>.Default.Equals(item, default) ?
      string.Empty :
      (string)property.GetValue(item)!;
  }
}

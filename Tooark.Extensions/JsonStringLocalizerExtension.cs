﻿using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Tooark.Utils;

namespace Tooark.Extensions;

/// <summary>
/// Método de extensão para StringLocalizer que utiliza cache distribuído e traduções em arquivos JSON.
/// </summary>
/// <param name="distributedCache">Cache distribuído.</param>
public class JsonStringLocalizerExtension(IDistributedCache distributedCache) : IStringLocalizer
{
  /// <summary>
  /// Localizador interno de strings JSON.
  /// </summary>
  private readonly InternalJsonStringLocalizer _internalLocalizer = new(distributedCache);


  /// <summary>
  /// Obtém a string localizada.
  /// </summary>
  public LocalizedString this[string name]
  {
    get
    {
      // Verifica se a chave é nula ou vazia
      if (string.IsNullOrEmpty(name))
      {
        return new LocalizedString(string.Empty, string.Empty, true);
      }

      // Obtém a string localizada
      string value = _internalLocalizer.GetLocalizedString(name);

      // Retorna a string localizada ou o nome fornecido para busca
      return new LocalizedString(name, value ?? name, value == null);
    }
  }

  /// <summary>
  /// Obtém a string localizada formatada com os argumentos fornecidos.
  /// </summary>
  public LocalizedString this[string name, params object[] arguments]
  {
    get
    {
      // Converte os argumentos em uma string
      string parameters = string.Join(";", arguments);

      // Adiciona os argumentos ao nome da string
      name = string.Join(";", name, parameters);

      // Obtém a string localizada
      string value = this[name];

      // Retorna a string localizada ou o nome fornecido para busca
      return new LocalizedString(name, value ?? name, value == null);
    }
  }

  /// <summary>
  /// Obtém todas as strings localizadas.
  /// </summary>
  /// <param name="includeParentCultures">Utilizado para avaliar se é a cultura padrão (True) ou a cultura atual (False).</param>
  /// <returns>Strings localizadas.</returns>
  public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
  {
    return _internalLocalizer.GetAllStrings(includeParentCultures);
  }
}

/// <summary>
/// Método interno de extensão para StringLocalizer que utiliza cache distribuído e traduções em arquivos JSON.
/// </summary>
internal class InternalJsonStringLocalizer
{
  /// <summary>
  /// Cache distribuído.
  /// </summary>
  private readonly IDistributedCache _distributedCache;

  /// <summary>
  /// Traduções carregadas.
  /// </summary>
  private readonly Dictionary<string, JsonDocument> _translations = [];


  /// <summary>
  /// Construtor da classe.
  /// </summary>
  /// <param name="distributedCache">Cache distribuído.</param>
  internal InternalJsonStringLocalizer(IDistributedCache distributedCache)
  {
    // Inicializa as variáveis
    _distributedCache = distributedCache;

    // Carrega as traduções
    LoadTranslations(Language.Default);
    LoadTranslations(Language.Current);
  }


  /// <summary>
  /// Obtém a string localizada.
  /// </summary>
  /// <param name="keyParameter">Chave da string localizada.</param>
  /// <param name="cultureSelect">Código de idioma selecionado. Parâmetro opcional.</param>
  /// <returns>String localizada.</returns>
  internal string GetLocalizedString(string keyParameter, string? cultureSelect = null)
  {
    // Verifica se a chave contém parâmetros
    var listInfo = keyParameter.Split(';');

    // Obtém o código de idioma selecionado ou o código de idioma padrão
    string culture = GetCulture(cultureSelect);

    // Verifica se o arquivo existe
    if (_translations.TryGetValue(culture, out var jsonDocument))
    {
      // Declara a chave do cache e o valor do cache para o cache distribuído
      string cacheKey = GetCacheKey(culture, listInfo[0]);

      // Verifica se a string localizada está no cache
      string? value = _distributedCache.GetString(cacheKey);

      // Se a string localizada não estiver no cache, obtenha-a do documento JSON
      if (string.IsNullOrEmpty(value))
      {
        // Obtém o valor da propriedade do documento JSON
        value = GetJsonValueFromDocument(listInfo[0], jsonDocument);

        // Verifica se a string não foi encontrada e a cultura não é a padrão
        if (string.IsNullOrEmpty(value) && culture != Language.Default)
        {
          // Tenta encontrar a string na cultura padrão
          value = GetLocalizedString(keyParameter, Language.Default);
        }
        else
        {
          // Se a string não for encontrada, retorna a chave
          value = string.IsNullOrEmpty(value) ? listInfo[0] : value;
        }
      }

      // Define cache com a string localizada
      _distributedCache.SetString(cacheKey, value);

      // Substitui a key com a string localizada
      listInfo[0] = value;

      // Substitui os parâmetros da string localizada
      ReplaceParametersInList(listInfo, jsonDocument);
    }

    // Retorna a string encontrada
    return ReplaceParameters(listInfo);
  }

  /// <summary>
  /// Substitui os parâmetros da string localizada.
  /// </summary>
  /// <param name="listInfo">Lista de strings localizadas.</param>
  /// <param name="jsonDocument">Documento JSON.</param>
  private static void ReplaceParametersInList(string[] listInfo, JsonDocument jsonDocument)
  {
    // Itera sobre os parâmetros da string localizada
    for (int i = 1; i < listInfo.Length; i++)
    {
      // Substitui parâmetros da string pelas strings localizadas
      var param = GetJsonValueFromDocument(listInfo[i], jsonDocument);

      // Substitui o parâmetro se ele não for nulo ou vazio
      listInfo[i] = string.IsNullOrEmpty(param) ? listInfo[i] : param;
    }
  }

  /// <summary>
  /// Obtém todas as strings localizadas.
  /// </summary>
  /// <param name="includeParentCultures">Utilizado para avaliar se é a cultura padrão (True) ou a cultura atual (False).</param>
  /// <returns>Strings localizadas.</returns>
  internal IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
  {
    // Obtém o código de idioma atual
    string culture = includeParentCultures ? Language.Default : Language.Current;

    // Verifica se o arquivo existe
    if (_translations.TryGetValue(culture, out var jsonDocument))
    {
      // Retorna todas as strings localizadas
      foreach (var property in jsonDocument.RootElement.EnumerateObject())
      {
        // Retorna a string localizada
        yield return new LocalizedString(property.Name, property.Value.GetString()!, false);
      }
    }
  }


  /// <summary>
  /// Obtém o código de idioma selecionado ou o código de idioma padrão.
  /// </summary>
  /// <param name="cultureSelect">Código de idioma selecionado. Parâmetro opcional.</param>
  /// <returns>Código de idioma selecionado ou o código de idioma padrão.</returns>
  private static string GetCulture(string? cultureSelect)
  {
    // Retorna o código de idioma selecionado ou o código de idioma padrão
    return cultureSelect ?? Language.Current;
  }

  /// <summary>
  /// Carrega as traduções do arquivo JSON.
  /// </summary>
  /// <param name="culture">Código de idioma.</param>
  public void LoadTranslations(string culture)
  {
    // Definir caminho para arquivos JSON
    string defaultFilePath = GetFilePath(culture, true);
    string additionalFilePath = GetFilePath(culture, false);

    // Verifica se o arquivo padrão, adicional ou em stream existe
    if (!_translations.TryGetValue(culture, out _))
    {
      // Lendo texto do arquivo JSON padrão
      var defaultJson = ReadJsonFile(defaultFilePath);
      var additionalJson = !string.IsNullOrEmpty(additionalFilePath) ? ReadJsonFile(additionalFilePath) : null;

      // Mesclando os dois JSONs, dando prioridade ao JSON adicional
      var combinedJson = MergeJson(defaultJson, additionalJson);

      // Adiciona as traduções ao dicionário
      _translations[culture] = combinedJson;
    }
  }

  /// <summary>
  /// Obtém o caminho do arquivo JSON padrão.
  /// </summary>
  /// <param name="culture">Código de idioma.</param>
  /// <param name="defaultFile">Indica se é o arquivo JSON padrão.</param>
  /// <returns>Caminho do arquivo JSON padrão.</returns>
  private static string GetFilePath(string culture, bool defaultFile = true)
  {
    // Define o complemento do arquivo JSON
    string complement = defaultFile ? ".default" : "";

    // Caminho relativo para o arquivo JSON
    string relativeFilePath = Path.Combine("Resources", $"{culture}{complement}.json");

    // Retorna o caminho completo do arquivo JSON
    return Path.Combine(AppContext.BaseDirectory, relativeFilePath);
  }

  /// <summary>
  /// Verifica se o arquivo existe.
  /// </summary>
  /// <param name="filePath">Caminho do arquivo JSON.</param>
  /// <returns>Verdadeiro se o arquivo existir.</returns>
  private static bool FileExists(string filePath)
  {
    // Retorna verdadeiro se o arquivo existir
    return File.Exists(filePath);
  }

  /// <summary>
  /// Lê o arquivo JSON.
  /// </summary>
  /// <param name="filePath">Caminho do arquivo JSON.</param>
  /// <returns>Documento JSON.</returns>
  private static JsonDocument ReadJsonFile(string filePath)
  {
    // Verifica se o arquivo existe
    if (!FileExists(filePath))
    {
      // Retorna nulo se o arquivo não existir
      return JsonDocument.Parse("{}");
    }

    // Lendo texto do arquivo JSON
    using var additionalResourceStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

    // Reseta a posição do stream para garantir que ele seja lido do início
    if (additionalResourceStream.CanSeek)
    {
      // Reseta a posição do stream para garantir que ele seja lido do início
      additionalResourceStream.Seek(0, SeekOrigin.Begin);
    }

    // Lendo texto do arquivo JSON
    using var streamReader = new StreamReader(additionalResourceStream);

    // Retorna o arquivo JSON
    return JsonDocument.Parse(streamReader.ReadToEnd());
  }

  /// <summary>
  /// Mescla os JSONs.
  /// </summary>
  /// <param name="defaultJson">JSON padrão.</param>
  /// <param name="additionalJson">JSON adicional.</param>
  /// <returns>Documento JSON mesclado.</returns>
  private static JsonDocument MergeJson(JsonDocument defaultJson, JsonDocument? additionalJson)
  {
    // Se o JSON adicional for nulo, retorne o JSON padrão
    if (additionalJson == null)
    {
      // Retorna o JSON padrão
      return defaultJson;
    }

    // Cria um novo dicionário para mesclar os JSONs
    var merged = new Dictionary<string, JsonElement>();

    // Adiciona todas as propriedades do JSON padrão ao dicionário
    foreach (var property in defaultJson.RootElement.EnumerateObject())
    {
      // Adiciona propriedade ao dicionário
      merged[property.Name] = property.Value;
    }

    // Se o JSON adicional não for nulo, adicione todas as propriedades do JSON adicional ao dicionário
    if (additionalJson != null)
    {
      // Adiciona todas as propriedades do JSON adicional ao dicionário
      foreach (var property in additionalJson.RootElement.EnumerateObject())
      {
        // Adiciona propriedade ao dicionário
        merged[property.Name] = property.Value;
      }
    }

    // Serializa o dicionário mesclado em um JSON
    var mergedJson = JsonSerializer.Serialize(merged);

    // Retorna o JSON mesclado
    return JsonDocument.Parse(mergedJson);
  }

  /// <summary>
  /// Obtém a chave do cache.
  /// </summary>
  /// <param name="culture">Código de idioma.</param>
  /// <param name="key">Chave da string localizada.</param>
  /// <returns>Chave do cache.</returns>
  private static string GetCacheKey(string culture, string key)
  {
    // Retorna a chave do cache
    return $"locale_{culture}_{key}";
  }

  /// <summary>
  /// Obtém o valor da propriedade do documento JSON.
  /// </summary>
  /// <param name="propertyName">Propriedade a buscar no documento JSON.</param>
  /// <param name="jsonDocument">Documento JSON.</param>
  /// <returns>Valor da propriedade do documento JSON.</returns>
  private static string GetJsonValueFromDocument(string propertyName, JsonDocument jsonDocument)
  {
    // Valor localizado
    string? value = null;

    // Tentar obter a propriedade do documento JSON
    if (jsonDocument.RootElement.TryGetProperty(propertyName, out var jsonElement))
    {
      // Atribui o valor da propriedade ao valor localizado
      value = jsonElement.GetString();
    }

    // Retorna a propriedade se ela não for encontrada
    return value ?? string.Empty;
  }

  /// <summary>
  /// Substitui os parâmetros da string localizada.
  /// </summary>
  /// <param name="keyAndParameters">Chave e parâmetros da string localizada.</param>
  /// <returns>String localizada com os parâmetros substituídos.</returns>
  private static string ReplaceParameters(string[] keyAndParameters)
  {
    // Se houver apenas um elemento retorna sem formatação
    if (keyAndParameters.Length == 1)
    {
      // Retorna a string localizada sem formatação
      return keyAndParameters[0];
    }

    try
    {
      // Retorna a string localizada com os parâmetros substituídos
      return string.Format(keyAndParameters[0], keyAndParameters[1..]);
    }
    catch (FormatException)
    {
      // Se houver um erro de formatação, retorna a string original sem formatação
      return string.Join(";", keyAndParameters ?? []);
    }
  }
}

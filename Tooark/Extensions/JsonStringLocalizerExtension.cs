using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using static Tooark.Utils.Util;

namespace Tooark.Extensions;

/// <summary>
/// Localizador de strings que obtém as strings localizadas de arquivos JSON.
/// </summary>
/// <param name="distributedCache">O cache distribuído a ser usado pelos localizadores.</param>
/// <param name="resourceAdditionalPaths">Caminhos adicionais para arquivos JSON de recursos. Parâmetro opcional.</param>
/// <param name="fileStream">O provedor de arquivos para arquivos JSON de recursos. Parâmetro opcional.</param>
/// <remarks>
/// Este localizador de strings obtém as strings localizadas de arquivos JSON.
/// </remarks>
/// <seealso cref="IStringLocalizer"/>
public class JsonStringLocalizerExtension(IDistributedCache distributedCache, Dictionary<string, string>? resourceAdditionalPaths = null, Stream? fileStream = null) : IStringLocalizer
{
  /// <summary>
  /// Localizador de strings interno que obtém as strings localizadas de arquivos JSON.
  /// </summary>
  private readonly InternalJsonStringLocalizer _internalLocalizer = new(distributedCache, resourceAdditionalPaths, fileStream);

  /// <summary>
  /// Obtém uma string localizada com base no nome fornecido.
  /// </summary>
  /// <param name="name">O nome (key) da string localizada a ser obtida.</param>
  /// <returns>
  /// Uma instância de <see cref="LocalizedString"/> contendo a string localizada, ou o nome fornecido para busca.
  /// </returns>
  /// <exception cref="JsonException">
  /// Lançada se houver um erro ao analisar o arquivo JSON.
  /// </exception>
  public LocalizedString this[string name]
  {
    get
    {
      // Obtém a string localizada
      string value = _internalLocalizer.GetLocalizedString(name);

      // Retorna a string localizada ou o nome fornecido para busca
      return new LocalizedString(name, value ?? name, value == null);
    }
  }

  /// <summary>
  /// Obtém uma string localizada formatada com base no nome fornecido e nos argumentos.
  /// </summary>
  /// <param name="name">O nome da string localizada.</param>
  /// <param name="arguments">Os argumentos a serem formatados na string localizada.</param>
  /// <returns>
  /// Um objeto <see cref="LocalizedString"/> que contém a string localizada formatada com os argumentos fornecidos.
  /// Se o recurso não for encontrado, retorna o <see cref="LocalizedString"/> original.
  /// </returns>
  public LocalizedString this[string name, params object[] arguments]
  {
    get
    {
      // Obtém a string localizada
      var actualValue = this[name];

      // Retorna a string localizada formatada com os argumentos fornecidos
      return actualValue.ResourceNotFound
        ? actualValue
        : new LocalizedString(name, string.Format(actualValue.Value, arguments), false);
    }
  }

  /// <summary>
  /// Recupera todas as strings localizadas de um arquivo JSON para a cultura especificada.
  /// </summary>
  /// <param name="includeParentCultures">Se verdadeiro, a cultura atual é usada. Caso contrário, a cultura padrão é usada.</param>
  /// <returns>Uma coleção enumerável de <see cref="LocalizedString"/> contendo todas as strings localizadas.</returns>
  /// <exception cref="FileNotFoundException">Lançada se o arquivo JSON para a cultura especificada não for encontrado.</exception>
  /// <exception cref="JsonException">Lançada se houver um erro ao analisar o arquivo JSON.</exception>
  public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
  {
    return _internalLocalizer.GetAllStrings(includeParentCultures);
  }
}

/// <summary>
/// Classe interna localizador de strings que obtém as strings localizadas de arquivos JSON.
/// </summary>
/// <param name="distributedCache">O cache distribuído a ser usado pelos localizadores.</param>
/// <param name="resourceAdditionalPaths">Caminhos adicionais para arquivos JSON de recursos. Parâmetro opcional.</param>
/// <param name="fileStream">O provedor de arquivos para arquivos JSON de recursos. Parâmetro opcional.</param>
/// <remarks>
/// Este localizador de strings obtém as strings localizadas de arquivos JSON.
/// </remarks>
/// <seealso cref="IStringLocalizer"/>
internal class InternalJsonStringLocalizer(IDistributedCache distributedCache, Dictionary<string, string>? resourceAdditionalPaths, Stream? fileStream)
{
  private readonly IDistributedCache _distributedCache = distributedCache;
  private readonly Dictionary<string, string>? _resourceAdditionalPaths = resourceAdditionalPaths;
  private readonly Stream? _fileStream = fileStream;

  /// <summary>
  /// Obtém uma string localizada com base na key fornecida, suporta parâmetro de idioma.
  /// </summary>
  /// <param name="keyParameter">A key da string localizada a ser obtida.</param>
  /// <param name="cultureSelect">O código da cultura para selecionar o arquivo JSON apropriado. Se nulo ou vazio, a cultura atual é usada.</param>
  /// <returns>
  /// Uma instância de <see cref="LocalizedString"/> contendo a string localizada, ou a key fornecida para busca.
  /// </returns>
  /// <exception cref="JsonException">
  /// Lançada se houver um erro ao analisar o arquivo JSON.
  /// </exception>
  internal string GetLocalizedString(string keyParameter, string? cultureSelect = null)
  {
    // Obtém o código de idioma atual
    string culture = GetCulture(cultureSelect);

    // Definir caminho para arquivos JSON
    string defaultFilePath = GetDefaultFilePath(culture);
    string additionalFilePath = GetAdditionalFilePath(culture);

    // Verifica se o arquivo existe
    if (FileExists(defaultFilePath, additionalFilePath))
    {
      // Verifica se a chave contém parâmetros
      var listInfo = keyParameter.Split(';');

      // Executa o loop para substituir os parâmetros na string localizada
      for (int i = 1; i < listInfo.Length; i++)
      {
        // Recupera o parâmetro da string localizada
        var getParam = GetJsonValue(listInfo[i], defaultFilePath, additionalFilePath);

        // Substitui os parâmetros na string localizada
        listInfo[i] = getParam ?? listInfo[i];
      }

      // Declara a chave do cache e o valor do cache para o cache distribuído
      string cacheKey = GetCacheKey(culture, listInfo[0]);
      string? cacheValue = _distributedCache.GetString(cacheKey);

      // Se a string não for nula/vazia, retorne o valor já armazenado em cache
      if (!string.IsNullOrEmpty(cacheValue))
      {
        // Substitui a key pela string localizada no cache
        listInfo[0] = cacheValue;

        // Retorna a string encontrada no cache
        return ReplaceParameters(listInfo);
      }

      // Se a string for nula, procuramos a propriedade nos arquivos JSON
      string value = GetJsonValue(listInfo[0], defaultFilePath, additionalFilePath);

      // Se encontrarmos a propriedade dentro do arquivo JSON, atualizamos o cache com esse resultado
      if (!string.IsNullOrEmpty(value))
      {
        // Atualiza o cache com a string encontrada
        _distributedCache.SetString(cacheKey, value);
      }
      else
      {
        if (cultureSelect == null)
        {
          // Se cultureSelect for nulo, tente encontrar a string na cultura padrão
          return GetLocalizedString(keyParameter, Languages.Default);
        }

        // Retorna a chave se a string não for encontrada
        return keyParameter;
      }

      // Substitui a key pela string localizada
      listInfo[0] = value;

      // Retorna a string encontrada
      return ReplaceParameters(listInfo);
    }
    else
    {
      // Se cultureSelect for nulo, tente encontrar a string na cultura padrão
      if (cultureSelect == null)
      {
        // Se o arquivo JSON padrão não existir, tente encontrar a string na cultura padrão
        return GetLocalizedString(keyParameter, Languages.Default);
      }
    }

    // Retorna a chave se a string não for encontrada
    return keyParameter;
  }

  /// <summary>
  /// Recupera todas as strings localizadas de um arquivo JSON para a cultura especificada.
  /// </summary>
  /// <param name="includeParentCultures">Se verdadeiro, a cultura atual é usada. Caso contrário, a cultura padrão é usada.</param>
  /// <returns>Uma coleção enumerável de <see cref="LocalizedString"/> contendo todas as strings localizadas.</returns>
  /// <exception cref="FileNotFoundException">Lançada se o arquivo JSON para a cultura especificada não for encontrado.</exception>
  /// <exception cref="JsonException">Lançada se houver um erro ao analisar o arquivo JSON.</exception>
  internal IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
  {
    // Obtém o código de idioma atual
    string culture = includeParentCultures ? Languages.Current : Languages.Default;

    // Definir caminho para arquivos JSON
    string defaultFilePath = GetDefaultFilePath(culture);
    string additionalFilePath = GetAdditionalFilePath(culture);

    // Verifica se o arquivo existe
    if (FileExists(defaultFilePath, additionalFilePath))
    {
      // Lendo texto do arquivo JSON padrão
      var defaultJson = ReadJsonFile(defaultFilePath);

      // Lendo texto do arquivo JSON adicional, se existir
      var additionalJson = !string.IsNullOrEmpty(additionalFilePath) ? ReadJsonFile(additionalFilePath) : null;

      // Lendo texto do arquivo JSON do stream, se existir
      var streamJson = _fileStream != null ? ConvertStreamToJson(_fileStream) : null;

      // Mesclando os dois JSONs, dando prioridade ao JSON adicional
      var combinedJson = MergeJson(defaultJson, additionalJson, streamJson);

      // Retorna todas as strings localizadas
      foreach (var property in combinedJson.RootElement.EnumerateObject())
      {
        yield return new LocalizedString(property.Name, property.Value.GetString()!, false);
      }
    }
  }

  /// <summary>
  /// Obtém o código de idioma atual.
  /// </summary>
  /// <param name="cultureSelect">O código de idioma a ser selecionado.</param>
  /// <returns>O código de idioma atual.</returns>
  private static string GetCulture(string? cultureSelect)
  {
    return string.IsNullOrEmpty(cultureSelect) ? Languages.Current : cultureSelect;
  }

  /// <summary>
  /// Obtém o caminho do arquivo JSON padrão.
  /// </summary>
  /// <param name="culture">O código de idioma para selecionar o arquivo JSON apropriado.</param>
  /// <returns>O caminho do arquivo JSON padrão.</returns>
  private static string GetDefaultFilePath(string culture)
  {
    // Caminho relativo para o arquivo JSON
    string relativeFilePath = $"Resources/{culture}.default.json";

    // Retorna o caminho completo do arquivo JSON
    return Path.GetFullPath(relativeFilePath);
  }

  /// <summary>
  /// Obtém o caminho do arquivo JSON adicional.
  /// </summary>
  /// <param name="culture">O código de idioma para selecionar o arquivo JSON apropriado.</param>
  /// <returns>O caminho do arquivo JSON adicional.</returns>
  private string GetAdditionalFilePath(string culture)
  {
    // Caminho relativo para o arquivo JSON
    return _resourceAdditionalPaths?.GetValueOrDefault(culture) ?? string.Empty;
  }

  /// <summary>
  /// Verifica se o arquivo JSON padrão ou adicional existe.
  /// </summary>
  /// <param name="defaultFilePath">O caminho do arquivo JSON padrão.</param>
  /// <param name="additionalFilePath">O caminho do arquivo JSON adicional.</param>
  /// <returns>Verdadeiro se o arquivo JSON padrão ou adicional existir, caso contrário, falso.</returns>
  private static bool FileExists(string defaultFilePath, string additionalFilePath)
  {
    // Verifica se o arquivo padrão ou adicional existe
    return File.Exists(defaultFilePath) || (!string.IsNullOrEmpty(additionalFilePath) && File.Exists(additionalFilePath));
  }

  /// <summary>
  /// Obtém a chave do cache para o cache distribuído.
  /// </summary>
  /// <param name="culture"></param>
  /// <param name="key"></param>
  /// <returns>A chave do cache.</returns>
  private static string GetCacheKey(string culture, string key)
  {
    // Retorna a chave do cache
    return $"locale_{culture}_{key}";
  }

  /// <summary>
  /// Obtém o valor da propriedade JSON com base na chave fornecida.
  /// </summary>
  /// <param name="propertyName">A chave da propriedade JSON a ser obtida.</param>
  /// <param name="defaultFilePath">O caminho do arquivo JSON padrão.</param>
  /// <param name="additionalFilePath">O caminho do arquivo JSON adicional.</param>
  /// <returns>O valor da propriedade JSON.</returns>
  /// <exception cref="JsonException">Lançada se houver um erro ao analisar o arquivo JSON.</exception>
  /// <exception cref="FileNotFoundException">Lançada se o arquivo JSON não for encontrado.</exception>
  private string GetJsonValue(string propertyName, string defaultFilePath, string additionalFilePath)
  {
    // Verifica se a propriedade e o caminho do arquivo são nulos, retorne valor padrão do tipo
    if (propertyName == null || defaultFilePath == null)
    {
      // Se a propriedade ou o caminho do arquivo for nulo, retorne valor padrão do tipo
      return default!;
    }

    // Lendo texto do arquivo JSON padrão
    var defaultJson = ReadJsonFile(defaultFilePath);

    // Lendo texto do arquivo JSON adicional, se existir
    var additionalJson = !string.IsNullOrEmpty(additionalFilePath) ? ReadJsonFile(additionalFilePath) : null;

    // Lendo texto do arquivo JSON do stream, se existir
    var streamJson = _fileStream != null ? ConvertStreamToJson(_fileStream) : null;

    // Se a propriedade for encontrada no JSON do stream, retorne o valor da propriedade
    if (streamJson != null && streamJson.RootElement.TryGetProperty(propertyName, out var streamJsonElement))
    {
      return streamJsonElement.GetString()!;
    }

    // Se a propriedade for encontrada no JSON adicional, retorne o valor da propriedade
    if (additionalJson != null && additionalJson.RootElement.TryGetProperty(propertyName, out var additionalJsonElement))
    {
      return additionalJsonElement.GetString()!;
    }

    // Se a propriedade for encontrada no JSON padrão, retorne o valor da propriedade
    if (defaultJson.RootElement.TryGetProperty(propertyName, out var defaultJsonElement))
    {
      return defaultJsonElement.GetString()!;
    }

    // Se a propriedade não for encontrada, retorne valor padrão do tipo
    return default!;
  }

  /// <summary>
  /// Substitui os parâmetros na string localizada.
  /// </summary>
  /// <param name="keyParameters">Lista com a string e parâmetros para serem substituídos</param>
  /// <returns>A string localizada com os parâmetros substituídos.</returns>
  /// <exception cref="FormatException">Lançada se houver um erro ao substituir os parâmetros na string localizada.</exception>
  private static string ReplaceParameters(string[] keyParameters)
  {
    // Se houver apenas um elemento retorna sem formatação
    if (keyParameters.Length == 1)
    {
      return keyParameters[0];
    }

    try
    {
      // Retorna a string localizada com os parâmetros substituídos
      return string.Format(keyParameters[0], keyParameters[1..]);
    }
    catch (FormatException)
    {
      // Se houver um erro de formatação, retorna a string original sem formatação
      return keyParameters?.ToString() ?? string.Empty;
    }
  }

  /// <summary>
  /// Lê um arquivo JSON e retorna um documento JSON.
  /// </summary>
  /// <param name="filePath">O caminho do arquivo JSON a ser lido.</param>
  /// <returns>Um documento JSON.</returns>
  /// <exception cref="JsonException">Lançada se houver um erro ao analisar o arquivo JSON.</exception>
  /// <exception cref="FileNotFoundException">Lançada se o arquivo JSON não for encontrado.</exception>
  private static JsonDocument ReadJsonFile(string filePath)
  {
    // Lendo texto do arquivo JSON
    using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

    // Verifica se o stream é nulo ou vazio
    if (fileStream == null || fileStream.Length == 0)
    {
      return default!;
    }

    // Reseta a posição do stream para garantir que ele seja lido do início
    if (fileStream.CanSeek)
    {
      fileStream.Seek(0, SeekOrigin.Begin);
    }

    using var streamReader = new StreamReader(fileStream);

    // Retorna o arquivo JSON
    return JsonDocument.Parse(streamReader.ReadToEnd());
  }

  /// <summary>
  ///  Mescla dois JSONs em um único JSON.
  /// </summary>
  /// <param name="defaultJson">O JSON padrão.</param>
  /// <param name="additionalJson">O JSON adicional. Parâmetro opcional.</param>
  /// <param name="streamJson">O JSON do stream. Parâmetro opcional.</param>
  /// <returns>Um JSON mesclado.</returns>
  private static JsonDocument MergeJson(JsonDocument defaultJson, JsonDocument? additionalJson, JsonDocument? streamJson = null)
  {
    // Se o JSON adicional for nulo, retorne o JSON padrão
    if (additionalJson == null && streamJson == null)
    {
      // Retorna o JSON padrão
      return defaultJson;
    }

    // Cria um novo dicionário para mesclar os JSONs
    var merged = new Dictionary<string, JsonElement>();

    // Adiciona todas as propriedades do JSON padrão ao dicionário
    foreach (var property in defaultJson.RootElement.EnumerateObject())
    {
      merged[property.Name] = property.Value;
    }

    // Se o JSON adicional não for nulo, adicione todas as propriedades do JSON adicional ao dicionário
    if (additionalJson != null)
    {
      // Adiciona todas as propriedades do JSON adicional ao dicionário
      foreach (var property in additionalJson.RootElement.EnumerateObject())
      {
        merged[property.Name] = property.Value;
      }
    }

    // Se o JSON do stream não for nulo, adicione todas as propriedades do JSON adicional vindas do stream ao dicionário
    if (streamJson != null)
    {
      // Adiciona todas as propriedades do JSON adicional vindas do stream ao dicionário
      foreach (var property in streamJson.RootElement.EnumerateObject())
      {
        merged[property.Name] = property.Value;
      }
    }

    // Serializa o dicionário mesclado em um JSON
    var mergedJson = JsonSerializer.Serialize(merged);

    // Retorna o JSON mesclado
    return JsonDocument.Parse(mergedJson);
  }

  /// <summary>
  /// Converte um stream em um documento JSON.
  /// </summary>
  /// <param name="stream">O stream a ser convertido em um documento JSON.</param>
  /// <returns>Um documento JSON.</returns>
  private static JsonDocument ConvertStreamToJson(Stream? stream)
  {
    if (stream == null || stream.Length == 0)
    {
      return default!;
    }

    // Reseta a posição do stream para garantir que ele seja lido do início
    if (stream.CanSeek)
    {
      stream.Seek(0, SeekOrigin.Begin);
    }

    // Lendo texto do stream
    using var memoryStream = new MemoryStream();
    stream.CopyTo(memoryStream);
    memoryStream.Seek(0, SeekOrigin.Begin);

    // Lendo texto do stream
    using var reader = new StreamReader(memoryStream);

    // Retorna o stream convertido em JSON
    return JsonDocument.Parse(reader.ReadToEnd());
  }
}

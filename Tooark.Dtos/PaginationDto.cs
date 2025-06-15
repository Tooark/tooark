using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace Tooark.Dtos;

/// <summary>
/// Classe para parâmetros de paginação.
/// </summary>
public class PaginationDto
{
  #region Constructors

  /// <summary>
  /// Construtor com parâmetro de total de registros.
  /// </summary>
  /// <param name="total">Total de registros.</param>
  /// <returns>Retorna um objeto de paginação.</returns>
  public PaginationDto(long total)
  {
    // Total de registros
    Total = total;
  }

  /// <summary>
  /// Construtor com parâmetro de requisição.
  /// </summary>
  /// <param name="request">Requisição HTTP atual.</param>
  /// <returns>Retorna um objeto de paginação.</returns>
  public PaginationDto(HttpRequest request)
  {
    // Monta o link atual da requisição
    CurrentLink = BuildCurrentLink(request);
  }

  /// <summary>
  /// Construtor com parâmetros de total de registros e requisição.
  /// </summary>
  /// <param name="total">Total de registros.</param>
  /// <param name="request">Requisição HTTP atual.</param>
  /// <returns>Retorna um objeto de paginação.</returns>
  public PaginationDto(long total, HttpRequest request)
  {
    // Total de registros
    Total = total;

    // URL base da requisição
    var baseUrl = $"{request.Scheme}://{request.Host}{request.Path}";

    // URL atual da requisição. Base + QueryString
    CurrentLink = $"{baseUrl}{request.QueryString}";

    // Verifica se existem registros
    if (total > 0)
    {
      // QueryString da requisição
      var query = QueryHelpers.ParseQuery(request.QueryString.ToString());

      // Define o índice da página da requisição
      PageIndex = GetQueryValue("SearchDto.PageIndex", query);

      // Define o tamanho da página da requisição
      PageSize = GetQueryValue("SearchDto.PageSize", query);

      // Se existir parâmetro Index e Size de paginação. E o tamanho da pagina for menor que o total de registros
      if (PageSize > 0 && PageIndex >= 0 && PageSize < Total)
      {
        // Define informações da página anterior
        SetPrevious(PageIndex, baseUrl, query);

        // Define informações da página seguinte
        SetNext(PageIndex, baseUrl, query);
      }
    }
  }

  /// <summary>
  /// Construtor com parâmetros de total de registros, tamanho da página, índice da página, índice da página anterior, índice da página seguinte e requisição.
  /// </summary>
  /// <param name="total">Total de registros.</param>
  /// <param name="pageSize">Tamanho da página.</param>
  /// <param name="pageIndex">Índice da página.</param>
  /// <param name="previous">Índice da página anterior.</param>
  /// <param name="next">Índice da página seguinte.</param>
  /// <param name="request">Requisição HTTP atual.</param>
  /// <returns>Retorna um objeto de paginação.</returns>
  public PaginationDto(long total, long pageSize, long pageIndex, long previous, long next, HttpRequest request)
  {
    // Atualiza os valores conforme os parâmetros
    Total = total;
    PageSize = pageSize;
    PageIndex = pageIndex;

    // URL base da requisição
    var baseUrl = $"{request.Scheme}://{request.Host}{request.Path}";

    // URL atual da requisição. Base + QueryString
    CurrentLink = $"{baseUrl}{request.QueryString}";

    // Se existir registros, Index e Size de paginação. E o tamanho da pagina for menor que o total de registros.
    if (total > 0 && PageSize > 0 && PageIndex >= 0 && PageSize < Total)
    {
      // QueryString da requisição
      var query = QueryHelpers.ParseQuery(request.QueryString.ToString());

      // Atualiza o tamanho da página na QueryString
      query["SearchDto.PageSize"] = PageSize.ToString();

      // Define informações da página anterior
      SetPrevious(previous + 1, baseUrl, query);

      // Define informações da página seguinte
      SetNext(next - 1, baseUrl, query);
    }
  }

  /// <summary>
  /// Construtor com parâmetros de total de registros, busca com paginação e requisição.
  /// </summary>
  /// <param name="total">Total de registros.</param>
  /// <param name="searchDto">Objeto de busca com paginação.</param>
  /// <param name="request">Requisição HTTP atual.</param>
  /// <returns>Retorna um objeto de paginação.</returns>
  public PaginationDto(long total, SearchDto searchDto, HttpRequest request)
  {
    // Atualiza os valores conforme os parâmetros
    Total = total;
    PageSize = searchDto.PageSize;
    PageIndex = searchDto.PageIndex;

    // URL base da requisição
    var baseUrl = $"{request.Scheme}://{request.Host}{request.Path}";

    // URL atual da requisição. Base + QueryString
    CurrentLink = $"{baseUrl}{request.QueryString}";

    // Se existir registros, Index e Size de paginação. E o tamanho da pagina for menor que o total de registros.
    if (total > 0 && PageSize > 0 && PageIndex >= 0 && PageSize < Total)
    {
      // QueryString da requisição
      var query = QueryHelpers.ParseQuery(request.QueryString.ToString());

      // Verifica se existe parâmetro de busca
      if (!string.IsNullOrEmpty(searchDto.Search))
      {
        // Atualiza parâmetro de busca na QueryString
        query["SearchDto.Search"] = searchDto.Search;
      }

      // Atualiza o tamanho da página na QueryString
      query["SearchDto.PageSize"] = PageSize.ToString();

      // Define informações da página anterior
      SetPrevious(PageIndex, baseUrl, query);

      // Define informações da página seguinte
      SetNext(PageIndex, baseUrl, query);
    }
  }

  #endregion

  #region Properties

  /// <summary>
  /// Total de registros.
  /// </summary>
  /// <value>Valor padrão é: 0.</value>
  public long Total { get; private set; } = 0;

  /// <summary>
  /// Tamanho da página.
  /// </summary>
  /// <value>Valor padrão é: 0.</value>
  public long PageSize { get; private set; } = 10;

  /// <summary>
  /// Índice da página.
  /// </summary>
  /// <value>Valor padrão é: 0.</value>
  public long PageIndex { get; private set; } = 1;

  /// <summary>
  /// Índice da página anterior.
  /// </summary>
  /// <value>Valor padrão é: nulo.</value>
  public long? Previous { get; private set; } = null;

  /// <summary>
  /// Índice da página seguinte.
  /// </summary>
  /// <value>Valor padrão é: nulo.</value>
  public long? Next { get; private set; } = null;

  /// <summary>
  /// Link da página atual.
  /// </summary>
  /// <value>Valor padrão é: nulo.</value>
  public string? CurrentLink { get; private set; } = null;

  /// <summary>
  /// Link da página anterior.
  /// </summary>
  /// <value>Valor padrão é: nulo.</value>
  public string? PreviousLink { get; private set; } = null;

  /// <summary>
  /// Link da página seguinte.
  /// </summary>
  /// <value>Valor padrão é: nulo.</value>
  public string? NextLink { get; private set; } = null;

  #endregion

  #region Private Methods

  /// <summary>
  /// Helper para montar o link atual da requisição.
  /// </summary>
  /// <param name="request">Requisição HTTP atual.</param>
  /// <returns>Retorna o link completo da requisição.</returns>
  private static string BuildCurrentLink(HttpRequest request)
  {
    return $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";
  }

  /// <summary>
  /// Função para obter o valor de um parâmetro da QueryString.
  /// </summary>
  /// <param name="key">Chave do parâmetro.</param>
  /// <param name="query">Dicionário de parâmetros da QueryString.</param>
  /// <returns>Retorna o valor do parâmetro.</returns>
  private static long GetQueryValue(string key, Dictionary<string, StringValues> query)
  {
    // Se existir a chave no dicionário e for possível converter para long, retorna o valor. Senão retorna 0.
    return
      query.TryGetValue(key, out StringValues value) &&
      long.TryParse(value, out long result) ?
      result :
      0;
  }

  /// <summary>
  /// Função para gerar um link de paginação.
  /// </summary>
  /// <param name="baseUrl">URL base.</param>
  /// <param name="query">Dicionário de parâmetros da QueryString.</param>
  /// <param name="pageIndex">Índice da página.</param>
  /// <returns>Retorna o link de paginação.</returns>
  private static string? GenerateLink(string baseUrl, Dictionary<string, StringValues> query, long pageIndex)
  {
    // Atualiza o índice da página na QueryString
    query["SearchDto.PageIndex"] = pageIndex.ToString();

    // Retorna o link de paginação.
    return $"{baseUrl}{QueryString.Create(query)}";
  }

  /// <summary>
  /// Função para definir o índice e link da página anterior.
  /// </summary>
  /// <param name="index">Índice da página.</param>
  /// <param name="baseUrl">URL base.</param>
  /// <param name="query">Dicionário de parâmetros da QueryString.</param>
  private void SetPrevious(long index, string baseUrl, Dictionary<string, StringValues> query)
  {
    // Se a página atual não for a primeira
    if (index > 0)
    {
      // Calcula o índice da página anterior
      Previous = index - 1;

      // Atualiza o índice da página na QueryString para gerar o link da página anterior
      PreviousLink = GenerateLink(baseUrl, query, index - 1);
    }
  }

  /// <summary>
  /// Função para definir o índice e link da página seguinte.
  /// </summary>
  /// <param name="index">Índice da página.</param>
  /// <param name="baseUrl">URL base.</param>
  /// <param name="query">Dicionário de parâmetros da QueryString.</param>
  private void SetNext(long index, string baseUrl, Dictionary<string, StringValues> query)
  {
    // Se a página atual não for a última
    if ((index + 1) * PageSize < Total)
    {
      // Calcula o índice da página seguinte
      Next = index + 1;

      // Atualiza o índice da página na QueryString para gerar o link da página seguinte
      NextLink = GenerateLink(baseUrl, query, index + 1);
    }
  }

  #endregion
}

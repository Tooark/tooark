using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace Tooark.Dtos;

/// <summary>
/// Classe para parâmetros de paginação.
/// </summary>
public class PaginationDto
{
  /// <summary>
  /// Total de registros.
  /// </summary>
  /// <value>Valor padrão é: 0.</value>
  public int Total { get; private set; } = 0;

  /// <summary>
  /// Tamanho da página.
  /// </summary>
  /// <value>Valor padrão é: 0.</value>
  public int PageSize { get; set; } = 0;

  /// <summary>
  /// Índice da página.
  /// </summary>
  /// <value>Valor padrão é: 0.</value>
  public int PageIndex { get; set; } = 0;

  /// <summary>
  /// Índice da página anterior.
  /// </summary>
  /// <value>Valor padrão é: nulo.</value>
  public int? Previous { get; set; } = null;

  /// <summary>
  /// Índice da página seguinte.
  /// </summary>
  /// <value>Valor padrão é: nulo.</value>
  public int? Next { get; set; } = null;

  /// <summary>
  /// Link da página atual.
  /// </summary>
  /// <value>Valor padrão é: nulo.</value>
  public string? CurrentLink { get; set; } = null;

  /// <summary>
  /// Link da página anterior.
  /// </summary>
  /// <value>Valor padrão é: nulo.</value>
  public string? PreviousLink { get; set; } = null;

  /// <summary>
  /// Link da página seguinte.
  /// </summary>
  /// <value>Valor padrão é: nulo.</value>
  public string? NextLink { get; set; } = null;


  /// <summary>
  /// Construtor com parâmetros de total de registros e requisição.
  /// </summary>
  /// <param name="total">Total de registros.</param>
  /// <param name="request">Requisição HTTP atual.</param>
  /// <returns>Retorna um objeto de paginação.</returns>
  public PaginationDto(int total, HttpRequest request)
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
      PageIndex = GetQueryValue("PageIndex", query);

      // Define o tamanho da página da requisição
      PageSize = GetQueryValue("PageSize", query);

      // Se existir parâmetro Index e Size de paginação. E o tamanho da pagina for menor que o total de registros
      if (PageSize > 0 && PageIndex >= 0 && PageSize < Total)
      {
        // Se a página atual não for a primeira
        if (PageIndex > 0)
        {
          // Calcula o índice da página anterior
          Previous = PageIndex - 1;

          // Atualiza o índice da página na QueryString para gerar o link da página anterior
          PreviousLink = GenerateLink(baseUrl, query, PageIndex - 1);
        }
        
        // Se a página atual não for a última
        if((PageIndex + 1) * PageSize < Total)
        {
          // Calcula o índice da página seguinte
          Next = PageIndex + 1;

          // Atualiza o índice da página na QueryString para gerar o link da página seguinte
          NextLink = GenerateLink(baseUrl, query, PageIndex + 1);
        }
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
  public PaginationDto(int total, int pageSize, int pageIndex, int previous, int next, HttpRequest request)
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
      query["PageSize"] = PageSize.ToString();

      // Se a página atual não for a primeira
      if (previous >= 0)
      {
        // Define o índice da página anterior
        Previous = previous;
    
        // Atualiza o índice da página na QueryString para gerar o link da página anterior
        PreviousLink = GenerateLink(baseUrl, query, previous);
      }

      // Se a página atual não for a última
      if (next * PageSize < Total)
      {
        // Define o índice da página seguinte
        Next = next;

        // Atualiza o índice da página na QueryString para gerar o link da página seguinte
        NextLink = GenerateLink(baseUrl, query, next);
      }
    }
  }


  /// <summary>
  /// Função para obter o valor de um parâmetro da QueryString.
  /// </summary>
  /// <param name="key">Chave do parâmetro.</param>
  /// <param name="query">Dicionário de parâmetros da QueryString.</param>
  /// <returns>Retorna o valor do parâmetro.</returns>
  private static int GetQueryValue(string key, Dictionary<string, StringValues> query)
  {
    // Se existir a chave no dicionário e for possível converter para inteiro, retorna o valor. Senão retorna 0.
    return
      query.TryGetValue(key, out StringValues value) &&
      int.TryParse(value, out int result) ?
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
  private static string? GenerateLink(string baseUrl, Dictionary<string, StringValues> query, int pageIndex)
  {
    // Atualiza o índice da página na QueryString
    query["PageIndex"] = pageIndex.ToString();

    // Retorna o link de paginação.
    return $"{baseUrl}{QueryString.Create(query)}";
  }
}

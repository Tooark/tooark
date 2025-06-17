using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Tooark.Extensions;

namespace Tooark.Dtos;

/// <summary>
/// Classe para parâmetros de busca.
/// </summary>
public class SearchDto : Dto
{
  #region Private Properties

  /// <summary>
  /// Informação privada a ser procurada.
  /// </summary>
  private string? _search = null!;

  /// <summary>
  /// Cache para a informação normalizada.
  /// </summary>
  private string? _searchNormalized = null;

  /// <summary>
  /// Índice privado da paginação.
  /// </summary>
  private long _pageIndex = 1;

  /// <summary>
  /// Tamanho privado da paginação.
  /// </summary>
  private long _pageSize = 50;

  #endregion

  #region Constructors

  /// <summary>
  /// Construtor padrão.
  /// </summary>
  public SearchDto()
  { }

  /// <summary>
  /// Construtor com parâmetro de busca.
  /// </summary>
  /// <param name="search">Informação a ser procurada.</param>
  public SearchDto(string? search)
  {
    Search = search;
  }

  /// <summary>
  /// Construtor com parâmetros de paginação.
  /// </summary>
  /// <param name="pageIndex">Índice da paginação.</param>
  /// <param name="pageSize">Tamanho da paginação.</param>
  public SearchDto(long pageIndex, long pageSize)
  {
    PageIndex = pageIndex;
    PageSize = pageSize;
  }

  /// <summary>
  /// Construtor com parâmetros de busca e paginação.
  /// </summary>
  /// <param name="search">Informação a ser procurada.</param>
  /// <param name="pageIndex">Índice da paginação.</param>
  /// <param name="pageSize">Tamanho da paginação.</param>
  public SearchDto(string? search, long pageIndex, long pageSize)
  {
    Search = search;
    PageIndex = pageIndex;
    PageSize = pageSize;
  }

  #endregion

  #region Properties

  /// <summary>
  /// Informação a ser procurada.
  /// </summary>
  public string? Search
  {
    get => _search;
    set
    {
      _search = value;
      _searchNormalized = null; // Invalida o cache ao alterar Search
    }
  }

  /// <summary>
  /// Informação a ser procurada normalizada.
  /// </summary>
  [BindNever]
  [JsonIgnore]
  public string? SearchNormalized
  {
    get
    {
      if (_searchNormalized == null && _search != null)
      {
        _searchNormalized = _search.ToNormalize();
      }
      
      return _searchNormalized;
    }
  }

  /// <summary>
  /// Índice da paginação.
  /// </summary>
  /// <value>Parâmetro padrão é 1.</value>
  /// <remarks>
  /// Utilizar valor 0 (zero) para ignorar índice.
  /// </remarks>
  public long PageIndex
  {
    get => _pageIndex;
    set => _pageIndex = value < 0 ? 0 : value;
  }

  /// <summary>
  /// Índice lógico da paginação.
  /// </summary>
  /// <value>Parâmetro padrão é 0.</value>
  [BindNever]
  [JsonIgnore]
  public long PageIndexLogical
  {
    get => _pageIndex > 0 ? _pageIndex - 1 : 0;
  }

  /// <summary>
  /// Tamanho da paginação.
  /// </summary>
  /// <value>Parâmetro padrão é 50.</value>
  /// <remarks>
  /// Utilizar valor 0 (zero) para ignorar tamanho.
  /// </remarks>
  public long PageSize
  {
    get => _pageSize;
    set => _pageSize = value < 0 ? 0 : value;
  }

  #endregion
}

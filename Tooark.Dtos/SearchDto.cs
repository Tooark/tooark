namespace Tooark.Dtos;

/// <summary>
/// Classe para parâmetros de busca.
/// </summary>
public abstract class SearchDto : Dto
{
  /// <summary>
  /// Índice privado da paginação.
  /// </summary>
  private long pageIndex = 1;

  /// <summary>
  /// Tamanho privado da paginação.
  /// </summary>
  private long pageSize = 50;


  /// <summary>
  /// Informação a ser procurada.
  /// </summary>
  public string? Search { get; set; }

  /// <summary>
  /// Índice da paginação.
  /// </summary>
  /// <value>Parâmetro padrão é 1.</value>
  /// <remarks>
  /// Utilizar valor 0 (zero) para ignorar índice.
  /// </remarks>
  public long PageIndex
  {
    get => pageIndex;
    set => pageIndex = value < 0 ? 0 : value;
  }

  /// <summary>
  /// Índice lógico da paginação.
  /// </summary>
  /// <value>Parâmetro padrão é 0.</value>
  public long PageIndexLogical
  {
    get => pageIndex > 0 ? pageIndex - 1 : 0;
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
    get => pageSize;
    set => pageSize = value < 0 ? 0 : value;
  }
}

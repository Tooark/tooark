namespace Tooark.Dtos;

/// <summary>
/// Classe para parâmetros de busca com parâmetro de ordenação.
/// </summary>
public abstract class SearchOrderDto : SearchDto
{
  /// <summary>
  /// Referencia a ser ordenada.
  /// </summary>
  /// <remarks>
  /// O nome da coluna da tabela para ser ordenada.
  /// </remarks>
  public string OrderBy { get; set; } = null!;

  /// <summary>
  /// Sentido da ordenação.Crescente=true ou Decrescente=false. Por padrão a ordenação é crescente.
  /// </summary>
  /// <value>Valor padrão é <c>true</c>.</value>
  /// <remarks>
  /// Para ordenação crescente atribuir <c>true</c> ou decrescente atribuir <c>false</c>.
  /// </remarks>
  public bool OrderAsc { get; set; } = true;
}

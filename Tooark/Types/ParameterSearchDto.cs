namespace Tooark.Types;

/// <summary>
/// Classe para parâmetros de busca.
/// </summary>
public abstract class ParameterSearchDto
{
  /// <summary>
  /// Informação a ser procurada.
  /// </summary>
  public string? Search { get; set; }

  /// <summary>
  /// Índice da paginação. Por padrão o valor é: 0.
  /// </summary>
  public int PageIndex { get; set; } = 0;

  /// <summary>
  /// Tamanho da paginação. Por padrão o valor é: 50. Para ignorar tamanho passar parâmetro: 0.
  /// </summary>
  public int PageSize { get; set; } = 50;
}

/// <summary>
/// Classe para parâmetros de busca com parâmetro de ordenação.
/// </summary>
public abstract class ParameterSearchOrderDto : ParameterSearchDto
{
  /// <summary>
  /// Parâmetros para ordenação.
  /// </summary>
  public Order? Order { get; set; }
}

/// <summary>
/// Classe para parâmetros de ordenação.
/// </summary>
public class Order
{
  /// <summary>
  /// Nome da coluna da tabela para ser ordenada, caso não exista ordena por ID.
  /// </summary>
  public string Field { get; set; } = null!;

  /// <summary>
  /// Sentido da ordenação. Crescente=true ou Decrescente=false. Por padrão a ordenação é crescente.
  /// </summary>
  public bool Asc { get; set; } = true;
}

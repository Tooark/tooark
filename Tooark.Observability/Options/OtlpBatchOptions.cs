namespace Tooark.Observability.Options;

/// <summary>
/// Opções do processador de exportação em lote (Batch) para OTLP.
/// </summary>
public class OtlpBatchOptions
{
  #region Private properties

  /// <summary>
  /// Tamanho máximo da fila interna de itens aguardando exportação. Padrão: 2048.
  /// </summary>
  private int _maxQueueSize = 2048;

  /// <summary>
  /// Intervalo (em ms) interno entre envios em lote. Padrão: 5000.
  /// </summary>
  private int _scheduledDelayMilliseconds = 5000;

  /// <summary>
  /// Timeout (em ms) máximo interno por tentativa de exportação. Padrão: 30000.
  /// </summary>
  private int _exporterTimeoutMilliseconds = 30000;

  /// <summary>
  /// Tamanho máximo (em itens) interno de cada lote enviado. Padrão: 512.
  /// </summary>
  private int _maxExportBatchSize = 512;

  #endregion

  #region Properties

  /// <summary>
  /// Tamanho máximo da fila interna de itens aguardando exportação. Padrão: 2048.
  /// </summary>
  /// <remarks>
  /// Se o valor for menor ou igual a zero, será usado o valor padrão.
  /// </remarks>
  public int MaxQueueSize 
  {
    get => _maxQueueSize;
    set => _maxQueueSize = (value > 0) ? value : 2048;
  }

  /// <summary>
  /// Intervalo (em ms) entre envios em lote. Padrão: 5000.
  /// </summary>
  /// <remarks>
  /// Se o valor for menor que zero, será usado o valor padrão.
  /// </remarks>
  public int ScheduledDelayMilliseconds 
  {
    get => _scheduledDelayMilliseconds;
    set => _scheduledDelayMilliseconds = (value >= 0) ? value : 5000;
  }

  /// <summary>
  /// Timeout (em ms) máximo por tentativa de exportação. Padrão: 30000.
  /// </summary>
  /// <remarks>
  /// Se o valor for menor ou igual a zero, será usado o valor padrão.
  /// </remarks>
  public int ExporterTimeoutMilliseconds 
  {
    get => _exporterTimeoutMilliseconds;
    set => _exporterTimeoutMilliseconds = (value > 0) ? value : 30000;
  }

  /// <summary>
  /// Tamanho máximo (em itens) de cada lote enviado. Padrão: 512.
  /// </summary>
  /// <remarks>
  /// Se o valor for maior que <see cref="MaxQueueSize"/>, será usado o valor de <see cref="MaxQueueSize"/>.
  /// Se o valor for menor ou igual a zero, será usado o valor padrão.
  /// </remarks>
  public int MaxExportBatchSize
  {
    get => _maxExportBatchSize;
    set => _maxExportBatchSize = (value < _maxQueueSize) ? (value > 0 ? value : 512) : _maxQueueSize;
  }

  #endregion
}

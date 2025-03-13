namespace Tooark.Enums;

/// <summary>
/// Provedores de Cloud.
/// </summary>
public sealed class ECloudProvider
{
  /// <summary>
  /// Nenhum provedor de Cloud.
  /// </summary>
  public static readonly ECloudProvider None = new(0, "None");

  /// <summary>
  /// Amazon Web Services.
  /// </summary>
  public static readonly ECloudProvider Amazon = new(1, "AWS");

  /// <summary>
  /// Google Cloud Platform.
  /// </summary>
  public static readonly ECloudProvider Google = new(2, "GCP");

  /// <summary>
  /// Microsoft Azure.
  /// </summary>
  public static readonly ECloudProvider Microsoft = new(3, "Azure");


  /// <summary>
  /// Construtor privado da classe.
  /// </summary>
  /// <param name="id">Identificador do provedor de Cloud.</param>
  /// <param name="description">Descrição do provedor de Cloud.</param>
  /// <returns>Uma instância da classe <see cref="ECloudProvider"/>.</returns>
  private ECloudProvider(int id, string description)
  {
    Id = id;
    Description = description;
  }


  /// <summary>
  /// Id do provedor de Cloud.
  /// </summary>
  private int Id { get; }

  /// <summary>
  /// Descrição do provedor de Cloud.
  /// </summary>
  private string Description { get; }


  /// <summary>
  /// Função que retorna um provedor de Cloud a partir de sua descrição.
  /// </summary>
  /// <param name="description">Descrição do provedor de Cloud.</param>
  /// <returns>Uma instância de <see cref="ECloudProvider"/>.</returns>
  private static ECloudProvider FromDescription(string description) => description switch
  {
    "Amazon" => Amazon,
    "AWS" => Amazon,
    "Google" => Google,
    "GCP" => Google,
    "Microsoft" => Microsoft,
    "Azure" => Microsoft,
    _ => None
  };

  /// <summary>
  /// Função que retorna um provedor de Cloud a partir de seu id.
  /// </summary>
  /// <param name="id">Id do provedor de Cloud.</param>
  /// <returns>Uma instância de <see cref="ECloudProvider"/>.</returns>
  private static ECloudProvider FromId(int id) => id switch
  {
    1 => Amazon,
    2 => Google,
    3 => Microsoft,
    _ => None
  };


  /// <summary>
  /// Sobrescrita do método <see cref="object.ToString"/> para retornar a descrição do provedor de Cloud.
  /// </summary>
  /// <returns>A descrição do provedor de Cloud.</returns>
  public override string ToString() => Description;

  /// <summary>
  /// Método que retorna o id do provedor de Cloud.
  /// </summary>
  /// <returns>O id do provedor de Cloud.</returns>
  public int ToInt() => Id;

  
  /// <summary>
  /// Conversão implícita de <see cref="ECloudProvider"/> para <see cref="int"/>.
  /// </summary>
  /// <param name="document">Instância de <see cref="ECloudProvider"/>.</param>
  /// <returns>Id do provedor de Cloud.</returns>
  public static implicit operator int(ECloudProvider document) => document.Id;

  /// <summary>
  /// Conversão implícita de <see cref="ECloudProvider"/> para <see cref="string"/>.
  /// </summary>
  /// <param name="document">Instância de <see cref="ECloudProvider"/>.</param>
  /// <returns>Descrição do provedor de Cloud.</returns>
  public static implicit operator string(ECloudProvider document) => document.Description;

  /// <summary>
  /// Conversão implícita de <see cref="int"/> para <see cref="ECloudProvider"/>.
  /// </summary>
  /// <param name="id">Id do provedor de Cloud.</param>
  /// <returns>Uma instância de <see cref="ECloudProvider"/>.</returns>
  public static implicit operator ECloudProvider(int id) => FromId(id);

  /// <summary>
  /// Conversão implícita de <see cref="string"/> para <see cref="ECloudProvider"/>.
  /// </summary>
  /// <param name="description">Descrição do provedor de Cloud.</param>
  /// <returns>Uma instância de <see cref="ECloudProvider"/>.</returns>
  public static implicit operator ECloudProvider(string description) => FromDescription(description);
}

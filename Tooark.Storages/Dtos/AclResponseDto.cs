using Amazon.S3.Model;
using Object = Google.Apis.Storage.v1.Data.Object;

namespace Tooark.Storages.Dtos;

/// <summary>
/// Classe de resposta sobre a ACL (Lista de Controle de Acesso).
/// </summary>
public class AclResponseDto
{
  /// <summary>
  /// Construtor padrão utilizando um retorno do Amazon S3.
  /// </summary>
  /// <param name="aclResponse">Resposta da requisição de ACL <see cref="GetACLResponse"/>.</param>
  public AclResponseDto(GetACLResponse aclResponse)
  {
    // Define os ACLs.
    Acls = [.. aclResponse.AccessControlList.Grants.Select(x => new AclDto(x))];

    // Define o dono do arquivo.
    Owner = aclResponse.AccessControlList.Owner.DisplayName;
  }

  /// <summary>
  /// Construtor padrão utilizando um retorno do Google Cloud Storage.
  /// </summary>
  /// <param name="aclResponse">Resposta da requisição de ACL <see cref="Object"/>.</param>
  public AclResponseDto(Object aclResponse)
  {
    // Define os ACLs.
    Acls = [.. aclResponse.Acl.Select(x => new AclDto(x))];

    // Define o dono do arquivo.
    Owner = aclResponse.Owner.Entity;
  }


  /// <summary>
  /// Lista de ACLs.
  /// </summary>
  public AclDto[] Acls { get; private set; }

  /// <summary>
  /// Dono do arquivo.
  /// </summary>
  public string Owner { get; private set; }
}

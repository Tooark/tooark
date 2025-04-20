using Amazon.S3.Model;
using ObjectAccessControl = Google.Apis.Storage.v1.Data.ObjectAccessControl;

namespace Tooark.Storages.Dtos;

/// <summary>
/// Classe de propriedades da ACL (Lista de Controle de Acesso).
/// </summary>
public class AclDto
{
  /// <summary>
  /// Construtor padrão utilizando um retorno do Amazon S3.
  /// </summary>
  /// <param name="grant">ACL do Amazon S3 <see cref="S3Grant"/>.</param>
  public AclDto(S3Grant grant)
  {
    // Define os valores.
    DisplayName = grant.Grantee.DisplayName;
    EmailAddress = grant.Grantee.EmailAddress;
    Link = grant.Grantee.URI;
    Type = grant.Grantee.Type;
    Permission = grant.Permission.Value;
  }

  /// <summary>
  /// Construtor padrão utilizando um retorno do Google Cloud Storage.
  /// </summary>
  /// <param name="acl">ACL do Google Cloud Storage <see cref="ObjectAccessControl"/>.</param>
  public AclDto(ObjectAccessControl acl)
  {
    // Define os valores.
    DisplayName = acl.Entity;
    EmailAddress = acl.Email;
    Link = acl.SelfLink;
    Type = acl.Kind;
    Permission = acl.Role;
  }


  /// <summary>
  /// Nome de exibição.
  /// </summary>
  public string DisplayName { get; private set; }

  /// <summary>
  /// Endereço de e-mail.
  /// </summary>
  public string EmailAddress { get; private set; }

  /// <summary>
  /// Link.
  /// </summary>
  public string Link { get; private set; }

  /// <summary>
  /// Tipo.
  /// </summary>
  public string Type { get; private set; }

  /// <summary>
  /// Permissão.
  /// </summary>
  public string Permission { get; private set; }
}

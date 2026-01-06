using System.IdentityModel.Tokens.Jwt;

namespace Tooark.Securities.Dtos;

/// <summary>
/// Data Transfer Object (DTO) para representar o conteúdo um usuário de uma validação de token.
/// </summary>
public class UserTokenDto
{
  #region Constructors

  public UserTokenDto(JwtSecurityToken token)
  {
    Id = token.Claims.First(x => x.Type == "id").Value;
    Login = token.Claims.First(x => x.Type == "login").Value;
    Security = token.Claims.First(x => x.Type == "security").Value;
  }

  /// <summary>
  /// Construtor padrão utilizando um erro.
  /// </summary>
  /// <param name="error">Mensagem de erro.</param>
  public UserTokenDto(string error)
  {
    ErrorToken = error;
  }

  #endregion

  #region Properties

  /// <summary>
  /// Identificador do usuário.
  /// </summary>
  public string Id { get; private set; } = string.Empty;

  /// <summary>
  /// Login do usuário.
  /// </summary>
  public string Login { get; private set; } = string.Empty;

  /// <summary>
  /// Chave de segurança do usuário.
  /// </summary>
  public string Security { get; private set; } = string.Empty;

  /// <summary>
  /// Mensagem de erro do token.
  /// </summary>
  public string ErrorToken { get; private set; } = string.Empty;

  #endregion

  #region Methods

  /// <summary>
  /// Retorna o identificador do usuário como um Guid.
  /// </summary>
  public Guid GetGuidId => Guid.TryParse(Id, out var id) ? id : Guid.Empty;

  /// <summary>
  /// Retorna o identificador do usuário como um inteiro.
  /// </summary>
  public int GetIntId => int.TryParse(Id, out var id) ? id : 0;

  /// <summary>
  /// Retorna a chave de segurança do usuário como um Guid.
  /// </summary>
  public Guid GetGuidSecurity => Guid.TryParse(Security, out var security) ? security : Guid.Empty;

  /// <summary>
  /// Retorna a chave de segurança do usuário como um inteiro.
  /// </summary>
  public int GetIntSecurity => int.TryParse(Security, out var security) ? security : 0;

  #endregion
}

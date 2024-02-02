using Microsoft.EntityFrameworkCore;
using Tooark.Entities.Mapping;

namespace Tooark.Entities.Context;

/// <summary>
/// Classe BaseContext que estende de DbContext.
/// Esta classe é usada para configurar o contexto do banco de dados com os mapeamentos
/// específicos das entidades do modelo. 
/// </summary>
public class BaseContext(DbContextOptions<BaseContext> options) : DbContext(options)
{
  /// <summary>
  /// Sobrescreve o método OnModelCreating para configurar o modelo de entidades.
  /// Este método é chamado durante a inicialização do contexto para configurar o modelo
  /// de banco de dados e seus mapeamentos.
  /// </summary>
  /// <param name="modelBuilder">Construtor de modelos para configurar entidades.</param>
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // Aplica as configurações de mapeamento para a BaseEntity.
    modelBuilder.ApplyConfiguration(new BaseEntityMap());

    // Aplica as configurações de mapeamento para a DetailedEntity.
    modelBuilder.ApplyConfiguration(new DetailedEntityMap());
  }
}

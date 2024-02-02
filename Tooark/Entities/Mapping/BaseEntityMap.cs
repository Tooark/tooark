using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tooark.Entities.Mapping;

/// <summary>
/// Classe de mapeamento para a entidade BaseEntity.
/// Define o mapeamento da entidade BaseEntity para a tabela correspondente no banco de dados.
/// Esta classe é usada para configurar aspectos comuns da BaseEntity, como sua chave primária.
/// </summary>
public class BaseEntityMap : IEntityTypeConfiguration<BaseEntity>
{
  /// <summary>
  /// Configura o mapeamento da entidade BaseEntity.
  /// </summary>
  /// <param name="builder">Construtor usado para configurar a entidade BaseEntity.</param>
  public void Configure(EntityTypeBuilder<BaseEntity> builder)
  {
    // Mapeia a propriedade Id para a coluna 'id' e define como chave primária obrigatória.
    builder.Property(b => b.Id)
      .HasColumnName("id")
      .HasColumnType("uuid")
      .IsRequired();
  }
}

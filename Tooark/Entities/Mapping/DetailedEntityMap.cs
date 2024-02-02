using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tooark.Entities.Mapping;

/// <summary>
/// Classe de mapeamento para a entidade DetailedEntity.
/// Define o mapeamento da entidade DetailedEntity para a tabela correspondente no banco de dados.
/// </summary>
public class DetailedEntityMap : IEntityTypeConfiguration<DetailedEntity>
{
  /// <summary>
  /// Configura as propriedades e relacionamentos da entidade DetailedEntity.
  /// </summary>
  /// <param name="builder">Construtor usado para configurar a entidade DetailedEntity.</param>
  public void Configure(EntityTypeBuilder<DetailedEntity> builder)
  {
    // Mapeia a propriedade CreatedBy para a coluna 'createdby' e define como obrigatória.
    builder.Property(b => b.CreatedBy)
      .HasColumnName("createdby")
      .HasColumnType("uuid")
      .IsRequired();

    // Mapeia a propriedade CreatedAt para a coluna 'createdat' e define como obrigatória.
    builder.Property(b => b.CreatedAt)
      .HasColumnName("createdat")
      .HasColumnType("timestamp with time zone")
      .IsRequired();

    // Mapeia a propriedade UpdatedBy para a coluna 'updatedby' e define como obrigatória.
    builder.Property(b => b.UpdatedBy)
      .HasColumnName("updatedby")
      .HasColumnType("uuid")
      .IsRequired();

    // Mapeia a propriedade UpdatedAt para a coluna 'updatedat' e define como obrigatória.
    builder.Property(b => b.UpdatedAt)
      .HasColumnName("updatedat")
      .HasColumnType("timestamp with time zone")
      .IsRequired();
  }
}

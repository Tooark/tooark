using Microsoft.EntityFrameworkCore;
using Tooark.Entities;
using Tooark.Entities.Mapping;

namespace Tooark.Tests.Entities.Mapping;

// Classe concreta para testes que herda de DetailedEntity.
public class TestDetailedEntity : DetailedEntity
{
}

// Contexto de teste do Entity Framework Core para TestDetailedEntity.
public class TestDetailedDbContext(DbContextOptions<TestDetailedDbContext> options) : DbContext(options)
{
  public DbSet<TestDetailedEntity> TestDetailedEntities { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new DetailedEntityMap());
  }
}

// Classe de testes para verificar o mapeamento de DetailedEntity.
public class DetailedEntityMapTests
{
  // A configuração do mapa de entidade base deve estar correta
  [Fact]
  public void DetailedEntityMap_Configuration_ShouldBeCorrect()
  {
    // Configura o contexto de teste para usar SQLite em memória.
    var options = new DbContextOptionsBuilder<TestDetailedDbContext>()
      .UseSqlite("DataSource=:memory:")
      .Options;

    using var context = new TestDetailedDbContext(options);

    // Obtém o modelo de entidade a partir do contexto.
    var model = context.Model;

    // Encontra a entidade TestBaseEntity no modelo.
    var entityType = model.FindEntityType(typeof(TestDetailedEntity));

    // Assert CreatedBy
    var createdByProperty = entityType!.FindProperty(nameof(DetailedEntity.CreatedBy));
    Assert.Equal("createdby", createdByProperty!.GetColumnName());
    Assert.Equal("uuid", createdByProperty!.GetColumnType());
    Assert.False(createdByProperty!.IsNullable);

    // Assert UpdatedBy
    var updatedByProperty = entityType.FindProperty(nameof(DetailedEntity.UpdatedBy));
    Assert.Equal("updatedby", updatedByProperty!.GetColumnName());
    Assert.Equal("uuid", updatedByProperty!.GetColumnType());
    Assert.False(updatedByProperty!.IsNullable);

    // Assert CreatedAt
    var createdAtProperty = entityType!.FindProperty(nameof(DetailedEntity.CreatedAt));
    Assert.Equal("createdat", createdAtProperty!.GetColumnName());
    Assert.Equal("timestamp with time zone", createdAtProperty!.GetColumnType());
    Assert.False(createdAtProperty!.IsNullable);

    // Assert UpdatedAt
    var updatedAtProperty = entityType.FindProperty(nameof(DetailedEntity.UpdatedAt));
    Assert.Equal("updatedat", updatedAtProperty!.GetColumnName());
    Assert.Equal("timestamp with time zone", updatedAtProperty!.GetColumnType());
    Assert.False(updatedAtProperty!.IsNullable);
  }

  // O mapa da entidade base deverá falhar se o nome da coluna estiver incorreto
  [Fact]
  public void DetailedEntityMap_ShouldFail_IfColumnNameIsIncorrect()
  {
    // Configura o contexto de teste para usar SQLite em memória.
    var options = new DbContextOptionsBuilder<TestDetailedDbContext>()
      .UseSqlite("DataSource=:memory:")
      .Options;

    using var context = new TestDetailedDbContext(options);

    // Obtém o modelo de entidade a partir do contexto.
    var model = context.Model;

    // Encontra a entidade TestBaseEntity no modelo.
    var entityType = model.FindEntityType(typeof(TestDetailedEntity));

    // Encontra a propriedade Id na entidade TestBaseEntity e verifica seu mapeamento.
    var createdByProperty = entityType!.FindProperty(nameof(DetailedEntity.CreatedBy));

    // Assert
    Assert.NotEqual("incorrectColumnName", createdByProperty!.GetColumnName());
  }
}

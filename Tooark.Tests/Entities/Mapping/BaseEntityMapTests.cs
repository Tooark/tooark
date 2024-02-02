using Microsoft.EntityFrameworkCore;
using Tooark.Entities;
using Tooark.Entities.Mapping;

namespace Tooark.Tests.Entities.Mapping;

// Classe concreta para testes que herda de BaseEntity
public class TestBaseEntity : BaseEntity
{
}

// Contexto de teste do Entity Framework Core para TestBaseEntity.
public class TestBaseDbContext(DbContextOptions<TestBaseDbContext> options) : DbContext(options)
{
  public DbSet<TestBaseEntity> TestEntities { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new BaseEntityMap());
  }
}

// Classe de testes para verificar o mapeamento de BaseEntity.
public class BaseEntityMapTests
{
  // A configuração do mapa de entidade base deve estar correta
  [Fact]
  public void BaseEntityMap_Configuration_ShouldBeCorrect()
  {
    // Configura o contexto de teste para usar SQLite em memória.
    var options = new DbContextOptionsBuilder<TestBaseDbContext>()
      .UseSqlite("DataSource=:memory:")
      .Options;

    using var context = new TestBaseDbContext(options);

    // Obtém o modelo de entidade a partir do contexto.
    var model = context.Model;

    // Encontra a entidade TestBaseEntity no modelo.
    var entityType = model.FindEntityType(typeof(TestBaseEntity));

    // Encontra a propriedade Id na entidade TestBaseEntity e verifica seu mapeamento.
    var idProperty = entityType!.FindProperty(nameof(BaseEntity.Id));

    // Assert
    Assert.Equal("id", idProperty!.GetColumnName());
    Assert.Equal("uuid", idProperty!.GetColumnType());
    Assert.False(idProperty!.IsNullable);
  }

  // O mapa da entidade base deverá falhar se o nome da coluna estiver incorreto
  [Fact]
  public void BaseEntityMap_ShouldFail_IfColumnNameIsIncorrect()
  {
    // Configura o contexto de teste para usar SQLite em memória.
    var options = new DbContextOptionsBuilder<TestBaseDbContext>()
      .UseSqlite("DataSource=:memory:")
      .Options;

    using var context = new TestBaseDbContext(options);

    // Obtém o modelo de entidade a partir do contexto.
    var model = context.Model;

    // Encontra a entidade TestBaseEntity no modelo.
    var entityType = model.FindEntityType(typeof(TestBaseEntity));

    // Encontra a propriedade Id na entidade TestBaseEntity e verifica seu mapeamento.
    var idProperty = entityType!.FindProperty(nameof(BaseEntity.Id));

    // Assert
    Assert.NotEqual("incorrectColumnName", idProperty!.GetColumnName());
  }
}

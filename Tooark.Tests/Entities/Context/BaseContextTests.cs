using Microsoft.EntityFrameworkCore;
using Tooark.Entities.Context;
using Tooark.Entities;

namespace Tooark.Tests.Entities.Context;

// Classe concreta para testes que herda de BaseEntity
public class TestBaseEntity : BaseEntity
{
}

// Classe concreta para testes que herda de DetailedEntity
public class TestDetailedEntity : DetailedEntity
{
}

// Classe concreta sem instância para testes que herda de BaseEntity
public class OtherTestEntity : BaseEntity
{
}

// Contexto de teste do Entity Framework Core para TestBaseEntity.
public class TestDbContext(DbContextOptions<BaseContext> options) : BaseContext(options)
{
  public DbSet<TestBaseEntity> BaseEntities { get; set; }
  public DbSet<TestDetailedEntity> DetailedEntities { get; set; }
}

public class BaseContextFailureTests
{
  // A configuração do contexto base deve estar correta
  [Fact]
  public void BaseContext_Configuration_ShouldBeCorrect()
  {
    // Configura o contexto de teste para usar SQLite em memória.
    var options = new DbContextOptionsBuilder<BaseContext>()
      .UseSqlite("DataSource=:memory:")
      .Options;

    using var context = new TestDbContext(options);

    // Obtém o modelo de entidade a partir do contexto.
    var model = context.Model;

    // Encontra a entidade TestBaseEntity no modelo.
    var entityType = model.FindEntityType(typeof(TestBaseEntity));

    // Assert
    Assert.NotNull(entityType);
  }

  // O contexto base deverá falhar se o mapeamento estiver incorreto
  [Fact]
  public void BaseContext_ShouldFail_IfMappingIsIncorrect()
  {
    // Configura o contexto de teste para usar SQLite em memória.
    var options = new DbContextOptionsBuilder<BaseContext>()
      .UseSqlite("DataSource=:memory:")
      .Options;

    using var context = new TestDbContext(options);

    // Obtém o modelo de entidade a partir do contexto.
    var model = context.Model;

    // Encontra a entidade TestBaseEntity no modelo.
    var entityType = model.FindEntityType(typeof(OtherTestEntity));

    // Assert
    Assert.Null(entityType);
  }
}

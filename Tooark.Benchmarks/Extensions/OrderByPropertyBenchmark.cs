using BenchmarkDotNet.Attributes;
using Tooark.Benchmarks.Moq.Models.Category;
using Tooark.Benchmarks.Moq.Services;
using Tooark.Extensions;

namespace Tooark.Benchmarks.Extensions;

// https://benchmarkdotnet.org/articles/overview.html

[MemoryDiagnoser]
public class OrderByPropertyBenchmark
{
  static readonly List<Category> _categories =
  [
    GenerateCategoryData.CreateCategory(1),
    GenerateCategoryData.CreateCategory(2),
    GenerateCategoryData.CreateCategory(3),
    GenerateCategoryData.CreateCategory(4),
    GenerateCategoryData.CreateCategory(5),
    GenerateCategoryData.CreateCategory(6),
    GenerateCategoryData.CreateCategory(7),
    GenerateCategoryData.CreateCategory(8),
    GenerateCategoryData.CreateCategory(9),
    GenerateCategoryData.CreateCategory(10),
    GenerateCategoryData.CreateCategory(11),
    GenerateCategoryData.CreateCategory(12),
    GenerateCategoryData.CreateCategory(13),
    GenerateCategoryData.CreateCategory(14),
    GenerateCategoryData.CreateCategory(15)
  ];

  [Benchmark(Baseline = true)]
  public void QueryableSimple()
  {
    var queryable = _categories.AsQueryable();
    _ = queryable.OrderByProperty("Type").ToList();
  }

  [Benchmark]
  public void EnumerableSimple()
  {
    var queryable = _categories.AsEnumerable();
    _ = queryable.OrderByProperty("Type").ToList();
  }

  [Benchmark]
  public void QueryableComplex()
  {
    var queryable = _categories.AsQueryable();
    _ = queryable.OrderByProperty("Subcategory.Name").ToList();
  }

  [Benchmark]
  public void EnumerableComplex()
  {
    var queryable = _categories.AsEnumerable();
    _ = queryable.OrderByProperty("Subcategory.Name").ToList();
  }
}

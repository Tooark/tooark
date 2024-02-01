using BenchmarkDotNet.Running;
using Tooark.Benchmarks.Utils;

namespace Tooark.Benchmarks.Extensions;

public class Program
{
  public static void Main(string[] args)
  {
    _ = BenchmarkRunner.Run<OrderByPropertyBenchmark>();
    _ = BenchmarkRunner.Run<NormalizedBenchmark>();
  }
}

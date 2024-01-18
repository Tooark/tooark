using BenchmarkDotNet.Running;

namespace Tooark.Benchmarks.Benchmark;

public class Program
{
  public static void Main(string[] args)
  {
    var summary = BenchmarkRunner.Run<OrderByPropertyBenchmark>();
  }
}

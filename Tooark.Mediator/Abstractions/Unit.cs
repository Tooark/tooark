namespace Tooark.Mediator.Abstractions;

public readonly struct Unit : IEquatable<Unit>
{
  public static readonly Unit Value = new();

  public static Task<Unit> Task => System.Threading.Tasks.Task.FromResult(Value);

  public override bool Equals(object? obj)
  {
    return obj is Unit;
  }

  public bool Equals(Unit other)
  {
    return true;
  }

  public override int GetHashCode()
  {
    return 0;
  }

  public static bool operator ==(Unit left, Unit right)
  {
    return true;
  }

  public static bool operator !=(Unit left, Unit right)
  {
    return false;
  }

  public override string ToString()
  {
    return "()";
  }
}

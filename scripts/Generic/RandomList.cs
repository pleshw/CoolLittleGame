using System;
using System.Collections.Generic;
using System.Linq;

namespace Generic;

public class RandomList<T> : List<T>
{
  private static readonly Random _randomGenerator = new();
  private T lastResult;

  public T Get()
  {
    return lastResult = this[_randomGenerator.Next(Count)];
  }

  public T GetExcept(T exception)
  {
    return GetExcept([exception]);
  }

  public T GetExcept(List<T> listExceptions)
  {
    List<T> range = this.Except(listExceptions).ToList();

    int index = _randomGenerator.Next(0, range.Count);
    return lastResult = range.ElementAt(index);
  }

  public T GetExceptLastResult()
  {
    if (lastResult == null)
    {
      return lastResult = Get();
    }

    return lastResult = GetExcept(lastResult);
  }

  public void FYatesShuffle()
  {
    int n = Count;
    while (n > 1)
    {
      n--;
      int k = _randomGenerator.Next(n + 1);
      (this[n], this[k]) = (this[k], this[n]);
    }
  }
}

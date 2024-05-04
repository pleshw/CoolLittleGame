
using System.Collections.Generic;
using Generic;

namespace Extensions;

public static class EnumerableExtensions
{
  public static RandomList<T> ToRandomList<T>(this List<T> list)
  {
    RandomList<T> randomList = [.. list];
    return randomList;
  }

  public static RandomList<T> ToRandomList<T>(this IEnumerable<T> list)
  {
    RandomList<T> randomList = [.. list];
    return randomList;
  }
}
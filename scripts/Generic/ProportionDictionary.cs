
using System;
using System.Collections.Generic;
using System.Linq;

namespace Generic;

/// <summary>
/// A dictionary will throw an error if the sum of its elements is not 1
/// </summary>
public class ProportionDictionary<T> : SortedDictionary<T, float>
{
  public new void Add(T key, float value)
  {
    if (!IsValidToAdd(value))
      throw new ArgumentException("The sum of values would exceed 1.");

    base.Add(key, value);
  }

  public new void Remove(T key)
  {
    base.Remove(key);
  }

  public new void Clear()
  {
    base.Clear();
  }

  public new float this[T key]
  {
    get { return base[key]; }
    set
    {
      if (!IsValidToSet(key, value))
        throw new ArgumentException("The sum of values would exceed 1(100%).");

      base[key] = value;
    }
  }

  private bool IsValidToAdd(float value)
  {
    float sum = Values.Sum() + value;
    return sum <= 1;
  }

  private bool IsValidToSet(T key, float value)
  {
    float currentSum = Values.Sum() - this[key];
    float sum = currentSum + value;
    return sum <= 1;
  }
}

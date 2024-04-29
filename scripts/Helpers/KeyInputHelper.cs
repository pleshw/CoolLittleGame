using System;
using Godot;

namespace Helpers;

// KeyList struct to store both device and keycode
public struct KeyInputHelper(int device, Key keyCode)
{
  public int Device = device;
  public Key KeyCode = keyCode;

  // Override GetHashCode and Equals to allow using KeyList as a dictionary key
  public override readonly int GetHashCode()
  {
    return HashCode.Combine(Device, KeyCode);
  }

  public override readonly bool Equals(object obj)
  {
    if (obj is not KeyInputHelper)
    {
      return false;
    }

    KeyInputHelper other = (KeyInputHelper)obj;
    return Device == other.Device && KeyCode == other.KeyCode;
  }

  public static bool operator ==(KeyInputHelper left, KeyInputHelper right)
  {
    return left.Equals(right);
  }

  public static bool operator !=(KeyInputHelper left, KeyInputHelper right)
  {
    return !(left == right);
  }
}

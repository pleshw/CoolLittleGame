using System.Text.RegularExpressions;

namespace Helpers;

public static partial class RegexHelper
{
  public static readonly Regex SpecialCharacterPattern = MyRegex();

  [GeneratedRegex("[^a-zA-Z0-9]")]
  private static partial Regex MyRegex();
}
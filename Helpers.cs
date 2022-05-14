namespace KrileDotNet;

public static class Helpers
{
    public static string FormatStringWithSpaces(object s)
    {
        return string.Concat(s.ToString().Select(x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
    }
}
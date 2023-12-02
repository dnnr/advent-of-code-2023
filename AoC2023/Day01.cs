using System.Text.RegularExpressions;

namespace AoC2023;

public abstract class Day01 : IDay
{
    public static string Part1(string input)
    {
        var regex = new Regex("[0-9]", RegexOptions.Compiled);
        var sum = 0;
        foreach (var line in Helpers.Lines(input))
        {
            char? first = null;
            char? second = null;
            foreach (var chr in line.ToCharArray())
            {
                if (!regex.Match(chr.ToString()).Success) continue;
                
                if (first == null)
                    first = chr;
                else
                    second = chr;
            }
            second ??= first;
            sum += int.Parse($"{first}{second}");
        }
        return sum.ToString();
    }
    
    public static string Part2(string input)
    {
        return "";
    }
}
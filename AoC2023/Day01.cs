using System.Text.RegularExpressions;
using FluentAssertions;
using Xunit;

namespace AoC2023;

public abstract class Day01 : IDay
{
    public static string Part1(string input)
    {
        var regex = new Regex("[0-9]", RegexOptions.Compiled);
        var sum = 0;
        foreach (var line in Helpers.Lines(input))
        {
            var matches = regex.Matches(line);

            var first = matches[0].Value;
            var second = matches.Last().Value;
            sum += int.Parse($"{first}{second}");
        }

        return sum.ToString();
    }

    public static string Part2(string input)
    {
        var sum = 0;
        foreach (var line in Helpers.Lines(input))
        {
            sum += Part2_ValueForLine(line);
        }

        return sum.ToString();
    }

    public static int Part2_ValueForLine(string line)
    {
        var regex = new Regex("[0-9]|one|two|three|four|five|six|seven|eight|nine");
        List<string> matches = new();

        Match matchObj = regex.Match(line);
        while (matchObj.Success)
        {
            matches.Add(matchObj.Value);
            matchObj = regex.Match(line, matchObj.Index + 1);
        }

        int MatchToDigit(string match) =>
            int.Parse(match.Replace("one", "1")
                .Replace("two", "2")
                .Replace("three", "3")
                .Replace("four", "4")
                .Replace("five", "5")
                .Replace("six", "6")
                .Replace("seven", "7")
                .Replace("eight", "8")
                .Replace("nine", "9"));

        var first = MatchToDigit(matches[0]);
        var second = MatchToDigit(matches.Last());
        return int.Parse($"{first}{second}");
    }
}

public class Day01Tests
{
    [Fact]
    public void Part1_SampleInput_CorrectResult()
    {
        const string input = """
                             1abc2
                             pqr3stu8vwx
                             a1b2c3d4e5f
                             treb7uchet
                             """;
        var result = Day01.Part1(input);
        result.Should().Be("142");
    }

    [InlineData("two1nine", 29)]
    [InlineData("eightwothree", 83)]
    [InlineData("abcone2threexyz", 13)]
    [InlineData("xtwone3four", 24)]
    [InlineData("4nineeightseven2", 42)]
    [InlineData("zoneight234", 14)]
    [InlineData("7pqrstsixteen", 76)]
    [InlineData("nine11sixsixeightwonpf", 92)]
    [Theory]
    public void Part2_SampleLine_CorrectResult(string input, int expectedResult)
    {
        var result = Day01.Part2_ValueForLine(input);
        result.Should().Be(expectedResult);
    }


    [Fact]
    public void Part2_SampleInput_CorrectResult()
    {
        const string input = """
                             two1nine
                             eightwothree
                             abcone2threexyz
                             xtwone3four
                             4nineeightseven2
                             zoneight234
                             7pqrstsixteen
                             """;
        var result = Day01.Part2(input);
        result.Should().Be("281");
    }
}
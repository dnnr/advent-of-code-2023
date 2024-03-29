using FluentAssertions;
using Xunit;

namespace AoC2023;

public class Day02 : IDay
{
    public record CubeSet(int Red, int Green, int Blue)
    {
        public int Power => Red * Green * Blue;
    }

    public record Game(int Id, List<CubeSet> CubeSets);
    
    public static string Part1(string input)
    {
        var sumOfPossibleGameIds = 0;
        foreach (var gameLine in Helpers.Lines(input))
        {
            var game = ParseGame(gameLine);
            var possible = game.CubeSets.All(cubeSet => cubeSet is { Red: <= 12, Green: <= 13, Blue: <= 14 });

            if (possible)
                sumOfPossibleGameIds += game.Id;
        }

        return sumOfPossibleGameIds.ToString();
    }

    public static string Part2(string input)
    {
        var sumOfPowers = 0;
        foreach (var gameLine in Helpers.Lines(input))
        {
            var game = ParseGame(gameLine);
            sumOfPowers += MinCubeSet(game).Power;
        }

        return sumOfPowers.ToString();
    }

    public static Game ParseGame(string line)
    {
        var colonSplit = line.Split(": ");
        var gameId = int.Parse(colonSplit[0].Split(" ").Last());
        var cubeSets = new List<CubeSet>();
        foreach (var cubeSetString in colonSplit[1].Split("; "))
        {
            int red = 0, green = 0, blue = 0;
            foreach (var cubeSpec in cubeSetString.Split(", "))
            {
                var cubeSpecSplit = cubeSpec.Split(" ", 2);
                var count = int.Parse(cubeSpecSplit[0]);
                var color = cubeSpecSplit[1];
                if (color == "red")
                    red = count;
                else if (color == "green")
                    green = count;
                else if (color == "blue")
                    blue = count;
            }
            cubeSets.Add(new CubeSet(red, green, blue));
        }

        return new Game(gameId, cubeSets);
    }

    public static CubeSet MinCubeSet(Game game)
    {
        int minRed = 0, minGreen = 0, minBlue = 0;
        foreach (var cubeSet in game.CubeSets)
        {
            minRed = int.Max(minRed, cubeSet.Red);
            minGreen = int.Max(minGreen, cubeSet.Green);
            minBlue = int.Max(minBlue, cubeSet.Blue);
        }

        return new CubeSet(minRed, minGreen, minBlue);
    }

    private readonly string _commonSample = """
                                            Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
                                            Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
                                            Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
                                            Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
                                            Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
                                            """;

    [Fact]
    public void ParseGame_SampleGame_ParsedCorrectly()
    {
        const string line = "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green";
        var game = ParseGame(line);

        game.Id.Should().Be(1);
        game.CubeSets.Should().HaveCount(3);
        game.CubeSets[0].Should().BeEquivalentTo(new CubeSet(4, 0, 3));
        game.CubeSets[1].Should().BeEquivalentTo(new CubeSet(1, 2, 6));
        game.CubeSets[2].Should().BeEquivalentTo(new CubeSet(0, 2, 0));
    }

    [Fact]
    public void Part1_SampleInput_CorrectResult()
    {
        Part1(_commonSample).Should().Be("8");
    }

    [Fact]
    public void Part2_SampleInput_CorrectResult()
    {
        Part2(_commonSample).Should().Be("2286");
    }

    [Fact]
    public void MinCubeSet_SampleInput_CorrectResult()
    {
        const string line = "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green";
        var minCubeSet = MinCubeSet(ParseGame(line));

        minCubeSet.Should().BeEquivalentTo(new CubeSet(4, 2, 6));
    }
}
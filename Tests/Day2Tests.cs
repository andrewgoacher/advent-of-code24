using AdventOfCode2024.Solvers;

namespace Tests;

public class Day2Tests
{
    private static readonly string[] _exampleInput =
    {
        "7 6 4 2 1",
        "1 2 7 8 9",
        "9 7 6 2 1",
        "1 3 2 4 5",
        "8 6 4 4 1",
        "1 3 6 7 9",
    };

    [Fact]
    public void Day2_WithExampleInputs_GivesExpectedResult()
    {
        var (part1, _) = new Day2(_exampleInput).Solve();

        Assert.Equal(2, part1);
    }

    [Fact]
    public void Day2_Part2_WithExampleInputs_GivesExpectedResult()
    {
        var (_, part2) = new Day2(_exampleInput).Solve();

        Assert.Equal(4, part2);
    }

    [Theory]
    [MemberData(nameof(AcceptableArrays))]
    public void Day2_Part_Examples_GiveExpectedResults(string inputs)
    {
        var (_, part2) = new Day2(new[] { inputs }).Solve();
        Assert.Equal(1, part2);
    }

    public static IEnumerable<object[]> AcceptableArrays()
    {
        yield return new object[] { "46 48 50 51 53 55 58 59 57" };
        yield return new object[] { "57 46 48 50 51 53 55 58 59" };
    }
}
using AdventOfCode2024;
using AdventOfCode2024.Solvers;

namespace Tests;

public class Day1Tests
{
    [Fact]
    public void Day1_WithExampleInputs_GivesExpectedResult()
    {
        string[] exampleInput = [
            "3   4",
            "4   3",
            "2   5",
            "1   3",
            "3   9",
            "3   3"
        ];

        var (part1, _) = new Day1(exampleInput).Solve();

        Assert.Equal(11, part1);
    }

    [Fact]
    public void Day1_Part2_WithExampleInputs_GivesExpectedResult()
    {
        string[] exampleInput = [
            "3   4",
            "4   3",
            "2   5",
            "1   3",
            "3   9",
            "3   3"
        ];

        var (_, part2) = new Day1(exampleInput).Solve();

        Assert.Equal(31, part2);
    }
}
using AdventOfCode2024;

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

        var result = Day1Solver.Part1(exampleInput);

        Assert.Equal(11, result);
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

        var result = Day1Solver.Part2(exampleInput);

        Assert.Equal(31, result);
    }
}
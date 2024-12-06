using AdventOfCode2024.Solvers;

namespace Tests;

public class Day6Tests
{
    [Fact]
    public void Part1_ExpectedVisits()
    {
        var input = new[]
        {
            "....#.....",
            ".........#",
            "..........",
            "..#.......",
            ".......#..",
            "..........",
            ".#..^.....",
            "........#.",
            "#.........",
            "......#...",
        };

        var (part1, _) = new Day6(input).Solve();

        Assert.Equal(41, part1);
    }

    [Fact]
    public void Part2_ExpectedLoops()
    {
        var input = new[]
        {
            "....#.....",
            ".........#",
            "..........",
            "..#.......",
            ".......#..",
            "..........",
            ".#..^.....",
            "........#.",
            "#.........",
            "......#...",
        };

        var (_, part2) = new Day6(input).Solve();

        Assert.Equal(6, part2);
    }
}
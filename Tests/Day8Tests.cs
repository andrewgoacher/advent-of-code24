using AdventOfCode2024.Solvers;

namespace Tests;

public class Day8Tests
{
    private readonly string[] _inputs = new[]
    {
        "............",
        "........0...",
        ".....0......",
        ".......0....",
        "....0.......",
        "......A.....",
        "............",
        "............",
        "........A...",
        ".........A..",
        "............",
        "............",
    };

    [Fact]
    public void Part1_ExpectedOutput()
    {
        var (part1, _) = new Day8(_inputs).Solve();

        Assert.Equal(14, part1);
    }

    [Fact]
    public void Part2_ExpectedOutput()
    {
        var (_, part2) = new Day8(_inputs).Solve();

        Assert.Equal(34, part2);
    }
}
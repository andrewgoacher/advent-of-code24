using AdventOfCode2024.Solvers;

namespace Tests;

public class Day9Tests
{
    private readonly string _input = "2333133121414131402";

    [Fact]
    public void Part1_GivesExpectedResult()
    {
        var (part1, _) = new Day9(new[] { _input }).Solve();

        Assert.Equal(1928, part1);
    }
}
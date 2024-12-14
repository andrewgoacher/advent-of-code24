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

    [Fact]
    public void Part2_GivesExpectedResult()
    {
        var (_, part2) = new Day9(new[] { _input }).Solve();

        Assert.Equal(2858, part2);
    }

    [Fact]
    public void BothTogether()
    {
        var (part1, part2) = new Day9(new[] { _input }).Solve();

       Assert.Multiple(() =>
       {
           Assert.Equal(1928, part1);
           Assert.Equal(2858, part2);
       });
    }
}
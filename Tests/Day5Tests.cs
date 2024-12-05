using AdventOfCode2024.Solvers;

namespace Tests;

public class Day5Tests
{
    private readonly string[] _exampleInput = new[]
    {
        "47|53",
        "97|13",
        "97|61",
        "97|47",
        "75|29",
        "61|13",
        "75|53",
        "29|13",
        "97|29",
        "53|29",
        "61|53",
        "97|53",
        "61|29",
        "47|13",
        "75|47",
        "97|75",
        "47|61",
        "75|61",
        "47|29",
        "75|13",
        "53|13",
        "",
        "75,47,61,53,29",
        "97,61,53,29,13",
        "75,29,13",
        "75,97,47,61,53",
        "61,13,29",
        "97,13,75,29,47",
    };

    [Fact]
    public void NoMappings_SumsMiddleNumbers()
    {
        var inputs = new string[]
        {
            "75,47,61,53,29",
            "97,61,53,29,13",
            "75,29,13",
            "75,97,47,61,53",
            "61,13,29",
            "97,13,75,29,47"
        };

        var (part1, _) = new Day5(inputs).Solve();
        var expected = 61 + 53 + 29 + 47 + 13 + 75;

        Assert.Equal(expected, part1);
    }

    [Fact]
    public void Day5_Part1_Example()
    {
        var (part1, _) = new Day5(_exampleInput).Solve();

        Assert.Equal(143, part1);
    }

    [Fact]
    public void Day5_Part2_Example()
    {
        var (_, part2) = new Day5(_exampleInput).Solve();

        Assert.Equal(123, part2);
    }
}
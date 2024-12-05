using AdventOfCode2024.Solvers;

namespace Tests;

public class Day5Tests
{
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
}
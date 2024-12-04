using AdventOfCode2024.Solvers;

namespace Tests;

public class Day4Tests
{
    private readonly string[] _inputs = new[]
    {
        "MMMSXXMASM",
        "MSAMXMSMSA",
        "AMXSXMAAMM",
        "MSAMASMSMX",
        "XMASAMXAMM",
        "XXAMMXXAMA",
        "SMSMSASXSS",
        "SAXAMASAAA",
        "MAMMMXMMMM",
        "MXMXAXMASX",
    };

    [Fact]
    public void WordInHorizontalLine_FindsAcceptedWord()
    {
        string[] input = new string[]
        {
            "_____XMAS____",
            "_____________"
        };

        var (part1, _) = new Day4(input).Solve();
        Assert.Equal(1, part1);
    }

    [Fact]
    public void WordReversedInHorizontalLine_FindsAcceptedWord()
    {
        string[] input = new string[]
        {
            "________",
            "__SAMX__"
        };

        var (part1, _) = new Day4(input).Solve();
        Assert.Equal(1, part1);
    }

    [Fact]
    public void WordInVerticalLine_FindsAcceptedWord()
    {
        string[] input = new string[]
        {
            "X_____",
            "M_____",
            "A_____",
            "S_____"
        };

        var (part1, _) = new Day4(input).Solve();
        Assert.Equal(1, part1);
    }

    [Fact]
    public void WordReversedInVerticalLine_FindsAcceptedWord()
    {
        string[] input = new string[]
        {
            "S_____",
            "A_____",
            "M_____",
            "X_____"
        };

        var (part1, _) = new Day4(input).Solve();
        Assert.Equal(1, part1);
    }

    [Fact]
    public void WordInDiagonalLine_FindsAcceptedWord()
    {
        string[] input = new string[]
        {
            "X___",
            "_M__",
            "__A_",
            "___S"
        };

        var (part1, _) = new Day4(input).Solve();
        Assert.Equal(1, part1);
    }

    [Fact]
    public void WordReversedInDiagonalLine_FindsAcceptedWord()
    {
        string[] input = new string[]
        {
            "S___",
            "_A__",
            "__M_",
            "___X"
        };

        var (part1, _) = new Day4(input).Solve();
        Assert.Equal(1, part1);
    }

    [Fact]
    public void WordBackToFrontInDiagonalLine_FindsAcceptedWord()
    {
        string[] input = new string[]
        {
            "___X",
            "__M_",
            "_A__",
            "S___"
        };

        var (part1, _) = new Day4(input).Solve();
        Assert.Equal(1, part1);
    }

    [Fact]
    public void WordReversedBackToFrontInDiagonalLine_FindsAcceptedWord()
    {
        string[] input = new string[]
        {
            "___S",
            "__A_",
            "_M__",
            "X___"
        };

        var (part1, _) = new Day4(input).Solve();
        Assert.Equal(1, part1);
    }

    [Fact]
    public void Part1_CollectsExpectedNumberOfValues()
    {
        var (part1, _) = new Day4(_inputs).Solve();
        Assert.Equal(18, part1);
    }
}
using AdventOfCode2024.Solvers;
using AdventOfCode2024.Tokenisation;

namespace Tests;

public class Day3Tests
{
    [Theory]
    [InlineData("mul(5,2)", typeof(MulToken))]
    [InlineData("milmul(2,3)", typeof(MulToken))]
    [InlineData("do()", typeof(DoToken))]
    [InlineData("undo()", typeof(DoToken))]
    [InlineData("don't()", typeof(DontToken))]
    [InlineData("doundon't()", typeof(DontToken))]
    public void CapturesToken(string token, Type expectedType)
    {
        var tokens = Tokeniser.Tokenise(token);
        var mulToken = tokens.Single();
        Assert.IsType(expectedType, mulToken);
    }

    [Theory]
    [InlineData("mul(5,2)",10)]
    [InlineData("milmul(2,3)", 6)]
    [InlineData("don't()", 0)]
    [InlineData("do()mul(1,3)don't()mul(5,6)", 3)]
    public void HasExpectedValue(string token, int expected)
    {
        var tokens = Tokeniser.Tokenise(token);
        var result = tokens.Select(token => token.Execute()).Sum();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("$")]
    [InlineData("$mul(4)")]
    public void DoesNotCapturesToken(string token)
    {
        var tokens = Tokeniser.Tokenise(token);
        Assert.Empty(tokens);
    }

    [Fact]
    public void Part1_SampleText_GivesExpectedResult()
    {
        const string input = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";

        var (part1, _) =  new Day3(new[] { input }).Solve();
        Assert.Equal(161, part1);
    }

    [Fact]
    public void Part2_SampleText_GivesExpectedResult()
    {
        const string input = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
        var (_, part2) = new Day3(new[] { input }).Solve();

        Assert.Equal(48, part2);
    }
}
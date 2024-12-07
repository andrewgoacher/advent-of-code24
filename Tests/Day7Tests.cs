using AdventOfCode2024.Day7;
using AdventOfCode2024.Solvers;

namespace Tests;

public class Day7Tests
{
    private static string[] _inputs = new[]
    {
        "190: 10 19",
        "3267: 81 40 27",
        "83: 17 5",
        "156: 15 6",
        "7290: 6 8 6 15",
        "161011: 16 10 13",
        "192: 17 8 14",
        "21037: 9 7 18 13",
        "292: 11 6 16 20",
    };

    [Theory]
    [InlineData(190, 10L, 19L)]
    [InlineData(3267, 81L, 40L, 27L)]
    [InlineData(292, 11L, 6L, 16L, 20L)]
    public void ForValidInput_OperationIsParsedAndDiscovered(long target, params long[] inputs)
    {
        var solver = new TargetSolver(target, inputs);

        Assert.True(solver.IsSolvable());
    }

    [Fact]
    public void Part1_GivesExpectedOutput()
    {
        var (part1, _) = new Day7(_inputs).Solve();

        Assert.Equal(3749, part1);
    }

    [Fact]
    public void Part2_GivesExpectedOutput()
    {
        var (_, part2) = new Day7(_inputs).Solve();

        Assert.Equal(11387, part2);
    }

    [Theory]
    [InlineData(Operator.Mul, 10, 12, 120)]
    [InlineData(Operator.Add, 12, 13, 25)]
    [InlineData(Operator.Combine, 12, 13, 1213)]
    public void Expression_2PartsWithOperator_GivesExpectedResult(Operator op, long lhs, long rhs, long expected)
    {
        var operators = new Operator[] { op };
        var inputs = new[] { lhs, rhs };
        var expression = new Expression(inputs, operators);
        var result = expression.Solve();
        Assert.Equal(expected, result);
    }

    [Fact]
    public void MultipartExpression_GivesExpectedResult()
    {
        var operators = new Operator[] { Operator.Add, Operator.Mul, Operator.Add, Operator.Mul };
        var inputs = new long[] { 1, 2, 3, 7, 2 };
        var expression = new Expression(inputs, operators);
        var result = expression.Solve();
        Assert.Equal(32, result);
    }
}
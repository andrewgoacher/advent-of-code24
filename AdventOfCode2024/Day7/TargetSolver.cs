using System.Collections;

namespace AdventOfCode2024.Day7;

public class TargetSolver
{
    private readonly long _target;
    private readonly List<Expression> _expressionCandidates = new();
    private readonly List<Expression> _validExpressions = new();

    public TargetSolver(long target, long[] inputs)
    {
        _target = target;
        _expressionCandidates.AddRange(GetValidExpressions(inputs));
    }

    public long Target => _target;

    public bool IsSolvable()
    {
        if (_validExpressions.Any())
        {
            return true;
        }

        foreach (var expression in _expressionCandidates)
        {
            var result = expression.Solve();
            if (result == _target)
            {
                _validExpressions.Add(expression);
            }
        }

        return _validExpressions.Any();
    }

    private static IEnumerable<Expression> GetValidExpressions(long[] inputs)
    {
        var operators = new[] { Operator.Add, Operator.Mul };
        int len = inputs.Length;
        int index = len - 1;
        int numberOfPermutations = 1 << index;
        int bitArrayLen = index;

        for (var i = 0; i < numberOfPermutations; i++)
        {
            var bits = new BitArray(new int[] { i });

            var validOperators = bits.Cast<bool>()
                .Take(bitArrayLen)
                .Reverse()
                .Select(b => b ? 1 : 0)
                .Select(x => operators[x])
                .ToArray();

            yield return new Expression(inputs, validOperators);
        }
    }
}
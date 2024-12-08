using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2024.Day7;

public class TargetSolver
{
    private readonly long _target;
    private readonly List<Expression> _expressionCandidates = new();
    private readonly List<Expression> _validExpressions = new();

    public TargetSolver(long target, long[] inputs, bool withCombinations = false)
    {
        _target = target;
        var validExpressions = withCombinations ?
            GetValidExpressionsEx(inputs, new [] { Operator.Add , Operator.Combine, Operator.Mul}) :
            GetValidExpressionsEx(inputs, new [] { Operator.Add , Operator.Mul});

        _expressionCandidates.AddRange(validExpressions);
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

    private static IEnumerable<Expression> GetValidExpressionsEx(long[] inputs, Operator[] operatorsInUse)
    {
        foreach (var operatorCombination in GetOperatorCombinations(inputs, operatorsInUse))
        {
            yield return new Expression(inputs, operatorCombination);
        }
    }

    private static IEnumerable<Operator[]> GetOperatorCombinations(long[] inputs, Operator[] operatorsInUse)
    {
        var numberOfOperatorsNeeded = inputs.Length - 1;

        var operatorMasterList = new List<List<Operator>>();
        foreach (var operatorX in operatorsInUse)
        {
            operatorMasterList.Add(new List<Operator> { operatorX });
        }

        numberOfOperatorsNeeded--;

        while (numberOfOperatorsNeeded > 0)
        {
            var newList = new List<List<Operator>>();
            for(var i = 0;i<operatorMasterList.Count;++i)
            {
                var list = operatorMasterList[i];
                foreach (var operatorX in operatorsInUse)
                {
                    var list2 = new List<Operator>(list);
                    list2.Add(operatorX);
                    newList.Add(list2);
                }
            }
            operatorMasterList = newList;

            numberOfOperatorsNeeded--;
        }

        foreach (var list in operatorMasterList)
        {
            yield return list.ToArray();
        }
    }
}
﻿namespace AdventOfCode2024.Day7;

public class Expression
{
    private readonly long[] _inputs;
    private readonly Operator[] _operators;

    public Expression(long[] inputs, Operator[] operators)
    {
        _inputs = inputs;
        _operators = operators;
    }

    public long Solve()
    {
        long current = _inputs[0];
        for (var i = 1; i < _inputs.Length; i++)
        {
            var next = _inputs[i];
            var operation = GetOperation(_operators[i-1]);
            current = operation(current, next);
        }

        return current;
    }

    private static Func<long, long, long> GetOperation(Operator op)
    {
        return op switch
        {
            Operator.Add => (a, b) => a + b,
            Operator.Mul => (a, b) => a * b,
            _ => throw new InvalidOperationException("Operator undefined.")
        };
    }
}
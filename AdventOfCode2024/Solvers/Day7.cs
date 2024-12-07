using AdventOfCode2024.Day7;

namespace AdventOfCode2024.Solvers;

public class Day7 : Solver
{
    private readonly List<(long, long[])> _inputs = new();

    public Day7(string[] input) : base(input)
    {
    }

    public static Day7 FromFile() => new(File.ReadAllLines("inputs/Day7.txt"));

    protected override void OnPrepare(string[] inputs)
    {
        foreach (var line in inputs)
        {
            var split = line.Split(":");
            var target = long.Parse(split[0]);
            var values = split[1].Split(new [] { ' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToArray();

            _inputs.Add((target, values));
        }
    }

    protected override  long Part1()
    {
        return _inputs.Select(input => new TargetSolver(input.Item1, input.Item2))
            .Where(solver => solver.IsSolvable())
            .Select(solver => solver.Target)
            .Sum();
    }

    protected override long Part2()
    {
        return 0;
    }
}
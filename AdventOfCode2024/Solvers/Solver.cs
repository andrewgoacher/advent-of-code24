namespace AdventOfCode2024.Solvers;

public abstract class Solver
{
    private readonly string[] _input;
    protected Solver(string[] input)
    {
        _input = input;
    }

    public (int, int) Solve()
    {
        OnPrepare(_input);

        var part1 = Part1();
        var part2 = Part2();

        return (part1, part2);
    }

    protected virtual void OnPrepare(string[] inputs)
    {
    }

    protected abstract int Part1();
    protected abstract int Part2();
}
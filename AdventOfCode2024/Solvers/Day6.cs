using AdventOfCode2024.Day6;

namespace AdventOfCode2024.Solvers;

public class Day6 : Solver
{
    private Grid _grid = null!;

    public Day6(string[] input) : base(input)
    {
    }

    public static Day6 FromFile() => new Day6(File.ReadAllLines("inputs/Day6.txt"));

    protected override void OnPrepare(string[] inputs)
    {
        _grid = new Grid(inputs);
    }

    protected override int Part1()
    {
        _grid.RunGrid();

        return _grid
            .GetVisitedNodes()
            .Count(x => x.IsVisited);
    }

    protected override int Part2()
    {
        return 0;
    }
}
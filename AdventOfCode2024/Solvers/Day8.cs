using AdventOfCode2024.Day8;

namespace AdventOfCode2024.Solvers;

public class Day8 : Solver
{
    private Grid _grid = null!;
    public Day8(string[] input) : base(input)
    {
    }

    public static Day8 FromFile() => new Day8(File.ReadAllLines("inputs/Day8.txt"));

    protected override void OnPrepare(string[] inputs)
    {
        base.OnPrepare(inputs);
        _grid = new(inputs);
    }

    protected override long Part1()
    {
        _grid.ScanForResonantNodes();

        return _grid.ResonantNodes.Count();
    }

    protected override long Part2()
    {
        _grid.ScanForResonantNodes(true);


        return _grid.ResonantNodes.Count();
    }
}
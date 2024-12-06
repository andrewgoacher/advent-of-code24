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
        return _grid.VisitNodes()
            .Select(vn => vn.Node)
            .Distinct()
            .Count();
    }

    protected override int Part2()
    {
        _grid.Reset();
        var nodes = _grid.VisitNodes().ToList();
        var neighbours = nodes.Select(n => n.Node)
            .Distinct()
            .Select(x => x.GetNeighbours())
            .SelectMany(x => x)
            .Distinct()
            .Where(n => n is VisitableNode)
            .Cast<VisitableNode>()
            .ToList();

        var count = 0;

        foreach (var neighbour in neighbours)
        {
            _grid.Reset();
            neighbour.SetAsObstacle();
            if (_grid.WillLoop())
            {
                count++;
            }
        }

        return count;
    }
}
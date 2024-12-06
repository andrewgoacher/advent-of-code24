using System.Text;
using AdventOfCode2024.Day6;

namespace AdventOfCode2024.Solvers;

public class Day6 : Solver
{
    private Guard _guard = null!;
    private Node[][] _grid = null!;

    public Day6(string[] input) : base(input)
    {
    }

    public static Day6 FromFile() => new Day6(File.ReadAllLines("inputs/Day6.txt"));

    protected override void OnPrepare(string[] inputs)
    {
        _grid = new Node[inputs.Length][];
        for (var i = 0; i < inputs.Length; i++)
        {
            _grid[i] = inputs[i]
                .ToCharArray()
                .Select((c,x) => ParseNode(c, x, i))
                .ToArray();
        }
    }

    protected override int Part1()
    {
        bool canMove = true;
        while (canMove)
        {
            canMove = _guard.Move(_grid);
        }

        return _grid
            .SelectMany(x => x)
            .Where(x => x is VisitableNode)
            .Count(x => x.IsVisited);
    }

    protected override int Part2()
    {
        return 0;
    }

    private Node ParseNode(char c, int x, int y)
    {
        var validGuardChars = new[] { '>', '<', '^', 'V' };

        return c switch
        {
            '.' => new VisitableNode(new Vector(x, y)),
            '#' => new Obstacle(new Vector(x, y)),
            var g when validGuardChars.Contains(g) => CreateGuardNode(x, y, c),
            _ => throw new InvalidOperationException("Invalid char"),
        };
    }

    private Node CreateGuardNode(int x, int y, char c)
    {
        var heading = c switch
        {
            'v' => new Vector(0, 1),
            '>' => new Vector(1, 0),
            '<' => new Vector(-1, 0),
            '^' => new Vector(0, -1),
            _ => throw new InvalidOperationException("Invalid char"),
        };

        _guard = new Guard(new Vector(x, y), heading);

        var visitableNode = new VisitableNode(new Vector(x, y));
        visitableNode.Visit();
        return visitableNode;
    }

#pragma warning disable S1144
    private void PrintGrid()
#pragma warning restore S1144
    {
        var sb = new StringBuilder();

        sb.AppendLine($"Pos({_guard.Position.X}, {_guard.Position.Y}): heading:({_guard.Heading.X}, {_guard.Heading.Y})");
        for (var i = 0; i < _grid.Length; i++)
        {
            for (var j = 0; j < _grid[i].Length; j++)
            {
                if (_guard.Position.X == j && _guard.Position.Y == i)
                {
                   sb.Append(_guard.Print());
                    continue;
                }
                var node = _grid[i][j];
                sb.Append(node.Print());
            }
            sb.AppendLine();
        }

        sb.AppendLine();
        sb.AppendLine();
        sb.AppendLine();

#pragma warning disable S1075
        var file = @"C://users//andre//Downloads//test.txt";
#pragma warning restore S1075
        if (File.Exists(file))
        {
            File.AppendAllText(file, sb.ToString());
        }
        else
        {
            File.WriteAllText(file, sb.ToString());
        }
    }
}
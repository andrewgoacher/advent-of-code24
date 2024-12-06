namespace AdventOfCode2024.Day6;

public class Grid
{
    private Guard _guard = null!;
    private readonly Node[][] _grid;
    private readonly List<VisitedNode> _visitedNodes = new();

    public Grid(string[] inputs)
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

    public void RunGrid()
    {
        bool canMove = true;
        _visitedNodes.Add(new VisitedNode(_guard.CurrentNode!, _guard.Heading));

        while (canMove)
        {
            canMove = _guard.Move(_grid);
            if (canMove)
            {
                _visitedNodes.Add(new VisitedNode(_guard.CurrentNode!, _guard.Heading));
            }
        }
    }

    public IEnumerable<VisitableNode> GetVisitedNodes()
    {
        return _grid.SelectMany(node => node)
            .Where(node => node is VisitableNode)
            .Cast<VisitableNode>()
            .Where(node => node.IsVisited);
    }

    private Node ParseNode(char c, int x, int y)
    {
        var validGuardChars = new[] { '>', '<', '^', 'V' };

        return c switch
        {
            '.' => new VisitableNode(new Vector(x, y), this),
            '#' => new Obstacle(new Vector(x, y), this),
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

        _guard = new Guard(new Vector(x, y), heading, this);

        var visitableNode = new VisitableNode(new Vector(x, y), this);
        visitableNode.Visit();
        return visitableNode;
    }

    public int Columns => _grid[0].Length;
    public int Rows => _grid.Length;

    public Node GetNodeAt(int x, int y)
    {
        return _grid[y][x];
    }
}
namespace AdventOfCode2024.Day6;

public class Guard : Node
{
    private Vector _heading;
    private Node? _currentNode;
    private readonly Vector _initialHeading;
    private readonly Vector _initialPosition;
    private readonly Node _initialNode;

    public Guard(Vector position, Vector heading, Grid grid, Node current) : base(position, false, grid)
    {
        _initialHeading = heading;
        _initialPosition = position;
        _heading = heading;
        _initialNode = current;
        _currentNode = current;
    }

    public Vector Heading => _heading;
    public Node? CurrentNode => _currentNode;

    public override void Reset()
    {
        _heading = _initialHeading;
        Position = _initialPosition;
        _currentNode = _initialNode;
    }

#pragma warning disable S2368
    public bool Move()
#pragma warning restore S2368
    {
        var nextNode = GetNextNode();
        _currentNode = nextNode;
        if (nextNode is null)
        {
            return false;
        }

        nextNode.Visit();
        Position = nextNode.Position;
        return true;
    }

    private Node? GetNextNode()
    {
        var nextPosition = Position.Add(_heading);
        if (nextPosition.X < 0 || nextPosition.Y < 0 || nextPosition.Y >= _grid.Columns ||
            nextPosition.X >= _grid.Rows)
        {
            // outside the bounds, so done moving
            return null;
        }

        var nextNode = _grid.GetNodeAt(nextPosition.X, nextPosition.Y);
        if (!nextNode.IsVisitable)
        {
            _heading = _heading.RotateRight();
            return GetNextNode();
        }

        return nextNode;
    }

    public override char Print()
    {
        return (_heading.X, _heading.Y) switch
        {
            (1, 0) => '>',
            (-1, 0) => '<',
            (0, 1) => 'v',
            (0, -1) => '^',
            _ => throw new InvalidCastException("Invalid char")
        };
    }
}
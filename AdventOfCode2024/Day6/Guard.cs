namespace AdventOfCode2024.Day6;

public class Guard : Node
{
    private Vector _heading;

    public Guard(Vector position, Vector heading) : base(position, false)
    {
        _heading = heading;
    }

    public Vector Heading => _heading;

#pragma warning disable S2368
    public bool Move(Node[][] grid)
#pragma warning restore S2368
    {
        var nextNode = GetNextNode(grid);
        if (nextNode is null)
        {
            return false;
        }

        nextNode.Visit();
        Position = nextNode.Position;
        return true;
    }

    private Node? GetNextNode(Node[][] grid)
    {
        var nextPosition = Position.Add(_heading);
        if (nextPosition.X < 0 || nextPosition.Y < 0 || nextPosition.X >= grid.Length ||
            nextPosition.Y >= grid[nextPosition.X].Length)
        {
            // outside the bounds, so done moving
            return null;
        }

        var nextNode = grid[nextPosition.Y][nextPosition.X];
        if (!nextNode.IsVisitable)
        {
            _heading = _heading.RotateRight();
            return GetNextNode(grid);
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
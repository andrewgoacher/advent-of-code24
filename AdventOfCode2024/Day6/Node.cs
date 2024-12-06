namespace AdventOfCode2024.Day6;

public abstract class Node
{
    public Vector Position { get; protected set; }
    public bool IsVisitable { get; }
    public bool IsVisited { get; private set; }

    protected readonly Grid _grid;

    protected Node(Vector position, bool isVisitable, Grid grid)
    {
        Position = position;
        IsVisitable = isVisitable;
        _grid = grid;
    }

    public void Visit()
    {
        if (!IsVisitable)
        {
            throw new InvalidOperationException("Node is not visited");
        }

        IsVisited = true;
    }

    public IEnumerable<Node> GetNeighbours()
    {
        var (x, y) = Position;
        if (x - 1 >= 0)
        {
            yield return _grid.GetNodeAt(x-1, y);
        }

        if (x + 1 < _grid.Columns)
        {
            yield return _grid.GetNodeAt(x+1, y);
        }

        if (y - 1 >= 0)
        {
            yield return _grid.GetNodeAt(x, y-1);
        }

        if (y + 1 < _grid.Rows)
        {
            yield return _grid.GetNodeAt(x, y+1);
        }
    }

    public abstract char Print();

    public virtual void Reset()
    {
        IsVisited = false;
    }
}
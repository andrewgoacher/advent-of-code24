namespace AdventOfCode2024.Day6;

public abstract class Node
{
    public Vector Position { get; protected set; }
    public bool IsVisitable { get; }
    public bool IsVisited { get; private set; }

    protected Node(Vector position, bool isVisitable)
    {
        Position = position;
        IsVisitable = isVisitable;
    }

    public void Visit()
    {
        if (!IsVisitable)
        {
            throw new InvalidOperationException("Node is not visited");
        }

        IsVisited = true;
    }

    public abstract char Print();

    public virtual void Reset()
    {
        IsVisited = false;
    }
}
namespace AdventOfCode2024.Day6;

public class VisitableNode : Node
{
    public VisitableNode(Vector position, Grid grid) : base(position, true, grid)
    {
    }

    public void SetAsObstacle()
    {
        IsVisitable = false;
    }

    public override void Reset()
    {
        IsVisitable = true;
        base.Reset();
    }

    public override char Print() => '.';
}
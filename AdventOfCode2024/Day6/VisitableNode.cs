namespace AdventOfCode2024.Day6;

public class VisitableNode : Node
{
    public VisitableNode(Vector position, Grid grid) : base(position, true, grid)
    {
    }

    public override char Print() => '.';
}
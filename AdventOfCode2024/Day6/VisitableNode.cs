namespace AdventOfCode2024.Day6;

public class VisitableNode : Node
{
    public VisitableNode(Vector position) : base(position, true)
    {
    }

    public override char Print() => '.';
}
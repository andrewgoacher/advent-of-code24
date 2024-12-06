namespace AdventOfCode2024.Day6;

public class Obstacle : Node
{
    public Obstacle(Vector position) : base(position, false)
    {
    }

    public override char Print() => '#';
}
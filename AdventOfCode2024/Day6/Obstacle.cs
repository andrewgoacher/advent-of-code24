namespace AdventOfCode2024.Day6;

public class Obstacle : Node
{
    public Obstacle(Vector position, Grid grid) : base(position, false, grid)
    {
    }

    public override char Print() => '#';

    public override void Reset()
    {
        // do nothing
    }
}